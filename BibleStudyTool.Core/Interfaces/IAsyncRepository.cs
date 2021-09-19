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
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<T> GetByIdAsync(string id);
        Task<T> CreateAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}
