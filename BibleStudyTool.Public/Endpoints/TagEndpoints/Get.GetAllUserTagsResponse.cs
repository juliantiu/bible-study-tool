using System;
using System.Collections.Generic;
using BibleStudyTool.Public.DTOs;

namespace BibleStudyTool.Public.Endpoints.TagEndpoints
{
    public class GetAllUserTagsResponse : ApiResponseBase
    {
        public IList<TagDto> Tags { get; set; }
        public string Uid { get; set; }
    }
}
