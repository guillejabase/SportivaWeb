using System.ComponentModel.DataAnnotations;

namespace SportivaWeb.DTOs
{
    public class UserDTO
    {
        [Required(ErrorMessage = "Ingrese email.")]
        [EmailAddress(ErrorMessage = "Email inválido.")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Ingrese contraseña.")]
        [MinLength(8, ErrorMessage = "La contraseña debe tener un mínimo de 8 caracteres.")]
        [MaxLength(16, ErrorMessage = "La contraseña debe tener un máximo de 16 caracteres.")]
        [RegularExpression(@"^[^\s]*$", ErrorMessage = "La contraseña no puede contener espacios.")]
        public string? Password { get; set; }
    }
}
