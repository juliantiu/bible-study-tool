using System;
using System.Collections.Generic;
using BibleStudyTool.Public.DTOs;

namespace BibleStudyTool.Public.Endpoints.BibleReadingEndpoints
{
    public class GetAllBibleVersionsResponse
    {
        public IEnumerable<BibleVersionDto> BibleVersions { get; set; }
    }
}
