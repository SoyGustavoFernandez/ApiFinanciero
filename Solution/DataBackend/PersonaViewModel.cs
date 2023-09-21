using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataBackend
{
    public class PersonaViewModel
    {
        /// <summary>
        /// Id del registro.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int NIdPersona { get; set; }

        /// <summary>
        /// Nombres completos de la persona.
        /// </summary>
        [Required(ErrorMessage = "El nombre de la persona es obligatorio")]
        [StringLength(200, ErrorMessage = "Se acepta máximo 200 caracteres")]
        public string? SNombres { get; set; }

        /// <summary>
        /// Género de la persona TblParametros - 1: Masculino / 2: Femenino.
        /// </summary>
        [Range(1, 2, ErrorMessage = "El valor debe ser 1: Masculino o 2: Femenino")]
        public int? NGenero { get; set; }

        /// <summary>
        /// Edad de la persona.
        /// </summary>
        [Range(19, 74, ErrorMessage = "Para poder registrarte tienes que ser mayor de 18 y menor de 75 años.")]
        public int? NEdad { get; set; }

        /// <summary>
        /// Número de identificación de la persona.
        /// </summary>
        [Required(ErrorMessage = "La identificación es un campo obligatorio")]
        [StringLength(10, ErrorMessage = "Se acepta máximo 10 caracteres")]
        public string? CIdentificacion { get; set; }

        /// <summary>
        /// Dirección de la persona.
        /// </summary>
        [Required(ErrorMessage = "La Dirección es un campo obligatorio")]
        [StringLength(200, ErrorMessage = "Se acepta máximo 200 caracteres")]
        public string? CDireccion { get; set; }

        /// <summary>
        /// Teléfono de la persona.
        /// </summary>
        [Required(ErrorMessage = "El número de teléfono es un campo obligatorio")]
        [StringLength(12, ErrorMessage = "Se acepta máximo 12 caracteres")]
        public string? CTelefono { get; set; }

        /// <summary>
        /// Estado del registro.
        /// </summary>
        public bool? LVigente { get; set; }
    }
}