using APIExercise.Core.DTOs;
using APIExercise.Core.Entities.Enums;
using APIExercise.Core.Interfaces.Repositories;
using APIExercise.Core.Interfaces.Services;

namespace APIExercise.Infrastructure.Implementations.Services
{
    public class ReportService : IReportService
    {
        private readonly IClientRepository _clientRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly ITransactionRepository _transactionRepository;

        public ReportService(IClientRepository clientRepository, IAccountRepository accountRepository, ITransactionRepository transactionRepository)
        {
            _clientRepository = clientRepository ?? throw new ArgumentNullException(nameof(clientRepository));
            _accountRepository = accountRepository ?? throw new ArgumentNullException(nameof(accountRepository));
            _transactionRepository = transactionRepository ?? throw new ArgumentNullException(nameof(transactionRepository));
        }

        public async Task<ReportDto> GenerateClientReport(Guid clientId, DateTime startDate, DateTime endDate)
        {
            var client = await _clientRepository.GetByIdAsync(clientId);
            if (client == null)
            {
                return new ReportDto();
            }

            var report = new ReportDto
            {
                ClientId = client.Id,
                ClientName = $"{client.FirstName} {client.LastName}",
                Accounts = new List<AccountReportDto>()
            };

            var accounts = await _accountRepository.GetAccountsByClientIdAsync(clientId);
            foreach (var account in accounts)
            {
                var accountReport = new AccountReportDto
                {
                    AccountId = account.Id,
                    StartingBalance = account.Balance, 
                    Transactions = new List<TransactionReportDto>()
                };

                var transactions = await _transactionRepository.GetTransactionsForAccountByDateAsync(account.Id, startDate, endDate);
                decimal totalTransactionAmount = 0;
                foreach (var transaction in transactions)
                {
                    var transactionReport = new TransactionReportDto
                    {
                        TransactionId = transaction.Id,
                        Amount = transaction.Amount,
                        TransactionDate = transaction.Date,
                        Type = transaction.TransactionType
                    };
                    accountReport.Transactions.Add(transactionReport);

                    if (transaction.TransactionType == TransactionType.Outcome)
                    {
                        totalTransactionAmount += transaction.Amount;
                    }
                    else
                    {
                        totalTransactionAmount -= transaction.Amount;
                    }
                }
                accountReport.EndingBalance = accountReport.StartingBalance + totalTransactionAmount;
                report.Accounts.Add(accountReport);
            }

            return report;
        }
    }
}
