using System;
using System.Collections.Generic;

namespace BibleStudyTool.Public.Endpoints.NoteTakingEndpoints
{
    public class UpdateTagGroupRequest
    {
        public int TagGroupId { get; set; }
        public IEnumerable<int> tagIds { get; set; }
    }
}
