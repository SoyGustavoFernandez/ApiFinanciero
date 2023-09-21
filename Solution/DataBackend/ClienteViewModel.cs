using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DataBackend
{
    public class ClienteViewModel : PersonaViewModel
    {
        /// <summary>
        /// Id del registro.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int NIdCliente { get; set; }

        /// <summary>
        /// Id de la tabla TblPersona.
        /// </summary>
        [Required(ErrorMessage = "El ID de la persona es obligatoria")]
        [ForeignKey("PersonaViewModel")]
        public int? NIdPersona { get; set; }

        /// <summary>
        /// Contraseña del Cliente.
        /// </summary>
        [Required(ErrorMessage = "La clave es obligatoria")]
        [StringLength(200, ErrorMessage = "Se acepta máximo 200 caracteres")]
        public string? SClave { get; set; }

        /// <summary>
        /// Estado del registro.
        /// </summary>
        [Required]
        public bool? LVigente { get; set; }
    }
}