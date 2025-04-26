using Microsoft.FluentUI.AspNetCore.Components;

namespace SportivaWeb.Models
{
    public class NavItem
    {
        public required string Href { get; set; }
        public required Icon Icon { get; set; }
        public required string Title { get; set; }
    }
}
