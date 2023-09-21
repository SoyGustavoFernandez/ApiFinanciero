using System;
using System.Collections.Generic;

namespace DataBackend.Models;

/// <summary>
/// Tabla donde se registran los datos de las personas.
/// </summary>
public partial class TblPersona
{
    /// <summary>
    /// Id del registro.
    /// </summary>
    public int NIdPersona { get; set; }

    /// <summary>
    /// Nombres completos de la persona.
    /// </summary>
    public string? SNombres { get; set; }

    /// <summary>
    /// Género de la persona TblParametros - 1: Masculino / 2: Femenino.
    /// </summary>
    public int? NGenero { get; set; }

    /// <summary>
    /// Edad de la persona.
    /// </summary>
    public int? NEdad { get; set; }

    /// <summary>
    /// Número de identificación de la persona.
    /// </summary>
    public string? CIdentificacion { get; set; }

    /// <summary>
    /// Dirección de la persona.
    /// </summary>
    public string? CDireccion { get; set; }

    /// <summary>
    /// Teléfono de la persona.
    /// </summary>
    public string? CTelefono { get; set; }

    /// <summary>
    /// Estado del registro.
    /// </summary>
    public bool? LVigente { get; set; }

    public virtual ICollection<TblCliente> TblClientes { get; set; } = new List<TblCliente>();
}
