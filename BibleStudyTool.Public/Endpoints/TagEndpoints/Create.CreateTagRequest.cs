using System;
namespace BibleStudyTool.Public.Endpoints.TagEndpoints
{
    public class CreateTagRequest
    {
        public string Color { get; set; }
        public string Label { get; set; }

        public CreateTagRequest()
        {
        }
    }
}
