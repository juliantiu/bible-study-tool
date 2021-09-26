using System;
using System.Collections.Generic;
using BibleStudyTool.Core.Entities.JoinEntities;

namespace BibleStudyTool.Public.Endpoints.TagNoteEndpoints
{
    public class DeleteTagNoteRequestObject
    {
        public int TagId { get; set; }
        public int NoteId { get; set; }
    }

    public class DeleteTagNoteRequest
    {
        public IList<TagNote> TagNotes { get; set; }

        public DeleteTagNoteRequest()
        {
        }
    }
}
