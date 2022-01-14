using System;
using BibleStudyTool.Core.Exceptions;

namespace BibleStudyTool.Core.Entities.BibleBookAggregate.Exceptions
{
    public class BibleVerseCrudActionException : EntityCrudActionException
    {
        public BibleVerseCrudActionException() => Timestamp = DateTime.UtcNow;
        public BibleVerseCrudActionException(string message) : base(message) => Timestamp = DateTime.UtcNow;
        public BibleVerseCrudActionException(string message, Exception inner) : base(message, inner) => Timestamp = DateTime.UtcNow;
    }
}
