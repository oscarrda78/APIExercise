using APIExercise.Core.Entities;
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
    public class ClientUpdateDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public ClientAddresDto Address { get; set; }
        public string StatusDescription
        {
            get
            {
                return Status.GetDescription();
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                    Status = EnumExtensions.GetEnumValueFromDescription<Status>(value);
            }
        }

        [JsonIgnore]
        public Status Status { get; set; }

    }
}
