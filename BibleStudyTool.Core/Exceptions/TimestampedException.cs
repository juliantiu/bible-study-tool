using System;
namespace BibleStudyTool.Core.Exceptions
{
    public class TimestampedException : Exception
    {
        public DateTime Timestamp { get; set; }

        public TimestampedException()
        {
            Timestamp = DateTime.UtcNow;
        }

        public TimestampedException(string message)
            : base(message)
        {
            Timestamp = DateTime.UtcNow;
        }

        public TimestampedException(string message, Exception inner)
            : base(message, inner)
        {
            Timestamp = DateTime.UtcNow;
        }
    }
}
