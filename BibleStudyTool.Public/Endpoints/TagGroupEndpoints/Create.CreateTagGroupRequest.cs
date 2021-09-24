using System;
namespace BibleStudyTool.Public.Endpoints.TagGroupEndpoints
{
    public class CreateTagGroupRequest
    {
        public string Uid { get; set; }
        public string Label { get; set; }

        public CreateTagGroupRequest()
        {
        }
    }
}
