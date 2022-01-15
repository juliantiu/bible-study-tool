using System;
using BibleStudyTool.Core.Exceptions;

namespace BibleStudyTool.Core.Entities.Exceptions
{
    public class TagGroupValidationException : EntityValidationException
    {
        public TagGroupValidationException()
        {
            Timestamp = DateTime.UtcNow;
        }

        public TagGroupValidationException(string message)
            : base(message)
        {
            Timestamp = DateTime.UtcNow;
        }

        public TagGroupValidationException(string message, Exception inner)
            : base(message, inner)
        {
            Timestamp = DateTime.UtcNow;
        }
    }
}
