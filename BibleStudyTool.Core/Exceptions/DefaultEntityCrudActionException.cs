using System;

namespace BibleStudyTool.Core.Exceptions
{
    public class DefaultEntityCrudActionException : EntityCrudActionException
    {
        public DefaultEntityCrudActionException()
        {
        }

        public DefaultEntityCrudActionException(string message)
            : base(message)
        {
        }

        public DefaultEntityCrudActionException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
