using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BibleStudyTool.Core.Exceptions;

namespace BibleStudyTool.Core.Interfaces
{
    public interface IAsyncRepository<T>
        where T : BaseEntity
    {
        Task<IReadOnlyList<T>> GetAllAsync<Y>() where Y : EntityCrudActionException;
        Task<T> GetByIdAsync<Y>(object[] keyValues) where Y : EntityCrudActionException;
        Task<T> CreateAsync<Y>(T entity) where Y : EntityCrudActionException;
        Task BulkCreateAsync<Y>(T[] entities) where Y : EntityCrudActionException;
        Task UpdateAsync<Y>(T entity) where Y : EntityCrudActionException;
        Task BulkUpdateAsync<Y>(T[] entities) where Y : EntityCrudActionException;
        Task DeleteAsync<Y>(T entity) where Y : EntityCrudActionException;
        Task BulkDeleteAsync<Y>(object[][] entityIds) where Y : EntityCrudActionException;
    }
}
