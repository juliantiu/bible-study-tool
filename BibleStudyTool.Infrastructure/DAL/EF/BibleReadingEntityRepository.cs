using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BibleStudyTool.Core.Exceptions;
using BibleStudyTool.Core.Interfaces;
using BibleStudyTool.Core.NonEntityTypes;
using Microsoft.EntityFrameworkCore;

namespace BibleStudyTool.Infrastructure.DAL.EF
{
    public class BibleReadingEntityRepository<T> : IAsyncRepository<T> where T : BaseEntity
    {
        private readonly BibleReadingDbContext _dbContext;

        /* QUESTION:
         * todo
         * How does dbContext get injected here if Startup.cs in the Public project does not depend on this class?
         */
        public BibleReadingEntityRepository(BibleReadingDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<T> GetByIdAsync(object[] keyValues)
        {
            try
            {
                return await _dbContext.Set<T>().FindAsync(keyValues);
            }
            catch (Exception ex)
            {
                throw
                    new EntityCrudActionException($"GetByIdAsync error :: {ex.Message}");
            }
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            try
            {
                return await _dbContext.Set<T>().ToListAsync();
            }
            catch (Exception ex)
            {
                throw
                    new EntityCrudActionException($"GetAllAsync error :: {ex.Message}");
            }
        }

        public async Task<IReadOnlyList<T>> GetByRawQuery(string query, string[] parameters)
        {
            try
            {
                /* Proper usage:
                 * 
                 * var user = new SqlParameter("user", "johndoe");
                 *
                 * var blogs = context.Blogs
                 *     .FromSqlRaw("EXECUTE dbo.GetMostPopularBlogsForUser @user", user)
                 *     .ToList();
                 * 
                 * * Reference
                 * ** https://docs.microsoft.com/en-us/ef/core/querying/raw-sql
                 * */
                return await _dbContext.Set<T>().FromSqlRaw(query, parameters).ToListAsync();
            }
            catch (Exception ex)
            {
                throw
                    new EntityCrudActionException($"GetByRawQuery error :: {ex.Message}");
            }
        }

        public async Task<T> CreateAsync(T entity)
        {
            try
            {
                await _dbContext.Set<T>().AddAsync(entity);
                await _dbContext.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex) when (ex is DbUpdateException
                                       || ex is DbUpdateConcurrencyException
                                       || ex is NotSupportedException
                                       || ex is ObjectDisposedException
                                       || ex is InvalidOperationException)
            {
                throw
                    new EntityCrudActionException($"CreateAsync error :: {ex.InnerException.Message}");
            }
        }

        public async Task BulkCreateAsync(T[] entities)
        {
            try
            {
                _dbContext.Set<T>().AddRange(entities);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex) when (ex is DbUpdateException
                                       || ex is DbUpdateConcurrencyException
                                       || ex is NotSupportedException
                                       || ex is ObjectDisposedException
                                       || ex is InvalidOperationException)
            {
                throw
                    new EntityCrudActionException($"BulkDeleteAsync error :: {ex.InnerException.Message}");
            }
        }

        public async Task DeleteAsync(T entity)
        {
            try
            {
                /* Since the querying of the entity is done by the caller of this method,
                 * we cannot just use the 'typical' way for deleting an entity. The 
                 * 'typical' way involves performing the querying and deletion of the
                 * entity within the same using block using the same db context:
                 * 
                 *   using (var context = new SchoolContext())
                 *   {
                 *       var std = context.Students.First<Student>();
                 *       context.Students.Remove(std);
                 *
                 *       // or
                 *       // context.Remove<Student>(std);
                 *
                 *       context.SaveChanges();
                 *   }
                 * 
                 * Instead, deletion can be tracked by using a Set<T>() call
                 * as done below:
                 */

                _dbContext.Set<T>().Remove(entity);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex) when (ex is DbUpdateException
                                       || ex is DbUpdateConcurrencyException
                                       || ex is NotSupportedException
                                       || ex is ObjectDisposedException
                                       || ex is InvalidOperationException)
            {
                throw
                    new EntityCrudActionException($"DeleteAsync error :: {ex.Message}");
            }
        }

        public async Task BulkDeleteAsync(object[][] entityIds)
        {
            try
            {
                IList<T> entitiesToDelete = new List<T>();
                var dbTable = _dbContext.Set<T>();
                foreach (var entityId in entityIds)
                {
                    entitiesToDelete.Add(await dbTable.FindAsync(entityId).ConfigureAwait(false));
                }

                _dbContext.Set<T>().RemoveRange(entitiesToDelete);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex) when (ex is DbUpdateException
                                       || ex is DbUpdateConcurrencyException
                                       || ex is NotSupportedException
                                       || ex is ObjectDisposedException
                                       || ex is InvalidOperationException)
            {
                throw
                    new EntityCrudActionException($"BulkDeleteAsync error :: {ex.Message}");
            }
        }

        public async Task UpdateAsync(T entity)
        {
            try
            {
                /* Since the querying of the entity is done by the caller of this method,
                 * we cannot just use the 'typical' way for updating an entity. The 
                 * 'typical' way involves performing the querying and modification of the
                 * entity within the same using block using the same db context:
                 * 
                 *   using (var context = new SchoolContext())
                 *   {
                 *       var std = context.Students.First<Student>(); 
                 *       std.FirstName = "Steve";
                 *       context.SaveChanges();
                 *   }
                 * 
                 * Instead, modification can be tracked by changing the state of the entity
                 * as done below:
                 */

                _dbContext.Entry(entity).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex) when (ex is DbUpdateException
                                       || ex is DbUpdateConcurrencyException
                                       || ex is NotSupportedException
                                       || ex is ObjectDisposedException
                                       || ex is InvalidOperationException)
            {
                throw
                    new EntityCrudActionException($"UpdateAsync error :: {ex.Message}");
            }
        }

        public async Task BulkUpdateAsync(T[] entities)
        {
            {
                try
                {
                    _dbContext.UpdateRange(entities);
                    await _dbContext.SaveChangesAsync();
                }
                catch (Exception ex) when (ex is DbUpdateException
                                           || ex is DbUpdateConcurrencyException
                                           || ex is NotSupportedException
                                           || ex is ObjectDisposedException
                                           || ex is InvalidOperationException)
                {
                    throw
                        new EntityCrudActionException($"BulkUpdateAsync error :: {ex.Message}");
                }
            }
        }
    }
}

/* NOTES & REFERENCES
 * ******************
 * * Repository Pattern
 * ** https://codewithmukesh.com/blog/repository-pattern-in-aspnet-core/
 * * Specification Pattern
 * ** https://codewithmukesh.com/blog/specification-pattern-in-aspnet-core/
 * * Differemce b/t Set<T> and DbSet<T>
 * ** https://stackoverflow.com/questions/53469498/difference-between-dbsett-property-and-sett-function-in-ef-core
 * 
 */
