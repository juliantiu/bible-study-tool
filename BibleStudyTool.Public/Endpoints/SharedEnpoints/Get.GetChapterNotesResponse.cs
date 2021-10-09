using System;
using System.Collections.Generic;
using BibleStudyTool.Public.DTOs;

namespace BibleStudyTool.Public.Endpoints.SharedEnpoints
{
    public class GetChapterNotesResponse : ApiResponseBase
    {
        public IList<NoteDto> ChapterNotes { get; set; }
    }
}
