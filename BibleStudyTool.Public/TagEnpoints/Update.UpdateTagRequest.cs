using System;
namespace BibleStudyTool.Public.TagEnpoints
{
    public class UpdateTagRequest
    {
        public int TagId { get; set; }
        public string Label { get; set; }

        public UpdateTagRequest()
        {
        }
    }
}
