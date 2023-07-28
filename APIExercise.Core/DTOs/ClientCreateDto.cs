using APIExercise.Core.Entities;
using APIExercise.Core.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace APIExercise.Core.DTOs
{
    public class ClientCreateDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string IdDocument { get; set; }
        public ClientAddresDto Address { get; set; }
        public string Password { get; set; }
        public DateTime DateOfBirth { get; set; }
        [JsonIgnore]
        public Status Status { get; set; } = Status.Active;
        public string PhoneNumber { get; set; }
    }

}
