using System;
using System.Collections.Generic;
using BibleStudyTool.Core.Entities.JoinEntities;
using BibleStudyTool.Core.NonEntityTypes;

namespace BibleStudyTool.Core.Entities
{
    public class Tag : BaseEntity
    {
        public string TagId { get; set; }

        public string Uid { get; set; }

        public string Label { get; set; }

        public IList<TagGroupTag> TagGroupTags { get; set; }
        public IList<TagNote> TagNotes { get; set; }

        public Tag() { }

        public Tag(string uid, string label)
        {
            Uid = uid;
            Label = label;
        }

        public Tag UpdateDetails(string label)
        {
            if (label is string la)
                Label = la;

            return this;
        }
    }
}
