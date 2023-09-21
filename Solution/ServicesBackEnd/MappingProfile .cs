using AutoMapper;
using DataBackend.Models;
using DataBackend;

namespace ServicesBackEnd
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<PersonaViewModel, TblPersona>();
            CreateMap<TblPersona, PersonaViewModel>();

            CreateMap<ClienteViewModel, TblCliente>();
            CreateMap<TblCliente, ClienteViewModel>();
        }
    }
}