using System;
using BibleStudyTool.Core.Interfaces;

namespace BibleStudyTool.Core.Entities.JoinEntities
{
    public class TagGroupNote : BaseEntity
    {
        public int TagGroupId { get; private set; }
        public TagGroup TagGroup { get; }

        public int NoteId { get; private set; }
        public Note Note { get; }

        public TagGroupNote()
        {
        }

        public TagGroupNote(int tagGroupId, int noteId)
        {
            TagGroupId = tagGroupId;
            NoteId = noteId;
        }
    }
}
