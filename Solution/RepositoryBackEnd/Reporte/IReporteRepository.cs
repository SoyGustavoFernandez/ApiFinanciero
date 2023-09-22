using DataBackend;

namespace RepositoryBackEnd.Reporte
{
    public interface IReporteRepository
    {
        Task<ReportesViewModel> GetEstadoCuenta(int idCliente, DateTime dFecInicio, DateTime dFecFin);
    }
}