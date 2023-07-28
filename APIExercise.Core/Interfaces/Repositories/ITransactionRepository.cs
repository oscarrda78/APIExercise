using APIExercise.Core.Entities;

namespace APIExercise.Core.Interfaces.Repositories
{
    public interface ITransactionRepository : IRepositoryBase<Transaction>
    {
        Task<decimal> GetDailyOutcomeAsync(Guid accountId, DateTime date);
        Task<IEnumerable<Transaction>> GetTransactionsForAccountByDateAsync(Guid accountId, DateTime startDate, DateTime endDate);

    }
}
