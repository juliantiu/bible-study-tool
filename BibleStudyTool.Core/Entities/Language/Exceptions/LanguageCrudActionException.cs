using System;
using BibleStudyTool.Core.Exceptions;

namespace BibleStudyTool.Core.Entities.Exceptions
{
    public class LanguageCrudActionException : EntityCrudActionException
    {
        public LanguageCrudActionException()
        {
            Timestamp = DateTime.UtcNow;
        }

        public LanguageCrudActionException(string message)
            : base(message)
        {
            Timestamp = DateTime.UtcNow;
        }

        public LanguageCrudActionException(string message, Exception inner)
            : base(message, inner)
        {
            Timestamp = DateTime.UtcNow;
        }
    }
}
