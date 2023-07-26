using APIExercise.Core.DTOs;

namespace APIExercise.Core.Interfaces.Services
{
    public interface IClientService
    {
        Task<IEnumerable<ClientReadDto>> GetAllAsync();
        Task<ClientReadDto> GetByIdAsync(Guid id);
        Task<ClientReadDto> AddAsync(ClientCreateDto clientDto);
        Task<bool> UpdateAsync(Guid id, ClientUpdateDto clientDto);
        Task<bool> DeleteAsync(Guid id);
    }
}
