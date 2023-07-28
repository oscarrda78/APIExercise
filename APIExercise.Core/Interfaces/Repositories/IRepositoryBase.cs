using APIExercise.Core.Entities;
using Microsoft.EntityFrameworkCore.Query;

namespace APIExercise.Core.Interfaces.Repositories
{
    public interface IRepositoryBase<T> where T : class, IBaseEntity
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(Guid id);

        Task<T> GetByIdAsync(Guid id, Func<IQueryable<T>, IIncludableQueryable<T, IBaseEntity>>? include = null);
        Task<T> AddAsync(T entity);
        Task<bool> UpdateAsync(T entity);
        Task<bool> DeleteAsync(Guid id);
    }
}
