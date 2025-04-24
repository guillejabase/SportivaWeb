using System.ComponentModel.DataAnnotations;

namespace SportivaWeb.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Email obligatorio.")]
        [EmailAddress(ErrorMessage = "Email inválido.")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "Contraseña obligatoria.")]
        [MinLength(8, ErrorMessage = "Mínimo 8 caracteres.")]
        [MaxLength(32, ErrorMessage = "Máximo 32 caracteres.")]
        [RegularExpression(@"^[^\s]*$", ErrorMessage = "La contraseña no puede contener espacios.")]
        public required string Password { get; set; }
    }
}
