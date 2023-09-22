using AutoMapper;
using DataBackend;
using RepositoryBackEnd.Cliente;
using RepositoryBackEnd.Cuenta;
using RepositoryBackEnd.Movimiento;
using RepositoryBackEnd.Persona;
using SharedBackEnd;

namespace ServicesBackEnd.Reporte
{
    public class ReporteService : IReporteService
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IPersonaRepository _personaRepository;
        private readonly ICuentaRepository _cuentaRepository;
        private readonly IMovimientoRepository _movimientoRepository;

        public ReporteService(IClienteRepository clienteRepository, IPersonaRepository personaRepository, ICuentaRepository cuentaRepository, IMovimientoRepository movimientoRepository)
        {
            _clienteRepository = clienteRepository;
            _personaRepository = personaRepository;
            _cuentaRepository = cuentaRepository;
            _movimientoRepository = movimientoRepository;
        }

        public async Task<ReportesViewModel> GetEstadoCuenta(int idCliente, DateTime dFecInicio, DateTime dFecFin)
        {
            var cliente = await _clienteRepository.GetClienteByIdAsync(idCliente);

            if (cliente == null)
                throw new ArgumentNullException(nameof(cliente));

            var persona = await _personaRepository.GetPersonaByIdAsync((int)cliente.NIdPersona);

            if (persona == null)
                throw new ArgumentNullException(nameof(persona));

            var ctas = await _cuentaRepository.GetCuentaByIdClienteAsync(idCliente);

            if (ctas == null)
                throw new ArgumentNullException(nameof(ctas));

            var estadoCuenta = new ReportesViewModel{
                Cliente = new ClienteViewModel(),
                Cuentas = new List<CuentaMovimientoViewModel>(),
                nTotalCredito = 0, 
                nTotalDebito   = 0
            };

            foreach (var cta in ctas.cuentas)
            {
                var movimientos = await _movimientoRepository.GetMovimientoByIdCuentaAsync(cta.NIdCuenta);

                movimientos.movimientos = movimientos.movimientos.FindAll(x => Convert.ToDateTime(x.DFechaMovimiento).Date >= dFecInicio.Date && Convert.ToDateTime(x.DFechaMovimiento).Date <= dFecFin.Date);

                decimal debitos = 0;
                decimal creditos = 0;

                foreach (var mvt in movimientos.movimientos)
                {
                    if (mvt.NTipoMovimiento == (int)Tipo_Movimiento.RETIRO)
                    {
                        debitos += Math.Abs((decimal)mvt.NValor);
                    }
                    else if (mvt.NTipoMovimiento == (int)Tipo_Movimiento.DEPOSITO)
                    {
                        creditos += Math.Abs((decimal)mvt.NValor);
                    }
                }

                estadoCuenta.nTotalDebito = debitos;
                estadoCuenta.nTotalCredito = creditos;
                estadoCuenta.Cliente = new ClienteViewModel
                {
                    CDireccion = persona.CDireccion,
                    CIdentificacion = persona.CIdentificacion,
                    CTelefono = persona.CTelefono,
                    NEdad = persona.NEdad,
                    NGenero = persona.NGenero,
                    SNombres = persona.SNombres,
                    NIdPersona = persona.NIdPersona,
                    NIdCliente = cliente.NIdCliente,
                    LVigente = cliente.LVigente,
                    SClave = cliente.SClave
                };
                estadoCuenta.Cuentas.Add(new CuentaMovimientoViewModel
                {
                    cuenta = new CuentaViewModel
                    {
                        NIdCuenta = cta.NIdCuenta,
                        NIdCliente = cta.NIdCliente,
                        NSaldoActual = cta.NSaldoActual,
                        NSaldoInicial = cta.NSaldoInicial,
                        NTipoCuenta = cta.NTipoCuenta,
                        SNumCuenta = cta.SNumCuenta,
                        LVigente = cta.LVigente
                    },
                    movimientos = movimientos.movimientos.Select(c => new MovimientoViewModel
                    {
                        NIdMovimiento = c.NIdMovimiento,
                        NIdCuenta = c.NIdCuenta,
                        DFechaMovimiento = c.DFechaMovimiento,
                        NTipoMovimiento = c.NTipoMovimiento,
                        NSaldoInicial = c.NSaldoInicial,
                        NValor = c.NValor,
                        NSaldoDisponible = c.NSaldoInicial + c.NValor,
                        LVigente = c.LVigente
                    }).ToList()
                });
            }

            return estadoCuenta;
        }
    }
}