using System;
using System.Collections.Generic;
using System.Linq;
using BibleStudyTool.Core.Entities.JoinEntities;

namespace BibleStudyTool.Core.Entities
{
    public class NoteWithTagsAndReferences
    {
        public int NoteId { get; set; }

        public string Uid { get; set; }

        public string Summary { get; set; }
        public string Text { get; set; }

        public IEnumerable<int> ReferencedNotes { get; set; }
        public IEnumerable<int> NoteVerseReferences { get; set; }
        public IEnumerable<Tag> Tags { get; set; }

        public NoteWithTagsAndReferences() { }

        public NoteWithTagsAndReferences(Note note)
        {
            NoteId = note.Id;
            Uid = note.Uid;
            Summary = note.Summary;
            Text = note.Text;
        }

        public NoteWithTagsAndReferences
            (Note note,
            IEnumerable<Tag> tags,
            IEnumerable<NoteReference> referencedNotes,
            IEnumerable<NoteVerseReference> noteVerseReferences)
        {
            NoteId = note.Id;
            Uid = note.Uid;
            Summary = note.Summary;
            Text = note.Text;

            Tags = tags;

            ReferencedNotes = referencedNotes.Select(rn => rn.Id);
            NoteVerseReferences = noteVerseReferences.Select(nvr => nvr.Id);
        }
    }
}
