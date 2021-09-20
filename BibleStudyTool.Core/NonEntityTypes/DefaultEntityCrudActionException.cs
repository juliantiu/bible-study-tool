using System;
namespace BibleStudyTool.Core.NonEntityTypes
{
    public class DefaultEntityCrudActionException : EntityCrudActionException
    {
        public DefaultEntityCrudActionException()
        {
            Timestamp = DateTime.UtcNow;
        }

        public DefaultEntityCrudActionException(string message)
            : base(message)
        {
            Timestamp = DateTime.UtcNow;
        }

        public DefaultEntityCrudActionException(string message, Exception inner)
            : base(message, inner)
        {
            Timestamp = DateTime.UtcNow;
        }
    }
}
