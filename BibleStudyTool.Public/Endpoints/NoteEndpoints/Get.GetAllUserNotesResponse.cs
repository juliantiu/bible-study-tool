using System;
using System.Collections.Generic;
using BibleStudyTool.Public.DTOs;

namespace BibleStudyTool.Public.Endpoints.NoteEndpoints
{
    public class GetAllUserNotesResponse : ApiResponseBase
    {
        public string Uid { get; set; }
        public IList<NoteDto> Notes { get; set; }
    }
}
