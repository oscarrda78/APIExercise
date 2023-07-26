using APIExercise.Core.DTOs;
using APIExercise.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace APIExercise.API.Controllers
{
    [Route("cuentas")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly ILogger<AccountController> _logger;

        public AccountController(IAccountService accountService, ILogger<AccountController> logger)
        {
            _accountService = accountService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAccounts()
        {
            try
            {
                var accounts = await _accountService.GetAllAsync();
                return Ok(accounts);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching all accounts.");
                return StatusCode(500, "Error interno del servidor. Por favor, intenta más tarde.");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAccountById(Guid id)
        {
            try
            {
                var account = await _accountService.GetByIdAsync(id);
                if (account == null) return NotFound("Cuenta no encontrada.");
                return Ok(account);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error fetching account with ID: {id}.");
                return StatusCode(500, "Error interno del servidor. Por favor, intenta más tarde.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateAccount([FromBody] AccountCreateDto accountDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var createdAccount = await _accountService.AddAsync(accountDto);
                return CreatedAtAction(nameof(GetAccountById), new { id = createdAccount.Id }, createdAccount);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating a new account.");
                return StatusCode(500, "Error interno del servidor al crear una cuenta. Por favor, intenta más tarde.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAccount(Guid id, [FromBody] AccountUpdateDto accountDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var isUpdated = await _accountService.UpdateAsync(id, accountDto);
                if (!isUpdated) return NotFound("Cuenta no encontrada o no pudo ser actualizada.");
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating account with ID: {id}.");
                return StatusCode(500, "Error interno del servidor al actualizar la cuenta. Por favor, intenta más tarde.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccount(Guid id)
        {
            try
            {
                var isDeleted = await _accountService.DeleteAsync(id);
                if (!isDeleted) return NotFound("Cuenta no encontrada o no pudo ser eliminada.");
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting account with ID: {id}.");
                return StatusCode(500, "Error interno del servidor al eliminar la cuenta. Por favor, intenta más tarde.");
            }
        }
    }
}
