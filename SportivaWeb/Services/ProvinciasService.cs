using Microsoft.Data.SqlClient;
using SportivaWeb.Models.DB;

namespace SportivaWeb.Services
{
    public interface IProvinciasService
    {
        Task<List<ProvinciaModel>> ObtenerAsync();
        Task<Dictionary<int, string>> ObtenerDiccionarioAsync();
    }

    public class ProvinciasService(IConfiguration configuration) : IProvinciasService
    {
        public async Task<List<ProvinciaModel>> ObtenerAsync()
        {
            List<ProvinciaModel> lista = [];

            using var conexion = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));

            await conexion.OpenAsync();

            using var comando = new SqlCommand("SELECT * FROM Provincias", conexion);
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
