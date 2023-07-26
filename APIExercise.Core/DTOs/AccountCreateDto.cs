using APIExercise.Core.Entities.Enums;

namespace APIExercise.Core.DTOs
{
    public class AccountCreateDto
    {
        public string AccountNumber { get; set; }
        public decimal InitialBalance { get; set; }
        public AccountType Type { get; set; }
        public int ClientId { get; set; }
    }
}
