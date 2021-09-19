using System;
using System.Collections.Generic;
using BibleStudyTool.Core.Entities.JoinEntities;
using BibleStudyTool.Core.NonEntityTypes;

namespace BibleStudyTool.Core.Entities.BibleAggregate
{
    public class BibleVerse : BaseEntity
    {
        public string BibleVerseId { get; private set; }

        public string BibleBookId { get; private set; }
        public BibleBook BibleBook { get; private set; }

        public int ChapterNumber { get; private set; }
        public int VerseNumber { get; private set; }
        public string Text { get; private set; }

        public IList<BibleVerseNote> BibleVerseNotes { get; set; }

        public BibleVerse() { }

        public BibleVerse(string bibleBookId, BibleBook bibleBook, int chapterNumber, int verseNumber, string text)
        {
            BibleBookId = bibleBookId;
            BibleBook = bibleBook;
            ChapterNumber = chapterNumber;
            VerseNumber = verseNumber;
            Text = text;
        }
    }
}
