using APIExercise.Core.Entities;
using APIExercise.Core.Entities.Enums;
using APIExercise.Core.Interfaces.Repositories;
using APIExercise.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace APIExercise.Infrastructure.Implementations.Repositories
{
    public class TransactionRepository : RepositoryBase<Transaction>, ITransactionRepository
    {
        private readonly AppDbContext _dbContext;

        public TransactionRepository(AppDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<decimal> GetDailyOutcomeAsync(Guid accountId, DateTime date)
        {
            var startDate = date.Date;
            var endDate = startDate.AddDays(1);

            return await _dbContext.Transactions
                .Where(t => t.AccountId == accountId && t.TransactionType == TransactionType.Outcome
                            && t.Date >= startDate && t.Date < endDate)
                .SumAsync(t => t.Amount);
        }

        public async Task<IEnumerable<Transaction>> GetTransactionsForAccountByDateAsync(Guid accountId, DateTime startDate, DateTime endDate)
        {
            return await _dbSet.Where(t => t.AccountId == accountId && t.Date >= startDate && t.Date <= endDate)
                           .ToListAsync();
        }

    }

}
