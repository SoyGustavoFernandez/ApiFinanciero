using DataBackend;
using DataBackend.Models;
using Microsoft.EntityFrameworkCore;
using SharedBackEnd;
using System;
using System.Runtime.CompilerServices;

namespace RepositoryBackEnd.Movimiento
{
    public class MovimientoRepository : IMovimientoRepository
    {
        private readonly RetoBackendContext _context;

        public MovimientoRepository(RetoBackendContext context)
        {
            _context = context;
        }

        public async Task<string> RealizarTransaccionAsync(MovimientoViewModel Movimiento)
        {
            using var transaction = _context.Database.BeginTransaction();

            try
            {
                var cuenta = await _context.TblCuenta.FindAsync(Movimiento.NIdCuenta);
                if (cuenta == null)
                {
                    transaction.Rollback();
                    return "La cuenta ingresada no existe";
                }

                if (cuenta.NSaldoActual < Movimiento.NValor && Movimiento.NTipoMovimiento == (int)Tipo_Movimiento.RETIRO)
                {
                    transaction.Rollback();
                    return "Saldo no disponible";
                }
            
                if (Movimiento.NTipoMovimiento == (int)Tipo_Movimiento.RETIRO)
                {
                    TblParametro parametro = await ObtenerLimiteDeTransaccionAsync((int)Limite_Diario_Retiro.GRUPO, (int)Limite_Diario_Retiro.PARAMETRO);

                    var movimientosDelDia = await _context.TblMovimientos
                        .Where(m => m.NIdCuenta == Movimiento.NIdCuenta &&
                                    m.DFechaMovimiento.Value.Date == Movimiento.DFechaMovimiento.Value.Date &&
                                    m.NTipoMovimiento == (int)Tipo_Movimiento.RETIRO)
                        .ToListAsync();

                    decimal sumaDebito = (decimal)movimientosDelDia.Sum(m => m.NValor);

                    if (Math.Abs(sumaDebito) + Movimiento.NValor > parametro.NValor)
                    {
                        transaction.Rollback();
                        return parametro.SValor;
                    }
                }

                var nuevoMovimiento = new TblMovimiento
                {
                    NIdCuenta = Movimiento.NIdCuenta,
                    DFechaMovimiento = Movimiento.DFechaMovimiento,
                    NTipoMovimiento = Movimiento.NTipoMovimiento,
                    NSaldoInicial = cuenta.NSaldoActual,
                    NValor = Movimiento.NTipoMovimiento == (int)Tipo_Movimiento.RETIRO ? (Movimiento.NValor * -1) : Movimiento.NValor,
                    LVigente = Movimiento.LVigente
                };

                _context.TblMovimientos.Add(nuevoMovimiento);

                cuenta.NSaldoActual = Movimiento.NTipoMovimiento == (int)Tipo_Movimiento.RETIRO ? cuenta.NSaldoActual - Movimiento.NValor : cuenta.NSaldoActual + Movimiento.NValor;

                await _context.SaveChangesAsync();

                transaction.Commit();
                return "Transacción realizada exitosamente";
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }
        }

        public async Task<MovimientoViewModel> GetMovimientoByIdAsync(int id)
        {
            var movimiento = await _context.TblMovimientos
                .Where(c => c.NIdMovimiento == id)
                .Select(c => new MovimientoViewModel
                {
                    NIdMovimiento = c.NIdMovimiento,
                    NIdCuenta = c.NIdCuenta,
                    DFechaMovimiento = c.DFechaMovimiento,
                    NTipoMovimiento = c.NTipoMovimiento,
                    NSaldoInicial = c.NSaldoInicial,
                    NValor = c.NValor,
                    LVigente = c.LVigente
                })
                .FirstOrDefaultAsync();

            return movimiento;
        }

        public async Task<CuentaMovimientoViewModel> GetMovimientoByIdCuentaAsync(int idCuenta)
        {
            var Cuenta = await _context.TblCuenta
                .Where(c => c.NIdCuenta == idCuenta)
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

            if (Cuenta == null)
            {
                return null;
            }

            var Movimientos = await _context.TblMovimientos
                .Where(c => c.NIdCuenta == idCuenta)
                .Select(c => new MovimientoViewModel
                {
                    NIdMovimiento = c.NIdMovimiento,
                    NIdCuenta = c.NIdCuenta,
                    DFechaMovimiento = c.DFechaMovimiento,
                    NTipoMovimiento = c.NTipoMovimiento,
                    NSaldoInicial = c.NSaldoInicial,
                    NValor = c.NValor,
                    NSaldoDisponible = c.NSaldoInicial + c.NValor,
                    LVigente = c.LVigente
                })
                .ToListAsync();

            var cuentaMovimientoViewModel = new CuentaMovimientoViewModel
            {
                cuenta = Cuenta,
                movimientos = Movimientos
            };

            return cuentaMovimientoViewModel;
        }

        public async Task<TblParametro> ObtenerLimiteDeTransaccionAsync(int idGrupo, int nParametro)
        {
            var parametro = await _context.TblParametros
                .Where(p => p.NGrupo == idGrupo && p.NParametro == nParametro)
                .FirstOrDefaultAsync();

            if (parametro != null)
            {
                return parametro;
            }

            throw new Exception("No se encontró el parámetro.");
        }
    }
}