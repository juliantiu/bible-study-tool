using System;
using System.Collections.Generic;
using System.Linq;
using BibleStudyTool.Core.NonEntityInterfaces;
using BibleStudyTool.Core.NonEntityTypes;

namespace BibleStudyTool.Core.Entities
{
    public class NoteWithAllReferencesSpecification : ISpecification<Note>
    {
        public IList<SpecificationClause> SpecificationClauses { get; set; }

        public NoteWithAllReferencesSpecification(string uid)
        {
            SpecificationClauses = new List<SpecificationClause>();

            WhereClause<Note> whereUid = new WhereClause<Note>();
            whereUid.Value = note => note.Uid == uid;

            IncludeClause<Note> includeNoteReferences = new IncludeClause<Note>();
            includeNoteReferences.PropertyName = "NoteReferences";

            IncludeClause<Note> includeBibleVerseNotes = new IncludeClause<Note>();
            includeBibleVerseNotes.PropertyName = "BibleVerseNotes";

            IncludeClause<Note> includeTags = new IncludeClause<Note>();
            includeTags.PropertyName = "TagNotes";

            IncludeClause<Note> includeTagGroups = new IncludeClause<Note>();
            includeTagGroups.PropertyName = "TagGroupNotes";


            SpecificationClauses.Add(whereUid);
            SpecificationClauses.Add(includeNoteReferences);
            SpecificationClauses.Add(includeBibleVerseNotes);
            SpecificationClauses.Add(includeTags);
            SpecificationClauses.Add(includeTagGroups);
        }
    }
}
