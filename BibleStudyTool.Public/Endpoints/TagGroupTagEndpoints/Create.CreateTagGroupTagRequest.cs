using System;
using System.Collections.Generic;

namespace BibleStudyTool.Public.Endpoints.TagGroupTagEndpoints
{
    public class CreateTagGroupTagRequestObject
    {
        public int TagGroupId { get; set; }
        public int TagId { get; set; }
    }

    public class CreateTagGroupTagRequest
    {
        public IList<CreateTagGroupTagRequestObject> TagGroupTags { get; set; }

        public CreateTagGroupTagRequest()
        {
        }
    }
}
