using BibleStudyTool.Core.Interfaces.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibleStudyTool.Core.Entities.BibleVerse
{
    public class BibleVerse : BaseEntity
    {
        public string VerseReferenceKey { get; private set; }
        public string Language { get; private set; }
        public string Version { get; private set; }
        public string BookKey { get; private set; }
        public string Text { get; private set; }
        public int ChapterNumber { get; private set; }
        public int VerseNumber { get; private set; }

        public BibleVerse
            (string verseReferenceKey,
            string language,
            string version,
            string bookKey,
            string text,
            int chapterNumber,
            int verseNumber)
        {
            VerseReferenceKey = verseReferenceKey;
            Language = language;
            Version = version;
            BookKey = bookKey;
            Text = text;
            ChapterNumber = chapterNumber;
            VerseNumber = verseNumber;
        }

    }
}
