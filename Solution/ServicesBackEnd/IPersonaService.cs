using DataBackend;

namespace ServicesBackEnd
{
    public interface IPersonaService
    {
        Task<IEnumerable<PersonaViewModel>> GetAllPersonasAsync();
        Task<PersonaViewModel> GetPersonaByIdAsync(int id);
        Task<int> CreatePersonaAsync(PersonaViewModel persona);
        Task<bool> UpdatePersonaAsync(int id, PersonaViewModel persona);
        Task<bool> DeletePersonaAsync(int id);
    }
}