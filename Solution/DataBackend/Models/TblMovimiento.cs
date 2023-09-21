using System;
using System.Collections.Generic;

namespace DataBackend.Models;

/// <summary>
/// Tabla donde se registran los movimientos de las cuentas.
/// </summary>
public partial class TblMovimiento
{
    /// <summary>
    /// Id del registro.
    /// </summary>
    public int NIdMovimiento { get; set; }

    /// <summary>
    /// Id de la tabla TblCuenta.
    /// </summary>
    public int? NIdCuenta { get; set; }

    /// <summary>
    /// Fecha del Movimiento.
    /// </summary>
    public DateTime? DFechaMovimiento { get; set; }

    /// <summary>
    /// Tipo de movimiento - TblParametros - 6: Retiro / 7: Depósito.
    /// </summary>
    public int? NTipoMovimiento { get; set; }

    /// <summary>
    /// Saldo antes del movimiento.
    /// </summary>
    public decimal? NSaldoInicial { get; set; }

    /// <summary>
    /// Valor del movimiento.
    /// </summary>
    public decimal? NValor { get; set; }

    /// <summary>
    /// Estado del registro.
    /// </summary>
    public bool? LVigente { get; set; }

    public virtual TblCuentum? NIdCuentaNavigation { get; set; }
}
