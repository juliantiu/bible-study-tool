using System;
using BibleStudyTool.Core.NonEntityTypes;

namespace BibleStudyTool.Core.Entities.JoinEntities
{
    public class TagGroupTag : BaseEntity
    {
        public long TagGroupId { get; private set; }
        public TagGroup TagGroup { get; }

        public long TagId { get; private set; }
        public Tag Tag { get;}

        public TagGroupTag()
        {
        }

        public TagGroupTag(long tagGroupId, long tagId)
        {
            TagGroupId = tagGroupId;
            TagId = tagId;
        }
    }
}
