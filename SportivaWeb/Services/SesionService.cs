using SportivaWeb.Models.DB;

namespace SportivaWeb.Services
{
    public interface ISesionService
    {
        UsuarioModel? Usuario { get; set; }

        event Action? EnSesionCambiada;

        void CerrarSesion();
        void IniciarSesion(UsuarioModel usuario);
    }

    public class SesionService : ISesionService
    {
        public UsuarioModel? Usuario { get; set; }

        public event Action? EnSesionCambiada;

        public void IniciarSesion(UsuarioModel usuario)
        {
            Usuario = usuario;
            EnSesionCambiada?.Invoke();
        }

        public void CerrarSesion()
        {
            Usuario = null;
            EnSesionCambiada?.Invoke();
        }
    }
}
