using System;
using System.Collections.Generic;
using System.Linq;
using BibleStudyTool.Core.Interfaces;
using BibleStudyTool.Core.NonEntityTypes;

namespace BibleStudyTool.Core.Entities.Specifications
{
    public class NoteFromNoteIdsSpecification : ISpecification<Note>
    {
        public IList<SpecificationClause> SpecificationsClauses { get; set; }

        public NoteFromNoteIdsSpecification(Note note, int[] noteIds)
        {
            SpecificationsClauses = new List<SpecificationClause>();
            PopulateIncludeClauses(note);
            PopulateWhereClauses(note, noteIds);
        }

        private void PopulateWhereClauses(Note note, int[] noteIds)
        {
            WhereClause<Note> whereUid = new WhereClause<Note>();
            whereUid.Expression = n => (n.Uid == note.Uid)
                                    && (noteIds.Contains(n.NoteId));
            SpecificationsClauses.Add(whereUid);
        }

        private void PopulateIncludeClauses(Note note)
        {
            IncludeClause includeNoteReferences = new IncludeClause();
            includeNoteReferences.PropertyName = nameof(note.ReferencedNotes);
            SpecificationsClauses.Add(includeNoteReferences);

            IncludeClause includeTags = new IncludeClause();
            includeTags.PropertyName = nameof(note.TagNotes);
            SpecificationsClauses.Add(includeTags);
        }
    }
}
