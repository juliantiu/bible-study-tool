using System;
using System.Collections.Generic;
using BibleStudyTool.Core.Interfaces;
using BibleStudyTool.Core.NonEntityTypes;

namespace BibleStudyTool.Core.Entities.Specifications
{
    public class TagGroupForUserSpecification : ISpecification<TagGroup>
    {
        public IList<SpecificationClause> SpecificationsClauses { get; set; }

        public TagGroupForUserSpecification(TagGroup tagGroup)
        {
            SpecificationsClauses = new List<SpecificationClause>();
            PopulateWhereClauses(tagGroup);
            PopulateIncludeClauses();
        }

        private void PopulateWhereClauses(TagGroup tagGroup)
        {
            WhereClause<TagGroup> whereUid = new WhereClause<TagGroup>();
            whereUid.Expression = tg => tg.Uid == tagGroup.Uid;
            SpecificationsClauses.Add(whereUid);
        }

        private void PopulateIncludeClauses()
        {
            var TagGroupReference = new TagGroup();
            IncludeClause includeTagGroupTags = new IncludeClause();
            includeTagGroupTags.PropertyName = nameof(TagGroupReference.TagGroupTags);
            SpecificationsClauses.Add(includeTagGroupTags);
        }
    }
}
