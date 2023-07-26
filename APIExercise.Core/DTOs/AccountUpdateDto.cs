using APIExercise.Core.Entities.Enums;

namespace APIExercise.Core.DTOs
{
    public class AccountUpdateDto
    {
        public decimal Balance { get; set; }
        public Status Status { get; set; }

    }
}
