using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DataBackend
{
    public class MovimientoViewModel
    {
        /// <summary>
        /// Id del registro.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int NIdMovimiento { get; set; }

        /// <summary>
        /// Id de la tabla TblCuenta.
        /// </summary>
        [Required(ErrorMessage = "El ID de la cuenta es obligatorio")]
        [ForeignKey("CuentaDTO")]
        public int? NIdCuenta { get; set; }

        /// <summary>
        /// Fecha del Movimiento.
        /// </summary>
        [Required(ErrorMessage = "La fecha del movimiento es un campo obligatorio")]
        public DateTime? DFechaMovimiento { get; set; }

        /// <summary>
        /// Tipo de movimiento - TblParametros - 6: Retiro / 7: Depósito.
        /// </summary>
        [Range(6, 7, ErrorMessage = "El valor debe ser 6: Retiro 7: Depósito")]
        public int? NTipoMovimiento { get; set; }

        /// <summary>
        /// Saldo antes del movimiento.
        /// </summary>
        public decimal? NSaldoInicial { get; set; }

        /// <summary>
        /// Valor del movimiento.
        /// </summary>
        [Required(ErrorMessage = "El valore del movimiento es obligatorio")]
        public decimal? NValor { get; set; }

        /// <summary>
        /// Estado del registro.
        /// </summary>
        [Required]
        public bool? LVigente { get; set; }
    }
}