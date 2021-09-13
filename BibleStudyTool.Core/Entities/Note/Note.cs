using System;
using System.Collections.Generic;
using BibleStudyTool.Core.Entities.JoinEntities;

namespace BibleStudyTool.Core.Entities
{
    public class Note: BaseEntity
    {
        public int NoteId { get; private set; }

        public string Uid { get; private set; }

        public string Summary { get; private set; }
        public string Text { get; private set; }

        public int NoteReferenceId { get; set; }
        public Note NoteReference { get; set; }
        public IList<Note> NoteReferences { get; set; }

        public IList<TagGroupNote> TagGroupNotes { get; set; }
        public IList<TagNote> TagNotes { get; set; }
        public IList<BibleVerseNote> BibleVerseNotes { get; set; }

        public Note() { }

        public Note(string uid, string summary, string text)
        {
            Uid = uid;
            Summary = summary;
            Text = text;
        }
    }
}
