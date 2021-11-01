using System;
using BibleStudyTool.Public.DTOs;

namespace BibleStudyTool.Public.Endpoints.NoteEndpoints
{
    public class UpdateNoteResponse : ApiResponseBase
    {
        public NoteDto Note { get; set; }

        public UpdateNoteResponse()
        {
        }
    }
}
