using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BibleStudyTool.Core.Entities;
using BibleStudyTool.Core.Entities.Exceptions;
using BibleStudyTool.Core.Exceptions;
using BibleStudyTool.Core.Interfaces;
using BibleStudyTool.Infrastructure.DAL.Npgsql;

namespace BibleStudyTool.Infrastructure.ServiceLayer
{
    public class NoteService : INoteService
    {
        private readonly IAsyncRepository<Note> _noteRepository;

        private readonly INoteReferenceService _noteReferenceService;
        private readonly INoteTagService _noteTagService;
        private readonly INoteVerseReferenceService _noteVerseReferenceService; 
        private readonly ITagService _tagService;

        private readonly NoteQueries _noteQueries;

        public NoteService
            (IAsyncRepository<Note> noteRepository,
            INoteReferenceService noteReferenceService,
            INoteTagService noteTagService,
            INoteVerseReferenceService noteVerseReferenceService,
            ITagService tagService,
            NoteQueries noteQueries)
        {

            _noteRepository = noteRepository;

            _noteReferenceService = noteReferenceService;
            _noteTagService = noteTagService;
            _noteVerseReferenceService = noteVerseReferenceService;
            _tagService = tagService;

            _noteQueries = noteQueries;
        }

        /// <summary>
        ///     Creates a new note.
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="summary"></param>
        /// <param name="text"></param>
        /// <param name="bibleVerseReferences"></param>
        /// <param name="noteReferences"></param>
        /// <param name="existingTags"></param>
        /// <param name="newTags"></param>
        /// <returns>
        ///     The created note along with its tags and note/verse references.
        /// </returns>
        public async Task<NoteWithTagsAndReferences> CreateAsync
            (string uid, string summary, string text, IEnumerable<NoteVerseReference> bibleVerseReferences,
             IEnumerable<int> noteReferences, IEnumerable<int> existingTags, IEnumerable<Tag> newTags)
        {
            // Create the new note
            var noteRef = new Note(uid, summary, text);
            var createdNote = await _noteRepository.CreateAsync(noteRef);

            // Create the new tags
            var createdTags = await createNewTags(newTags);

            // Associate existing tags and new tags with note
            await associateTagsWithNoteAsync(createdNote.Id, existingTags, createdTags);

            // Add the note references
            await addNoteReferencesAsync(createdNote.Id, noteReferences);

            // Add note verse references
            await addNoteVerseReferencesAsync(createdNote.Id, bibleVerseReferences);

            // Get all the note's tags and references
            var noteWithTagsAndReferences = await getNoteWithTagsAndReferencesAsync(createdNote);

            return noteWithTagsAndReferences;
        }

        /// <summary>
        ///     Deletes a note.
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="noteId"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        /// <exception cref="UnauthorizedException"></exception>
        public async Task DeleteAsync(string uid, int noteId)
        {
            var note = await getNoteByIdAsync(noteId);
            if (note == null)
            {
                throw new Exception("The note to be deleted does not exist.");
            }
            else if (note.Uid != uid)
            {
                throw new UnauthorizedException("The logged in user does not own the note to be deleted.");
            }
            await _noteRepository.DeleteAsync(note);
        }

        /// <summary>
        ///     Gets all of the notes associated with an entire bible chapter.
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="bibleBookId"></param>
        /// <param name="chapterNumber"></param>
        /// <returns>
        ///     A list of notes associated with a bible chapter along with its
        ///     tags and note/verse references.
        /// </returns>
        public async Task<IEnumerable<NoteWithTagsAndReferences>> GetAllChapterNotesAsync(string uid, int bibleBookId, int chapterNumber)
        {
            // Get all notes for a chapter
            var notes = new List<NoteWithTagsAndReferences>();
            var notesForChapter = await _noteQueries.GetChapterNotesQueryAsync(uid, bibleBookId, chapterNumber);
            var noteIds = notesForChapter.Select(n => n.Id).ToArray();

            // Get all tags for all of the notes acquired from above
            var noteTagsMapping = await _noteTagService.GetTagsForNotesAsync(noteIds);

            // Get all the note references of all the notes acquired from above
            var noteToReferenceMapping = await getAllNotesReferencesAsync(noteIds);

            // Map all tags and references to each note
            foreach (var note in notesForChapter)
            {
                var noteId = note.Id;
                IList<Tag> tags = new List<Tag>();
                if (noteTagsMapping.TryGetValue(noteId, out var outTags))
                {
                    tags = outTags;
                }

                var noteReferences = new ValueTuple<IList<NoteReference>, IList<NoteReference>>();
                if (noteToReferenceMapping.TryGetValue(noteId, out var outNoteReferences))
                {
                    noteReferences = outNoteReferences;
                }

                var noteWithTagsAndReferences = new NoteWithTagsAndReferences(note);
                noteWithTagsAndReferences.Tags = tags;
                noteWithTagsAndReferences.ReferencedByTheseNotes = noteReferences.Item1.Select(cnr => cnr.Id);
                noteWithTagsAndReferences.NoteReferences = noteReferences.Item2.Select(pnr => pnr.Id);
                notes.Add(noteWithTagsAndReferences);
            }

            return notes;
        }

        /// <summary>
        ///     Updates a note.
        /// </summary>
        /// <param name="noteId"></param>
        /// <param name="uid"></param>
        /// <param name="summary"></param>
        /// <param name="text"></param>
        /// <param name="tagIds"></param>
        /// <param name="noteVerseReferences"></param>
        /// <param name="noteReferenceIds"></param>
        /// <param name="newTags"></param>
        /// <returns>
        ///     The updated note along with its note/verse references.
        /// </returns>
        /// <exception cref="UnauthorizedException"></exception>
        public async Task<NoteWithTagsAndReferences> UpdateAsync
            (int noteId, string uid, string summary, string text,
            IEnumerable<int> tagIds, IEnumerable<NoteVerseReference> noteVerseReferences,
            IEnumerable<int> noteReferenceIds, IEnumerable<Tag> newTags)
        {
            var note = await getNoteByIdAsync(noteId);
            if (note.Uid != uid)
            {
                throw new UnauthorizedException("The note to be updated does not belong to the logged-in user.");
            }

            // Update note details
            note.UpdateDetails(summary, text);
            await _noteRepository.UpdateAsync(note);

            // Create the new tags
            var createdTags = await createNewTags(newTags);

            // Associate and disassociate tags accordingly
            await updateTagsList(noteId, tagIds, createdTags);

            // Associate and disassociate note and note references accordingly
            await updateNoteReferenceList(note.Id, noteReferenceIds.ToHashSet());

            // Associate and disassociate note and note verse references accordingly
            await updateNoteVerseReferenceList(note.Id, noteVerseReferences);

            var noteWithTagsAndReferences = await getNoteWithTagsAndReferencesAsync(note);
            return noteWithTagsAndReferences;
        }

        #region************************************************** HELPER METHODS
        // *********************************************************************
        // *********************************************************************

        /// <summary>
        ///     Creates new tags.
        /// </summary>
        /// <param name="newTags"></param>
        /// <returns>
        ///     The list of newly created tags.
        /// </returns>
        private async Task<IEnumerable<Tag>> createNewTags(IEnumerable<Tag> newTags)
        {
            var createdTags = new List<Tag>();
            if (newTags != null)
            {
                foreach (var newTag in newTags)
                {
                    try
                    {
                        createdTags.Add(await _tagService.CreateTagAsync(newTag.Uid, newTag.Label, newTag.Color));
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"{DateTime.Now} :: Something went wrong with creating a new tag for a note ({newTag.Label}) :: {ex.Message}");
                    }
                }
            }
            return createdTags;
        }

        /// <summary>
        ///     Associates tags with notes in the DB.
        /// </summary>
        /// <param name="noteId"></param>
        /// <param name="tagIds"></param>
        /// <param name="createdTags"></param>
        /// <returns></returns>
        private async Task associateTagsWithNoteAsync(int noteId, IEnumerable<int> tagIds, IEnumerable<Tag> createdTags)
        {
            try
            {
                IDictionary<int, IList<Tag>> existingNoteTags = new Dictionary<int, IList<Tag>>();
                try
                {
                    existingNoteTags = await _noteTagService.GetTagsForNotesAsync(new int[1] { noteId });
                }
                catch
                {
                    Console.WriteLine("Failed to get all of the note tags."); // TODO: log 
                }

                // Identiy which of the inputted tagIds already exist as note tags. 
                IList<int> allTagNotes = tagIds.ToList();
                if (existingNoteTags != null && existingNoteTags.ContainsKey(noteId))
                {
                    foreach (var tagId in tagIds)
                    {
                        if (existingNoteTags[noteId].Any(t => t.Id == tagId))
                        {
                            allTagNotes.Remove(tagId);
                        }
                    }
                }

                foreach (var newlyCreatedTag in createdTags)
                {
                    allTagNotes.Add(newlyCreatedTag.Id);
                }

                await _noteTagService.BulkCreateNoteTagsAsync(noteId, allTagNotes);
            }
            catch
            {
                Console.WriteLine($"{DateTime.Now} :: Something went wrong wtih associating a tag with the note.");
            }

        }

        /// <summary>
        ///     Adds note references to a note.
        /// </summary>
        /// <param name="noteId"></param>
        /// <param name="noteReferenceIds"></param>
        /// <returns></returns>
        private async Task addNoteReferencesAsync(int noteId, IEnumerable<int> noteReferenceIds)
        {
            try
            {
                await _noteReferenceService.AssignReferencesAsync(noteId, noteReferenceIds);
            }
            catch
            {
                Console.WriteLine($"{DateTimeOffset.UtcNow} :: Something went wrong with assigning a note reference to the note.");
            }
        }

        /// <summary>
        ///     Adds verse references to a note.
        /// </summary>
        /// <param name="noteId"></param>
        /// <param name="bibleReferenceIds"></param>
        /// <returns></returns>
        private async Task addNoteVerseReferencesAsync(int noteId, IEnumerable<NoteVerseReference> bibleReferenceIds)
        {
            try
            {
                await _noteVerseReferenceService.AssignReferencesAsync(noteId, bibleReferenceIds);
            }
            catch
            {
                Console.WriteLine($"{DateTimeOffset.UtcNow} :: Something went wrong with assigning a verse reference to the note.");
            }
        }

        /// <summary>
        ///     Gets a note by its ID.
        /// </summary>
        /// <param name="noteId"></param>
        /// <returns>
        ///     The note specified by its ID.
        /// </returns>
        private async Task<Note> getNoteByIdAsync(int noteId)
        {
            var keyId = new object[] { noteId };
            return await _noteRepository.GetByIdAsync(keyId);
        }

        /// <summary>
        ///     Gets a note's references and the parent references.
        /// </summary>
        /// <param name="noteIds"></param>
        /// <returns>
        ///     A dictionary mapping between a note and its corresponding
        ///     not references and parent note references.
        /// </returns>
        private async Task<IDictionary<int, (IList<NoteReference> parentNoteReferences, IList<NoteReference> childrenNoteReferences)>> getAllNotesReferencesAsync(int[] noteIds)
        {
            var referenceMapping = new Dictionary<int, (IList<NoteReference>, IList<NoteReference>)>();

            try
            {
                var parentNoteReferences =
                    await _noteReferenceService
                        .GetParentNoteReferencesAsync(noteIds);

                foreach (var parentNoteReference in parentNoteReferences)
                {
                    if (referenceMapping.TryGetValue(parentNoteReference.NoteId, out var references))
                    {
                        (var pnr, var _) = references;
                        pnr.Add(parentNoteReference);
                    }
                    else
                    {
                        (IList<NoteReference>, IList<NoteReference>) refernces =
                            (new List<NoteReference>() { parentNoteReference }, null);

                        referenceMapping.Add(parentNoteReference.NoteId, refernces);
                    }
                }
            }
            catch
            {
                Console.WriteLine("Failed to get parent note references for notes."); // TODO: log
            }

            try
            {
                var noteReferences =
                    await _noteReferenceService
                        .GetNotesReferencesAsync(noteIds);

                foreach (var noteReference in noteReferences)
                {
                    if (referenceMapping.TryGetValue(noteReference.NoteId, out var references))
                    {
                        (var _, var nr) = references;
                        nr.Add(noteReference);
                    }
                    else
                    {
                        (IList<NoteReference>, IList<NoteReference>) refernces =
                            (null, new List<NoteReference>() { noteReference });

                        referenceMapping.Add(noteReference.NoteId, refernces);
                    }
                }
            }
            catch
            {
                Console.WriteLine("Failed to get note references for notes."); // TODO: log
            }

            return referenceMapping;
        }

        /// <summary>
        ///     Gets all provided notes' verse references.
        /// </summary>
        /// <param name="noteIds"></param>
        /// <returns>
        ///     A mapping between notes and their corresponding verse references.
        /// </returns>
        private async Task<IDictionary<int, IList<NoteVerseReference>>> getAllNotesVerseReferencesAsync(int[] noteIds)
        {
            var referenceMapping = new Dictionary<int, IList<NoteVerseReference>>();

            try
            {
                var noteVerseReferences =
                    await _noteVerseReferenceService
                        .GetNotesVerseReferencesAsync(noteIds);

                foreach (var noteVerseReference in noteVerseReferences)
                {
                    if (referenceMapping.TryGetValue(noteVerseReference.NoteId, out var verseReferences))
                    {
                        verseReferences.Add(noteVerseReference);
                    }
                    else
                    {
                        IList<NoteVerseReference> verseRefernces =
                            new List<NoteVerseReference>() { noteVerseReference };

                        referenceMapping.Add(noteVerseReference.NoteId, verseRefernces);
                    }
                }
            }
            catch
            {
                Console.WriteLine("Failed to get note references for notes."); // TODO: log
            }

            return referenceMapping;
        }

        /// <summary>
        ///     Gets all the tags associated with a note.
        /// </summary>
        /// <param name="noteId"></param>
        /// <returns>
        ///     The list of tags associated with a note.
        /// </returns>
        private async Task<IEnumerable<Tag>> getNoteTagsAsync(int noteId)
        {
            IList<Tag> noteTags = new List<Tag>();
            try
            {
                var noteTagMappings = await _noteTagService.GetTagsForNotesAsync(new int[1] { noteId });
                if (noteTagMappings.TryGetValue(noteId, out var tags))
                {
                    noteTags = tags;
                }
            }
            catch
            {
                Console.WriteLine("Failed to get tags for note."); // TODO: Log
            }
            return noteTags;
        }

        /// <summary>
        ///     Gets a note along with its tags and note/verse references.
        /// </summary>
        /// <param name="note"></param>
        /// <returns>
        ///     The note along with its tags and note/verse references.
        /// </returns>
        private async Task<NoteWithTagsAndReferences> getNoteWithTagsAndReferencesAsync(Note note)
        {
            // Get all the user's tags
            var noteTags = await getNoteTagsAsync(note.Id);
            
            // Create an instance of notewith tags and references
            var noteDetails = new NoteWithTagsAndReferences(note, noteTags, null, null);

            // Get all the note's references
            var noteReferences = await getAllNotesReferencesAsync(new int[1] { note.Id });
            var noteReferencesForNote = noteReferences[note.Id];

            noteDetails.ReferencedByTheseNotes = noteReferencesForNote.parentNoteReferences.Select(pnr => pnr.Id);
            noteDetails.NoteReferences = noteReferencesForNote.childrenNoteReferences.Select(cnr => cnr.Id);

            // Get all the note's verse references
            var noteVerseReferences = await getAllNotesVerseReferencesAsync(new int[1] { note.Id });
            var noteVerseReferencesForNote = noteVerseReferences[note.Id];

            noteDetails.NoteVerseReferences = noteVerseReferencesForNote;

            return noteDetails;
        }

        /// <summary>
        ///     Updates a note's tag list.
        /// </summary>
        /// <param name="noteId"></param>
        /// <param name="tagIds"></param>
        /// <param name="newTags"></param>
        /// <returns></returns>
        private async Task updateTagsList(int noteId, IEnumerable<int> tagIds, IEnumerable<Tag> newTags)
        {
            IDictionary<int, IList<Tag>> tags = new Dictionary<int, IList<Tag>>();
            IList<int> tagsToBeDeleted = new List<int>();
            IList<int> tagsToBeAdded = tagIds.ToList();

            foreach (var tag in newTags)
            {
                tagsToBeAdded.Add(tag.Id);
            }

            try
            {
                // Get the tags associated with the note
                tags = await _noteTagService.GetTagsForNotesAsync(new int[1] { noteId });

                if (tags != null && tags.ContainsKey(noteId))
                {
                    // Determine which tags need to be added
                    foreach (var tag in tags[noteId])
                    {
                        if (tagsToBeAdded.Contains(tag.Id) == false)
                        {
                            tagsToBeAdded.Remove(tag.Id);
                        }
                    }

                    // Determine which tags need to be deleted
                    foreach(var tagId in tagIds)
                    {
                        if (tags[noteId].Any(t => t.Id == tagId) == false)
                        {
                            tagsToBeDeleted.Add(tagId);
                        }
                    }
                }
            }
            catch
            {
                Console.WriteLine("Failed to get all of the note's tags."); //TODO: log
            }

            try
            {
                // Delete tags from note
                await _noteTagService.RemoveTagsFromNote(noteId, tagsToBeDeleted);
            }
            catch
            {
                Console.WriteLine("Failed to remove some or all tags from note."); //TODO: log
            }

            try
            {
                // Associate tags with note
                await associateTagsWithNoteAsync(noteId, tagIds, newTags);
            }
            catch
            {
                Console.WriteLine("Failed to add some or all new tags to note."); //TODO: log
            }
        }

        /// <summary>
        ///     Updates the association between notes and
        ///     note references.
        /// </summary>
        /// <param name="noteId"></param>
        /// <param name="noteReferenceIds"></param>
        /// <returns></returns>
        private async Task updateNoteReferenceList
            (int noteId, HashSet<int> noteReferenceIds)
        {
            // Get all note references that the given note references
            (var _, var allNoteReferences) =
                (await getAllNotesReferencesAsync(new int[1] { noteId }))
                    [noteId];

            if (allNoteReferences != null)
            {
                // Find out which note references need to be deleted
                // (references from allNoteReferences that are not in noteReferenceIds)
                var noteReferencesToBeDeleted = new List<int>();

                foreach (var noteReference in allNoteReferences)
                {
                    var noteRefId = noteReference.Id;
                    if (noteReferenceIds.Contains(noteRefId) == false)
                    {
                        noteReferencesToBeDeleted.Add(noteRefId);
                    }
                }

                // Find out which note references need to be deleted
                // (references from noteReferenceIds that are not in allNoteReferences)
                var noteReferencesToBeAdded = new List<int>();
                foreach (var noteReferenceId in noteReferenceIds)
                {
                    if (allNoteReferences.Any(nr => nr.Id == noteReferenceId) == false)
                    {
                        noteReferencesToBeAdded.Add(noteReferenceId);
                    }
                }

                // Remove the stale note reference from the DB
                try
                {
                    await _noteReferenceService.RemoveReferencesAsync(noteId, noteReferencesToBeDeleted);
                }
                catch
                {
                    Console.WriteLine("Failed to remove references from note."); //TODO: log
                }

                // Add the new note references in the DC
                try
                {
                    await _noteReferenceService.AssignReferencesAsync(noteId, noteReferencesToBeAdded);
                }
                catch
                {
                    Console.WriteLine("Failed to add references to note."); //TODO: log
                }
            }
        }

        /// <summary>
        ///     Updates the association between notes and
        ///     note verse references.
        /// </summary>
        /// <param name="noteId"></param>
        /// <param name="noteVerseReferences"></param>
        /// <returns></returns>
        private async Task updateNoteVerseReferenceList
            (int noteId, IEnumerable<NoteVerseReference> noteVerseReferences)
        {
            // Get all note verse references that the given note references
            var allNoteVerseReferences =
                (await getAllNotesVerseReferencesAsync(new int[1] { noteId }))
                    [noteId];

            if (allNoteVerseReferences != null)
            {
                // Find out which note references need to be deleted
                // (references from allNoteVerseReferences that are not in noteVerseReference)
                var noteReferencesToBeDeleted = new List<NoteVerseReference>();

                foreach (var noteVerseReference in allNoteVerseReferences)
                {
                    if (noteVerseReferences.Any(nvr => nvr.Id == noteVerseReference.Id) == false)
                    {
                        noteReferencesToBeDeleted.Add(noteVerseReference);
                    }
                }

                // Find out which note references need to be deleted
                // (references from noteVerseReferences that are not in allVerseNoteReferences)
                var noteReferencesToBeAdded = new List<NoteVerseReference>();
                foreach (var noteVerseReference in noteVerseReferences)
                {
                    if (allNoteVerseReferences.Any(nr => nr.Id == noteVerseReference.Id) == false)
                    {
                        noteReferencesToBeAdded.Add(noteVerseReference);
                    }
                }

                // Remove the stale note verse reference from the DB
                try
                {
                    await _noteVerseReferenceService.RemoveReferencesAsync(noteId, noteReferencesToBeDeleted);
                }
                catch
                {
                    Console.WriteLine("Failed to remove verse references from note."); //TODO: log
                }

                // Add the new note verse references in the DB
                try
                {
                    await _noteVerseReferenceService.AssignReferencesAsync(noteId, noteReferencesToBeAdded);
                }
                catch
                {
                    Console.WriteLine("Failed to add verse references to note."); //TODO: log
                }
            }
        }
        #endregion
    }
}
