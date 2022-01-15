using System;
namespace BibleStudyTool.Core.Exceptions
{
    public class TimestampedException : Exception
    {
        public DateTimeOffset Timestamp { get; set; }

        public TimestampedException()
        {
            Timestamp = DateTimeOffset.UtcNow;
        }

        public TimestampedException(string message)
            : base(message)
        {
            Timestamp = DateTimeOffset.UtcNow;
        }

        public TimestampedException(string message, Exception inner)
            : base(message, inner)
        {
            Timestamp = DateTimeOffset.UtcNow;
        }
    }
}
