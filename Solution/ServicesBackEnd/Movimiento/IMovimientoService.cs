using DataBackend;

namespace ServicesBackEnd.Movimiento
{
    public interface IMovimientoService
    {
        Task<MovimientoViewModel> GetMovimientoByIdAsync(int id);
        Task<CuentaMovimientoViewModel> GetMovimientoByIdCuentaAsync(int idCuenta);
        Task<string> RealizarTransaccionAsync(MovimientoViewModel Movimiento);
    }
}