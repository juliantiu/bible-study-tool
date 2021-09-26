using System;
using BibleStudyTool.Core.Exceptions;

namespace BibleStudyTool.Core.Entities.JoinEntities
{
    public class TagGroupNoteCrudActionException : EntityCrudActionException
    {
        public TagGroupNoteCrudActionException() => Timestamp = DateTime.UtcNow;

        public TagGroupNoteCrudActionException(string message)
            : base(message) => Timestamp = DateTime.UtcNow;

        public TagGroupNoteCrudActionException(string message, Exception inner)
            : base(message, inner) => Timestamp = DateTime.UtcNow;
    }
}
