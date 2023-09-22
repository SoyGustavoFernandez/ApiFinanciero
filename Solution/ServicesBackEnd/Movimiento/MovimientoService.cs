using AutoMapper;
using DataBackend;
using RepositoryBackEnd.Cuenta;
using RepositoryBackEnd.Movimiento;
using System.ComponentModel.DataAnnotations;

namespace ServicesBackEnd.Movimiento
{
    public class MovimientoService : IMovimientoService
    {
        private readonly IMovimientoRepository _movimientoRepository;
        private readonly IMapper _mapper;

        public MovimientoService(IMovimientoRepository movimientoRepository, IMapper mapper)
        {
            _movimientoRepository = movimientoRepository;
            _mapper = mapper;
        }

        public async Task<string> RealizarTransaccionAsync(MovimientoViewModel Movimiento)
        {
            if (Movimiento.NIdCuenta == null)
            {
                throw new ValidationException("La cuenta es obligatoria");
            }

            var movimientoEntity = _mapper.Map<MovimientoViewModel>(Movimiento);

            return await _movimientoRepository.RealizarTransaccionAsync(movimientoEntity);
        }

        public async Task<MovimientoViewModel> GetMovimientoByIdAsync(int id)
        {
            var movimiento = await _movimientoRepository.GetMovimientoByIdAsync(id);

            var movimientoViewModel = _mapper.Map<MovimientoViewModel>(movimiento);

            return movimientoViewModel;
        }

        public async Task<CuentaMovimientoViewModel> GetMovimientoByIdCuentaAsync(int idCuenta)
        {
            var movimiento = await _movimientoRepository.GetMovimientoByIdCuentaAsync(idCuenta);

            var movimientoViewModel = _mapper.Map<CuentaMovimientoViewModel>(movimiento);

            return movimientoViewModel;
        }
    }
}