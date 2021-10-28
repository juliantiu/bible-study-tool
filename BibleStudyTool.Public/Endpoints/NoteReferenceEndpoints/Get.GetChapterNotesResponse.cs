using System.Collections.Generic;
using BibleStudyTool.Public.DTOs;

namespace BibleStudyTool.Public.Endpoints.NoteReferenceEndpoints
{
    public class GetChapterNotesResponse : ApiResponseBase
    {
        public IList<NoteReferenceDto> NoteReferencesForChapter { get; set; } = new List<NoteReferenceDto>();
    }
}
