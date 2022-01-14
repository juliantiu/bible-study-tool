using System;
using BibleStudyTool.Core.Exceptions;
using BibleStudyTool.Core.NonEntityTypes;

namespace BibleStudyTool.Core.Interfaces
{
    public interface IEntityCrudActionExceptionFactory
    {
        public EntityCrudActionException CreateEntityCrudActionException<T>(string message) where T : EntityCrudActionException;
    }
}
