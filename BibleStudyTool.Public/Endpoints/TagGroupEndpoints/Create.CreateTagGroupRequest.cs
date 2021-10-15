using System;
using System.Collections.Generic;
using BibleStudyTool.Core.Entities;

namespace BibleStudyTool.Public.Endpoints.TagGroupEndpoints
{
    public class CreateTagGroupRequest
    {
        public IList<int> TagIds { get; set; } 

        public CreateTagGroupRequest()
        {
        }
    }
}
