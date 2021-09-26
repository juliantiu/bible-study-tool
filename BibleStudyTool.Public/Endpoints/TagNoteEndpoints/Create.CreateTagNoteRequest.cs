using System;
using System.Collections.Generic;

namespace BibleStudyTool.Public.Endpoints.TagNoteEndpoints
{
    public class CreateTagNoteRequestObject
    {
        public int TagId { get; set; }
        public int NoteId { get; set; }
    }

    public class CreateTagNoteRequest
    {
        public IList<CreateTagNoteRequestObject> TagNotes { get; set; }

        public CreateTagNoteRequest()
        {
        }
    }
}
