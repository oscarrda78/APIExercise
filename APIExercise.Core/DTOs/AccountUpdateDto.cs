using APIExercise.Core.Entities.Enums;
using APIExercise.Core.Utilities;
using System.Text.Json.Serialization;

namespace APIExercise.Core.DTOs
{
    public class AccountUpdateDto
    {
        public decimal Balance { get; set; }
        public string StatusDescription
        {
            get
            {
                return Status.GetDescription();
            }
            set
            {
                if(!string.IsNullOrEmpty(value))
                    Status = EnumExtensions.GetEnumValueFromDescription<Status>(value);
            }
        }

        [JsonIgnore]
        public Status Status { get; set; }

    }
}
