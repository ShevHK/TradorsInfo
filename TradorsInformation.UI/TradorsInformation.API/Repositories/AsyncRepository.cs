using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TradorsInformation.DB.Context;

namespace TradorsInformation.API.Repositories
{
    public class AsyncRepository<TEntity> : IAsyncRepository<TEntity> where TEntity : class
    {
        private readonly AppDbContext _context;
        /// <summary>
        /// Initializes a new instance of the <see cref="AsyncRepository{TEntity}"/> class.
        /// </summary>
        /// <param name="context">The database context.</param>
        /// <exception cref="ArgumentNullException">Thrown when the <paramref name="context"/> parameter is null.</exception>
        public AsyncRepository(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        /// <inheritdoc/>
        public Task AddAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            _context.Add(entity);
            return _context.SaveChangesAsync();
        }
        /// <inheritdoc/>
        public Task DeleteAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            _context.Remove(entity);
            return _context.SaveChangesAsync();
        }
        /// <inheritdoc/>
        public Task<IQueryable<TEntity>> GetAllAsync()
        {
            return Task.FromResult(_context.Set<TEntity>().AsQueryable());
        }
        /// <inheritdoc/>
        public Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> expression)
        {
            return _context.Set<TEntity>().FirstOrDefaultAsync(expression);
        }
        /// <inheritdoc/>
        public Task UpdateAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            _context.Update(entity);
            return _context.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public Task<bool> IsExist(TEntity entity)
        {
            if (entity == null)
            {
                return Task.FromResult(false);
            }
            return _context.Set<TEntity>().ContainsAsync(entity);
        }
    }

}
