using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DataBackend
{
    public class CuentaViewModel
    {
        /// <summary>
        /// Id del registro.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int NIdCuenta { get; set; }

        /// <summary>
        /// Id de la tabla TblCliente.
        /// </summary>
        [Required(ErrorMessage = "El ID del cliente es obligatorio")]
        [ForeignKey("ClienteDTO")]
        public int? NIdCliente { get; set; }

        /// <summary>
        /// Número de cuenta.
        /// </summary>
        [Required(ErrorMessage = "El número de cuenta es obligatorio")]
        [StringLength(25, ErrorMessage = "Se acepta máximo 25 caracteres")]
        public string? SNumCuenta { get; set; }

        /// <summary>
        /// Tipo de cuenta - TblParametros - 4: Ahorros / 5: Corriente.
        /// </summary>
        [Range(4, 5, ErrorMessage = "El valor debe ser 4: Ahorros o 5: Corriente")]
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
        [Required]
        public bool? LVigente { get; set; }
    }
}