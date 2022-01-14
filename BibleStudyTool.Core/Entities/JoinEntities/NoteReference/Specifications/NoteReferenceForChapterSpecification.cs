using System;
using System.Collections.Generic;
using System.Linq;
using BibleStudyTool.Core.Interfaces;
using BibleStudyTool.Core.NonEntityTypes;

namespace BibleStudyTool.Core.Entities.JoinEntities.Specifications
{
    public class NoteReferenceForChapterSpecification : ISpecification<NoteReference>
    {
        public IList<SpecificationClause> SpecificationsClauses { get; set; }

        public NoteReferenceForChapterSpecification(int[] bibleVerseIds)
        {
            SpecificationsClauses = new List<SpecificationClause>();
            PopulateWhereClauses(bibleVerseIds);
        }

        private void PopulateWhereClauses(int[] bibleVerseIds)
        {
            WhereClause<NoteReference> whereReferencedBibleVerseIds = new WhereClause<NoteReference>();
            whereReferencedBibleVerseIds.Expression = nr => (bibleVerseIds.Contains(nr.ReferencedBibleVerseId ?? 0));
            SpecificationsClauses.Add(whereReferencedBibleVerseIds);
        }
    }
}
