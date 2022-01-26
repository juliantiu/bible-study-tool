using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BibleStudyTool.Core.Entities;

namespace BibleStudyTool.Infrastructure.ServiceLayer
{
    public interface INoteService
    {
        Task<NoteWithTagsAndReferences> CreateAsync
            (string uid, string summary, string text, IEnumerable<NoteVerseReference> noteVerseReferences,
            IEnumerable<int> noteReferences, IEnumerable<int> existingTags, IEnumerable<Tag> newTags);
        Task DeleteAsync(string uid, int noteId);
        Task<IEnumerable<NoteWithTagsAndReferences>> GetAllChapterNotesAsync(string uid, int bibleBookId, int chapterNumber);
        Task<NoteWithTagsAndReferences> UpdateAsync
            (int noteId, string uid, string summary, string text,
            IEnumerable<int> tagIds, IEnumerable<int> bibleVerseIds,
            IEnumerable<int> noteReferenceIds, IEnumerable<Tag> newTags);
    }
}
