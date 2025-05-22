using Microsoft.Data.SqlClient;
using SportivaWeb.Models.DB;

namespace SportivaWeb.Services
{
    public interface IEventosService
    {
        Task<List<EventoModel>> ObtenerAsync();
    }

    public class EventosService(IConfiguration configuration) : IEventosService
    {
        public async Task<List<EventoModel>> ObtenerAsync()
        {
            List<EventoModel> eventos = [];

            using var conexion = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));

            await conexion.OpenAsync();

            using var comando = new SqlCommand("SELECT * FROM Eventos", conexion);
            using var lector = await comando.ExecuteReaderAsync();

            while (await lector.ReadAsync())
            {
                eventos.Add(new()
                {
                    Id = lector.GetInt32(lector.GetOrdinal("Id")),
                    Nombre = lector.GetString(lector.GetOrdinal("Nombre")),
                    Fecha = lector.GetDateTime(lector.GetOrdinal("Fecha")),
                    Ubi = lector.GetString(lector.GetOrdinal("Ubi"))
                });
            }

            return eventos;
        }
    }
}
