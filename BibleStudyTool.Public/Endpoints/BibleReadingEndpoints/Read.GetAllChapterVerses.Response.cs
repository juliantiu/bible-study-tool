using System;
using System.Collections.Generic;
using BibleStudyTool.Public.DTOs;

namespace BibleStudyTool.Public.Endpoints.BibleReadingEndpoints
{
    public class GetAllChapterVersesResponse
    {
        public IEnumerable<ChapterVersesDto> ChapterVerses { get; set; }
    }
}
