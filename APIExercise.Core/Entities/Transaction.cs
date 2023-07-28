using APIExercise.Core.Entities.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIExercise.Core.Entities
{
    public class Transaction : IBaseEntity
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "La fecha es obligatoria.")]
        [DataType(DataType.Date, ErrorMessage = "Fecha inválida.")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "El monto es obligatorio.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El monto debe ser positivo y distinto de cero.")]
        [DataType(DataType.Currency, ErrorMessage = "Monto inválido.")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "El tipo de transacción es obligatorio.")]
        [EnumDataType(typeof(TransactionType), ErrorMessage = "Tipo de transacción inválido.")]
        public TransactionType TransactionType { get; set; }

        [Required(ErrorMessage = "El ID de la cuenta es obligatorio.")]
        [ForeignKey(nameof(Account))]
        public Guid AccountId { get; set; }

        [InverseProperty("Transactions")]
        public virtual Account Account { get; set; }

        [Required(ErrorMessage = "El ID de la cuenta destino es obligatorio.")]
        [ForeignKey(nameof(CounterpartyAccount))]
        public Guid CounterpartyAccountId { get; set; }

        [InverseProperty("CounterpartyTransactions")]
        public virtual Account CounterpartyAccount { get; set; }
    }
}