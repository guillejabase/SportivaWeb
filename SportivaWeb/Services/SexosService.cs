using Microsoft.Data.SqlClient;
using SportivaWeb.Models.DB;

namespace SportivaWeb.Services
{
    public interface ISexosService
    {
        Task<List<SexoModel>> ObtenerAsync();
        Task<Dictionary<int, string>> ObtenerDiccionarioAsync();
    }

    public class SexosService(IConfiguration configuration) : ISexosService
    {
        public async Task<List<SexoModel>> ObtenerAsync()
        {
            List<SexoModel> lista = [];

            using var conexion = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));

            await conexion.OpenAsync();

            using var comando = new SqlCommand("SELECT * FROM Sexos", conexion);
            using var lector = await comando.ExecuteReaderAsync();

            while (await lector.ReadAsync())
            {
                lista.Add(new()
                {
                    Id = lector.GetInt32(lector.GetOrdinal("Id")),
                    Nombre = lector.GetString(lector.GetOrdinal("Nombre"))
                });
            }

            return lista;
        }

        public async Task<Dictionary<int, string>> ObtenerDiccionarioAsync()
        {
            var lista = await ObtenerAsync();

            return lista.ToDictionary(o => o.Id, o => o.Nombre);
        }
    }
}
