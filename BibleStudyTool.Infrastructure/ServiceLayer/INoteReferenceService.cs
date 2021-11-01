using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BibleStudyTool.Core.Entities.JoinEntities;

namespace BibleStudyTool.Infrastructure.ServiceLayer
{
    public interface INoteReferenceService
    {
        Task AssignNoteReferencesAsync(int noteId, IEnumerable<int> referencedNotes, IEnumerable<int> referencedBibleVerses);
        Task<IEnumerable<NoteReference>> GetNoteReferencesAsync(int[] noteIds);
        Task<IEnumerable<NoteReference>> GetParentNoteReferencesAsync(int[] noteIds);
    }
}
