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
        public string Language { get; private set; }
        public string VersionAbbreviation { get; private set; }
        public string BookKey { get; private set; }
        public string VerseText { get; private set; }
        public int ChapterNumber { get; private set; }
        public int VerseNumber { get; private set; }

        public BibleVerse
            (string language,
            string versionAbbreviation,
            string bookId,
            string verseText,
            int chapterNumber,
            int verseNumber)
        {
            Language = language;
            VersionAbbreviation = versionAbbreviation;
            BookKey = bookId;
            VerseText = verseText;
            ChapterNumber = chapterNumber;
            VerseNumber = verseNumber;
        }

    }
}
