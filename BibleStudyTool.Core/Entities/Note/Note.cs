using System;
using System.Collections.Generic;
using BibleStudyTool.Core.Entities.JoinEntities;

namespace BibleStudyTool.Core.Entities
{
    public class Note : BaseEntity
    {
        public string NoteId { get; set; }

        public string Uid { get; set; }

        public string Summary { get; set; }
        public string Text { get; set; }

        public IList<TagGroupNote> TagGroupNotes { get; set; }
        public IList<TagNote> TagNotes { get; set; }
        public IList<BibleVerseNote> BibleVerseNotes { get; set; }
        public IList<NoteReference> Notes { get; set; }
        public IList<NoteReference> NoteReferences { get; set; }

        public Note() { }

        public Note(string uid, string summary, string text)
        {
            Uid = uid;
            Summary = summary;
            Text = text;
        }

        public Note UpdateDetails(string summary, string text)
        {
            if (summary is string su)
                Summary = su;
            if (text is string te)
                Text = te;

            return this;
        }
    }
}
