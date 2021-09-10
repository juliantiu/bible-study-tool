using System;
using BibleStudyTool.Core.Entities.BibleAggregate;

namespace BibleStudyTool.Core.Entities.JoinEntities
{
    public class VerseNote : BaseEntity
    {
        public int BibleVerseId { get; private set; }
        public BibleVerse BibleVerse { get; private set; }

        public int NoteId { get; private set; }
        public Note Note { get; private set; }

        public VerseNote(int bibleVerseId, BibleVerse bibleVerse, int noteId, Note note)
        {
            BibleVerseId = bibleVerseId;
            BibleVerse = bibleVerse;
            NoteId = noteId;
            Note = note;
        }
    }
}
