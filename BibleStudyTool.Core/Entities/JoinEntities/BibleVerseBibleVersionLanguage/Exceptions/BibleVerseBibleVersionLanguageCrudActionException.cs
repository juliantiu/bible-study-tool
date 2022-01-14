using System;
using BibleStudyTool.Core.Exceptions;

namespace BibleStudyTool.Core.Entities.JoinEntities.Exceptions
{
    public class BibleVerseBibleVersionLanguageCrudActionException : EntityCrudActionException
    {
        public BibleVerseBibleVersionLanguageCrudActionException() => Timestamp = DateTime.UtcNow;
        public BibleVerseBibleVersionLanguageCrudActionException(string message) : base(message) => Timestamp = DateTime.UtcNow;
        public BibleVerseBibleVersionLanguageCrudActionException(string message, Exception inner) : base(message, inner) => Timestamp = DateTime.UtcNow;
    }
}
