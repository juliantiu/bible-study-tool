using System;
using System.Collections.Generic;
using BibleStudyTool.Core.Interfaces;
using BibleStudyTool.Core.NonEntityTypes;

namespace BibleStudyTool.Core.Entities.BibleBookAggregate.Specifications
{
    public class BibleVerseForChapterAndBookSpecification : ISpecification<BibleVerse>
    {
        public IList<SpecificationClause> SpecificationsClauses { get; set; }

        public BibleVerseForChapterAndBookSpecification(BibleVerse bibleVerse)
        {
            SpecificationsClauses = new List<SpecificationClause>();
            PopulateWhereClauses(bibleVerse);
        }

        private void PopulateWhereClauses(BibleVerse bibleVerse)
        {
            WhereClause<BibleVerse> whereBibleBookIdAndChapterNumber = new WhereClause<BibleVerse>();
            whereBibleBookIdAndChapterNumber.Expression = b => (b.BibleBookId == bibleVerse.BibleBookId)
                                                            && (b.ChapterNumber == bibleVerse.ChapterNumber);
            SpecificationsClauses.Add(whereBibleBookIdAndChapterNumber);
        }
    }
}
