using System;
using BibleStudyTool.Core.Entities;
using BibleStudyTool.Public.DTOs;

namespace BibleStudyTool.Public.Endpoints.NoteEndpoints
{
    public class CreateNoteResponse : ApiResponseBase
    {
        public NoteDto Note { get; set; }

        public CreateNoteResponse()
        {
        }
    }
}
