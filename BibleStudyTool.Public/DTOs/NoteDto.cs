using System;
using System.Collections.Generic;
using System.Linq;
using BibleStudyTool.Core.Entities;
using BibleStudyTool.Core.Entities.JoinEntities;

namespace BibleStudyTool.Public.DTOs
{
    public class NoteDto
    {
        public IList<NoteDto> ReferencedIn { get; set; }
        public IList<NoteDto> ReferencedNotes { get; set; }
        public IList<TagNote> Tags { get; set; }

        public int NoteId { get; set; }

        public string Uid { get; set; }

        public string Summary { get; set; }
        public string Text { get; set; }

        public NoteDto()
        {
        }

        public NoteDto(Note noteRef)
        {
            NoteId = noteRef.NoteId;
            Uid = noteRef.Uid;
            Summary = noteRef.Summary;
            Text = noteRef.Summary;

            Tags = noteRef.TagNotes;
        }
    }
}
