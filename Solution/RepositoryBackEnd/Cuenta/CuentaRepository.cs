using DataBackend;
using DataBackend.Models;
using Microsoft.EntityFrameworkCore;
using SharedBackEnd;

namespace RepositoryBackEnd.Cuenta
{
    public class CuentaRepository : ICuentaRepository
    {
        private readonly RetoBackendContext _context;

        public CuentaRepository(RetoBackendContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CuentaViewModel>> GetAllCuentasAsync()
        {
            var Cuentas = await _context.TblCuenta
                .Select(c => new CuentaViewModel
                {
                    NIdCuenta = c.NIdCuenta,
                    NIdCliente = c.NIdCliente,
                    SNumCuenta = c.SNumCuenta,
                    NTipoCuenta = c.NTipoCuenta,
                    NSaldoInicial = c.NSaldoInicial,
                    NSaldoActual = c.NSaldoActual,
                    LVigente = c.LVigente
                })
                .ToListAsync();

            return Cuentas;
        }

        public async Task<CuentaViewModel> GetCuentaByIdAsync(int id)
        {
            var Cuenta = await _context.TblCuenta
                .Where(c => c.NIdCuenta == id)
                .Select(c => new CuentaViewModel
                {
                    NIdCuenta = c.NIdCuenta,
                    NIdCliente = c.NIdCliente,
                    SNumCuenta = c.SNumCuenta,
                    NTipoCuenta = c.NTipoCuenta,
                    NSaldoInicial = c.NSaldoInicial,
                    NSaldoActual = c.NSaldoActual,
                    LVigente = c.LVigente
                })
                .FirstOrDefaultAsync();

            return Cuenta;
        }

        public async Task<ClienteCuentaViewModel> GetCuentaByIdClienteAsync(int idCliente)
        {
            var Cliente = await _context.TblClientes
                .Where(c => c.NIdCliente == idCliente)
                .Select(c => new ClienteViewModel
                {
                    NIdCliente = c.NIdCliente,
                    NIdPersona = c.NIdPersona,
                    SClave = c.SClave,
                    LVigente = c.LVigente
                })
                .FirstOrDefaultAsync();

            if (Cliente == null)
            {
                return null;
            }

            var Cuentas = await _context.TblCuenta
                .Where(c => c.NIdCliente == idCliente)
                .Select(c => new CuentaViewModel
                {
                    NIdCliente = c.NIdCliente,
                    NIdCuenta = c.NIdCuenta,
                    SNumCuenta = c.SNumCuenta,
                    NTipoCuenta = c.NTipoCuenta,
                    NSaldoInicial = c.NSaldoInicial,
                    NSaldoActual = c.NSaldoActual,
                    LVigente = c.LVigente
                })
                .ToListAsync();

            var clienteCuentaViewModel = new ClienteCuentaViewModel
            {
                cliente = Cliente,
                cuentas = Cuentas
            };

            return clienteCuentaViewModel;
        }

        public async Task<int> CreateCuentaAsync(CuentaViewModel Cuenta)
        {
            if (Cuenta == null)
                throw new ArgumentNullException(nameof(Cuenta));

            var nuevoaCuenta = new TblCuentum
            {
                NIdCliente = Cuenta.NIdCliente,
                SNumCuenta = Cuenta.SNumCuenta,
                NTipoCuenta = Cuenta.NTipoCuenta,
                NSaldoInicial = Cuenta.NSaldoInicial,
                NSaldoActual = Cuenta.NSaldoInicial,
                LVigente = Cuenta.LVigente
            };

            _context.TblCuenta.Add(nuevoaCuenta);
            await _context.SaveChangesAsync();

            return nuevoaCuenta.NIdCuenta;
        }

        public async Task<bool> UpdateCuentaAsync(int id, CuentaViewModel Cuenta)
        {
            if (Cuenta == null)
                throw new ArgumentNullException(nameof(Cuenta));

            var CuentaExistente = await _context.TblCuenta.FirstOrDefaultAsync(p => p.NIdCuenta == id);

            if (CuentaExistente == null)
                return false;

            CuentaExistente.NIdCliente = Cuenta.NIdCliente;
            CuentaExistente.SNumCuenta = Cuenta.SNumCuenta;
            CuentaExistente.NTipoCuenta = Cuenta.NTipoCuenta;
            CuentaExistente.NSaldoInicial = Cuenta.NSaldoInicial;
            CuentaExistente.NSaldoActual = Cuenta.NSaldoActual;
            CuentaExistente.LVigente = Cuenta.LVigente;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteCuentaAsync(int id)
        {
            var Cuenta = await _context.TblCuenta.FirstOrDefaultAsync(p => p.NIdCuenta == id);

            if (Cuenta == null)
                return false;

            _context.TblCuenta.Remove(Cuenta);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
