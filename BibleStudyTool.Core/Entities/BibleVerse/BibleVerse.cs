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
        public string VersionAbbr { get; private set; }
        public string Version { get; private set; }
        public string BookId { get; private set; }
        public string BookAbbr { get; private set; }
        public string BookName { get; private set; }
        public string Text { get; private set; }
        public int ChapterNumber { get; private set; }
        public int VerseNumber { get; private set; }

        public BibleVerse
            (string language,
            string versionAbbr,
            string version,
            string bookId,
            string bookAbbr,
            string bookName,
            string text,
            int chapterNumber,
            int verseNumber)
        {
            Language = language;
            VersionAbbr = versionAbbr;
            Version = version;
            BookId = bookId;
            BookAbbr = bookAbbr;
            BookName = bookName;
            Text = text;
            ChapterNumber = chapterNumber;
            VerseNumber = verseNumber;
        }

    }
}
