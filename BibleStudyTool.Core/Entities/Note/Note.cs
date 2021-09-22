using System;
using System.Collections.Generic;
using BibleStudyTool.Core.Entities.JoinEntities;
using BibleStudyTool.Core.NonEntityTypes;

namespace BibleStudyTool.Core.Entities
{
    public class Note : BaseEntity
    {
        public int NoteId { get; }

        public string Uid { get; }

        public string Summary { get; private set; }
        public string Text { get; private set; }

        public IList<TagGroupNote> TagGroupNotes { get; }
        public IList<TagNote> TagNotes { get; }
        public IList<NoteReference> ReferencedNotes { get; }
        public IList<NoteReference> ReferencedIn { get; }

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
