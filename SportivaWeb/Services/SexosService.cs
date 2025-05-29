using Microsoft.Data.SqlClient;
using SportivaWeb.Models;

namespace SportivaWeb.Services
{
    public interface ISexosService
    {
        Task<SexoModel?> ObtenerAsync(int id);
        Task<List<SexoModel>> ObtenerListaAsync();
        Task<Dictionary<int, string>> ObtenerDiccionarioAsync();
    }

    public class SexosService(IConfiguration configuration) : ISexosService
    {
        public async Task<List<SexoModel>> ObtenerListaAsync()
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
            var lista = await ObtenerListaAsync();
            return lista.ToDictionary(o => o.Id, o => o.Nombre);
        }

        public async Task<SexoModel?> ObtenerAsync(int id)
        {
            var lista = await ObtenerListaAsync();
            return lista.FirstOrDefault(o => o.Id == id);
        }
    }
}
