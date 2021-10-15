using System;
using System.Collections.Generic;
using BibleStudyTool.Public.DTOs;

namespace BibleStudyTool.Public.Endpoints.SharedEnpoints
{
    public class GetAllUserTagsAndTagGroupsResponse : ApiResponseBase
    {
        public IList<TagDto> Tags { get; set; } = new List<TagDto>();
        public IList<TagGroupDto> TagGroups { get; set; } = new List<TagGroupDto>();
        public string Uid { get; set; }
    }
}
