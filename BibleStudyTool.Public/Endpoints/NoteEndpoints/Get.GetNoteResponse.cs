using System;
using System.Collections.Generic;
using BibleStudyTool.Public.DTOs;

namespace BibleStudyTool.Public.Endpoints.NoteEndpoints
{
    public class GetAllUserNotesWithTagsAndReferencesResponse : ApiResponseBase
    {
        public string Uid { get; set; }
        public IList<NoteDto> Notes { get; set; }
    }
}
