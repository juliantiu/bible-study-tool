using System;
using System.Collections.Generic;
using BibleStudyTool.Public.DTOs;

namespace BibleStudyTool.Public.Endpoints.SharedEnpoints
{
    public class GetAllNotesWithTagsAndReferencesForChapterResponse : ApiResponseBase
    {
        public IList<NoteDto> NotesForChapter { get; set; }
    }
}
