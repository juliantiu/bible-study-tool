using System;
using BibleStudyTool.Core.Entities;

namespace BibleStudyTool.Public.DTOs
{
    public class TagDto
    {
        public int TagId { get; set; }
        public string Color { get; set; }
        public string Label { get; set; }
        public string Uid { get; set; }

        public TagDto() { }

        public TagDto(Tag tag)
        {
            TagId = tag.Id;
            Color = tag.Color;
            Label = tag.Label;
            Uid = tag.Uid;
        }
    }
}
