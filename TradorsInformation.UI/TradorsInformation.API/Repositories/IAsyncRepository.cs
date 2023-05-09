using System.Linq.Expressions;

namespace TradorsInformation.API.Repositories
{
    public interface IAsyncRepository<T> where T : class
    {
        /// <summary>
        /// Adds a new entity to the database and saves changes asynchronously.
        /// </summary>
        /// <param name="entity">The entity to add to the database.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task AddAsync(T entity);

        /// <summary>
        /// Deletes an entity from the database and saves changes asynchronously.
        /// </summary>
        /// <param name="entity">The entity to delete from the database.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task DeleteAsync(T entity);

        /// <summary>
        /// Updates an existing entity in the database and saves changes asynchronously.
        /// </summary>
        /// <param name="entity">The entity to update in the database.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task UpdateAsync(T entity);

        /// <summary>
        /// Gets an entity from the database that matches the specified expression asynchronously.
        /// </summary>
        /// <param name="expression">The expression to match.</param>
        /// <returns>A task representing the asynchronous operation. Returns the matching entity, or null if no such entity exists.</returns>
        Task<T?> GetAsync(Expression<Func<T, bool>> expression);

        /// <summary>
        /// Determines whether the specified entity exists in the database asynchronously.
        /// </summary>
        /// <param name="entity">The entity to check for existence in the database.</param>
        /// <returns>A task representing the asynchronous operation. Returns true if the entity exists in the database, otherwise false.</returns>
        Task<bool> IsExist(T entity);

        /// <summary>
        /// Gets all entities from the database asynchronously.
        /// </summary>
        /// <returns>A task representing the asynchronous operation. Returns all entities in the database as an IQueryable.</returns>
        Task<IQueryable<T>> GetAllAsync();

    }
}
