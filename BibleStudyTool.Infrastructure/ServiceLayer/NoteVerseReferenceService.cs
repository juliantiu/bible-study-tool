using BibleStudyTool.Core.Entities;
using BibleStudyTool.Core.Exceptions;
using BibleStudyTool.Core.Interfaces;
using BibleStudyTool.Infrastructure.DAL.Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibleStudyTool.Infrastructure.ServiceLayer
{
    public class NoteVerseReferenceService : INoteVerseReferenceService
    {
        private readonly IAsyncRepository<NoteVerseReference> _noteVerseReferenceRepository;
        private readonly NoteVerseReferenceQueries _noteVerseReferenceQueries;


        public NoteVerseReferenceService(IAsyncRepository<NoteVerseReference> noteVerseReferenceRepository, NoteVerseReferenceQueries noteVerseReferenceQueries)
        {
            _noteVerseReferenceQueries = noteVerseReferenceQueries;
            _noteVerseReferenceRepository = noteVerseReferenceRepository;
        }

        public async Task AssignReferencesAsync(int noteId, IEnumerable<NoteVerseReference> referencedVerses)
        {
            await _noteVerseReferenceRepository.BulkCreateAsync<EntityCrudActionException>(referencedVerses.ToArray());
        }

        public Task<IEnumerable<NoteVerseReference>> GetNotesVerseReferencesAsync(int[] noteIds)
        {
            return _noteVerseReferenceQueries.GetNoteVerseReferences(noteIds);
        }
    }
}
