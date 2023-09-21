using AutoMapper;
using DataBackend;
using RepositoryBackEnd;

namespace ServicesBackEnd
{
    public class CuentaService : ICuentaService
    {
        private readonly ICuentaRepository _CuentaRepository;
        private readonly IMapper _mapper;

        public CuentaService(ICuentaRepository CuentaRepository, IMapper mapper)
        {
            _CuentaRepository = CuentaRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CuentaViewModel>> GetAllCuentasAsync()
        {
            var Cuentas = await _CuentaRepository.GetAllCuentasAsync();

            var CuentasViewModel = _mapper.Map<IEnumerable<CuentaViewModel>>(Cuentas);

            return CuentasViewModel;
        }

        public async Task<CuentaViewModel> GetCuentaByIdAsync(int id)
        {
            var Cuenta = await _CuentaRepository.GetCuentaByIdAsync(id);

            var CuentaViewModel = _mapper.Map<CuentaViewModel>(Cuenta);

            return CuentaViewModel;
        }

        public async Task<ClienteCuentaViewModel> GetCuentaByIdClienteAsync(int idCliente)
        {
            var Cuenta = await _CuentaRepository.GetCuentaByIdClienteAsync (idCliente);

            var clienteViewModel = _mapper.Map<ClienteCuentaViewModel>(Cuenta);

            return clienteViewModel;
        }

        public async Task<int> CreateCuentaAsync(CuentaViewModel Cuenta)
        {
            var CuentaEntity = _mapper.Map<CuentaViewModel>(Cuenta);

            return await _CuentaRepository.CreateCuentaAsync(CuentaEntity);
        }

        public async Task<bool> UpdateCuentaAsync(int id, CuentaViewModel Cuenta)
        {
            var CuentaEntity = _mapper.Map<CuentaViewModel>(Cuenta);

            var updated = await _CuentaRepository.UpdateCuentaAsync(id, CuentaEntity);

            return updated;
        }

        public async Task<bool> DeleteCuentaAsync(int id)
        {
            var deleted = await _CuentaRepository.DeleteCuentaAsync(id);

            return deleted;
        }
    }
}