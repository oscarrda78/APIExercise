using APIExercise.Core.DTOs;
using APIExercise.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace APIExercise.API.Controllers
{
    [Route("informes")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;
        private readonly ILogger<ReportController> _logger;

        public ReportController(IReportService reportService, ILogger<ReportController> logger)
        {
            _reportService = reportService ?? throw new ArgumentNullException(nameof(reportService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet("cliente/{clientId}")]
        public async Task<IActionResult> GetClientReport(Guid clientId, DateTime startDate, DateTime endDate)
        {
            try
            {
                var informe = await _reportService.GenerateClientReport(clientId, startDate, endDate);

                if (informe == null)
                {
                    _logger.LogWarning($"Report not found for client with ID {clientId} between {startDate} and {endDate}.");
                    return NotFound(new { Mensaje = "Informe no encontrado para el cliente especificado." });
                }

                _logger.LogInformation($"Report generated successfully for customer with ID {clientId}.");
                return Ok(informe);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to generate report for customer with ID {clientId}.");
                return StatusCode(500, new { Mensaje = "Error interno del servidor. Por favor, inténtelo de nuevo más tarde." });
            }
        }
    }
}
