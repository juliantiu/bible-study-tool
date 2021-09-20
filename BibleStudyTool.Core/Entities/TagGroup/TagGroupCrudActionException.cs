using System;
using BibleStudyTool.Core.NonEntityTypes;

namespace BibleStudyTool.Core.Entities
{
    public class TagGroupCrudActionException : EntityCrudActionException
    {
        public TagGroupCrudActionException()
        {
            Timestamp = DateTime.UtcNow;
        }

        public TagGroupCrudActionException(string message)
            : base(message)
        {
            Timestamp = DateTime.UtcNow;
        }

        public TagGroupCrudActionException(string message, Exception inner)
            : base(message, inner)
        {
            Timestamp = DateTime.UtcNow;
        }
    }
}
