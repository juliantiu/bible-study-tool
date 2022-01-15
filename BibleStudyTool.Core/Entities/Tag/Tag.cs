using System;
using System.Collections.Generic;
using BibleStudyTool.Core.Entities.JoinEntities;
using BibleStudyTool.Core.Interfaces;

namespace BibleStudyTool.Core.Entities
{
    public class Tag : BaseEntity
    {
        public int Id { get; set; }

        public string Color { get; private set; }
        public string Label { get; private set; }
        public string Uid { get; private set; }

        public IEnumerable<GroupedTag> GroupedTags { get; }
        public IEnumerable<NoteTag> NoteTags { get; }

        public Tag() { }

        public Tag(string uid, string label, string color)
        {
            Uid = uid;
            Label = label;
            Color = color;
        }

        public Tag(int tagId, string uid, string label, string color)
        {
            Id = tagId;
            Uid = uid;
            Label = label;
            Color = color;
        }

        public void SetTagCreator(string uid)
        {
            Uid = uid;
        }

        public Tag UpdateDetails(string label, string color)
        {
            Label = label;
            Color = color;

            return this;
        }
    }
}
