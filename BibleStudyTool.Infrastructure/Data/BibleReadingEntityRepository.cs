using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BibleStudyTool.Core.NonEntityInterfaces;
using BibleStudyTool.Core.NonEntityTypes;
using Microsoft.EntityFrameworkCore;

namespace BibleStudyTool.Infrastructure.Data
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

        public async Task<T> GetByIdAsync(string id)
        {
            try
            {
                var keyValues = new object[] { id };
                return await _dbContext.Set<T>().FindAsync(keyValues);
            }
            catch (Exception)
            {
                throw; // todo throw an error factory query for the type
            }
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            try
            {
                return await _dbContext.Set<T>().ToListAsync();
            }
            catch (Exception)
            {
                throw; // todo throw an error factory query for the type
            }
        }

        public async Task<IReadOnlyList<T>> GetBySpefication(ISpecification<T> specification)
        {
            var entityTableQuery = _dbContext.Set<T>().AsQueryable();
            var entityTableQueryWithSpecifications = ApplySpecifications(entityTableQuery, specification.SpecificationClauses);
            return await entityTableQueryWithSpecifications.ToListAsync();
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
            catch
            {
                throw; // todo throw an error factory query for the type
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
                Console.WriteLine($"{DateTime.UtcNow} :: CreateAsync error :: {ex.Message}"); // todo Log this in a logging service
                throw; // todo throw an error factory query for the type
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
                Console.WriteLine($"{DateTime.UtcNow} :: UpdateAsync error :: {ex.Message}"); // todo Log this in a logging service
                throw; // todo throw an error factory query for the type
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
                Console.WriteLine($"{DateTime.UtcNow} :: UpdateAsync error :: {ex.Message}"); // todo Log this in a logging service
                throw; // todo throw an error factory query for the type
            }
        }

        public IQueryable<T> ApplySpecifications(IQueryable<T> entityTableQuery, IList<SpecificationClause> specificationClauses)
        {
            foreach (var queryClause in specificationClauses)
            {
                if (queryClause is WhereClause<T> whereClause)
                    entityTableQuery.Where(whereClause.Value);
                if (queryClause is IncludeClause<T> includeClause)
                    entityTableQuery.Include(includeClause.PropertyName);
            }
            return entityTableQuery;
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
