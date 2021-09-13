using System;
using System.Collections.Generic;
using BibleStudyTool.Core.Entities.JoinEntities;

namespace BibleStudyTool.Core.Entities
{
    public class TagGroup: BaseEntity
    {
        public int TagGroupId { get; private set; }

        public string Uid { get; private set; }

        public string Label { get; private set; }

        public IList<TagGroupNote> TagGroupNotes { get; set; }
        public IList<TagGroupTag> TagGroupTags { get; set; }

        public TagGroup() { }

        public TagGroup(string uid, string label)
        {
            Uid = uid;
            Label = label;
        }
    }
}
