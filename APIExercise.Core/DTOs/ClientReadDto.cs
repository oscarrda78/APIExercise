using APIExercise.Core.Entities.Enums;
using APIExercise.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace APIExercise.Core.DTOs
{
    public class ClientReadDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }

        public string StatusDescription
        {
            get
            {
                return Status.GetDescription();
            }
            set
            {
                Status = EnumExtensions.GetEnumValueFromDescription<Status>(value);
            }
        }
        [JsonIgnore]
        public Status Status { get; private set; }

    }
}
