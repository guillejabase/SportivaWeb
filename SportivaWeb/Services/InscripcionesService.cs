using Microsoft.Data.SqlClient;
using SportivaWeb.Models;
using System.Data.Common;
using System.Transactions;

namespace SportivaWeb.Services
{
    public interface IInscripcionesService
    {
        //Task AgregarAsync(InscripcionModel inscripcion);
        Task AgregarAsync(InscriptoModel inscripto, int eventoId);
        Task<InscripcionModel?> ObtenerAsync(int id);
        Task<List<InscripcionModel>> ObtenerListaAsync();
    }

    public class InscripcionesService(IConfiguration configuration) : IInscripcionesService
    {
        private readonly string conexionString = configuration.GetConnectionString("DefaultConnection")!;

        public async Task<List<InscripcionModel>> ObtenerListaAsync()
        {
            List<InscripcionModel> lista = [];

            using var conexion = new SqlConnection(conexionString);
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

        public async Task AgregarAsync(InscriptoModel inscripto, int eventoId)
        {
            using var conexion = new SqlConnection(conexionString);
            await conexion.OpenAsync();

            using var transaccion = conexion.BeginTransaction();

            try
            {
                var comandoSelectInscripto = new SqlCommand("SELECT TOP 1 Id FROM INSCRIPTOS WHERE Email = @Email OR DNI = @DNI", conexion, transaccion);
                comandoSelectInscripto.Parameters.AddWithValue("@Email", inscripto.Email);
                comandoSelectInscripto.Parameters.AddWithValue("@DNI", inscripto.DNI);

                var resultado = await comandoSelectInscripto.ExecuteScalarAsync();
                int inscriptoId;

                if (resultado is int existenteId)
                {
                    inscriptoId = existenteId;
                }
                else
                {
                    var comandoInsertInscripto = new SqlCommand(@"
                        INSERT INTO INSCRIPTOS (Email, Nombres, Apellidos, FechaNaci, Sexo, DNI, Provincia, CodPostal, Telefono)
                        OUTPUT INSERTED.Id
                        VALUES (@Email, @Nombres, @Apellidos, @FechaNaci, @Sexo, @DNI, @Provincia, @CodPostal, @Telefono)
                    ", conexion, transaccion);
                    comandoInsertInscripto.Parameters.AddWithValue("@Email", inscripto.Email);
                    comandoInsertInscripto.Parameters.AddWithValue("@Nombres", inscripto.Nombres);
                    comandoInsertInscripto.Parameters.AddWithValue("@Apellidos", inscripto.Apellidos);
                    comandoInsertInscripto.Parameters.AddWithValue("@FechaNaci", inscripto.FechaNaci);
                    comandoInsertInscripto.Parameters.AddWithValue("@Sexo", inscripto.Sexo);
                    comandoInsertInscripto.Parameters.AddWithValue("@DNI", inscripto.DNI);
                    comandoInsertInscripto.Parameters.AddWithValue("@Provincia", inscripto.Provincia);
                    comandoInsertInscripto.Parameters.AddWithValue("@CodPostal", inscripto.CodPostal);
                    comandoInsertInscripto.Parameters.AddWithValue("@Telefono", inscripto.Telefono);

                    inscriptoId = (int)await comandoInsertInscripto.ExecuteScalarAsync();
                }

                var comandoInsertInscripcion = new SqlCommand(@"
                    IF NOT EXISTS (
                        SELECT 1 FROM INSCRIPCIONES WHERE Inscripto = @InscriptoId AND Evento = @EventoId
                    )
                    BEGIN
                        INSERT INTO INSCRIPCIONES (Inscripto, Evento, Fecha)
                        VALUES (@InscriptoId, @EventoId, @Fecha)
                    END
                    ELSE
                    BEGIN
                        THROW 50000, 'Ya estás inscripto a este evento.', 1;
                    END
                ", conexion, transaccion);

                comandoInsertInscripcion.Parameters.AddWithValue("@InscriptoId", inscriptoId);
                comandoInsertInscripcion.Parameters.AddWithValue("@EventoId", eventoId);
                comandoInsertInscripcion.Parameters.AddWithValue("@Fecha", DateTime.Now);

                await comandoInsertInscripcion.ExecuteNonQueryAsync();

                transaccion.Commit();
            }
            catch
            {
                transaccion.Rollback();
                throw;
            }
        }
    }
}
