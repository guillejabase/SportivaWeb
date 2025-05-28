using Microsoft.FluentUI.AspNetCore.Components;

namespace SportivaWeb.Models
{
    public class NavItem
    {
        public required string Ruta { get; set; }
        public required Icon Icono { get; set; }
        public required string Titulo { get; set; }
        public required int[] Roles { get; set; }
    }
}
