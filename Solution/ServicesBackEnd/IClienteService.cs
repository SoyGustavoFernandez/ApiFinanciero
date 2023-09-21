using DataBackend;

namespace ServicesBackEnd
{
    public interface IClienteService
    {
        Task<IEnumerable<ClienteViewModel>> GetAllClientesAsync();
        Task<ClienteViewModel> GetClienteByIdAsync(int id);
        Task<ClienteViewModel> GetClienteByIdPersonaAsync(int idPersona);
        Task<int> CreateClienteAsync(ClienteViewModel cliente);
        Task<bool> UpdateClienteAsync(int id, ClienteViewModel Cliente);
        Task<bool> DeleteClienteAsync(int id);
    }
}