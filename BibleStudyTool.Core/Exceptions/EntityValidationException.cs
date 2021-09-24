using System;
namespace BibleStudyTool.Core.Exceptions
{
    public class EntityValidationException : Exception
    {
        public DateTime Timestamp { get; set; }

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
