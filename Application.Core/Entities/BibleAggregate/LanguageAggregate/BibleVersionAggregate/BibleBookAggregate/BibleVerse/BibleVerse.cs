using System;
using System.Collections.Generic;
using BibleStudyTool.Core.Entities.JoinEntities;

namespace BibleStudyTool.Core.Entities.BibleAggregate
{
    public class BibleVerse : BaseEntity
    {
        public int BibleBookId { get; private set; }
        public BibleBook BibleBook { get; private set; }

        public int ChapterNumber { get; private set; }
        public int VerseNumber { get; private set; }
        public string Text { get; private set; }

        public IList<VerseNote> VerseNotes { get; set; }

        public BibleVerse(int bibleBookId, BibleBook bibleBook, int chapterNumber, int verseNumber, string text)
        {
            BibleBookId = bibleBookId;
            BibleBook = bibleBook;
            ChapterNumber = chapterNumber;
            VerseNumber = verseNumber;
            Text = text;
        }
    }
}
