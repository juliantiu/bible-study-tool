using System;
using BibleStudyTool.Core.Interfaces;

namespace BibleStudyTool.Core.Entities.JoinEntities
{
    public enum NoteReferenceType
    {
        BibleVerse,
        Note
    }

    public class NoteReference : BaseEntity
    {
        public int NoteId { get; private set; }
        public Note Note { get; }

        public int ReferenceId { get; private set; }
        public Note ReferencedNote { get; }
        public BibleVerse ReferencedBibleVerse { get; }

        public NoteReferenceType NoteReferenceType { get; private set; }

        public NoteReference()
        {
        }

        public NoteReference(int noteId, int referenceId, NoteReferenceType noteReferenceType)
        {
            NoteId = noteId;
            ReferenceId = referenceId;
            NoteReferenceType = noteReferenceType;
        }
    }
}
