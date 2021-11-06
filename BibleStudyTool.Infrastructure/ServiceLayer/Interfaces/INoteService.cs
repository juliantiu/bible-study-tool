using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BibleStudyTool.Core.Entities;

namespace BibleStudyTool.Infrastructure.ServiceLayer
{
    public interface INoteService
    {
        Task<NoteWithTagsAndReferences> CreateAsync
            (string uid, string summary, string text, IEnumerable<int> bibleVerseReferences,
            IEnumerable<int> noteReferences, IEnumerable<int> existingTags, IEnumerable<Tag> newTags);
        Task<bool> DeleteAsync(string uid, int noteId);
        Task<IEnumerable<NoteWithTagsAndReferences>> GetAllChapterNotes(string uid, int bibleBookId, int chapterNumber);
        Task<Note> UpdateAsync(int noteId, string uid, string summary, string text);
    }
}
