using System;
using System.Collections.Generic;

namespace BibleStudyTool.Core.Entities
{
    public class TagGroupWithTags : TagGroupBase
    {
        public IEnumerable<Tag> TagGroupTags { get; private set; }

        public TagGroupWithTags()
        {
        }

        public TagGroupWithTags(TagGroup tagGroup, IEnumerable<Tag> tagGroupTags)
        {
            Uid = tagGroup.Uid;
            TagGroupId = tagGroup.TagGroupId;
            TagGroupTags = tagGroupTags;
        }
    }
}
