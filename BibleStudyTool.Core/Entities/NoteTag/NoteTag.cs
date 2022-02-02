using System;
using BibleStudyTool.Core.Interfaces;

namespace BibleStudyTool.Core.Entities
{
    public class NoteTag : BaseEntity
    {
        public int TagId { get; private set; }
        public Tag Tag { get; private set; }

        public int NoteId { get; private set; }
        public Note Note { get; private set; }

        public NoteTag()
        {
        }

        public NoteTag(int tagId, int noteId)
        {
            TagId = tagId;
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
            TagId = tagId;
        }

        public void AssociateTag(int tagId, Tag tag)
        {
            TagId = tagId;
            Tag = tag;
        }

        public void AssociateTag(Tag tag)
        {
            Tag = tag;
            TagId = tag.Id;
        }
    }
}
