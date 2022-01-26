using BibleStudyTool.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibleStudyTool.Infrastructure.ServiceLayer
{
    public interface INoteVerseReferenceService
    {
        Task AssignReferencesAsync(int noteId, IEnumerable<NoteVerseReference> referencedVerses);
        Task<IEnumerable<NoteVerseReference>> GetNotesVerseReferencesAsync(int[] noteIds);
    }
}
