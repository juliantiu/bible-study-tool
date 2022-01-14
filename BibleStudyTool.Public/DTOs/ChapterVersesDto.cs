using System;
namespace BibleStudyTool.Public.DTOs
{
    public class ChapterVersesDto
    {
        public bool IsNewTestament { get; set; }

        public int BibleVerseId { get; }
        public int BibleVersionId { get; }
        public int ChapterNumber { get; set; }
        public int VerseNumber { get; set; }

        public string LanguageCode { get; set; }
        public string Text { get; set; }
    }
}
