using System;
using BibleStudyTool.Core.NonEntityTypes;

namespace BibleStudyTool.Core.Entities.JoinEntities
{
    public class TagNote : BaseEntity
    {
        public string TagNoteId { get; private set; }

        public string TagId { get; private set; }
        public Tag Tag { get; private set; }

        public string NoteId { get; private set; }
        public Note Note { get; private set; }

        public TagNote() { }

        public TagNote(string tagId, Tag tag, string noteId, Note note)
        {
            TagId = tagId;
            Tag = tag;
            NoteId = noteId;
            Note = note;
        }
    }
}
