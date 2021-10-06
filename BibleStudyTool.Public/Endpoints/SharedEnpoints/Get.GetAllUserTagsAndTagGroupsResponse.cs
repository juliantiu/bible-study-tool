using System;
using System.Collections.Generic;
using BibleStudyTool.Public.DTOs;

namespace BibleStudyTool.Public.Endpoints.SharedEnpoints
{
    public class GetAllUserTagsAndTagGroupsResponse : ApiResponseBase
    {
        public IList<TagDto> Tags { get; set; }
        public IList<TagGroupDto> TagGroups { get; set; }
        public string Uid { get; set; }
    }
}
