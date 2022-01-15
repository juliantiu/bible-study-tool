using System;
namespace BibleStudyTool.Core.Exceptions
{
    public abstract class EntityCrudActionException : TimestampedException
    {
        public EntityCrudActionException()
        {
        }
        public EntityCrudActionException(string message)
            : base(message)
        {
        }
        public EntityCrudActionException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
