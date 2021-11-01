using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BibleStudyTool.Core.Entities;
using BibleStudyTool.Core.Entities.Exceptions;
using BibleStudyTool.Core.Entities.JoinEntities;
using BibleStudyTool.Core.Interfaces;
using BibleStudyTool.Infrastructure.DAL.Npgsql;
using BibleStudyTool.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;

namespace BibleStudyTool.Infrastructure.ServiceLayer
{
    public class NoteService : INoteService
    {
        private readonly NoteQueries _noteQueries;
        private readonly INoteReferenceService _noteReferenceService;
        private readonly IAsyncRepository<Note> _noteRepository;
        private readonly ITagNoteService _tagNoteService;
        private readonly ITagService _tagService;

        public NoteService
            (NoteQueries noteQueries, INoteReferenceService noteReferenceService, IAsyncRepository<Note> noteRepository,
            ITagNoteService tagNoteService, ITagService tagService)
        {
            _noteQueries = noteQueries;
            _noteReferenceService = noteReferenceService;
            _noteRepository = noteRepository;
            _tagNoteService = tagNoteService;
            _tagService = tagService;
        }

        public async Task<Note> CreateAsync
            (string uid, string summary, string text, IEnumerable<int> bibleVerseReferences,
             IEnumerable<int> noteReferences, IEnumerable<int> existingTags, IEnumerable<Tag> newTags)
        {
            // Create the new note
            var noteRef = new Note(uid, summary, text);
            var createdNote = await _noteRepository.CreateAsync<NoteCrudActionException>(noteRef);

            // Create the new tags
            var createdTags = await createNewTags(newTags);

            // Associate existing tags and new tags with note
            await associateTagsWithNote(createdNote.NoteId, existingTags, createdTags);

            // Add the note references
            await addNoteReferences(createdNote.NoteId, noteReferences, bibleVerseReferences);

            return createdNote;
        }

        public async Task<bool> DeleteAsync(string uid, int noteId)
        {
            var note = await getNoteByIdAsync(noteId);
            if (note.Uid != uid)
            {
                return false;
            }
            await _noteRepository.DeleteAsync<NoteCrudActionException>(note);
            return true;
        }

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

        public async Task<Note> UpdateAsync(int noteId, string uid, string summary, string text)
        {
            var note = await getNoteByIdAsync(noteId);
            if (note.Uid != uid)
            {
                return null;
            }
            note.UpdateDetails(summary, text);
            await _noteRepository.UpdateAsync<NoteCrudActionException>(note);
            return note;
        }

        #region HELPER METHODS
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

        private async Task associateTagsWithNote(int noteId, IEnumerable<int> existingTags, IEnumerable<Tag> createdTags)
        {
            try
            {
                var allTagNotes = new List<int>();
                foreach (var existingTagId in existingTags)
                    allTagNotes.Add(existingTagId);
                foreach (var newlyCreatedTag in createdTags)
                    allTagNotes.Add(newlyCreatedTag.TagId);
                await _tagNoteService.BulkCreateTagNotesAsync(noteId, allTagNotes);
            }
            catch
            {
                Console.WriteLine($"{DateTime.Now} :: Something went wrong wtih associating a tag with the note.");
            }

        }

        private async Task addNoteReferences(int noteId, IEnumerable<int> noteReferences, IEnumerable<int> bibleVerseReferences)
        {
            try
            {
                await _noteReferenceService.AssignNoteReferencesAsync(noteId, noteReferences, bibleVerseReferences);
            }
            catch
            {
                Console.WriteLine($"{DateTime.Now} :: Something went wrong with assigning a reference to the note.");
            }
        }

        private async Task<Note> getNoteByIdAsync(int noteId)
        {
            var keyId = new object[] { noteId };
            return await _noteRepository.GetByIdAsync<NoteCrudActionException>(keyId);
        }

        private async Task<IDictionary<int, IList<IList<int>>>> getAllNoteReferencesAsync(int[] noteIds)
        {
            var allReferences = new Dictionary<int, IList<IList<int>>>();
            var parentNoteReferences = await _noteReferenceService.GetParentNoteReferencesAsync(noteIds);
            assignReferencesToNote(allReferences, parentNoteReferences);
            var noteReferences = await _noteReferenceService.GetNoteReferencesAsync(noteIds);
            assignReferencesToNote(allReferences, noteReferences);

            return allReferences;
        }

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
        #endregion
    }
}
