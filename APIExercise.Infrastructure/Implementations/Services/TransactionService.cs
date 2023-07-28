using APIExercise.Core.DTOs;
using APIExercise.Core.Entities;
using APIExercise.Core.Entities.Enums;
using APIExercise.Core.Interfaces.Repositories;
using APIExercise.Core.Interfaces.Services;
using AutoMapper;

namespace APIExercise.Infrastructure.Implementations.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;

        public TransactionService(ITransactionRepository transactionRepository,
                                  IAccountRepository accountRepository,
                                  IMapper mapper)
        {
            _transactionRepository = transactionRepository ?? throw new ArgumentNullException(nameof(transactionRepository));
            _accountRepository = accountRepository ?? throw new ArgumentNullException(nameof(accountRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<TransactionReadDto>> GetAllAsync()
        {
            var transactions = await _transactionRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<TransactionReadDto>>(transactions);
        }

        public async Task<TransactionReadDto> GetByIdAsync(Guid id)
        {
            var transaction = await _transactionRepository.GetByIdAsync(id);
            return _mapper.Map<TransactionReadDto>(transaction);
        }

        public async Task<TransactionReadDto> AddAsync(TransactionCreateDto transactionDto)
        {
            var account = await _accountRepository.GetByIdAsync(transactionDto.AccountId);
            if (account == null)
            {
                throw new ArgumentException("La cuenta asociada no existe.");
            }

            decimal newBalance;
            if (transactionDto.TransactionType == TransactionType.Income)
            {
                newBalance = account.Balance + transactionDto.Amount;
            }
            else
            {
                if (account.Balance <= 0)
                {
                    throw new InvalidOperationException("Saldo no disponible.");
                }

                var today = DateTime.Now.Date;
                var dailyOutcome = await _transactionRepository.GetDailyOutcomeAsync(account.Id, today);
                if (dailyOutcome + transactionDto.Amount > 1000)
                {
                    throw new InvalidOperationException("Cupo diario excedido.");
                }

                newBalance = account.Balance - transactionDto.Amount;
            }

            account.Balance = newBalance;
            await _accountRepository.UpdateAsync(account);

            var transaction = _mapper.Map<Transaction>(transactionDto);
            var createdTransaction = await _transactionRepository.AddAsync(transaction);
            return _mapper.Map<TransactionReadDto>(createdTransaction);
        }
    }
}