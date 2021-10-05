using System;
using System.Diagnostics;
using BibleStudyTool.Core.Exceptions;

namespace BibleStudyTool.Core.Entities
{
    public class TagCrudActionException : EntityCrudActionException
    {
        public TagCrudActionException()
        {
            Timestamp = DateTime.UtcNow;
        }

        public TagCrudActionException(string message)
            : base(message)
        {
            Timestamp = DateTime.UtcNow;
        }

        public TagCrudActionException(string message, Exception inner)
            : base(message, inner)
        {
            Timestamp = DateTime.UtcNow;
        }
    }
}
