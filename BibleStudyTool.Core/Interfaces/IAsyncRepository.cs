using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BibleStudyTool.Core.Exceptions;

namespace BibleStudyTool.Core.Interfaces
{
    public interface IAsyncRepository<T>
        where T : BaseEntity
    {
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<T> GetByIdAsync(object[] keyValues);
        Task<T> CreateAsync(T entity);
        Task BulkCreateAsync(T[] entities);
        Task UpdateAsync(T entity);
        Task BulkUpdateAsync(T[] entities);
        Task DeleteAsync(T entity);
        Task BulkDeleteAsync(object[][] entityIds);
    }
}
