using AutoMapper;
using DataBackend;
using RepositoryBackEnd;

namespace ServicesBackEnd
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IMapper _mapper;

        public ClienteService(IClienteRepository clienteRepository, IMapper mapper)
        {
            _clienteRepository = clienteRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ClienteViewModel>> GetAllClientesAsync()
        {
            var clientes = await _clienteRepository.GetAllClientesAsync();

            var clientesViewModel = _mapper.Map<IEnumerable<ClienteViewModel>>(clientes);

            return clientesViewModel;
        }

        public async Task<ClienteViewModel> GetClienteByIdAsync(int id)
        {
            var cliente = await _clienteRepository.GetClienteByIdAsync(id);

            var clienteViewModel = _mapper.Map<ClienteViewModel>(cliente);

            return clienteViewModel;
        }

        public async Task<PersonaClienteViewModel> GetClienteByIdPersonaAsync(int idPersona)
        {
            var cliente = await _clienteRepository.GetClienteByIdPersonaAsync(idPersona);

            var personaViewModel = _mapper.Map<PersonaClienteViewModel>(cliente);

            return personaViewModel;
        }

        public async Task<int> CreateClienteAsync(ClienteViewModel cliente)
        {
            var clienteEntity = _mapper.Map<ClienteViewModel>(cliente);
         
            return await _clienteRepository.CreateClienteAsync(clienteEntity);
        }

        public async Task<bool> UpdateClienteAsync(int id, ClienteViewModel cliente)
        {
            var clienteEntity = _mapper.Map<ClienteViewModel>(cliente);

            var updated = await _clienteRepository.UpdateClienteAsync(id, clienteEntity);

            return updated;
        }

        public async Task<bool> DeleteClienteAsync(int id)
        {
            var deleted = await _clienteRepository.DeleteClienteAsync(id);

            return deleted;
        }
    }
}