using System;
using System.Collections.Generic;

namespace DataBackend.Models;

/// <summary>
/// Tabla donde se registran los parámetros configurables.
/// </summary>
public partial class TblParametro
{
    /// <summary>
    /// Id del registro.
    /// </summary>
    public int NIdParametro { get; set; }

    /// <summary>
    /// Nombre del parámetro.
    /// </summary>
    public string? SNombre { get; set; }

    /// <summary>
    /// Número del grupo del parámetro.
    /// </summary>
    public int? NGrupo { get; set; }

    /// <summary>
    /// Valor del parámetro, se utilizará para hacer join en todas las demás tablas.
    /// </summary>
    public int? NParametro { get; set; }

    /// <summary>
    /// Valor numérico del parámetro.
    /// </summary>
    public int? NValor { get; set; }

    /// <summary>
    /// Valor en texto del parámetro.
    /// </summary>
    public string? SValor { get; set; }

    /// <summary>
    /// Estado del registro.
    /// </summary>
    public bool? LVigente { get; set; }
}
