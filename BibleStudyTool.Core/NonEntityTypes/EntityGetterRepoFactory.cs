using System;
using BibleStudyTool.Core.Entities;
using BibleStudyTool.Core.Interfaces;

namespace BibleStudyTool.Core.NonEntityTypes
{
    public class EntityGetterRepoFactory<T> : IEntityGetterRepoFactory<T> where T : EntityGetterRepository
    {
        public EntityGetterRepository CreateRepository() 
        {
            // Works too:
            //if (typeof(T) == typeof(NoteGetterRepository))
            //{
            //    return new NoteGetterRepository();
            //}

            switch (typeof(T))
            {
                case Type noteEntityGetterType when noteEntityGetterType == typeof(NoteGetterRepository):
                    return new NoteGetterRepository();
            }

            return new NoteGetterRepository();
        }
    }
}
