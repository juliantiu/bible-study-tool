using System;
using BibleStudyTool.Core.NonEntityTypes;

namespace BibleStudyTool.Core.NonEntityInterfaces
{
    public interface IEntityGetterRepoFactory<T> where T : EntityGetterRepository
    {
        public EntityGetterRepository CreateRepository();
    }
}
