using System;
using System.Collections.Generic;
using System.Linq;

namespace BibleStudyTool.Core.Entities
{
    public class NoteWithTagsAndReferences
    {
        public int NoteId { get; set; }

        public string Uid { get; set; }

        public string Summary { get; set; }
        public string Text { get; set; }

        public IEnumerable<int> NoteReferences { get; set; }
        public IEnumerable<int> ReferencedByTheseNotes { get; set; }
        public IEnumerable<NoteVerseReference> NoteVerseReferences { get; set; }
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

            NoteReferences = referencedNotes?.Select(rn => rn.Id) ?? new int[0];
            NoteVerseReferences = noteVerseReferences ?? new NoteVerseReference[0];
        }
    }
}
