using System;
using System.Collections.Generic;
using System.Linq;

namespace BibleStudyTool.Core.Entities
{
    public class TagGroupWithTags : TagGroupBase
    {
        public IList<Tag> Tags { get; private set; }

        public TagGroupWithTags()
        {
        }

        public TagGroupWithTags(TagGroup tagGroup, IEnumerable<Tag> tags)
        {
            Uid = tagGroup.Uid;
            Id = tagGroup.Id;
            Tags = tags.ToList();
        }
    }
}
