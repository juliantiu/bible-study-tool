using System;
namespace BibleStudyTool.Public.Endpoints.TagGroupEndpoints
{
    public class UpdateTagGroupRequest
    {
        public string TagGroupId { get; set; }
        public string Label { get; set; }

        public UpdateTagGroupRequest()
        {
        }
    }
}
