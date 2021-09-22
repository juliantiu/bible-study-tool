using System;
using System.Collections.Generic;
using BibleStudyTool.Core.Entities.JoinEntities;
using BibleStudyTool.Core.NonEntityTypes;

namespace BibleStudyTool.Core.Entities
{
    public class Tag : BaseEntity
    {
        public int TagId { get; set; }

        public string Color { get; private set; }
        public string Label { get; private set; }
        public string Uid { get; private set; }

        public IList<TagGroupTag> TagGroupTags { get; }
        public IList<TagNote> TagNotes { get; }

        public Tag() { }

        public Tag(string uid, string label, string color)
        {
            Uid = uid;
            Label = label;
            Color = color;
        }

        public Tag UpdateDetails(string label, string color)
        {
            if (label is string la)
                Label = la;
            if (color is string co)
                Color = co;

            return this;
        }
    }
}
