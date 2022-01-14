using System;
using System.Collections.Generic;
using BibleStudyTool.Core.Interfaces;
using BibleStudyTool.Core.NonEntityTypes;

namespace BibleStudyTool.Core.Entities.JoinEntities.Specifications
{
    public class BibleBookAbbreviationInLanguageAndStyleSpecification : ISpecification<BibleBookAbbreviationLanguage>
    {
        public IList<SpecificationClause> SpecificationsClauses { get; set; }

        public BibleBookAbbreviationInLanguageAndStyleSpecification(BibleBookAbbreviationLanguage bibleBookAbbreviationLanguage)
        {
            SpecificationsClauses = new List<SpecificationClause>();
            PopulateWhereClauses(bibleBookAbbreviationLanguage);
        }

        private void PopulateWhereClauses(BibleBookAbbreviationLanguage bibleBookAbbreviationLanguage)
        {
            WhereClause<BibleBookAbbreviationLanguage> whereLanguageCodeAndStyle = new WhereClause<BibleBookAbbreviationLanguage>();
            whereLanguageCodeAndStyle.Expression = bbal => (bbal.LanguageCode == bibleBookAbbreviationLanguage.LanguageCode)
                                                        && (bbal.Style == bibleBookAbbreviationLanguage.Style);
            SpecificationsClauses.Add(whereLanguageCodeAndStyle);
        }
    }
}
