using System;
using System.Collections.Generic;
using BibleStudyTool.Core.Entities.JoinEntities;

namespace BibleStudyTool.Public.Endpoints.TagGroupNoteEndpoints
{
    public class CreateTagGroupNoteRequestObject
    {
        public int TagGroupId { get; set; }
        public int NoteId { get; set; }
    }

    public class CreateTagGroupNoteRequest
    {
        public IList<TagGroupNote> TagGroupNotes { get; set; }

        public CreateTagGroupNoteRequest()
        {
        }
    }
}
