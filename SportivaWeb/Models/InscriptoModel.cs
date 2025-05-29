using System.ComponentModel.DataAnnotations;

namespace SportivaWeb.Models
{
    public class InscriptoModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Ingrese email.")]
        [EmailAddress(ErrorMessage = "El email es inválido.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Ingrese nombres.")]
        [MaxLength(32, ErrorMessage = "Los nombres juntos deben tener un máximo de 32 caracteres.")]
        public string Nombres { get; set; } = string.Empty;

        [Required(ErrorMessage = "Ingrese apellidos.")]
        [MaxLength(32, ErrorMessage = "Los apellidos juntos deben tener un máximo de 32 caracteres.")]
        public string Apellidos { get; set; } = string.Empty;

        [Required(ErrorMessage = "Ingrese provincia.")]
        public int Provincia { get; set; } // FK ProvinciaModel

        [Required(ErrorMessage = "Ingrese código postal.")]
        [StringLength(4, ErrorMessage = "Código postal inválido.")]
        public string CodPostal { get; set; } = string.Empty;

        [Required(ErrorMessage = "Ingrese DNI.")]
        [StringLength(8, ErrorMessage = "El DNI debe tener como máximo 8 dígitos.")]
        public string DNI { get; set; } = string.Empty;

        [Required(ErrorMessage = "Ingrese fecha de nacimiento.")]
        public DateTime FechaNaci { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Ingrese sexo.")]
        public int Sexo { get; set; } // FK SexoModel

        [Required(ErrorMessage = "Ingrese teléfono.")]
        public string Telefono { get; set; } = string.Empty;
    }
}
