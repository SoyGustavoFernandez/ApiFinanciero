using AutoMapper;
using DataBackend;
using RepositoryBackEnd.Persona;

namespace ServicesBackEnd.Persona
{
    public class PersonaService : IPersonaService
    {
        private readonly IPersonaRepository _personaRepository;
        private readonly IMapper _mapper;

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