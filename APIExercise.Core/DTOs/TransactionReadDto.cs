namespace APIExercise.Core.DTOs
{
    public class TransactionReadDto
    {
        public Guid Id { get; set; }

        public Guid AccountId { get; set; }

        public decimal Amount { get; set; }

        public string Description { get; set; }

        public DateTime Date { get; set; }
    }
}
