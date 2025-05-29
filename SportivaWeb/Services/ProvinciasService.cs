using Microsoft.Data.SqlClient;
using SportivaWeb.Models;

namespace SportivaWeb.Services
{
    public interface IProvinciasService
    {
        Task<ProvinciaModel?> ObtenerAsync(int id);
        Task<Dictionary<int, string>> ObtenerDiccionarioAsync();
        Task<List<ProvinciaModel>> ObtenerListaAsync();
    }

    public class ProvinciasService(IConfiguration configuration) : IProvinciasService
    {
        public async Task<List<ProvinciaModel>> ObtenerListaAsync()
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
            var lista = await ObtenerListaAsync();
            return lista.ToDictionary(o => o.Id, o => o.Nombre);
        }

        public async Task<ProvinciaModel?> ObtenerAsync(int id)
        {
            var lista = await ObtenerListaAsync();
            return lista.FirstOrDefault(o => o.Id == id);
        }
    }
}
