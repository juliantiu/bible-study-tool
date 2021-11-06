using System;
using BibleStudyTool.Core.Interfaces;

namespace BibleStudyTool.Core.Entities
{
    public class TagGroupBase : BaseEntity
    {
        public int TagGroupId { get; protected set; }

        public string Uid { get; protected set; }

        public TagGroupBase()
        {
        }
    }
}
