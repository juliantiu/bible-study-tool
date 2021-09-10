using System;
namespace BibleStudyTool.Core.Entities.JoinEntities
{
    public class NoteReference: BaseEntity
    {
        public int NoteId { get; private set; }
        public Note Note { get; private set; }

        public int NoteReferenceId { get; private set; }
        public Note NoteRef { get; private set; }

        public NoteReference(int noteId, Note note, int noteReferenceId, Note noteRef)
        {
            NoteId = noteId;
            Note = note;
            NoteReferenceId = noteReferenceId;
            NoteRef = noteRef;
        }
    }
}
