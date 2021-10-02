using System;
using System.Collections.Generic;

namespace BibleStudyTool.Public.Endpoints.SharedEndpoints
{
    public class BibleVerseForChapterItem
    {
        public bool IsNewTestament { get; set; }
        public int BibleVerseId { get; set; }
        public int ChapterNumber { get; set; }
        public int VerseNumber { get; set; }
        public string Text { get; set; }
    }

    public class GetBibleVersesForChapterResponse : ApiResponseBase
    {
        public IList<BibleVerseForChapterItem> BibleVersesForChapter { get; set; } = new List<BibleVerseForChapterItem>();
        public int ChapterNumber { get; set; }
        public int BibleBookId { get; set; }
        public string LanguageCode { get; set; }
    }
}
