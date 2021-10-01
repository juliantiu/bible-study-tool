using System;
using BibleStudyTool.Core.Exceptions;

namespace BibleStudyTool.Core.Entities.JoinEntities.Exceptions
{
    public class BibleBookAbbreviationLanguageCrudActionException : EntityCrudActionException
    {
        public BibleBookAbbreviationLanguageCrudActionException() => Timestamp = DateTime.UtcNow;
        public BibleBookAbbreviationLanguageCrudActionException(string message) : base(message) => Timestamp = DateTime.UtcNow;
        public BibleBookAbbreviationLanguageCrudActionException(string message, Exception inner) : base(message, inner) => Timestamp = DateTime.UtcNow;
    }
}
