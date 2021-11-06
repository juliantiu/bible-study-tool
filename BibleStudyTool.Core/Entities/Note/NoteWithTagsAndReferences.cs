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
        public IEnumerable<int> ReferencedBibleVerses { get; set; }
        public IEnumerable<Tag> Tags { get; set; }

        public NoteWithTagsAndReferences() { }

        public NoteWithTagsAndReferences(Note note)
        {
            NoteId = note.NoteId;
            Uid = note.Uid;
            Summary = note.Summary;
            Text = note.Text;
        }

        public NoteWithTagsAndReferences(Note note, IEnumerable<Tag> tags, IEnumerable<NoteReference> noteReferences)
        {
            NoteId = note.NoteId;
            Uid = note.Uid;
            Summary = note.Summary;
            Text = note.Text;

            Tags = tags;

            var referencedNotes = new List<int>();
            var referencedBibleVerseIds = new List<int>();
            foreach (var noteReference in noteReferences)
            {
                if (noteReference.ReferencedNoteId is int rn)
                {
                    referencedNotes.Add(rn);
                }
                else if (noteReference.ReferencedBibleVerseId is int rbv)
                {
                    referencedBibleVerseIds.Add(rbv);
                }
            }

            ReferencedNotes = referencedNotes;
            ReferencedBibleVerses = referencedBibleVerseIds;
        }
    }
}
