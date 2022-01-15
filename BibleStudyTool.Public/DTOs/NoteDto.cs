using System;
using System.Collections.Generic;
using System.Linq;
using BibleStudyTool.Core.Entities;
using BibleStudyTool.Core.Entities.JoinEntities;

namespace BibleStudyTool.Public.DTOs
{
    public class NoteReferencesContainer
    {
        public IList<int> ReferencedNotes { get; set; } = new List<int>();
        public IList<int> ReferencedBibleVerses { get; set; } = new List<int>();
    }

    public class NoteDto
    {
        public NoteReferencesContainer References { get; set; }
        public IEnumerable<int> Tags { get; set; }

        public int NoteId { get; set; }

        public string Uid { get; set; }

        public string Summary { get; set; }
        public string Text { get; set; }

        public NoteDto()
        {
        }

        public NoteDto(Note noteRef)
        {
            NoteId = noteRef.Id;
            Uid = noteRef.Uid;
            Summary = noteRef.Summary;
            Text = noteRef.Text;
            Tags = noteRef.NoteTags.Select(tagNote => tagNote.Id);
        }

        public NoteDto(Note noteRef, NoteReferencesContainer noteReferenceContainer)
        {
            NoteId = noteRef.Id;
            Uid = noteRef.Uid;
            Summary = noteRef.Summary;
            Text = noteRef.Text;
            Tags = noteRef.NoteTags.Select(tagNote => tagNote.Id);
            References = noteReferenceContainer;
        }
    }
}
