using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace eFaktura.Core
{
    /// <summary>
    /// Represents generic data service.
    /// </summary>s
    public class GenericDataService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GenericDataService"/> class.
        /// </summary>
        /// <param name="context">Db context.</param>
        public GenericDataService(DbContext context) => Context = context ?? throw new ArgumentNullException(nameof(context));

        /// <summary>
        /// Gets the database context.
        /// </summary>
        protected DbContext Context { get; }

        /// <summary>
        /// Retrieves a single entity from the database with tracking enabled.
        /// </summary>
        /// <typeparam name="T">The type of entity to retrieve.</typeparam>
        /// <param name="whereExpression">The expression used to filter entities to be returned.</param>
        /// <returns>Entity of the specified type as an asynchronous operation.</returns>
        public async Task<T> ReadSingleAsync<T>(Expression<Func<T, bool>> whereExpression)
            where T : class
        {
            return await Context.Set<T>()
                .Where(whereExpression)
                .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Retrieves a single entity from the database without tracking.
        /// </summary>
        /// <typeparam name="T">The type of entity to retrieve.</typeparam>
        /// <param name="whereExpression">The expression used to filter entities to be returned.</param>
        /// <returns>Entity of the specified type as an asynchronous operation.</returns>
        public async Task<T> ReadSingleNoTrackingAsync<T>(Expression<Func<T, bool>> whereExpression)
            where T : class
        {
            return await Context.Set<T>()
                .AsNoTracking()
                .Where(whereExpression)
                .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Retrieves a single entity from the database without tracking and provides an expression used to project
        /// <typeparamref name="TEntity"/> into a new form.
        /// </summary>
        /// <typeparam name="TEntity">The type of entity to query.</typeparam>
        /// <typeparam name="TResult">The type to return as a projection of the entity.</typeparam>
        /// <param name="whereExpression">The expression used to filter entities to be returned.</param>
        /// <param name="selectExpression">The expression used to project entity into a new form.</param>
        /// <returns>Entity of the specified type as an asynchronous operation.</returns>
        public async Task<TResult> ReadSingleWithSelectNoTrackingAsync<TEntity, TResult>(
            Expression<Func<TEntity, bool>> whereExpression,
            Expression<Func<TEntity, TResult>> selectExpression)
            where TEntity : class
            where TResult : class
        {
            return await Context.Set<TEntity>()
                .AsNoTracking()
                .Where(whereExpression)
                .Select(selectExpression)
                .FirstOrDefaultAsync();
        }


        /// <summary>
        /// Retrieves a single entity from the database and provides an expression used to project
        /// <typeparamref name="TEntity"/> into a new form.
        /// </summary>
        /// <typeparam name="TEntity">The type of entity to query.</typeparam>
        /// <typeparam name="TResult">The type to return as a projection of the entity.</typeparam>
        /// <param name="whereExpression">The expression used to filter entities to be returned.</param>
        /// <param name="selectExpression">The expression used to project entity into a new form.</param>
        /// <returns>Entity of the specified type as an asynchronous operation.</returns>
        public async Task<TResult> ReadSingleWithSelectAsync<TEntity, TResult>(
            Expression<Func<TEntity, bool>> whereExpression,
            Expression<Func<TEntity, TResult>> selectExpression)
            where TEntity : class
            where TResult : class
        {
            return await Context.Set<TEntity>()
                .Where(whereExpression)
                .Select(selectExpression)
                .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Retrieves a collection of entities from the database with tracking enabled.
        /// </summary>
        /// <typeparam name="T">The type of entity to retrieve.</typeparam>
        /// <param name="whereExpression">The expression used to filter entities to be returned.</param>
        /// <returns>A collection of entities of the specified type as an asynchronous operation.</returns>
        public async Task<List<T>> ReadManyAsync<T>(Expression<Func<T, bool>> whereExpression)
            where T : class
        {
            return await Context.Set<T>()
                .Where(whereExpression)
                .ToListAsync();
        }

        /// <summary>
        /// Retrieves a collection of entities from the database without tracking.
        /// </summary>
        /// <typeparam name="T">The type of entity to retrieve.</typeparam>
        /// <returns>A collection of entities of the specified type as an asynchronous operation.</returns>
        public async Task<List<T>> ReadManyNoTrackingAsync<T>()
            where T : class
        {
            return await Context.Set<T>()
                .AsNoTracking()
                .ToListAsync();
        }

        /// <summary>
        /// Retrieves a collection of entities from the database ordered without tracking.
        /// </summary>
        /// <param name="order">The expression used to sort the entities in a Descending order.</param>
        /// <typeparam name="T">The type of entity to retrieve.</typeparam>
        /// <returns>A collection of entities of the specified type as an asynchronous operation.</returns>
        public async Task<List<T>> ReadManyAscOrderAsNoTrackingAsync<T>(Expression<Func<T, object>> order)
            where T : class
        {
            return await Context.Set<T>()
                .AsNoTracking()
                .OrderBy(order)
                .ToListAsync();
        }

        /// <summary>
        /// Retrieves a collection of entities from the database ordered without tracking.
        /// </summary>
        /// <param name="order">The expression used to sort the entities in a Descending order.</param>
        /// <typeparam name="T">The type of entity to retrieve.</typeparam>
        /// <returns>A collection of entities of the specified type as an asynchronous operation.</returns>
        public async Task<List<T>> ReadManyDescOrderAsNoTrackingAsync<T>(Expression<Func<T, object>> order)
            where T : class
        {
            return await Context.Set<T>()
                .AsNoTracking()
                .OrderByDescending(order)
                .ToListAsync();
        }

        /// <summary>
        /// Retrieves a collection of entities from the database ordered without tracking.
        /// </summary>
        /// <typeparam name="TEntity">The type of entity to query.</typeparam>
        /// <typeparam name="TResult">The type to return as a projection of the entity.</typeparam>
        /// <param name="groupExpression">The expression used to group entity.</param>
        /// <param name="selectExpression">The expression used to project entities into a new form.</param>
        /// <returns></returns>
        public async Task<List<TResult>> ReadManyGroupByAsNoTrackingAsync<TEntity, TResult>(
            Expression<Func<TEntity, bool>> whereExpression,
            Expression<Func<TEntity, object>> groupExpression,
            Expression<Func<IGrouping<object, TEntity>, TResult>> selectExpression)
            where TEntity : class
            where TResult : class
        {
            return await Context.Set<TEntity>()
                .AsNoTracking()
                .Where(whereExpression)
                .GroupBy(groupExpression)
                .Select(selectExpression)
                .ToListAsync();
        }

        /// <summary>
        /// Retrieves a collection of entities from the database without tracking.
        /// </summary>
        /// <typeparam name="TEntity">The type of entity to query.</typeparam>
        /// <typeparam name="TResult">The type to return as a projection of the entity.</typeparam>
        /// <param name="selectExpression">The expression used to project entities into a new form.</param>
        /// <returns>A collection of entities of the specified type as an asynchronous operation.</returns>
        public async Task<List<TResult>> ReadManyDescOrderWithSelectNoWhereAsNoTrackingAsync<TEntity, TResult>(
            Expression<Func<TEntity, TResult>> selectExpression,
            Expression<Func<TResult, object>> order)
            where TEntity : class
            where TResult : class
        {
            return await Context.Set<TEntity>()
                .AsNoTracking()
                .Select(selectExpression)
                .OrderByDescending(order)
                .ToListAsync();
        }

        /// <summary>
        /// Retrieves a collection of entities from the database without tracking.
        /// </summary>
        /// <typeparam name="TEntity">The type of entity to query.</typeparam>
        /// <typeparam name="TResult">The type to return as a projection of the entity.</typeparam>
        /// <param name="selectExpression">The expression used to project entities into a new form.</param>
        /// <returns>A collection of entities of the specified type as an asynchronous operation.</returns>
        public async Task<List<TResult>> ReadManyAscOrderWithSelectNoWhereAsNoTrackingAsync<TEntity, TResult>(
            Expression<Func<TEntity, TResult>> selectExpression,
            Expression<Func<TResult, object>> order)
            where TEntity : class
            where TResult : class
        {
            return await Context.Set<TEntity>()
                .AsNoTracking()
                .Select(selectExpression)
                .OrderBy(order)
                .ToListAsync();
        }

        /// <summary>
        /// Retrieves a collection of entities from the database without tracking.
        /// </summary>
        /// <typeparam name="TEntity">The type of entity to query.</typeparam>
        /// <typeparam name="TResult">The type to return as a projection of the entity.</typeparam>
        /// <param name="whereExpression">The expression used to filter entities to be returned.</param>
        /// <param name="selectExpression">The expression used to project entities into a new form.</param>
        /// <returns>A collection of entities of the specified type as an asynchronous operation.</returns>
        public async Task<List<TResult>> ReadManyAscOrderWithSelectAsNoTrackingAsync<TEntity, TResult>(
            Expression<Func<TEntity, bool>> whereExpression,
            Expression<Func<TEntity, TResult>> selectExpression,
            Expression<Func<TResult, object>> order)
            where TEntity : class
            where TResult : class
        {
            return await Context.Set<TEntity>()
                .AsNoTracking()
                .Where(whereExpression)
                .Select(selectExpression)
                .OrderBy(order)
                .ToListAsync();
        }

        /// <summary>
        /// Retrieves a collection of entities from the database without tracking.
        /// </summary>
        /// <typeparam name="TEntity">The type of entity to query.</typeparam>
        /// <typeparam name="TResult">The type to return as a projection of the entity.</typeparam>
        /// <param name="selectExpression">The expression used to project entities into a new form.</param>
        /// <returns>A collection of entities of the specified type as an asynchronous operation.</returns>
        public async Task<List<TResult>> ReadManyWithSelectNoWhereAsNoTrackingAsync<TEntity, TResult>(
            Expression<Func<TEntity, TResult>> selectExpression)
            where TEntity : class
            where TResult : class
        {
            return await Context.Set<TEntity>()
                .AsNoTracking()
                .Select(selectExpression)
                .ToListAsync();
        }

        /// <summary>
        /// Retrieves a collection of entities from the database  and provides an expression used to
        /// project each entity of type <typeparamref name="TEntity"/> into a new form.
        /// </summary>
        /// <typeparam name="TEntity">The type of entity to query.</typeparam>
        /// <typeparam name="TResult">The type to return as a projection of the entity.</typeparam>
        /// <param name="whereExpression">The expression used to filter entities to be returned.</param>
        /// <param name="selectExpression">The expression used to project entities into a new form.</param>
        /// <returns>A collection of entities of the specified type as an asynchronous operation.</returns>
        public async Task<List<TResult>> ReadManyWithSelectAsync<TEntity, TResult>(
            Expression<Func<TEntity, bool>> whereExpression,
            Expression<Func<TEntity, TResult>> selectExpression)
            where TEntity : class
            where TResult : class
        {
            return await Context.Set<TEntity>()
                .Where(whereExpression)
                .Select(selectExpression)
                .ToListAsync();
        }

        /// <summary>
        /// Retrieves a collection of entities from the database without tracking and provides an expression used to
        /// project each entity of type <typeparamref name="TEntity"/> into a new form.
        /// </summary>
        /// <typeparam name="TEntity">The type of entity to query.</typeparam>
        /// <typeparam name="TResult">The type to return as a projection of the entity.</typeparam>
        /// <param name="whereExpression">The expression used to filter entities to be returned.</param>
        /// <param name="selectExpression">The expression used to project entities into a new form.</param>
        /// <returns>A collection of entities of the specified type as an asynchronous operation.</returns>
        public async Task<List<TResult>> ReadManyWithSelectNoTrackingAsync<TEntity, TResult>(
            Expression<Func<TEntity, bool>> whereExpression,
            Expression<Func<TEntity, TResult>> selectExpression)
            where TEntity : class
            where TResult : class
        {
            return await Context.Set<TEntity>()
                .AsNoTracking()
                .Where(whereExpression)
                .Select(selectExpression)
                .ToListAsync();
        }

        /// <summary>
        ///  Retrieves a collection of entities from the database without tracking and provides an expression used to
        /// project each entity of type <typeparamref name="TEntity"/> into a new form with ordering.
        /// </summary>
        /// <typeparam name="TEntity">The type of entity to query.</typeparam>
        /// <typeparam name="TResult">The type to return as a projection of the entity.</typeparam>
        /// <param name="whereExpression">The expression used to filter entities to be returned.</param>
        /// <param name="selectExpression">The expression used to project entities into a new form.</param>
        /// <param name="orderBy"> the expression used for ordering by specific property in descending order</param>
        /// <returns>A collection of entities of the specified type as an asynchronous operation.</returns>
        public async Task<List<TResult>> ReadManyWithSelectNoTrackingOrderByAsync<TEntity, TResult>(
            Expression<Func<TEntity, bool>> whereExpression,
            Expression<Func<TEntity, TResult>> selectExpression,
            Expression<Func<TResult, object>> orderBy)
            where TEntity : class
            where TResult : class
        {
            return await Context.Set<TEntity>()
                .AsNoTracking()
                .Where(whereExpression)
                .Select(selectExpression)
                .OrderByDescending(orderBy)
                .ToListAsync();
        }

        /// <summary>
        /// Retrieves a single entity from the database without tracking and provides an expression used to project
        /// <typeparamref name="TEntity"/> into a new form.
        /// </summary>
        /// <typeparam name="TEntity">The type of entity to query.</typeparam>
        /// <typeparam name="TResult">The type to return as a projection of the entity.</typeparam>
        /// <param name="whereExpression">The expression used to filter entities to be returned.</param>
        /// <param name="selectExpression">The expression used to project entity into a new form.</param>
        /// <param name="includeExpressions">The expression used to project entities into a new form.</param>
        /// <returns>Entity of the specified type as an asynchronous operation.</returns>
        public async Task<TResult> ReadSingleWithSelectIncludeNoTrackingAsync<TEntity, TResult>(
            Expression<Func<TEntity, bool>> whereExpression,
            Expression<Func<TEntity, TResult>> selectExpression,
            params Expression<Func<TEntity, object>>[] includeExpressions)
            where TEntity : class
            where TResult : class
        {
            var query = Context.Set<TEntity>()
                .AsNoTracking()
                .Where(whereExpression);

            foreach (var includeExpression in includeExpressions)
                query.Include(includeExpression);

            return await query
                .Select(selectExpression)
                .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Adds a collection of entities to the db context.
        /// </summary>
        /// <typeparam name="T">The type of entity to save.</typeparam>
        /// <param name="entities">A collection of entities to save.</param>
        public void AddEntities<T>(params T[] entities)
            where T : class
        {
            CheckNullEntities(entities);
            foreach (T entity in entities)
                Context.Add(entity);
        }

        /// <summary>
        /// Adds a collection of entities to the db context and performs a save.
        /// </summary>
        /// <typeparam name="T">The type of entity to save.</typeparam>
        /// <param name="entities">A collection of entities to save.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task AddAndSaveEntitiesAsync<T>(params T[] entities)
            where T : class
        {
            AddEntities(entities);
            await Save();
        }

        /// <summary>
        /// Removes a collection of entities from the db context and performs a save.
        /// </summary>
        /// <typeparam name="T">The type of entity to remove.</typeparam>
        /// <param name="entities">A collection of entities to remove.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task RemoveAndSaveEntities<T>(params T[] entities)
            where T : class
        {
            RemoveEntities(entities);
            await Save();
        }

        /// <summary>
        /// Saves the changes to the database.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task Save()
        {
            await Context.SaveChangesAsync();
        }

        /// <summary>
        /// Removes a collection of entities from the db context.
        /// </summary>
        /// <typeparam name="T">The type of entity to remove.</typeparam>
        /// <param name="entities">A collection of entities to remove.</param>
        private void RemoveEntities<T>(params T[] entities)
            where T : class
        {
            CheckNullEntities(entities);
            foreach (T entity in entities)
                Context.Remove(entity);
        }

        private void CheckNullEntities<T>(params T[] entities)
        {
            if (entities == null || entities.Length == 0 || entities.Any(element => element == null))
                throw new ArgumentNullException(nameof(entities), "An array cannot be null or empty or contain null values");
        }

        private async Task AddAndSaveAuditEntities<T>(params T[] entities)
            where T : class
        {
            AddEntities(entities);
            await Context.SaveChangesAsync();
        }
    }
}
