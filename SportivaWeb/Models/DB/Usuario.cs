using System.ComponentModel.DataAnnotations;

namespace SportivaWeb.Models.DB
{
    public class Usuario
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Ingrese email.")]
        [EmailAddress(ErrorMessage = "El email es inválido.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Ingrese contraseña.")]
        [MinLength(8, ErrorMessage = "La contraseña debe tener un mínimo de 8 caracteres.")]
        [MaxLength(16, ErrorMessage = "La contraseña debe tener un máximo de 16 caracteres.")]
        [RegularExpression(@"^[^\s]*$", ErrorMessage = "La contraseña no puede contener espacios.")]
        public string Contra { get; set; } = string.Empty;
    }
}
