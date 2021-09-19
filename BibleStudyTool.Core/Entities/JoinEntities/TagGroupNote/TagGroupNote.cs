using System;
using BibleStudyTool.Core.NonEntityTypes;

namespace BibleStudyTool.Core.Entities.JoinEntities
{
    public class TagGroupNote : BaseEntity
    {
        public string TagGroupNoteId { get; private set; }

        public string TagGroupId { get; private set; }
        public TagGroup TagGroup { get; private set; }

        public string NoteId { get; private set; }
        public Note Note { get; private set; }

        public TagGroupNote() { }

        public TagGroupNote(string tagGroupId, TagGroup tagGroup, string noteId, Note note)
        {
            TagGroupId = tagGroupId;
            TagGroup = tagGroup;
            NoteId = noteId;
            Note = note;
        }
    }
}
