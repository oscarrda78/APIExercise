using APIExercise.Core.DTOs;
using APIExercise.Core.Interfaces.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace APIExercise.API.Controllers
{
    [Route("transacciones")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;
        private readonly IMapper _mapper;
        private readonly ILogger<TransactionController> _logger;

        public TransactionController(ITransactionService transactionService, IMapper mapper, ILogger<TransactionController> logger)
        {
            _transactionService = transactionService ?? throw new ArgumentNullException(nameof(transactionService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTransactions()
        {
            try
            {
                var transactions = await _transactionService.GetAllAsync();
                return StatusCode(200, transactions);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error obteniendo todas las transacciones.");
                return StatusCode(500, "Error interno del servidor.");
            }
        }

        [HttpGet("{id}", Name = "GetTransactionById")]
        public async Task<IActionResult> GetTransactionById(Guid id)
        {
            try
            {
                var transaction = await _transactionService.GetByIdAsync(id);
                if (transaction == null)
                {
                    return StatusCode(404, "Transacción no encontrada.");
                }
                return StatusCode(200, transaction);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error obteniendo la transacción con ID: {id}.");
                return StatusCode(500, "Error interno del servidor.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateTransaction(TransactionCreateDto transactionDto)
        {
            try
            {
                var createdTransaction = await _transactionService.AddAsync(transactionDto);
                return StatusCode(201, createdTransaction);
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex, "Error al crear la transacción debido a argumentos inválidos.");
                return StatusCode(400, new { mensaje = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex, "Error al crear la transacción debido a una operación inválida.");
                return StatusCode(400, new { mensaje = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear la transacción.");
                return StatusCode(500, "Error interno del servidor.");
            }
        }
    }
}