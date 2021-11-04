using System;
using System.Collections.Generic;
using BibleStudyTool.Core.Entities;
using BibleStudyTool.Public.DTOs;

namespace BibleStudyTool.Public.Endpoints.BibleReadingEndpoints
{
    public class GetAllLanguagesRespons
    {
        public IEnumerable<LanguageDto> Languages { get; set; }
    }
}
