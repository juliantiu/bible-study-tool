using System;
namespace BibleStudyTool.Core.Entities.JoinEntities
{
    public class TagNote: BaseEntity
    {
        public int TagNoteId { get; private set; }

        public int TagId { get; private set; }
        public Tag Tag { get; private set; }

        public int NoteId { get; private set; }
        public Note Note { get; private set; }

        public TagNote() { }

        public TagNote(int tagId, Tag tag, int noteId, Note note)
        {
            TagId = tagId;
            Tag = tag;
            NoteId = noteId;
            Note = note;
        }
    }
}
