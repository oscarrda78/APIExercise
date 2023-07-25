using APIExercise.Core.Entities.Enums;
using APIExercise.Core.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIExercise.Core.Entities
{
    public class Account
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "El número de cuenta es obligatorio.")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "El número de cuenta debe tener entre 1 y 50 caracteres.")]
        public string AccountNumber { get; set; }

        [Required(ErrorMessage = "El tipo de cuenta es obligatorio.")]
        [EnumDataType(typeof(AccountType), ErrorMessage = "Tipo de cuenta inválido.")]
        public AccountType AccountType { get; set; }

        [Required(ErrorMessage = "El saldo inicial es obligatorio.")]
        [Range(0, double.MaxValue, ErrorMessage = "El saldo inicial no puede ser negativo.")]
        public decimal InitialBalance { get; set; }

        [Required(ErrorMessage = "El estado de la cuenta es obligatorio.")]
        [EnumDataType(typeof(Status), ErrorMessage = "Estado inválido.")]
        public Status Status { get; set; }

        [Required(ErrorMessage = "El ID del cliente es obligatorio.")]
        [ForeignKey(nameof(Client))]
        public Guid ClientId { get; set; }

        public virtual Client Client { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
        public virtual ICollection<Transaction> CounterpartyTransactions { get; set; }
    }
}
