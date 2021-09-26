using System;
using BibleStudyTool.Core.Exceptions;

namespace BibleStudyTool.Core.Entities.JoinEntities
{
    public class TagNoteCrudActionException : EntityCrudActionException
    {
        public TagNoteCrudActionException() => Timestamp = DateTime.UtcNow;

        public TagNoteCrudActionException(string message)
            : base(message) => Timestamp = DateTime.UtcNow;

        public TagNoteCrudActionException(string message, Exception inner)
            : base(message, inner) => Timestamp = DateTime.UtcNow;
    }
}
