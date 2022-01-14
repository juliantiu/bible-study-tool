using System.Collections.Generic;
using BibleStudyTool.Core.Interfaces;
using BibleStudyTool.Core.NonEntityTypes;

namespace BibleStudyTool.Core.Entities.Specifications
{
    public class NoteWithAllTagsAndReferencesSpecification : ISpecification<Note>
    {
        public IList<SpecificationClause> SpecificationsClauses { get; set; }

        public NoteWithAllTagsAndReferencesSpecification(Note note)
        {
            SpecificationsClauses = new List<SpecificationClause>();
            PopulateIncludeClauses(note);
            PopulateWhereClauses(note);
        }

        private void PopulateWhereClauses(Note note)
        {
            WhereClause<Note> whereUid = new WhereClause<Note>();
            whereUid.Expression = n => n.Uid == note.Uid;
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
