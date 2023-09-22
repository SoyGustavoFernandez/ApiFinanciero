using DataBackend;
using DataBackend.Models;
using Microsoft.EntityFrameworkCore;
using RepositoryBackEnd.Movimiento;
using SharedBackEnd;

namespace RepositoryBackEnd.Reporte
{
    public class ReporteRepository : IReporteRepository
    {
        private readonly RetoBackendContext _context;
        private readonly IMovimientoRepository _iMovimientoRepository;

        public ReporteRepository(RetoBackendContext context, IMovimientoRepository iMovimientoRepository)
        {
            _context = context;
            _iMovimientoRepository = iMovimientoRepository;
        }

        public async Task<ReportesViewModel> GetEstadoCuenta(int idCliente, DateTime dFecInicio, DateTime dFecFin)
        {
            var cliente = await _context.TblClientes.FindAsync(idCliente);
            
            if (cliente == null)
                throw new ArgumentNullException(nameof(cliente));

            var persona = await _context.TblPersonas.FindAsync(cliente.NIdPersonaNavigation);
            
            if (persona == null)
                throw new ArgumentNullException(nameof(persona));
            
            var ctas = await _context.TblCuenta.Where(c => c.NIdCliente == idCliente).ToListAsync();
            
            if (ctas == null)
                throw new ArgumentNullException(nameof(ctas));

            var estadoCuenta = new ReportesViewModel();

            foreach (var cta in ctas)
            {
                var movimientos = await _iMovimientoRepository.GetMovimientoByIdCuentaAsync(cta.NIdCuenta);

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
                estadoCuenta.Cuenta.Add(new CuentaMovimientoViewModel
                {
                    movimientos = movimientos.movimientos.Select(c => new MovimientoViewModel
                    {
                        NIdMovimiento = c.NIdMovimiento,
                        NIdCuenta = c.NIdCuenta,
                        DFechaMovimiento = c.DFechaMovimiento,
                        NTipoMovimiento = c.NTipoMovimiento,
                        NSaldoInicial = c.NSaldoInicial,
                        NValor = c.NValor,
                        LVigente = c.LVigente
                    }).ToList()
                });
            }

            return estadoCuenta;
        }
    }
}