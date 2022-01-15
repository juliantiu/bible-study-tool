using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BibleStudyTool.Core.Entities;

namespace BibleStudyTool.Infrastructure.ServiceLayer
{
    public interface INoteReferenceService
    {
        Task AssignReferencesAsync(int noteId, IEnumerable<int> referencedNotes);
        Task<IEnumerable<NoteReference>> GetNotesReferencesAsync(int[] noteIds);
        Task<IEnumerable<NoteReference>> GetParentNoteReferencesAsync(int[] noteIds);
        Task RemoveReferencesAsync(int noteId, IEnumerable<int> referencedNotes);
    }
}
