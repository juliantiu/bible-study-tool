using System;
using System.Collections.Generic;
using BibleStudyTool.Core.Entities.JoinEntities;
using BibleStudyTool.Core.Interfaces;

namespace BibleStudyTool.Core.Entities
{
    public class TagGroup : TagGroupBase
    {

        public IEnumerable<GroupedTag> GroupedTags { get; private set; }

        public TagGroup() { }

        public TagGroup(string uid)
        {
            Uid = uid;
        }

        public TagGroup(int tagGroupId, string uid)
        {
            Id = tagGroupId;
            Uid = uid;
        }

        public void AssignTags(IEnumerable<GroupedTag> groupedTags)
        {
            GroupedTags = groupedTags;
        }

        public void SetTagGroupCretor(string uid)
        {
            Uid = uid;
        }
    }
}
