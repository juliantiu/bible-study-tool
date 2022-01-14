using System;
using System.Collections.Generic;
using BibleStudyTool.Core.Entities.JoinEntities;
using BibleStudyTool.Core.Interfaces;

namespace BibleStudyTool.Core.Entities
{
    public class BibleVerse : BaseEntity
    {
        public int BibleVerseId { get; }

        public int ChapterNumber { get; private set; }
        public int VerseNumber { get; private set; }

        public int BibleBookId { get; private set; }
        public BibleBook BibleBook { get; }

        public IList<BibleVerseBibleVersionLanguage> BibleVerseBibleVersionLanguages { get; }
        public IList<NoteReference> ReferencedNotes { get; }

        public BibleVerse()
        {
        }

        public BibleVerse(int chapterNumber, int bibleBookId)
        {
            ChapterNumber = chapterNumber;
            BibleBookId = bibleBookId;
        }

        public BibleVerse(int chapterNumber, int verseNumber, int bibleBookId)
        {
            ChapterNumber = chapterNumber;
            VerseNumber = verseNumber;
            BibleBookId = bibleBookId;
        }

        public BibleVerse(int bibleVerseId, int chapterNumber, int verseNumber, int bibleBookId)
        {
            BibleVerseId = bibleVerseId;
            ChapterNumber = chapterNumber;
            VerseNumber = verseNumber;
            BibleBookId = bibleBookId;
        }
    }
}
