using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BibleStudyTool.Core.Exceptions;
using BibleStudyTool.Core.Interfaces;
using BibleStudyTool.Core.NonEntityTypes;
using Microsoft.EntityFrameworkCore;

namespace BibleStudyTool.Infrastructure.Data
{
    public class BibleReadingEntityRepository<T> : IAsyncRepository<T> where T : BaseEntity
    {
        private readonly BibleReadingDbContext _dbContext;
        private readonly IEntityCrudActionExceptionFactory _entityCrudActionExceptionFactory;

        /* QUESTION:
         * todo
         * How does dbContext get injected here if Startup.cs in the Public project does not depend on this class?
         */
        public BibleReadingEntityRepository(BibleReadingDbContext dbContext,
                                            IEntityCrudActionExceptionFactory entityCrudActionExceptionFactory)
        {
            _dbContext = dbContext;
            _entityCrudActionExceptionFactory = entityCrudActionExceptionFactory;
        }

        public async Task<T> GetByIdAsync<Y>(object[] keyValues)
            where Y : EntityCrudActionException
        {
            try
            {
                return await _dbContext.Set<T>().FindAsync(keyValues);
            }
            catch (Exception ex)
            {
                throw
                    _entityCrudActionExceptionFactory
                        .CreateEntityCrudActionException<Y>($"GetByIdAsync error :: {ex.Message}");
            }
        }

        public async Task<IReadOnlyList<T>> GetAllAsync<Y>()
            where Y : EntityCrudActionException
        {
            try
            {
                return await _dbContext.Set<T>().ToListAsync();
            }
            catch (Exception ex)
            {
                throw
                    _entityCrudActionExceptionFactory
                        .CreateEntityCrudActionException<Y>($"GetAllAsync error :: {ex.Message}");
            }
        }

        public async Task<IReadOnlyList<T>> GetBySpefication<Y>(ISpecification<T> specification)
            where Y : EntityCrudActionException
        {
            try
            {
                var entityTableQuery = _dbContext.Set<T>().AsQueryable();
                var entityTableQueryWithSpecifications = ApplySpecifications(entityTableQuery, specification.SpecificationsClauses);
                return await entityTableQueryWithSpecifications.ToListAsync();
            }
            catch (Exception ex)
            {
                throw
                    _entityCrudActionExceptionFactory
                        .CreateEntityCrudActionException<Y>($"GetBySpecification error :: {ex.Message}");
            }
        }

        public async Task<IReadOnlyList<T>> GetByRawQuery<Y>(string query, string[] parameters)
            where Y : EntityCrudActionException
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
                    _entityCrudActionExceptionFactory
                        .CreateEntityCrudActionException<Y>($"GetByRawQuery error :: {ex.Message}");
            }
        }

        public async Task<T> CreateAsync<Y>(T entity)
            where Y : EntityCrudActionException 
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
                    _entityCrudActionExceptionFactory
                        .CreateEntityCrudActionException<Y>($"CreateAsync error :: {ex.Message}");
            }
        }

        public async Task BulkCreateAsync<Y>(T[] entities) where Y : EntityCrudActionException
        {
            try
            {
                using (var ctx = new BibleReadingDbContext())
                {
                    ctx.RemoveRange(entities);
                    await ctx.SaveChangesAsync();
                }
            }
            catch (Exception ex) when (ex is DbUpdateException
                                       || ex is DbUpdateConcurrencyException
                                       || ex is NotSupportedException
                                       || ex is ObjectDisposedException
                                       || ex is InvalidOperationException)
            {
                throw
                    _entityCrudActionExceptionFactory
                        .CreateEntityCrudActionException<Y>($"BulkDeleteAsync error :: {ex.Message}");
            }
        }

        public async Task DeleteAsync<Y>(T entity)
            where Y : EntityCrudActionException
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
                    _entityCrudActionExceptionFactory
                        .CreateEntityCrudActionException<Y>($"DeleteAsync error :: {ex.Message}");
            }
        }

        public async Task BulkDeleteAsync<Y>(object[][] entityIds)
            where Y : EntityCrudActionException
        {
            try
            {
                using (var ctx = new BibleReadingDbContext())
                {
                    IList<T> entitiesToDelete = new List<T>();
                    var dbTable = ctx.Set<T>();
                    foreach (var entityId in entityIds)
                    {
                        entitiesToDelete.Add(await dbTable.FindAsync(entityId).ConfigureAwait(false));
                    }

                    ctx.RemoveRange(entitiesToDelete);
                    await ctx.SaveChangesAsync();
                }
            }
            catch (Exception ex) when (ex is DbUpdateException
                                       || ex is DbUpdateConcurrencyException
                                       || ex is NotSupportedException
                                       || ex is ObjectDisposedException
                                       || ex is InvalidOperationException)
            {
                throw
                    _entityCrudActionExceptionFactory
                        .CreateEntityCrudActionException<Y>($"BulkDeleteAsync error :: {ex.Message}");
            }
        }

        public async Task UpdateAsync<Y>(T entity)
            where Y : EntityCrudActionException
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
                    _entityCrudActionExceptionFactory
                        .CreateEntityCrudActionException<Y>($"UpdateAsync error :: {ex.Message}");
            }
        }

        public async Task BulkUpdateAsync<Y>(T[] entities) where Y : EntityCrudActionException
        {
            {
                try
                {
                    using (var ctx = new BibleReadingDbContext())
                    {
                        ctx.UpdateRange(entities);
                        await ctx.SaveChangesAsync();
                    }
                }
                catch (Exception ex) when (ex is DbUpdateException
                                           || ex is DbUpdateConcurrencyException
                                           || ex is NotSupportedException
                                           || ex is ObjectDisposedException
                                           || ex is InvalidOperationException)
                {
                    throw
                        _entityCrudActionExceptionFactory
                            .CreateEntityCrudActionException<Y>($"BulkUpdateAsync error :: {ex.Message}");
                }
            }
        }

        #region HELPER FUNCTIONS

        public IQueryable<T> ApplySpecifications(IQueryable<T> entityTableQuery, IList<SpecificationClause> specificationClauses)
        {
            foreach (var queryClause in specificationClauses)
            {
                if (queryClause is WhereClause<T> whereClause)
                    entityTableQuery.Where(whereClause.Expression);
                if (queryClause is IncludeClause includeClause)
                    entityTableQuery.Include(includeClause.PropertyName);
            }
            return entityTableQuery;
        }

        #endregion
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
