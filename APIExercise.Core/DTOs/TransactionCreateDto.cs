using APIExercise.Core.Entities.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace APIExercise.Core.DTOs
{
    public class TransactionCreateDto
    {
        [Required]
        public Guid AccountId { get; set; }

        [Required]
        public decimal Amount { get; set; }

        public string Description { get; set; }
        public TransactionType TransactionType { get; set; }
    }
}
