using System.ComponentModel.DataAnnotations;

namespace SportivaWeb.DTOs
{
    public class UserDTO
    {
        [Required(ErrorMessage = "Email obligatorio.")]
        [EmailAddress(ErrorMessage = "Email inválido.")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Contraseña obligatoria.")]
        public string? Password { get; set; }
    }
}
