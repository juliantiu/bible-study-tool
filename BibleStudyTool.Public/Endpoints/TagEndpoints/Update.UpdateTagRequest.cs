using System;
namespace BibleStudyTool.Public.Endpoints.TagEndpoints
{
    public class UpdateTagRequest
    {
        public string TagId { get; set; }
        public string Label { get; set; }

        public UpdateTagRequest()
        {
        }
    }
}
