﻿using System.ComponentModel.DataAnnotations;

namespace SportivaWeb.Models.DB
{
    public class Evento
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Ingrese nombre.")]
        [MaxLength(16, ErrorMessage = "El nombre debe tener un máximo de 16 caracteres.")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "Ingrese fecha.")]
        public DateTime Fecha { get; set; } = DateTime.MaxValue;

        [Required(ErrorMessage = "Ingrese ubicación.")]
        [MaxLength(32, ErrorMessage = "La descripción de la ubicación debe tener un máximo de 32 carácteres.")]
        public string Ubicacion { get; set; } = string.Empty;
    }
}
