using System;
using BibleStudyTool.Core.Entities;

namespace BibleStudyTool.Public.Endpoints.TagEndpoints
{
    public class CreateTagResponse : ApiResponseBase
    {
        public Tag Tag { get; set; }
        public CreateTagResponse()
        {
        }
    }
}
