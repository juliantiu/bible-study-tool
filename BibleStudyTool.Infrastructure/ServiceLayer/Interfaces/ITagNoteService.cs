using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BibleStudyTool.Core.Entities;
using BibleStudyTool.Core.Entities.JoinEntities;

namespace BibleStudyTool.Infrastructure.ServiceLayer
{
    public interface ITagNoteService
    {
        Task BulkCreateTagNotesAsync(int noteId, IEnumerable<int> tagIds);
        Task<IDictionary<int, IList<Tag>>> GetTagsForNotesAsync(int[] noteIds);
        Task RemoveTagsFromNote(int noteId, IEnumerable<int> tagIdsToDelete);
    }
}
