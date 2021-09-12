using System;
namespace BibleStudyTool.Core.Entities.BibleAggregate
{
    public class BibleBookAbbreviation: BaseEntity
    {
        public int BibleBookAbbreviationId { get; private set; }

        public int BibleBookId { get; private set; }
        public BibleBook BibleBook { get; private set; }

        public string Abbreviation { get; private set; }

        public BibleBookAbbreviation() { }

        public BibleBookAbbreviation(int bibleBookId, BibleBook bibleBook, string abbreviation)
        {
            BibleBookId = bibleBookId;
            BibleBook = bibleBook;
            Abbreviation = abbreviation;
        }
    }
}
