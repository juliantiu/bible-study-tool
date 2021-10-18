using System;
using System.Collections.Generic;
using BibleStudyTool.Public.DTOs;

namespace BibleStudyTool.Public.Endpoints.TagEndpoints
{
    public class BulkCreateTagsRequest
    {
        public IList<TagDto> Tags { get; set; }

        public BulkCreateTagsRequest()
        {
        }
    }
}
