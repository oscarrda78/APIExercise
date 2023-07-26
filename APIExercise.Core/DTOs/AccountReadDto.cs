using APIExercise.Core.Entities.Enums;

namespace APIExercise.Core.DTOs
{
    public class AccountReadDto
    {
        public int Id { get; set; }
        public string AccountNumber { get; set; }
        public decimal Balance { get; set; }
        public AccountType Type { get; set; }
        public DateTime CreatedDate { get; set; }
        public Status Status { get; set; }
    }
}
