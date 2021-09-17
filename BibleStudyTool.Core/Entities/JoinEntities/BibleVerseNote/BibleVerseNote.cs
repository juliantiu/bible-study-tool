using System;
using BibleStudyTool.Core.Entities.BibleAggregate;

namespace BibleStudyTool.Core.Entities.JoinEntities
{
    public class BibleVerseNote : BaseEntity
    {
        public string BibleVerseNoteId { get; private set; }

        public string BibleVerseId { get; private set; }
        public BibleVerse BibleVerse { get; private set; }

        public string NoteId { get; private set; }
        public Note Note { get; private set; }

        public BibleVerseNote() { }

        public BibleVerseNote(string bibleVerseId, BibleVerse bibleVerse, string noteId, Note note)
        {
            BibleVerseId = bibleVerseId;
            BibleVerse = bibleVerse;
            NoteId = noteId;
            Note = note;
        }
    }
}
