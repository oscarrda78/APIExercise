using APIExercise.Core.Entities.Enums;

namespace APIExercise.Core.DTOs
{
    public class ReportDto
    {
        public Guid ClientId { get; set; }
        public string ClientName { get; set; }
        public List<AccountReportDto> Accounts { get; set; }
    }

    public class AccountReportDto
    {
        public Guid AccountId { get; set; }
        public decimal StartingBalance { get; set; }
        public decimal EndingBalance { get; set; }
        public List<TransactionReportDto> Transactions { get; set; }
    }

    public class TransactionReportDto
    {
        public Guid TransactionId { get; set; }
        public decimal Amount { get; set; }
        public DateTime TransactionDate { get; set; }
        public TransactionType Type { get; set; }
        public string Description { get; set; }
    }
}
