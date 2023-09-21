using System;
using System.Collections.Generic;

namespace DataBackend.Models;

/// <summary>
/// Tabla donde se registran los datos de los clientes.
/// </summary>
public partial class TblCliente
{
    /// <summary>
    /// Id del registro.
    /// </summary>
    public int NIdCliente { get; set; }

    /// <summary>
    /// Id de la tabla TblPersona.
    /// </summary>
    public int? NIdPersona { get; set; }

    /// <summary>
    /// Contraseña del Cliente.
    /// </summary>
    public string? SClave { get; set; }

    /// <summary>
    /// Estado del registro.
    /// </summary>
    public bool? LVigente { get; set; }

    public virtual TblPersona? NIdPersonaNavigation { get; set; }

    public virtual ICollection<TblCuentum> TblCuenta { get; set; } = new List<TblCuentum>();
}
