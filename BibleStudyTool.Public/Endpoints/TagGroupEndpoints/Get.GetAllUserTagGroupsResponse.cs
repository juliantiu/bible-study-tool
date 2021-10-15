using System;
using System.Collections.Generic;
using BibleStudyTool.Public.DTOs;

namespace BibleStudyTool.Public.Endpoints.TagGroupEndpoints
{
    public class GetAllUserTagGroupsResponse : ApiResponseBase
    {
        public IList<TagGroupDto> TagGroups { get; set; } = new List<TagGroupDto>();
        public string Uid { get; set; } = string.Empty;
    }
}
