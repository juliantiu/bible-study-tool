using System;
using System.Collections.Generic;
using BibleStudyTool.Public.DTOs;

namespace BibleStudyTool.Public.Endpoints.BibleReadingEndpoints
{
    public class GetAllBibleBooksResponse
    {
        public IEnumerable<BibleBookDto> BibleBooks { get; set; }
    }
}
