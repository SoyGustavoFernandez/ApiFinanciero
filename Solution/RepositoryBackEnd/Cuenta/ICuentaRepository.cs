using DataBackend;

namespace RepositoryBackEnd.Cuenta
{
    public interface ICuentaRepository
    {
        Task<IEnumerable<CuentaViewModel>> GetAllCuentasAsync();
        Task<CuentaViewModel> GetCuentaByIdAsync(int id);
        Task<ClienteCuentaViewModel> GetCuentaByIdClienteAsync(int idCliente);
        Task<int> CreateCuentaAsync(CuentaViewModel Cuenta);
        Task<bool> UpdateCuentaAsync(int id, CuentaViewModel Cuenta);
        Task<bool> DeleteCuentaAsync(int id);
    }
}