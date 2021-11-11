using System;
using System.Collections.Generic;
using BibleStudyTool.Core.Entities.JoinEntities;
using BibleStudyTool.Core.Interfaces;

namespace BibleStudyTool.Core.Entities
{
    public class TagGroup : TagGroupBase
    {

        public IEnumerable<TagGroupTag> TagGroupTags { get; private set; }

        public TagGroup() { }

        public TagGroup(string uid)
        {
            Uid = uid;
        }

        public TagGroup(int tagGroupId, string uid)
        {
            TagGroupId = tagGroupId;
            Uid = uid;
        }

        public void AssignTags(IEnumerable<TagGroupTag> tagGroupTags)
        {
            TagGroupTags = tagGroupTags;
        }
    }
}
