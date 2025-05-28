using Microsoft.Data.SqlClient;
using SportivaWeb.Models.DB;

namespace SportivaWeb.Services
{
    public interface IInscriptosService
    {
        Task<List<InscriptoModel>> ObtenerAsync();
    }

    public class InscriptosService(IConfiguration configuration) : IInscriptosService
    {
        public async Task<List<InscriptoModel>> ObtenerAsync()
        {
            List<InscriptoModel> lista = [];

            using var conexion = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));

            await conexion.OpenAsync();

            using var comando = new SqlCommand("SELECT * FROM Inscriptos", conexion);
            using var lector = await comando.ExecuteReaderAsync();

            while (await lector.ReadAsync())
            {
                lista.Add(new()
                {
                    Id = lector.GetInt32(lector.GetOrdinal("Id")),
                    Email = lector.GetString(lector.GetOrdinal("Email")),
                    Nombres = lector.GetString(lector.GetOrdinal("Nombres")),
                    Apellidos = lector.GetString(lector.GetOrdinal("Apellidos")),
                    Provincia = lector.GetInt32(lector.GetOrdinal("Provincia")),
                    CodPostal = lector.GetInt32(lector.GetOrdinal("CodPostal")),
                    DNI = lector.GetInt32(lector.GetOrdinal("DNI")),
                    FechaNaci = lector.GetDateTime(lector.GetOrdinal("FechaNaci")),
                    Sexo = lector.GetInt32(lector.GetOrdinal("Sexo")),
                    Telefono = lector.GetString(lector.GetOrdinal("Telefono"))
                });
            }

            return lista;
        }
    }
}
