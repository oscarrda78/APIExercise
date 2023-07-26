using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIExercise.Core.Entities
{
    public class Address
    {
        [Key]
        [ForeignKey(nameof(Person))]
        public Guid Id { get; set; }

        [StringLength(100, ErrorMessage = "La calle no debe exceder los 100 caracteres.")]
        public string? Street { get; set; }

        [StringLength(50, ErrorMessage = "La ciudad no debe exceder los 50 caracteres.")]
        public string? City { get; set; }

        [StringLength(50, ErrorMessage = "El estado no debe exceder los 50 caracteres.")]
        public string? State { get; set; }

        [StringLength(10, ErrorMessage = "El código postal no debe exceder los 10 caracteres.")]
        public string? PostalCode { get; set; }

        [StringLength(50, ErrorMessage = "El país no debe exceder los 50 caracteres.")]
        public string? Country { get; set; }
        public virtual Person Person { get; set; }
    }
}