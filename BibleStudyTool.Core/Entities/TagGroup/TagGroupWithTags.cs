using System;
using System.Collections.Generic;
using System.Linq;

namespace BibleStudyTool.Core.Entities
{
    public class TagGroupWithTags : TagGroupBase
    {
        public IList<Tag> TagGroupTags { get; private set; }

        public TagGroupWithTags()
        {
        }

        public TagGroupWithTags(TagGroup tagGroup, IEnumerable<Tag> tagGroupTags)
        {
            Uid = tagGroup.Uid;
            Id = tagGroup.Id;
            TagGroupTags = tagGroupTags.ToList();
        }
    }
}
