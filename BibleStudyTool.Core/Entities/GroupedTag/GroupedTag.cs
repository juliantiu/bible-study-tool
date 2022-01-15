using System;
using BibleStudyTool.Core.Interfaces;

namespace BibleStudyTool.Core.Entities
{
    public class GroupedTag : BaseEntity
    {
        public int TagGroupId { get; private set; }
        public TagGroup TagGroup { get; }

        public int TagId { get; private set; }
        public Tag Tag { get;}

        public GroupedTag()
        {
        }

        public GroupedTag(int tagGroupId, int tagId)
        {
            TagGroupId = tagGroupId;
            TagId = tagId;
        }
    }
}
