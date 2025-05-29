using Microsoft.FluentUI.AspNetCore.Components;
using SportivaWeb.Models;

namespace SportivaWeb.Services
{
    public interface ISesionService
    {
        UsuarioModel? Usuario { get; set; }

        event Action? EnSesionCambiada;

        void Cerrar();
        void Iniciar(UsuarioModel usuario);
    }

    public class SesionService : ISesionService
    {
        public UsuarioModel? Usuario { get; set; }

        public event Action? EnSesionCambiada;

        public void Iniciar(UsuarioModel usuario)
        {
            Usuario = usuario;
            EnSesionCambiada?.Invoke();
        }

        public void Cerrar()
        {
            Usuario = null;
            EnSesionCambiada?.Invoke();
        }
    }
}
