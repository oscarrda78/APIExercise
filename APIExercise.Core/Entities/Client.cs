using APIExercise.Core.Entities.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace APIExercise.Core.Entities
{
    public class Client : Person
    {
        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        [PasswordPropertyText]
        [StringLength(4, MinimumLength = 4, ErrorMessage = "La contraseña debe tener 4 caracteres.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "El estado del cliente es obligatorio.")]
        [EnumDataType(typeof(Status), ErrorMessage = "Estado inválido.")]
        public Status Status { get; set; }

        public virtual ICollection<Account> Accounts { get; set; }
    }
}