using System;
using BibleStudyTool.Core.NonEntityTypes;

namespace BibleStudyTool.Core.Entities.JoinEntities
{
    public class TagGroupNote : BaseEntity
    {
        public long TagGroupId { get; private set; }
        public TagGroup TagGroup { get; }

        public long NoteId { get; private set; }
        public Note Note { get; }

        public TagGroupNote()
        {
        }

        public TagGroupNote(long tagGroupId, long noteId)
        {
            TagGroupId = tagGroupId;
            NoteId = noteId;
        }
    }
}
