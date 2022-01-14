using System;
using System.Collections.Generic;
using BibleStudyTool.Core.Interfaces;
using BibleStudyTool.Core.NonEntityTypes;

namespace BibleStudyTool.Core.Entities.JoinEntities.Specifications
{
    public class BibleBookInLanguageAndStyleSpecification : ISpecification<BibleBookLanguage>
    {
        public IList<SpecificationClause> SpecificationsClauses { get; set; }

        public BibleBookInLanguageAndStyleSpecification(BibleBookLanguage bibleBookLanguage)
        {
            SpecificationsClauses = new List<SpecificationClause>();
            PopulateWhereClauses(bibleBookLanguage);
        }

        private void PopulateWhereClauses(BibleBookLanguage bibleBookLanguage)
        {
            WhereClause<BibleBookLanguage> whereLanguageCodeAndStyle = new WhereClause<BibleBookLanguage>();
            whereLanguageCodeAndStyle.Expression = bbl => (bbl.LanguageCode == bibleBookLanguage.LanguageCode)
                                                       && (bbl.Style == bibleBookLanguage.Style);
            SpecificationsClauses.Add(whereLanguageCodeAndStyle);
        }
    }
}
