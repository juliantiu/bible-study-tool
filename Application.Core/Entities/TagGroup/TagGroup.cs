using System;
using System.Collections.Generic;
using BibleStudyTool.Core.Entities.JoinEntities;

namespace BibleStudyTool.Core.Entities
{
    public class TagGroup: BaseEntity
    {
        public int Uid { get; private set; }

        public string Label { get; private set; }

        public IList<TagGroupNote> TagGroupNotes { get; set; }
        public IList<TagGroupTag> TagGroupTags { get; set; }

        public TagGroup(int uid, string label)
        {
            Uid = uid;
            Label = label;
        }
    }
}
