using DataBackend;

namespace RepositoryBackEnd.Cliente
{
    public interface IClienteRepository
    {
        Task<IEnumerable<ClienteViewModel>> GetAllClientesAsync();
        Task<ClienteViewModel> GetClienteByIdAsync(int id);
        Task<PersonaClienteViewModel> GetClienteByIdPersonaAsync(int idPersona);
        Task<int> CreateClienteAsync(ClienteViewModel Cliente);
        Task<bool> UpdateClienteAsync(int id, ClienteViewModel Cliente);
        Task<bool> DeleteClienteAsync(int id);
    }
}