
using APIExercise.Core.DTOs;

namespace APIExercise.Core.Interfaces.Services
{
    public interface IReportService
    {
        Task<ReportDto> GenerateClientReport(Guid clientId, DateTime startDate, DateTime endDate);
    }
}
