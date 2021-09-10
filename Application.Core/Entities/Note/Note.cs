using System;
using System.Collections.Generic;
using BibleStudyTool.Core.Entities.JoinEntities;

namespace BibleStudyTool.Core.Entities
{
    public class Note: BaseEntity
    {
        public int Uid { get; private set; }

        public string Summary { get; private set; }
        public string Text { get; private set; }

        public IList<NoteReference> NoteReferences { get; set; }
        public IList<TagGroupNote> TagGroupNotes { get; set; }
        public IList<TagNote> TagNotes { get; set; }
        public IList<VerseNote> VerseNotes { get; set; }

        public Note(int uid, string summary, string text)
        {
            Uid = uid;
            Summary = summary;
            Text = text;
        }
    }
}
