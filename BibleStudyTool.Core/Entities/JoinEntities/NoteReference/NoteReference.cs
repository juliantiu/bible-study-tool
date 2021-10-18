using System;
using BibleStudyTool.Core.Interfaces;

namespace BibleStudyTool.Core.Entities.JoinEntities
{
    public class NoteReference : BaseEntity
    {
        public int NoteReferenceSurrogateKey { get; set; }

        public int NoteId { get; private set; }
        public Note Note { get; }

        public int? ReferencedNoteId { get; private set; }
        public Note ReferencedNote { get; }

        public int? ReferencedBibleVerseId { get; private set; }
        public BibleVerse ReferencedBibleVerse { get; }

        public NoteReference()
        {
        }

        public NoteReference(int noteId, int? referencedNoteId, int? referencedBibleVerseId)
        {
            NoteId = noteId;
            ReferencedNoteId = referencedNoteId;
            ReferencedBibleVerseId = referencedBibleVerseId;
        }
    }
}
