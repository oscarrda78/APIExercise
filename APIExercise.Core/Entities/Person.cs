using System.ComponentModel.DataAnnotations;

namespace APIExercise.Core.Entities
{
    public class Person
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "El nombre debe tener entre 1 y 50 caracteres.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "El apellido es obligatorio.")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "El apellido debe tener entre 1 y 50 caracteres.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "El documento de identidad es obligatorio.")]
        [StringLength(8, MinimumLength = 8, ErrorMessage = "El documento de identidad debe tener 8 caracteres.")]
        public string IdDocument { get; set; }

        [StringLength(15, ErrorMessage = "El número de teléfono no puede exceder 15 caracteres.")]
        [Phone(ErrorMessage = "Formato de número de teléfono inválido.")]
        public string? PhoneNumber { get; set; }

        [Required(ErrorMessage = "La dirección es obligatoria.")]
        public virtual Address Address { get; set; }
    }
}