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
        public long NoteReferenceId { get; }

        public long OwningNoteId { get; private set; }
        public Note OwningNote { get; }

        public long ReferencedNoteId { get; private set; }
        public Note ReferencedNote { get; }

        public int BibleVerseId { get; private set; }
        public BibleVerse BibleVerse { get; }

        public NoteReferenceType NoteReferenceType { get; private set; }

        public NoteReference()
        {
        }

        public NoteReference(long owningNoteId, long referencedNoteId, int bibleVerseId, NoteReferenceType noteReferenceType)
        {
            OwningNoteId = owningNoteId;
            ReferencedNoteId = referencedNoteId;
            BibleVerseId = bibleVerseId;
            NoteReferenceType = noteReferenceType;
        }
    }
}
