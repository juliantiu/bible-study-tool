using System;
namespace BibleStudyTool.Core.Entities.JoinEntities
{
    public class NoteReference : BaseEntity
    {
        public string NoteReferenceId { get; private set; }

        public string NoteId { get; private set; }
        public Note Note { get; private set; }

        public string ReferenceId { get; private set; }
        public Note Reference { get; private set; }

        public NoteReference()
        {
        }
    }
}

/* Notes & References
 * ******************
 * * Self-referencing, many-to-many relationships
 * ** https://stackoverflow.com/questions/61134713/entity-framework-core-3-0-creating-a-self-referencing-many-to-many-relationshi
 * */