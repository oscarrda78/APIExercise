using APIExercise.Core.Entities;
using APIExercise.Core.Interfaces.Repositories;
using APIExercise.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace APIExercise.Infrastructure.Implementations.Repositories
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class, IBaseEntity
    {
        protected readonly AppDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public RepositoryBase(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<T> AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity == null) return false;

            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }
        public async Task<T> GetByIdAsync(Guid id, Func<IQueryable<T>, IIncludableQueryable<T, IBaseEntity>>? include = null)
        {
            IQueryable<T> query = _dbSet;

            if (include != null)
            {
                query = include(query);
            }

            return await query.SingleOrDefaultAsync(e => e.Id.Equals(id));
        }

        public async Task<bool> UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
