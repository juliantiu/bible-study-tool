using System;
using BibleStudyTool.Core.Interfaces;

namespace BibleStudyTool.Core.Entities
{
    public class NoteTag : BaseEntity
    {
        public int Id { get; private set; }
        public Tag Tag { get; private set; }

        public int NoteId { get; private set; }
        public Note Note { get; private set; }

        public NoteTag()
        {
        }

        public NoteTag(int tagId, int noteId)
        {
            Id = tagId;
            NoteId = noteId;
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
            NoteId = note.Id;
        }

        public void AssociateTag(int tagId)
        {
            Id = tagId;
        }

        public void AssociateTag(int tagId, Tag tag)
        {
            Id = tagId;
            Tag = tag;
        }

        public void AssociateTag(Tag tag)
        {
            Tag = tag;
            Id = tag.Id;
        }
    }
}
