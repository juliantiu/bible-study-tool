using System;
using BibleStudyTool.Core.Entities.BibleAggregate;

namespace BibleStudyTool.Core.Entities.JoinEntities
{
    public class BibleVerseNote : BaseEntity
    {
        public int BibleVerseNoteId { get; private set; }

        public int BibleVerseId { get; private set; }
        public BibleVerse BibleVerse { get; private set; }

        public int NoteId { get; private set; }
        public Note Note { get; private set; }

        public BibleVerseNote() { }

        public BibleVerseNote(int bibleVerseId, BibleVerse bibleVerse, int noteId, Note note)
        {
            BibleVerseId = bibleVerseId;
            BibleVerse = bibleVerse;
            NoteId = noteId;
            Note = note;
        }
    }
}
