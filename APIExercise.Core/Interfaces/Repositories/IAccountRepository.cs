using APIExercise.Core.Entities;

namespace APIExercise.Core.Interfaces.Repositories
{
    public interface IAccountRepository : IRepositoryBase<Account>
    {
        Task<IEnumerable<Account>> GetAccountsByClientIdAsync(Guid clientId);
    }
}
