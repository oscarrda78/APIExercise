using APIExercise.Core.DTOs;
using APIExercise.Core.Entities;
using APIExercise.Core.Interfaces.Repositories;
using APIExercise.Core.Interfaces.Services;
using AutoMapper;

namespace APIExercise.Infrastructure.Implementations.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;

        public AccountService(IAccountRepository accountRepository, IMapper mapper)
        {
            _accountRepository = accountRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AccountReadDto>> GetAllAsync()
        {
            var accounts = await _accountRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<AccountReadDto>>(accounts);
        }

        public async Task<AccountReadDto> GetByIdAsync(Guid id)
        {
            var account = await _accountRepository.GetByIdAsync(id);
            return _mapper.Map<AccountReadDto>(account);
        }

        public async Task<AccountReadDto> AddAsync(AccountCreateDto accountDto)
        {
            var account = _mapper.Map<Account>(accountDto);
            var createdAccount = await _accountRepository.AddAsync(account);
            return _mapper.Map<AccountReadDto>(createdAccount);
        }

        public async Task<bool> UpdateAsync(Guid id, AccountUpdateDto accountDto)
        {
            var existingAccount = await _accountRepository.GetByIdAsync(id);
            _mapper.Map(accountDto, existingAccount);
            return await _accountRepository.UpdateAsync(existingAccount);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            return await _accountRepository.DeleteAsync(id);
        }
    }
}
