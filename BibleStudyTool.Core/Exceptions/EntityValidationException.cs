using System;
namespace BibleStudyTool.Core.Exceptions
{
    public class EntityValidationException : TimestampedException
    {
        public EntityValidationException()
        {
        }

        public EntityValidationException(string message)
            : base(message)
        {
        }

        public EntityValidationException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
