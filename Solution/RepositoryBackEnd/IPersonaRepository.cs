using DataBackend;
using DataBackend.Models;

namespace RepositoryBackEnd
{
    public interface IPersonaRepository
    {
        Task<IEnumerable<PersonaViewModel>> GetAllPersonasAsync();
        Task<PersonaViewModel> GetPersonaByIdAsync(int id);
        Task<int> CreatePersonaAsync(PersonaViewModel persona);
        Task<bool> UpdatePersonaAsync(int id, PersonaViewModel persona);
        Task<bool> DeletePersonaAsync(int id);
    }
}