using Microsoft.Data.SqlClient;
using SportivaWeb.Models.DB;

namespace SportivaWeb.Services
{
    public interface IEventosService
    {
        Task<EventoModel?> ObtenerAsync(int id);
        Task<List<EventoModel>> ObtenerListaAsync();
    }

    public class EventosService(IConfiguration configuration) : IEventosService
    {
        public async Task<List<EventoModel>> ObtenerListaAsync()
        {
            List<EventoModel> lista = [];

            using var conexion = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
            await conexion.OpenAsync();

            using var comando = new SqlCommand("SELECT * FROM Eventos", conexion);
            using var lector = await comando.ExecuteReaderAsync();

            while (await lector.ReadAsync())
            {
                lista.Add(new()
                {
                    Id = lector.GetInt32(lector.GetOrdinal("Id")),
                    Nombre = lector.GetString(lector.GetOrdinal("Nombre")),
                    Descri = lector.GetString(lector.GetOrdinal("Descri")),
                    FechaApeIns = lector.GetDateTime(lector.GetOrdinal("FechaApeIns")),
                    FechaCieIns = lector.GetDateTime(lector.GetOrdinal("FechaCieIns")),
                    FechaInicio = lector.GetDateTime(lector.GetOrdinal("FechaInicio")),
                    Provincia = lector.GetInt32(lector.GetOrdinal("Provincia")),
                    Imagen = lector.GetString(lector.GetOrdinal("Imagen")),
                    Precio = lector.GetDecimal(lector.GetOrdinal("Precio")),
                    Usuario = lector.GetInt32(lector.GetOrdinal("Usuario"))
                });
            }

            return lista;
        }

        public async Task<EventoModel?> ObtenerAsync(int id)
        {
            var lista = await ObtenerListaAsync();
            return lista.FirstOrDefault(o => o.Id == id);
        }
    }
}
