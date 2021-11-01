using System;
using System.Collections.Generic;
using BibleStudyTool.Core.Entities.JoinEntities;

namespace BibleStudyTool.Core.Entities
{
    public class NoteWithTagsAndReferences
    {
        public int NoteId { get; set; }

        public string Uid { get; set; }

        public string Summary { get; set; }
        public string Text { get; set; }

        public IList<int> ReferencedNotes { get; set; }
        public IList<int> ReferencedBibleVerses { get; set; }
        public IList<Tag> Tags { get; set; }

        public NoteWithTagsAndReferences() { }

        public NoteWithTagsAndReferences(Note note)
        {
            NoteId = note.NoteId;
            Uid = note.Uid;
            Summary = note.Summary;
            Text = note.Text;
        }
    }
}
