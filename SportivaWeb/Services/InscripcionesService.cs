using Microsoft.Data.SqlClient;
using SportivaWeb.Models.DB;

namespace SportivaWeb.Services
{
    public interface IInscripcionesService
    {
        Task AgregarAsync(InscripcionModel inscripcion);
        Task<InscripcionModel?> ObtenerAsync(int id);
        Task<List<InscripcionModel>> ObtenerListaAsync();
    }

    public class InscripcionesService(IConfiguration configuration) : IInscripcionesService
    {
        private readonly string Conexion = configuration.GetConnectionString("DefaultConnection")!;

        public async Task<List<InscripcionModel>> ObtenerListaAsync()
        {
            List<InscripcionModel> lista = [];

            using var conexion = new SqlConnection(Conexion);
            await conexion.OpenAsync();

            using var comando = new SqlCommand("SELECT * FROM Inscripciones", conexion);
            using var lector = await comando.ExecuteReaderAsync();

            while (await lector.ReadAsync())
            {
                lista.Add(new()
                {
                    Id = lector.GetInt32(lector.GetOrdinal("Id")),
                    Inscripto = lector.GetInt32(lector.GetOrdinal("Inscripto")),
                    Evento = lector.GetInt32(lector.GetOrdinal("Evento")),
                    Fecha = lector.GetDateTime(lector.GetOrdinal("Fecha"))
                });
            }

            return lista;
        }

        public async Task<InscripcionModel?> ObtenerAsync(int id)
        {
            var lista = await ObtenerListaAsync();
            return lista.FirstOrDefault(o => o.Id == id);
        }

        public async Task AgregarAsync(InscripcionModel inscripcion)
        {
            using var conexion = new SqlConnection(Conexion);
            await conexion.OpenAsync();

            using var comando = new SqlCommand("INSERT INTO Inscripciones (Inscripto, Evento, Fecha) VALUES (@Inscripto, @Evento, @Fecha)", conexion);
            comando.Parameters.AddWithValue("@Inscripto", inscripcion.Inscripto);
            comando.Parameters.AddWithValue("@Evento", inscripcion.Evento);
            comando.Parameters.AddWithValue("@Fecha", inscripcion.Fecha);

            await comando.ExecuteNonQueryAsync();
        }
    }
}
