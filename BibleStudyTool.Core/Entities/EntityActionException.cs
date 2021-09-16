using System;
namespace BibleStudyTool.Core.Entities
{
    public enum ActionType
    {
        GetAll,
        Get,
        Create,
        Update,
        Delete
    };

    public class EntityActionException : Exception
    {
        public ActionType ActionType { get; set; }
        public string ActionMessage { get; set; }
        public string EntityName { get; set; }

        public EntityActionException()
        {
        }

        public EntityActionException(string entityName, ActionType actionType)
        {
            ActionType = actionType;
            EntityName = entityName;
        }

        public EntityActionException(string message)
            : base(message)
        {
        }

        public EntityActionException(string message, Exception inner)
            : base(message, inner)
        {
        }

        public void SetActionMessage(string actionMessage)
        {
            ActionMessage = actionMessage;
        }
    }
}
