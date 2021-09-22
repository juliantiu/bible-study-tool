using System;
namespace BibleStudyTool.Core.Entities.JoinEntities
{
    public enum NoteReferenceType
    {
        BibleVerse,
        Note
    }

    public class NoteReference
    {
        public int OwningNoteId { get; private set; }
        public Note OwningNote { get; }

        public int ReferenceId { get; private set; }
        public Note ReferencedNote { get; }
        public BibleVerse ReferencedBibleVerse { get; }

        public NoteReferenceType NoteReferenceType { get; private set; }

        public NoteReference()
        {
        }

        public NoteReference(int owningNoteId, int referenceId, NoteReferenceType noteReferenceType)
        {
            OwningNoteId = owningNoteId;
            ReferenceId = referenceId;
            NoteReferenceType = noteReferenceType;
        }
    }
}
