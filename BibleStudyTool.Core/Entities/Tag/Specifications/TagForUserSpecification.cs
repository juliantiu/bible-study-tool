using System;
using System.Collections.Generic;
using BibleStudyTool.Core.Interfaces;
using BibleStudyTool.Core.NonEntityTypes;

namespace BibleStudyTool.Core.Entities.Specifications
{
    public class TagForUserSpecification : ISpecification<Tag>
    {
        public IList<SpecificationClause> SpecificationsClauses { get; set; }

        public TagForUserSpecification(Tag tag)
        {
            SpecificationsClauses = new List<SpecificationClause>();
            PopulateWhereClauses(tag);
        }

        private void PopulateWhereClauses(Tag tag)
        {
            WhereClause<Tag> whereUid = new WhereClause<Tag>();
            whereUid.Expression = t => t.Uid == tag.Uid;
            SpecificationsClauses.Add(whereUid);
        }
    }
}
