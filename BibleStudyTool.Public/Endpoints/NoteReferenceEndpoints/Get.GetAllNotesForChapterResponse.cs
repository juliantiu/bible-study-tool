using System.Collections.Generic;
using BibleStudyTool.Public.DTOs;

namespace BibleStudyTool.Public.Endpoints.NoteReferenceEndpoints
{
    public class GetAllNotesForChapterResponse : ApiResponseBase
    {
        public IList<NoteReferenceDto> NoteReferencesForChapter { get; set; }
    }
}
