using APIExercise.Core.Entities.Enums;
using APIExercise.Core.Utilities;
using System.Text.Json.Serialization;

namespace APIExercise.Core.DTOs
{
    public class AccountReadDto
    {
        public Guid Id { get; set; }
        public string AccountNumber { get; set; }
        public decimal Balance { get; set; }
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
        public DateTime CreatedDate { get; set; }
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
