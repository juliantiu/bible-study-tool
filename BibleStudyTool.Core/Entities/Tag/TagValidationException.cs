using System;
using BibleStudyTool.Core.Exceptions;

namespace BibleStudyTool.Core.Entities
{
    public class TagValidationException : EntityValidationException
    {
        public TagValidationException()
        {
            Timestamp = DateTime.UtcNow;
        }

        public TagValidationException(string message)
            : base(message)
        {
            Timestamp = DateTime.UtcNow;
        }

        public TagValidationException(string message, Exception inner)
            : base(message, inner)
        {
            Timestamp = DateTime.UtcNow;
        }
    }
}
