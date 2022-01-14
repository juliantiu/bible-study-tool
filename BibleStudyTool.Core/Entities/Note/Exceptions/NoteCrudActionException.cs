using System;
using BibleStudyTool.Core.Exceptions;

namespace BibleStudyTool.Core.Entities.Exceptions
{
    public class NoteCrudActionException : EntityCrudActionException
    {
        public NoteCrudActionException()
        {
            Timestamp = DateTime.UtcNow;
        }

        public NoteCrudActionException(string message)
            : base(message)
        {
            Timestamp = DateTime.UtcNow;
        }

        public NoteCrudActionException(string message, Exception inner)
            : base(message, inner)
        {
            Timestamp = DateTime.UtcNow;
        }
    }
}
