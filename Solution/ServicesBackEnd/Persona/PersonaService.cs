using AutoMapper;
using DataBackend;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RepositoryBackEnd.Persona;

namespace ServicesBackEnd.Persona
{
    public class PersonaService : IPersonaService
    {
        private readonly IPersonaRepository _personaRepository;
        private readonly IMapper _mapper;

        public PersonaService() {}

        public PersonaService(IPersonaRepository personaRepository, IMapper mapper)
        {
            _personaRepository = personaRepository;
            _mapper = mapper;
        }

        public async Task<int> CreatePersonaAsync(PersonaViewModel persona)
        {
            var personaEntity = _mapper.Map<PersonaViewModel>(persona);

            return await _personaRepository.CreatePersonaAsync(personaEntity);
        }

        public PersonaViewModel CreatePersonaTest(PersonaViewModel objPersona)
        {
            if (string.IsNullOrEmpty(objPersona.SNombres))
                throw new ArgumentException("El nombre de la persona es obligatorio.");

            if (objPersona.SNombres.Length > 200)
                throw new ArgumentException("Se acepta máximo 200 caracteres.");

            if (objPersona.NGenero != 1 && objPersona.NGenero != 2)
                throw new ArgumentException("El valor debe ser 1: Masculino o 2: Femenino");

            if (objPersona.NEdad < 19 || objPersona.NEdad > 75)
                throw new ArgumentException("Para poder registrarte tienes que ser mayor de 18 y menor de 75 años.");

            if (string.IsNullOrEmpty(objPersona.CIdentificacion))
                throw new ArgumentException("La identificación es un campo obligatorio.");

            if (objPersona.CIdentificacion.Length > 10)
                throw new ArgumentException("Se acepta máximo 10 caracteres.");

            if (string.IsNullOrEmpty(objPersona.CDireccion))
                throw new ArgumentException("La Dirección es un campo obligatorio.");

            if (objPersona.CDireccion.Length > 200)
                throw new ArgumentException("Se acepta máximo 200 caracteres.");

            if (string.IsNullOrEmpty(objPersona.CTelefono))
                throw new ArgumentException("El número de teléfono es un campo obligatorio.");

            if (objPersona.CTelefono.Length > 200)
                throw new ArgumentException("Se acepta máximo 12 caracteres.");

            return objPersona;
        }

        public async Task<bool> DeletePersonaAsync(int id)
        {
            var deleted = await _personaRepository.DeletePersonaAsync(id);

            return deleted;
        }

        public async Task<IEnumerable<PersonaViewModel>> GetAllPersonasAsync()
        {
            var personas = await _personaRepository.GetAllPersonasAsync();

            var personasViewModel = _mapper.Map<IEnumerable<PersonaViewModel>>(personas);

            return personasViewModel;
        }

        public async Task<PersonaViewModel> GetPersonaByIdAsync(int id)
        {
            var persona = await _personaRepository.GetPersonaByIdAsync(id);

            var personaViewModel = _mapper.Map<PersonaViewModel>(persona);

            return personaViewModel;
        }

        public async Task<bool> UpdatePersonaAsync(int id, PersonaViewModel persona)
        {
            var personaEntity = _mapper.Map<PersonaViewModel>(persona);

            var updated = await _personaRepository.UpdatePersonaAsync(id, personaEntity);

            return updated;
        }
    }
}