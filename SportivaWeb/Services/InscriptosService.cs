using Microsoft.Data.SqlClient;
using SportivaWeb.Models;

namespace SportivaWeb.Services
{
    public interface IInscriptosService
    {
        Task AgregarAsync(InscriptoModel inscripto);
        Task<InscriptoModel?> ObtenerAsync(int id);
        Task<List<InscriptoModel>> ObtenerListaAsync();
    }

    public class InscriptosService(IConfiguration configuration) : IInscriptosService
    {
        private readonly string? Conexion = configuration.GetConnectionString("DefaultConnection");

        public async Task<List<InscriptoModel>> ObtenerListaAsync()
        {
            using var conexion = new SqlConnection(Conexion);
            await conexion.OpenAsync();

            using var comando = new SqlCommand("SELECT * FROM Inscriptos", conexion);
            using var lector = await comando.ExecuteReaderAsync();

            List<InscriptoModel> lista = [];

            while (await lector.ReadAsync())
            {
                lista.Add(new()
                {
                    Id = lector.GetInt32(lector.GetOrdinal("Id")),
                    Email = lector.GetString(lector.GetOrdinal("Email")),
                    Nombres = lector.GetString(lector.GetOrdinal("Nombres")),
                    Apellidos = lector.GetString(lector.GetOrdinal("Apellidos")),
                    FechaNaci = lector.GetDateTime(lector.GetOrdinal("FechaNaci")),
                    Sexo = lector.GetInt32(lector.GetOrdinal("Sexo")),
                    DNI = lector.GetString(lector.GetOrdinal("DNI")),
                    Provincia = lector.GetInt32(lector.GetOrdinal("Provincia")),
                    CodPostal = lector.GetString(lector.GetOrdinal("CodPostal")),
                    Telefono = lector.GetString(lector.GetOrdinal("Telefono"))
                });
            }

            return lista;
        }

        public async Task<InscriptoModel?> ObtenerAsync(int id)
        {
            var lista = await ObtenerListaAsync();
            return lista.FirstOrDefault(o => o.Id == id);
        }

        public async Task AgregarAsync(InscriptoModel inscripto)
        {
            using var conexion = new SqlConnection(Conexion);
            await conexion.OpenAsync();

            using var comando = new SqlCommand("INSERT INTO Inscriptos (Email, Nombres, Apellidos, FechaNaci, Sexo, DNI, Provincia, CodPostal, Telefono)" +
                "VALUES (@Email, @Nombres, @Apellidos, @FechaNaci, @Sexo, @DNI, @Provincia, @CodPostal, @Telefono)", conexion);

            comando.Parameters.AddWithValue("@Email", inscripto.Email);
            comando.Parameters.AddWithValue("@Nombres", inscripto.Nombres);
            comando.Parameters.AddWithValue("@Apellidos", inscripto.Apellidos);
            comando.Parameters.AddWithValue("@FechaNaci", inscripto.FechaNaci);
            comando.Parameters.AddWithValue("@Sexo", inscripto.Sexo);
            comando.Parameters.AddWithValue("@DNI", inscripto.DNI);
            comando.Parameters.AddWithValue("@Provincia", inscripto.Provincia);
            comando.Parameters.AddWithValue("@CodPostal", inscripto.CodPostal);
            comando.Parameters.AddWithValue("@Telefono", inscripto.Telefono);

            await comando.ExecuteNonQueryAsync();
        }
    }
}
