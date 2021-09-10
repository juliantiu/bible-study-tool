using System;
using System.Collections.Generic;
using BibleStudyTool.Core.Entities.JoinEntities;

namespace BibleStudyTool.Core.Entities
{
    public class Tag: BaseEntity
    {
        public int Uid { get; private set; }

        public string Label { get; private set; }

        public IList<TagGroupTag> TagGroupTags { get; set; }
        public IList<TagNote> TagNotes { get; set; }

        public Tag(int uid, string label)
        {
            Uid = uid;
            Label = label;
        }
    }
}
