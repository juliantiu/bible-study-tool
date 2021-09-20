using System;
using BibleStudyTool.Core.Entities;
using BibleStudyTool.Core.Interfaces;

namespace BibleStudyTool.Core.NonEntityTypes
{
    public class EntityCrudActionExceptionFactory : IEntityCrudActionExceptionFactory
    {
        public EntityCrudActionException CreateEntityCrudActionException<T>(string message) where T : EntityCrudActionException
        {
            switch (typeof(T))
            {
                case Type noteCrudActionExeption when noteCrudActionExeption == typeof(NoteCrudActionException):
                    return new NoteCrudActionException(message);
                case Type tagCrudActionExeption when tagCrudActionExeption == typeof(TagCrudActionException):
                    return new TagCrudActionException(message);
                case Type tagGroupCrudActionExeption when tagGroupCrudActionExeption == typeof(TagGroupCrudActionException):
                    return new TagGroupCrudActionException(message);
                default:
                    return new DefaultEntityCrudActionException($"Unknown entity CRUD action exception occured: {message}");
            }
        }
    }
}
