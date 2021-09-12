using System;
namespace BibleStudyTool.Core.Entities.JoinEntities
{
    public class TagGroupTag: BaseEntity
    {
        public int TagGroupTagId { get; private set; }

        public int TagGroupId { get; private set; }
        public TagGroup TagGroup { get; private set; }

        public int TagId { get; private set; }
        public Tag Tag { get; private set; }

        public TagGroupTag() { }

        public TagGroupTag(int tagGroupId, TagGroup tagGroup, int tagId, Tag tag)
        {
            TagGroupId = tagGroupId;
            TagGroup = tagGroup;
            TagId = tagId;
            Tag = tag;
        }
    }
}
