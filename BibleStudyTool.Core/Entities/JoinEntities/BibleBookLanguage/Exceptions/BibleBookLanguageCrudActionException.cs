using System;
using BibleStudyTool.Core.Exceptions;

namespace BibleStudyTool.Core.Entities.JoinEntities.Exceptions
{
    public class BibleBookLanguageCrudActionException : EntityCrudActionException
    {
        public BibleBookLanguageCrudActionException() => Timestamp = DateTime.UtcNow;
        public BibleBookLanguageCrudActionException(string message) : base(message) => Timestamp = DateTime.UtcNow;
        public BibleBookLanguageCrudActionException(string message, Exception inner) : base(message, inner) => Timestamp = DateTime.UtcNow;
    }
}
