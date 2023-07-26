using APIExercise.Core.DTOs;

namespace APIExercise.Core.Interfaces.Services
{
    public interface IAccountService
    {
        Task<IEnumerable<AccountReadDto>> GetAllAsync();
        Task<AccountReadDto> GetByIdAsync(Guid id);
        Task<AccountReadDto> AddAsync(AccountCreateDto accountDto);
        Task<bool> UpdateAsync(Guid id, AccountUpdateDto accountDto);
        Task<bool> DeleteAsync(Guid id);
    }
}
