using APIExercise.Core.Entities.Enums;
using APIExercise.Core.Utilities;
using System.Reflection.Metadata.Ecma335;
using System.Text.Json.Serialization;

namespace APIExercise.Core.DTOs
{
    public class AccountCreateDto
    {
        public string AccountNumber { get; set; }
        public decimal InitialBalance { get; set; }
        public string TypeDescription
        {
            get
            {
                return AccountType.GetDescription();
            }
            set
            {
                AccountType = EnumExtensions.GetEnumValueFromDescription<AccountType>(value);
            }
        }

        [JsonIgnore]
        public AccountType AccountType { get; private set; }
        [JsonIgnore]
        public Status Status { get; set; } = Status.Active;

        public Guid ClientId { get; set; }
    }
}
