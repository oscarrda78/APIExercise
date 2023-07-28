using APIExercise.Core.DTOs;
using APIExercise.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace APIExercise.API.Controllers
{
    [Route("clientes")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;
        private readonly ILogger<ClientController> _logger;

        public ClientController(IClientService clientService, ILogger<ClientController> logger)
        {
            _clientService = clientService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllClients()
        {
            try
            {
                var clients = await _clientService.GetAllAsync();
                return Ok(clients);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching all clients.");
                return StatusCode(500, "Error interno del servidor. Por favor, intenta más tarde.");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetClientById(Guid id)
        {
            try
            {
                var client = await _clientService.GetByIdAsync(id);
                if (client == null) return NotFound("Cliente no encontrado.");
                return Ok(client);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error fetching client with ID: {id}.");
                return StatusCode(500, "Error interno del servidor. Por favor, intenta más tarde.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateClient([FromBody] ClientCreateDto clientDto)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState
                        .Where(x => x.Value!.Errors.Count > 0)
                        .ToDictionary(k => k.Key, v => v.Value!.Errors.Select(e => e.ErrorMessage).ToArray());

                var errorResponse = new
                {
                    statusCode = (int)HttpStatusCode.BadRequest,
                    mensaje = "Datos de entrada inválidos.",
                    errores = errors
                };

                return BadRequest(errorResponse);
            }

            try
            {
                var createdClient = await _clientService.AddAsync(clientDto);
                return CreatedAtAction(nameof(GetClientById), new { id = createdClient.Id }, createdClient);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating a new client.");
                return StatusCode(500, "Error interno del servidor al crear un cliente. Por favor, intenta más tarde.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateClient(Guid id, [FromBody] ClientUpdateDto clientDto)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState
                        .Where(x => x.Value!.Errors.Count > 0)
                        .ToDictionary(k => k.Key, v => v.Value!.Errors.Select(e => e.ErrorMessage).ToArray());

                var errorResponse = new
                {
                    statusCode = (int)HttpStatusCode.BadRequest,
                    mensaje = "Datos de entrada inválidos.",
                    errores = errors
                };

                return BadRequest(errorResponse);
            }

            try
            {
                var isUpdated = await _clientService.UpdateAsync(id, clientDto);
                if (!isUpdated) return NotFound("Cliente no encontrado o no pudo ser actualizado.");
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating client with ID: {id}.");
                return StatusCode(500, "Error interno del servidor al actualizar el cliente. Por favor, intenta más tarde.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClient(Guid id)
        {
            try
            {
                var isDeleted = await _clientService.DeleteAsync(id);
                if (!isDeleted) return NotFound("Cliente no encontrado o no pudo ser eliminado.");
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting client with ID: {id}.");
                return StatusCode(500, "Error interno del servidor al eliminar el cliente. Por favor, intenta más tarde.");
            }
        }
    }
}
