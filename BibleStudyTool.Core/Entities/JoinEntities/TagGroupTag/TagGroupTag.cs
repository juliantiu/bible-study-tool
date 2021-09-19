using System;
using BibleStudyTool.Core.NonEntityTypes;

namespace BibleStudyTool.Core.Entities.JoinEntities
{
    public class TagGroupTag : BaseEntity
    {
        public string TagGroupTagId { get; private set; }

        public string TagGroupId { get; private set; }
        public TagGroup TagGroup { get; private set; }

        public string TagId { get; private set; }
        public Tag Tag { get; private set; }

        public TagGroupTag() { }

        public TagGroupTag(string tagGroupId, TagGroup tagGroup, string tagId, Tag tag)
        {
            TagGroupId = tagGroupId;
            TagGroup = tagGroup;
            TagId = tagId;
            Tag = tag;
        }
    }
}
