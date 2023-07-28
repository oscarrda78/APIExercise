using APIExercise.Core.Entities;
using APIExercise.Core.Interfaces.Repositories;
using APIExercise.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace APIExercise.Infrastructure.Implementations.Repositories
{
    public class AccountRepository : RepositoryBase<Account>, IAccountRepository
    {

        public AccountRepository(AppDbContext context) : base(context)
        {
        }
        public async Task<IEnumerable<Account>> GetAccountsByClientIdAsync(Guid clientId)
        {
            return await _dbSet.Where(a => a.ClientId == clientId).ToListAsync();
        }

    }
}
