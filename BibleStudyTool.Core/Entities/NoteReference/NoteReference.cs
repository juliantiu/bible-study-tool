using System;
using BibleStudyTool.Core.Interfaces;

namespace BibleStudyTool.Core.Entities
{
    public class NoteReference : BaseEntity
    {
        public int Id { get; private set; }

        public int NoteId { get; private set; }
        public Note Note { get; private set; }

        public int ReferencedNoteId { get; private set; }
        public Note ReferencedNote { get; private set; }

        public NoteReference()
        {
        }

        public NoteReference(int noteId, int referencedNoteId)
        {
            NoteId = noteId;
            ReferencedNoteId = referencedNoteId;
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

        public void AssociateNoteReference(int referencedNoteId)
        {
            ReferencedNoteId = referencedNoteId;
        }

        public void AssociateNoteReference(int referencedNoteId, Note referencedNote)
        {
            ReferencedNoteId = referencedNoteId;
            ReferencedNote = referencedNote;
        }

        public void AssociateNoteReference(Note referencedNote)
        {
            ReferencedNote = referencedNote;
            NoteId = referencedNote.Id;
        }
    }
}
