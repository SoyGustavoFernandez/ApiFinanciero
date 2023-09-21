using DataBackend;
using DataBackend.Models;
using Microsoft.EntityFrameworkCore;
using SharedBackEnd;

namespace RepositoryBackEnd.Cliente
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly RetoBackendContext _context;

        public ClienteRepository(RetoBackendContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ClienteViewModel>> GetAllClientesAsync()
        {
            var clientes = await _context.TblClientes
                .Select(p => new ClienteViewModel
                {
                    NIdCliente = p.NIdCliente,
                    NIdPersona = p.NIdPersona,
                    SClave = p.SClave,
                    LVigente = p.LVigente
                })
                .ToListAsync();

            return clientes;
        }

        public async Task<ClienteViewModel> GetClienteByIdAsync(int id)
        {
            var cliente = await _context.TblClientes
                .Where(p => p.NIdCliente == id)
                .Select(p => new ClienteViewModel
                {
                    NIdCliente = p.NIdCliente,
                    NIdPersona = p.NIdPersona,
                    SClave = p.SClave,
                    LVigente = p.LVigente
                })
                .FirstOrDefaultAsync();

            return cliente;
        }

        public async Task<PersonaClienteViewModel> GetClienteByIdPersonaAsync(int idPersona)
        {
            var Persona = await _context.TblPersonas
                 .Where(p => p.NIdPersona == idPersona)
                 .Select(p => new PersonaViewModel
                 {
                     NIdPersona = p.NIdPersona,
                     SNombres = p.SNombres,
                     CIdentificacion = p.CIdentificacion,
                     CDireccion = p.CDireccion,
                     CTelefono = p.CTelefono,
                     NEdad = p.NEdad,
                     NGenero = p.NGenero,
                     LVigente = p.LVigente
                 })
                 .FirstOrDefaultAsync();

            if (Persona == null)
            {
                return null;
            }

            var Clientes = await _context.TblClientes
                .Where(c => c.NIdPersona == idPersona)
                .Select(c => new ClienteViewModel
                {
                    NIdCliente = c.NIdCliente,
                    SClave = c.SClave,
                    LVigente = c.LVigente,
                    CDireccion = Persona.CDireccion,
                    SNombres = Persona.SNombres,
                    CIdentificacion = Persona.CIdentificacion,
                    CTelefono = Persona.CTelefono,
                    NEdad = Persona.NEdad,
                    NGenero = Persona.NGenero,
                    NIdPersona = Persona.NIdPersona
                })
                .ToListAsync();

            var personaClienteViewModel = new PersonaClienteViewModel
            {
                persona = Persona,
                clientes = Clientes
            };

            return personaClienteViewModel;
        }

        public async Task<int> CreateClienteAsync(ClienteViewModel Cliente)
        {
            if (Cliente == null)
                throw new ArgumentNullException(nameof(Cliente));

            var nuevoCliente = new TblCliente
            {
                SClave = Encriptar.EncryptarCadena(Cliente.SClave),
                NIdPersona = Cliente.NIdPersona,
                LVigente = Cliente.LVigente
            };

            _context.TblClientes.Add(nuevoCliente);
            await _context.SaveChangesAsync();

            return nuevoCliente.NIdCliente;
        }

        public async Task<bool> UpdateClienteAsync(int id, ClienteViewModel Cliente)
        {
            if (Cliente == null)
                throw new ArgumentNullException(nameof(Cliente));

            var ClienteExistente = await _context.TblClientes.FirstOrDefaultAsync(p => p.NIdCliente == id);

            if (ClienteExistente == null)
                return false;

            ClienteExistente.SClave = Encriptar.EncryptarCadena(Cliente.SClave);
            ClienteExistente.LVigente = Cliente.LVigente;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteClienteAsync(int id)
        {
            var Cliente = await _context.TblClientes.FirstOrDefaultAsync(p => p.NIdCliente == id);

            if (Cliente == null)
                return false;

            _context.TblClientes.Remove(Cliente);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}