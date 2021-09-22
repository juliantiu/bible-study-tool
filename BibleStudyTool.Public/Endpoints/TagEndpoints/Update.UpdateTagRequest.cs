using System;
namespace BibleStudyTool.Public.Endpoints.TagEndpoints
{
    public class UpdateTagRequest
    {
        public string Color { get; set; }
        public string Label { get; set; }
        public string TagId { get; set; }

        public UpdateTagRequest()
        {
        }
    }
}
