using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BibleStudyTool.Core.Entities.JoinEntities;
using BibleStudyTool.Core.Interfaces;
using BibleStudyTool.Infrastructure.DAL.Npgsql;

namespace BibleStudyTool.Infrastructure.ServiceLayer
{
    public class NoteReferenceService : INoteReferenceService
    {
        private readonly IAsyncRepository<NoteReference> _noteReferenceRepository;
        private readonly NoteReferenceQueries _noteReferenceQueries;

        public NoteReferenceService(IAsyncRepository<NoteReference> noteReferenceRepository, NoteReferenceQueries noteReferenceQueries) 
        {
            _noteReferenceRepository = noteReferenceRepository;
            _noteReferenceQueries = noteReferenceQueries;
        }

        public async Task AssignNoteReferencesAsync(int noteId, IEnumerable<int> referencedNotes, IEnumerable<int> referencedBibleVerses)
        {
            var noteReferences = new List<NoteReference>();
            foreach (var referencedNote in referencedNotes)
                noteReferences.Add(new NoteReference(noteId, referencedNote, null));
            foreach (var referencedBibleVerse in referencedBibleVerses)
                noteReferences.Add(new NoteReference(noteId, null, referencedBibleVerse));
            await _noteReferenceRepository.BulkCreateAsync<NoteReferenceCrudActionException>(noteReferences.ToArray());
        }

        public Task<IEnumerable<NoteReference>> GetNoteReferencesAsync(int[] noteIds)
        {
            return _noteReferenceQueries.GetNoteReferences(noteIds);
        }

        public Task<IEnumerable<NoteReference>> GetParentNoteReferencesAsync(int[] noteIds)
        {
            return _noteReferenceQueries.GetParentNoteReferencesQueryAsync(noteIds);
        }
    }
}
