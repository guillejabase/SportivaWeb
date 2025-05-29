using Microsoft.Data.SqlClient;
using SportivaWeb.Models;

namespace SportivaWeb.Services
{
    public interface IRolesService
    {
        Task<RolModel?> ObtenerAsync(int id);
        Task<Dictionary<int, string>> ObtenerDiccionarioAsync();
        Task<List<RolModel>> ObtenerListaAsync();
    }

    public class RolesService(IConfiguration configuration) : IRolesService
    {
        public async Task<List<RolModel>> ObtenerListaAsync()
        {
            List<RolModel> lista = [];

            using var conexion = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
            await conexion.OpenAsync();

            using var comando = new SqlCommand("SELECT * FROM Roles", conexion);
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

        public async Task<RolModel?> ObtenerAsync(int id)
        {
            var lista = await ObtenerListaAsync();
            return lista.FirstOrDefault(o => o.Id == id);
        }
    }
}
