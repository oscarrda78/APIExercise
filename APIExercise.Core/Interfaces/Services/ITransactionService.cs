using APIExercise.Core.DTOs;

namespace APIExercise.Core.Interfaces.Services
{
    public interface ITransactionService
    {
        Task<IEnumerable<TransactionReadDto>> GetAllAsync();

        Task<TransactionReadDto> GetByIdAsync(Guid id);

        Task<TransactionReadDto> AddAsync(TransactionCreateDto transactionDto);
    }
}
