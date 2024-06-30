
using ExchangeBook.Data;
using Microsoft.EntityFrameworkCore;

namespace ExchangeBook.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T>
        where T : class
    {
        protected readonly BookExchangeDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public BaseRepository(BookExchangeDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }
        public virtual async Task AddAsync(T? entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public virtual async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }

        public virtual void UpdateAsync(T entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public virtual async Task<T?> DeleteAsync(int id)
        {
            T? existing = await _dbSet.FindAsync(id);
            if (existing is not null)
            {
                _dbSet.Remove(existing);
                return null;
            }
            await _context.SaveChangesAsync(); // Save changes after deleting
            Console.WriteLine("Existing" + existing.ToString());
            return existing;
        }

        public virtual async Task<T?> GetAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            return entity;
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            var entities = await _dbSet.ToListAsync();
            return entities;
        }

        public virtual async Task<int> GetCountAsync()
        {
            var count = await _dbSet.CountAsync();
            return count;
        }

    }
}

