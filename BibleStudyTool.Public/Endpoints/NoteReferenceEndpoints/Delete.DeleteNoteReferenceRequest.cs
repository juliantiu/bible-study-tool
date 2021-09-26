using System;
using System.Collections.Generic;

namespace BibleStudyTool.Public.Endpoints.NoteReferenceEndpoints
{
    public class DeleteNoteReferenceRequestObject
    {
        public int NoteId { get; set; }
        public int ReferenceId { get; set; }
    }

    public class DeleteNoteReferenceRequest
    {
        public IList<DeleteNoteReferenceRequestObject> NoteReferences { get; set; }

        public DeleteNoteReferenceRequest()
        {
        }
    }
}
