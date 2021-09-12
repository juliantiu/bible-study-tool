using System;
namespace BibleStudyTool.Core.Entities.JoinEntities
{
    public class TagGroupNote: BaseEntity
    {
        public int TagGroupNoteId { get; private set; }

        public int TagGroupId { get; private set; }
        public TagGroup TagGroup { get; private set; }

        public int NoteId { get; private set; }
        public Note Note { get; private set; }

        public TagGroupNote() { }

        public TagGroupNote(int tagGroupId, TagGroup tagGroup, int noteId, Note note)
        {
            TagGroupId = tagGroupId;
            TagGroup = tagGroup;
            NoteId = noteId;
            Note = note;
        }
    }
}
