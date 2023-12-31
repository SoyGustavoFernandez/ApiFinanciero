﻿using DataBackend;

namespace ServicesBackEnd.Cliente
{
    public interface IClienteService
    {
        Task<IEnumerable<ClienteViewModel>> GetAllClientesAsync();
        Task<ClienteViewModel> GetClienteByIdAsync(int id);
        Task<PersonaClienteViewModel> GetClienteByIdPersonaAsync(int idPersona);
        Task<int> CreateClienteAsync(ClienteViewModel cliente);
        Task<bool> UpdateClienteAsync(int id, ClienteViewModel Cliente);
        Task<bool> DeleteClienteAsync(int id);
    }
}