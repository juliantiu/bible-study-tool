using System;
namespace BibleStudyTool.Core.Entities.BibleAggregate
{
    public class BibleBookAbbreviation : BaseEntity
    {
        public string BibleBookAbbreviationId { get; private set; }

        public string BibleBookId { get; private set; }
        public BibleBook BibleBook { get; private set; }

        public string Abbreviation { get; private set; }

        public BibleBookAbbreviation() { }

        public BibleBookAbbreviation(string bibleBookId, BibleBook bibleBook, string abbreviation)
        {
            BibleBookId = bibleBookId;
            BibleBook = bibleBook;
            Abbreviation = abbreviation;
        }
    }
}
