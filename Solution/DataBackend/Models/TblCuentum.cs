using System;
using System.Collections.Generic;

namespace DataBackend.Models;

/// <summary>
/// Tabla donde se registran los datos de las cuentas.
/// </summary>
public partial class TblCuentum
{
    /// <summary>
    /// Id del registro.
    /// </summary>
    public int NIdCuenta { get; set; }

    /// <summary>
    /// Id de la tabla TblCliente.
    /// </summary>
    public int? NIdCliente { get; set; }

    /// <summary>
    /// Número de cuenta.
    /// </summary>
    public string? SNumCuenta { get; set; }

    /// <summary>
    /// Tipo de cuenta - TblParametros - 4: Ahorros / 5: Corriente.
    /// </summary>
    public int? NTipoCuenta { get; set; }

    /// <summary>
    /// Saldo inicial de la cuenta.
    /// </summary>
    public decimal? NSaldoInicial { get; set; }

    /// <summary>
    /// Saldo actual de la cuenta.
    /// </summary>
    public decimal? NSaldoActual { get; set; }

    /// <summary>
    /// Estado del registro.
    /// </summary>
    public bool? LVigente { get; set; }

    public virtual TblCliente? NIdClienteNavigation { get; set; }

    public virtual ICollection<TblMovimiento> TblMovimientos { get; set; } = new List<TblMovimiento>();
}
