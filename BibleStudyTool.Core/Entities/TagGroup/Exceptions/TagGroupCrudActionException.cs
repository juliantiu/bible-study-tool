using System;
using BibleStudyTool.Core.Exceptions;
using BibleStudyTool.Core.NonEntityTypes;

namespace BibleStudyTool.Core.Entities.Exceptions
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
