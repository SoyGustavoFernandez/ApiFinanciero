using DataBackend;

namespace RepositoryBackEnd.Movimiento
{
    public interface IMovimientoRepository
    {
        Task<MovimientoViewModel> GetMovimientoByIdAsync(int id);
        Task<CuentaMovimientoViewModel> GetMovimientoByIdCuentaAsync(int idCuenta);
        Task<string> RealizarTransaccionAsync(MovimientoViewModel Movimiento);
    }
}