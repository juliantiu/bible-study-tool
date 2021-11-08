using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BibleStudyTool.Core.Entities;
using BibleStudyTool.Core.Entities.Exceptions;
using BibleStudyTool.Core.Entities.JoinEntities;
using BibleStudyTool.Core.Exceptions;
using BibleStudyTool.Core.Interfaces;
using BibleStudyTool.Infrastructure.DAL.Npgsql;

namespace BibleStudyTool.Infrastructure.ServiceLayer
{
    public class NoteService : INoteService
    {
        private readonly IAsyncRepository<Note> _noteRepository;

        private readonly INoteReferenceService _noteReferenceService;
        private readonly ITagNoteService _tagNoteService;
        private readonly ITagService _tagService;

        private readonly NoteQueries _noteQueries;

        public NoteService
            (IAsyncRepository<Note> noteRepository,
            INoteReferenceService noteReferenceService,
            ITagNoteService tagNoteService,
            ITagService tagService,
            NoteQueries noteQueries)
        {

            _noteRepository = noteRepository;

            _noteReferenceService = noteReferenceService;
            _tagNoteService = tagNoteService;
            _tagService = tagService;

            _noteQueries = noteQueries;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="summary"></param>
        /// <param name="text"></param>
        /// <param name="bibleVerseReferences"></param>
        /// <param name="noteReferences"></param>
        /// <param name="existingTags"></param>
        /// <param name="newTags"></param>
        /// <returns></returns>
        public async Task<NoteWithTagsAndReferences> CreateAsync
            (string uid, string summary, string text, IEnumerable<int> bibleVerseReferences,
             IEnumerable<int> noteReferences, IEnumerable<int> existingTags, IEnumerable<Tag> newTags)
        {
            // Create the new note
            var noteRef = new Note(uid, summary, text);
            var createdNote = await _noteRepository.CreateAsync<NoteCrudActionException>(noteRef);

            // Create the new tags
            var createdTags = await createNewTags(newTags);

            // Associate existing tags and new tags with note
            await associateTagsWithNoteAsync(createdNote.NoteId, existingTags, createdTags);

            // Add the note references
            await addNoteReferences(createdNote.NoteId, noteReferences, bibleVerseReferences);

            // Get all the note's tags and references
            var noteWithTagsAndReferences = await getNoteWithTagsAndReferencesAsync(createdNote);

            return noteWithTagsAndReferences;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="noteId"></param>
        /// <returns></returns>
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
            await _noteRepository.DeleteAsync<NoteCrudActionException>(note);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="bibleBookId"></param>
        /// <param name="chapterNumber"></param>
        /// <returns></returns>
        public async Task<IEnumerable<NoteWithTagsAndReferences>> GetAllChapterNotes(string uid, int bibleBookId, int chapterNumber)
        {
            // Get all notes for a chapter
            var notes = new List<NoteWithTagsAndReferences>();
            var notesForChapter = await _noteQueries.GetChapterNotesQueryAsync(uid, bibleBookId, chapterNumber);
            var noteIds = notesForChapter.Select(n => n.NoteId).ToArray();

            // Get all tags for all of the notes acquired from above
            var noteTagsMapping = await _tagNoteService.GetTagsForNotesAsync(noteIds);

            // Get all the note references of all the notes acquired from above
            var noteToReferenceMapping = await getAllNoteReferencesAsync(noteIds);

            // Map all tags and references to each note
            foreach (var note in notesForChapter)
            {
                var noteId = note.NoteId;
                IList<Tag> tags = new List<Tag>();
                if (noteTagsMapping.TryGetValue(noteId, out var outTags))
                {
                    tags = outTags;
                }

                IList<IList<int>> references = new List<IList<int>>() { new List<int>(), new List<int>() };
                if (noteToReferenceMapping.TryGetValue(noteId, out var outReferences))
                {
                    references = outReferences;
                }

                var noteWithTagsAndReferences = new NoteWithTagsAndReferences(note);
                noteWithTagsAndReferences.Tags = tags;
                noteWithTagsAndReferences.ReferencedNotes = references[0];
                noteWithTagsAndReferences.ReferencedBibleVerses = references[1];
                notes.Add(noteWithTagsAndReferences);
            }

            return notes;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="noteId"></param>
        /// <param name="uid"></param>
        /// <param name="summary"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public async Task<NoteWithTagsAndReferences> UpdateAsync
            (int noteId, string uid, string summary, string text,
            IEnumerable<int> tagIds, IEnumerable<int> bibleVerseIds,
            IEnumerable<int> noteReferenceIds, IEnumerable<Tag> newTags)
        {
            var note = await getNoteByIdAsync(noteId);
            if (note.Uid != uid)
            {
                throw new UnauthorizedException("The note to be updated does not belong to the logged-in user.");
            }

            // Update note details
            note.UpdateDetails(summary, text);
            await _noteRepository.UpdateAsync<NoteCrudActionException>(note);

            // Create the new tags
            var createdTags = await createNewTags(newTags);

            // Associate and disassociate tags accordingly
            await updateTagsList(noteId, tagIds, createdTags);

            // Associate and disassociate noteReferences accordingly
            await updateReferenceList(noteId, bibleVerseIds, noteReferenceIds);

            var noteWithTagsAndReferences = await getNoteWithTagsAndReferencesAsync(note);
            return noteWithTagsAndReferences;
        }

        #region************************************************** HELPER METHODS
        // *********************************************************************
        // *********************************************************************
        /// <summary>
        /// 
        /// </summary>
        /// <param name="newTags"></param>
        /// <returns></returns>
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
        /// 
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
                    existingNoteTags = await _tagNoteService.GetTagsForNotesAsync(new int[1] { noteId });
                }
                catch
                {
                    Console.WriteLine("Failed to get all of the note tags."); // TODO: log 
                }

                // Identiy which og the inputted tagIds already exist as ntoe tags. 
                IList<int> allTagNotes = tagIds.ToList();
                if (existingNoteTags != null && existingNoteTags.ContainsKey(noteId))
                {
                    foreach (var tagId in tagIds)
                    {
                        if (existingNoteTags[noteId].Any(t => t.TagId == tagId))
                        {
                            allTagNotes.Remove(tagId);
                        }
                    }
                }

                foreach (var newlyCreatedTag in createdTags)
                {
                    allTagNotes.Add(newlyCreatedTag.TagId);
                }

                await _tagNoteService.BulkCreateTagNotesAsync(noteId, allTagNotes);
            }
            catch
            {
                Console.WriteLine($"{DateTime.Now} :: Something went wrong wtih associating a tag with the note.");
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="noteId"></param>
        /// <param name="noteReferences"></param>
        /// <param name="bibleVerseReferences"></param>
        /// <returns></returns>
        private async Task addNoteReferences(int noteId, IEnumerable<int> noteReferences, IEnumerable<int> bibleVerseReferences)
        {
            try
            {
                await _noteReferenceService.AssignReferencesAsync(noteId, noteReferences, bibleVerseReferences);
            }
            catch
            {
                Console.WriteLine($"{DateTime.Now} :: Something went wrong with assigning a reference to the note.");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="noteId"></param>
        /// <returns></returns>
        private async Task<Note> getNoteByIdAsync(int noteId)
        {
            var keyId = new object[] { noteId };
            return await _noteRepository.GetByIdAsync<NoteCrudActionException>(keyId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="noteIds"></param>
        /// <returns></returns>
        private async Task<IDictionary<int, IList<IList<int>>>> getAllNoteReferencesAsync(int[] noteIds)
        {
            var allReferences = new Dictionary<int, IList<IList<int>>>();
            //var parentNoteReferences = await _noteReferenceService.GetParentNoteReferencesAsync(noteIds);
            //assignReferencesToNote(allReferences, parentNoteReferences);

            try
            {
                var noteReferences = await _noteReferenceService.GetNotesReferencesAsync(noteIds);
                assignReferencesToNote(allReferences, noteReferences);
            }
            catch
            {
                Console.WriteLine("Failed to get note references for notes."); // TODO: log
            }

            return allReferences;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="allReferences"></param>
        /// <param name="referenceSource"></param>
        private void assignReferencesToNote
            (Dictionary<int, IList<IList<int>>> allReferences, IEnumerable<NoteReference> referenceSource)
        {
            foreach (var reference in referenceSource)
            {
                var noteId = reference.NoteId;
                if (!(allReferences.ContainsKey(noteId)))
                {
                    allReferences[noteId] = new List<IList<int>>() { new List<int>(), new List<int>() };
                }

                assignReferenceToNote(allReferences, noteId, reference);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="allReferences"></param>
        /// <param name="noteId"></param>
        /// <param name="reference"></param>
        private void assignReferenceToNote
            (Dictionary<int, IList<IList<int>>> allReferences, int noteId, NoteReference reference)
        {
            if ((reference.ReferencedBibleVerseId > 0)
                && (reference.ReferencedBibleVerseId is int rbvid)
                && (!allReferences[noteId][1].Contains(rbvid)))
            {
                allReferences[noteId][1].Add(reference.ReferencedBibleVerseId ?? 0);
            }
            else if ((reference.ReferencedNoteId > 0)
                     && (reference.ReferencedNoteId is int rnid)
                     && (!allReferences[noteId][0].Contains(rnid)))
            {
                allReferences[noteId][0].Add(reference.ReferencedNoteId ?? 0);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="noteId"></param>
        /// <returns></returns>
        private async Task<IEnumerable<Tag>> getNoteTagsAsync(int noteId)
        {
            IList<Tag> noteTags = new List<Tag>();
            try
            {
                var noteTagMappings = await _tagNoteService.GetTagsForNotesAsync(new int[1] { noteId });
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
        /// 
        /// </summary>
        /// <param name="noteId"></param>
        /// <returns></returns>
        private async Task<IEnumerable<NoteReference>> getNoteReferences(int noteId)
        {
            IEnumerable<NoteReference> noteReferences = new List<NoteReference>();
            try
            {
                noteReferences = await _noteReferenceService.GetNotesReferencesAsync(new int[1] { noteId });
            }
            catch
            {
                Console.WriteLine("Failed to get references for note.");
            }
            return noteReferences;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="note"></param>
        /// <returns></returns>
        private async Task<NoteWithTagsAndReferences> getNoteWithTagsAndReferencesAsync(Note note)
        {
            // Get all the user's tags
            var noteTags = await getNoteTagsAsync(note.NoteId);

            // Get all the note's references
            var references = await getNoteReferences(note.NoteId);

            return new NoteWithTagsAndReferences(note, noteTags, references);
        }

        /// <summary>
        /// 
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
                tagsToBeAdded.Add(tag.TagId);
            }

            try
            {
                // Get the tags associated with the note
                tags = await _tagNoteService.GetTagsForNotesAsync(new int[1] { noteId });
                if (tags != null && tags.ContainsKey(noteId))
                {
                    foreach (var tag in tags[noteId])
                    {
                        if (tagsToBeAdded.Contains(tag.TagId) == false)
                        {
                            tagsToBeAdded.Remove(tag.TagId);
                        }
                    }
                    foreach(var tagId in tagIds)
                    {
                        if (tags[noteId].Any(t => t.TagId == tagId) == false)
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
                await _tagNoteService.RemoveTagsFromNote(noteId, tagsToBeDeleted);
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
        /// 
        /// </summary>
        /// <param name="noteId"></param>
        /// <param name="bibleVerseReferenceIds"></param>
        /// <param name="noteReferenceIds"></param>
        /// <returns></returns>
        private async Task updateReferenceList
            (int noteId, IEnumerable<int> bibleVerseReferenceIds, IEnumerable<int> noteReferenceIds)
        {
            var allNoteReferences = await getAllNoteReferencesAsync(new int[1] { noteId });
            IList<int> noteReferencesToBeDeleted = new List<int>();
            IList<int> bibleVerseReferencesToBeDeleted = new List<int>();
            IList<int> noteReferencesToBeAdded = new List<int>();
            IList<int> bibleVerseReferencesToBeAdded = new List<int>();

            if ((allNoteReferences != null) && (allNoteReferences.ContainsKey(noteId)))
            {
                foreach (var noteReference in allNoteReferences[noteId][0])
                {
                    if (noteReferenceIds.Contains(noteReference) == false)
                    {
                        noteReferencesToBeDeleted.Add(noteReference);
                    }
                }

                foreach (var noteReferenceId in noteReferenceIds)
                {
                    if (allNoteReferences[noteId][0].Contains(noteReferenceId) == false)
                    {
                        noteReferencesToBeAdded.Add(noteReferenceId);
                    }
                }

                foreach (var bibleVerseReference in allNoteReferences[noteId][1])
                {
                    if (noteReferenceIds.Contains(bibleVerseReference) == false)
                    {
                        bibleVerseReferencesToBeDeleted.Add(bibleVerseReference);
                    }
                }

                foreach (var bibleVerseReferenceId in bibleVerseReferenceIds)
                {
                    if (allNoteReferences[noteId][1].Contains(bibleVerseReferenceId) == false)
                    {
                        bibleVerseReferencesToBeAdded.Add(bibleVerseReferenceId);
                    }
                }

                try
                {
                    await _noteReferenceService.RemoveReferencesAsync(noteId, noteReferencesToBeDeleted, bibleVerseReferencesToBeDeleted);
                }
                catch
                {
                    Console.WriteLine("Failed to remove references from note."); //TODO: log
                }

                try
                {
                    await _noteReferenceService.AssignReferencesAsync(noteId, noteReferencesToBeAdded, bibleVerseReferencesToBeAdded);
                }
                catch
                {
                    Console.WriteLine("Failed to add references to note."); //TODO: log
                }
            }
        }
        #endregion
    }
}
