using System.Collections.Generic;

namespace BibleStudyTool.Public.Endpoints.TagGroupTagEndpoints
{
    public class DeleteTagGroupTagRequestObject
    {
        public int TagGroupId { get; set; }
        public int TagId { get; set; }
    }

    public class DeleteTagGroupTagRequest
    {
        public IList<DeleteTagGroupTagRequestObject> TagGroupTags { get; set; }

        public DeleteTagGroupTagRequest()
        {
        }
    }
}
