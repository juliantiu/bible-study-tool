using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BibleStudyTool.Core.Entities;
using BibleStudyTool.Core.Entities.Exceptions;
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

        public async Task AssignReferencesAsync(int noteId, IEnumerable<int> referencedNotes)
        {
            var noteReferences = new List<NoteReference>();

            foreach (var referencedNote in referencedNotes)
            {
                noteReferences.Add(new NoteReference(noteId, referencedNote));
            }

            await _noteReferenceRepository.BulkCreateAsync<NoteReferenceCrudActionException>(noteReferences.ToArray());
        }

        public async Task RemoveReferencesAsync(int noteId, IEnumerable<int> referencedNotes)
        {
            await _noteReferenceQueries.DeleteNoteReferences(noteId, referencedNotes);
        }

        public Task<IEnumerable<NoteReference>> GetNotesReferencesAsync(int[] noteIds)
        {
            return _noteReferenceQueries.GetNoteReferences(noteIds);
        }

        public Task<IEnumerable<NoteReference>> GetParentNoteReferencesAsync(int[] noteIds)
        {
            return _noteReferenceQueries.GetParentNoteReferencesQueryAsync(noteIds);
        }
    }
}
