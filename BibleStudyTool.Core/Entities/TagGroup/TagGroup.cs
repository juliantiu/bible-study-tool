using System;
using System.Collections.Generic;
using BibleStudyTool.Core.Entities.JoinEntities;
using BibleStudyTool.Core.NonEntityTypes;

namespace BibleStudyTool.Core.Entities
{
    public class TagGroup : BaseEntity
    {
        public int TagGroupId { get; private set; }

        public string Label { get; private set; }
        public string Uid { get; private set; }

        public IList<TagGroupNote> TagGroupNotes { get; }
        public IList<TagGroupTag> TagGroupTags { get; }

        public TagGroup() { }

        public TagGroup(string uid, string label)
        {
            Uid = uid;
            Label = label;
        }
    }
}
