using APIExercise.Core.Entities;
using APIExercise.Core.Entities.Enums;

namespace APIExercise.Core.Interfaces
{
    public interface IAccountFactory
    {
        Account CreateAccount(string accountNumber, decimal initialBalance, AccountType type, int clientId);
    }
}
