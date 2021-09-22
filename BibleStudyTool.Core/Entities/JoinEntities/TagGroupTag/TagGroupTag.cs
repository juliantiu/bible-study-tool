using System;
using BibleStudyTool.Core.NonEntityTypes;

namespace BibleStudyTool.Core.Entities.JoinEntities
{
    public class TagGroupTag : BaseEntity
    {
        public int TagGroupId { get; private set; }
        public TagGroup TagGroup { get; }

        public int TagId { get; private set; }
        public Tag Tag { get;}

        public TagGroupTag()
        {
        }

        public TagGroupTag(int tagGroupId, int tagId)
        {
            TagGroupId = tagGroupId;
            TagId = tagId;
        }
    }
}
