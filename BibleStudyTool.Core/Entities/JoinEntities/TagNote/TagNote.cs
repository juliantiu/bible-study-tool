using System;
using BibleStudyTool.Core.Interfaces;

namespace BibleStudyTool.Core.Entities.JoinEntities
{
    public class TagNote : BaseEntity
    {
        public int TagId { get; private set; }
        public Tag Tag { get; }

        public int NoteId { get; private set; }
        public Note Note { get;}

        public TagNote()
        {
        }

        public TagNote(int tagId, int noteId)
        {
            TagId = tagId;
            NoteId = noteId;
        }
    }
}
