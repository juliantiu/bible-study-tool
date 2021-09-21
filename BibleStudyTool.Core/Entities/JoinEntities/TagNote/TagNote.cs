using System;
using BibleStudyTool.Core.NonEntityTypes;

namespace BibleStudyTool.Core.Entities.JoinEntities
{
    public class TagNote : BaseEntity
    {
        public long TagId { get; private set; }
        public Tag Tag { get; }

        public long NoteId { get; private set; }
        public Note Note { get;}

        public TagNote()
        {
        }

        public TagNote(long tagId, long noteId)
        {
            TagId = tagId;
            NoteId = noteId;
        }
    }
}
