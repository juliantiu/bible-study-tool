using System;
using System.Collections.Generic;
using System.Linq;
using BibleStudyTool.Core.Interfaces;
using BibleStudyTool.Core.NonEntityTypes;

namespace BibleStudyTool.Core.Entities.JoinEntities.Specifications
{
    public class BibleVerseTextForChapterAndBookSpecifications : ISpecification<BibleVerseBibleVersionLanguage>
    {
        public IList<SpecificationClause> SpecificationsClauses { get; set; }

        public BibleVerseTextForChapterAndBookSpecifications(BibleVerseBibleVersionLanguage bibleVerseBibleVersionLanguage,
                                                             IEnumerable<int> bibleVerseIds)
        {
            SpecificationsClauses = new List<SpecificationClause>();
            PopulateWhereClauses(bibleVerseBibleVersionLanguage, bibleVerseIds);
        }

        private void PopulateWhereClauses(BibleVerseBibleVersionLanguage bibleVerseBibleVersionLanguage,
                                          IEnumerable<int> bibleVerseIds)
        {
            WhereClause<BibleVerseBibleVersionLanguage> whereUid = new WhereClause<BibleVerseBibleVersionLanguage>();
            whereUid.Expression = b => (b.LanguageCode == bibleVerseBibleVersionLanguage.LanguageCode)
                                    && (b.BibleVersionId == bibleVerseBibleVersionLanguage.BibleVersionId)
                                    && (bibleVerseIds.Contains(b.BibleVerseId));
            SpecificationsClauses.Add(whereUid);
        }
    }
}
