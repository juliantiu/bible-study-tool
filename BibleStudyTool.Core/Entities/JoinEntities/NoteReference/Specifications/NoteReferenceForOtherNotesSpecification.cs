using System;
using System.Collections.Generic;
using System.Linq;
using BibleStudyTool.Core.Interfaces;
using BibleStudyTool.Core.NonEntityTypes;

namespace BibleStudyTool.Core.Entities.JoinEntities.Specifications
{
    public class NoteReferenceForOtherNotesSpecification : ISpecification<NoteReference>
    {
        public IList<SpecificationClause> SpecificationsClauses { get; set; }

        public NoteReferenceForOtherNotesSpecification(int[] noteIds)
        {
            SpecificationsClauses = new List<SpecificationClause>();
            PopulateWhereClauses(noteIds);
        }

        private void PopulateWhereClauses(int[] noteIds)
        {
            WhereClause<NoteReference> whereReferencedNoteIds = new WhereClause<NoteReference>();
            whereReferencedNoteIds.Expression = nr => (nr.ReferencedNoteId != null)
                                                   && (noteIds.Contains(nr.NoteId));

            SpecificationsClauses.Add(whereReferencedNoteIds);
        }
    }
}
