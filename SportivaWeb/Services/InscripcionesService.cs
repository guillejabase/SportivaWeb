using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using SportivaWeb.Models.DB;

namespace SportivaWeb.Services
{
    public interface IInscripcionesService
    {
        Task<List<InscripcionModel>> ObtenerAsync();
    }

    public class InscripcionesService(IConfiguration configuration) : IInscripcionesService
    {
        public async Task<List<InscripcionModel>> ObtenerAsync()
        {
            List<InscripcionModel> lista = [];

            using var conexion = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));

            await conexion.OpenAsync();

            using var comando = new SqlCommand("SELECT * FROM Inscripciones", conexion);
            using var lector = await comando.ExecuteReaderAsync();

            while (await lector.ReadAsync())
            {
                lista.Add(new()
                {
                    Id = lector.GetInt32(lector.GetOrdinal("Id")),
                });
            }

            return lista;
        }
    }
}
