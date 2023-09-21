using DataBackend;
using DataBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace RepositoryBackEnd.Persona
{
    public class PersonaRepository : IPersonaRepository
    {
        private readonly RetoBackendContext _context;

        public PersonaRepository(RetoBackendContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PersonaViewModel>> GetAllPersonasAsync()
        {
            var personas = await _context.TblPersonas
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
                .ToListAsync();

            return personas;
        }

        public async Task<PersonaViewModel> GetPersonaByIdAsync(int id)
        {
            var persona = await _context.TblPersonas
                .Where(p => p.NIdPersona == id)
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

            return persona;
        }

        public async Task<int> CreatePersonaAsync(PersonaViewModel persona)
        {
            if (persona == null)
                throw new ArgumentNullException(nameof(persona));

            var nuevaPersona = new TblPersona
            {
                SNombres = persona.SNombres,
                CIdentificacion = persona.CIdentificacion,
                CDireccion = persona.CDireccion,
                CTelefono = persona.CTelefono,
                NEdad = persona.NEdad,
                NGenero = persona.NGenero,
                LVigente = persona.LVigente
            };

            _context.TblPersonas.Add(nuevaPersona);
            await _context.SaveChangesAsync();

            return nuevaPersona.NIdPersona;
        }

        public async Task<bool> UpdatePersonaAsync(int id, PersonaViewModel persona)
        {
            if (persona == null)
                throw new ArgumentNullException(nameof(persona));

            var personaExistente = await _context.TblPersonas.FirstOrDefaultAsync(p => p.NIdPersona == id);

            if (personaExistente == null)
                return false;

            personaExistente.SNombres = persona.SNombres;
            personaExistente.CIdentificacion = persona.CIdentificacion;
            personaExistente.CDireccion = persona.CDireccion;
            personaExistente.CTelefono = persona.CTelefono;
            personaExistente.NEdad = persona.NEdad;
            personaExistente.NGenero = persona.NGenero;
            personaExistente.LVigente = persona.LVigente;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeletePersonaAsync(int id)
        {
            var persona = await _context.TblPersonas.FirstOrDefaultAsync(p => p.NIdPersona == id);

            if (persona == null)
                return false;

            _context.TblPersonas.Remove(persona);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}