using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BibleStudyTool.Core.Entities;

namespace BibleStudyTool.Core.NonEntityInterfaces
{
    public interface IAsyncRepository<T>
        where T : BaseEntity
    {
        Task<IList<T>> GetAllAsync();
        Task<T> GetByIdAsync(string id);
        Task<T> CreateAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}
