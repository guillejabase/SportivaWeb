using System.ComponentModel.DataAnnotations;

namespace SportivaWeb.Models.DB
{
    public class EventoModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Ingrese nombre.")]
        [MaxLength(16, ErrorMessage = "El nombre debe tener un máximo de 16 caracteres.")]
        public string Nombre { get; set; } = string.Empty;

        [MaxLength(1024, ErrorMessage = "La descripción debe tener un máximo de 1024 caracteres.")]
        public string? Descri { get; set; }

        [Required(ErrorMessage = "Ingrese fecha de apertura de inscripción.")]
        public DateTime FechaApeIns { get; set; }

        [Required(ErrorMessage = "Ingrese fecha de cierre de inscripción.")]
        public DateTime FechaCieIns { get; set; }

        [Required(ErrorMessage = "Ingrese fecha del inicio del evento.")]
        public DateTime FechaInicio { get; set; }

        [Required(ErrorMessage = "Ingrese provincia.")]
        public int Provincia { get; set; } // FK ProvinciaModel

        public string? Imagen { get; set; }

        [Required(ErrorMessage = "Ingrese precio.")]
        public decimal Precio { get; set; }

        public int Usuario { get; set; } // FK UsuarioModel
    }
}
