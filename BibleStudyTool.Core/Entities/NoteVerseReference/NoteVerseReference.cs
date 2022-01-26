using BibleStudyTool.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibleStudyTool.Core.Entities
{
    public class NoteVerseReference : BaseEntity
    {
        public int Id { get; private set; }

        public int NoteId { get; private set; }
        public Note Note { get; private set; }

        public string BibleBook { get; private set; }
        public int BookChapter { get; private set; }
        public int ChapterVerseNumber { get; private set; }

        public NoteVerseReference()
        {
        }

        public NoteVerseReference(int noteId, string bibleBook, int bookChapter, int chapterVerseNumber)
        {
            NoteId = noteId;
            BibleBook = bibleBook;
            BookChapter = bookChapter; 
            ChapterVerseNumber = chapterVerseNumber;
        }

        public void AssociateNote(int noteId)
        {
            NoteId = noteId;
        }

        public void AssociateNote(int noteId, Note note)
        {
            NoteId = noteId;
            Note = note;
        }

        public void AssociateNote(Note note)
        {
            Note = note;
            NoteId = Note.Id;
        }

        public void AssociateBibleVerse(string bibleBook, int bookChapter, int chapterVerseNumber)
        {
            BibleBook = bibleBook;
            BookChapter = bookChapter;
            ChapterVerseNumber= chapterVerseNumber;
        }
    }
}
