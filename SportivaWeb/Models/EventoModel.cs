using System.ComponentModel.DataAnnotations;

namespace SportivaWeb.Models
{
    public class EventoModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Ingrese nombre.")]
        [MaxLength(16, ErrorMessage = "El nombre debe tener un máximo de {1} caracteres.")]
        public string Nombre { get; set; } = string.Empty;

        [MaxLength(2048, ErrorMessage = "La descripción debe tener un máximo de {1} caracteres.")]
        public string? Descri { get; set; }

        [Required(ErrorMessage = "Ingrese fecha de apertura de inscripción.")]
        public DateTime FechaApeIns { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Ingrese fecha de cierre de inscripción.")]
        public DateTime FechaCieIns { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Ingrese fecha del inicio del evento.")]
        public DateTime FechaInicio { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Ingrese provincia.")]
        public int Provincia { get; set; } // FK ProvinciaModel

        [Required(ErrorMessage = "Ingrese código postal.")]
        [StringLength(4, ErrorMessage = "Código postal inválido.")]
        public string CodPostal { get; set; } = string.Empty;

        [Required(ErrorMessage = "Ingrese dirección.")]
        public string Direccion { get; set; } = string.Empty;

        public string? Imagen { get; set; } = string.Empty;

        [Required(ErrorMessage = "Ingrese precio.")]
        public decimal Precio { get; set; }

        public int Usuario { get; set; } // FK UsuarioModel
    }
}
