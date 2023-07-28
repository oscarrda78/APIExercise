using APIExercise.Core.DTOs;
using APIExercise.Core.Entities;
using APIExercise.Core.Interfaces.Repositories;
using APIExercise.Core.Interfaces.Services;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace APIExercise.Infrastructure.Implementations.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;
        private readonly IMapper _mapper;

        public ClientService(IClientRepository clientRepository, IMapper mapper)
        {
            _clientRepository = clientRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ClientReadDto>> GetAllAsync()
        {
            var clients = await _clientRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ClientReadDto>>(clients);
        }

        public async Task<ClientReadDto> GetByIdAsync(Guid id)
        {
            var client = await _clientRepository.GetByIdAsync(id);
            return _mapper.Map<ClientReadDto>(client);
        }

        public async Task<ClientReadDto> AddAsync(ClientCreateDto clientDto)
        {
            var client = _mapper.Map<Client>(clientDto);
            var createdClient = await _clientRepository.AddAsync(client);
            return _mapper.Map<ClientReadDto>(createdClient);
        }

        public async Task<bool> UpdateAsync(Guid id, ClientUpdateDto clientDto)
        {
            var client = await _clientRepository.GetByIdAsync(id, include: source => source.Include(c => c.Address));
            if (client == null) return false;
            if (string.IsNullOrEmpty(clientDto.StatusDescription)) 
                clientDto.Status = client.Status;

            _mapper.Map(clientDto.Address, client.Address);
            _mapper.Map(clientDto, client);
            return await _clientRepository.UpdateAsync(client);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            return await _clientRepository.DeleteAsync(id);
        }
    }
}
