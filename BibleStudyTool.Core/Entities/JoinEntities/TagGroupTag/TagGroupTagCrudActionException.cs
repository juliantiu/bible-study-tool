using System;
using BibleStudyTool.Core.Exceptions;

namespace BibleStudyTool.Core.Entities.JoinEntities
{
    public class TagGroupTagCrudActionException : EntityCrudActionException
    {
        public TagGroupTagCrudActionException() => Timestamp = DateTime.UtcNow;

        public TagGroupTagCrudActionException(string message)
            : base(message) => Timestamp = DateTime.UtcNow;

        public TagGroupTagCrudActionException(string message, Exception inner)
            : base(message, inner) => Timestamp = DateTime.UtcNow;
    }
}
