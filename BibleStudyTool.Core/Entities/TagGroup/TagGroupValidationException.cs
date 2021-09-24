using System;
using BibleStudyTool.Core.Exceptions;
using BibleStudyTool.Core.NonEntityTypes;

namespace BibleStudyTool.Core.Entities
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
