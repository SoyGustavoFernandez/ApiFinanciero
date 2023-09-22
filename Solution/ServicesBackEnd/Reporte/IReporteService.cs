using DataBackend;

namespace ServicesBackEnd.Reporte
{
    public interface IReporteService
    {
        Task<ReportesViewModel> GetEstadoCuenta(int idCliente, DateTime dFecInicio, DateTime dFecFin);
    }
}