using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BibleStudyTool.Core.Entities;
using BibleStudyTool.Core.NonEntityTypes;

namespace BibleStudyTool.Core.Interfaces
{
    public interface IAsyncRepository<T>
        where T : BaseEntity
    {
        Task<IReadOnlyList<T>> GetAllAsync<Y>() where Y : EntityCrudActionException;
        Task<T> GetByIdAsync<Y>(string id) where Y : EntityCrudActionException;
        Task<IReadOnlyList<T>> GetByRawQuery<Y>(string query, string[] parameters) where Y : EntityCrudActionException;
        Task<IReadOnlyList<T>> GetBySpefication<Y>(ISpecification<T> specification) where Y : EntityCrudActionException;
        Task<T> CreateAsync<Y>(T entity) where Y : EntityCrudActionException;
        Task UpdateAsync<Y>(T entity) where Y : EntityCrudActionException;
        Task DeleteAsync<Y>(T entity) where Y : EntityCrudActionException;
    }
}
