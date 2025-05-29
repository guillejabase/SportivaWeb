using Microsoft.Data.SqlClient;
using SportivaWeb.Models.DB;

namespace SportivaWeb.Services
{
    public interface IEventosService
    {
        Task AgregarAsync(EventoModel evento);
        Task<bool> EliminarAsync(int id);
        Task<bool> ModificarAsync(EventoModel evento);
        Task<EventoModel?> ObtenerAsync(int id);
        Task<List<EventoModel>> ObtenerListaAsync();
    }

    public class EventosService(IConfiguration configuration) : IEventosService
    {
        private readonly string Conexion = configuration.GetConnectionString("DefaultConnection")!;

        public async Task<List<EventoModel>> ObtenerListaAsync()
        {
            List<EventoModel> lista = [];

            using var conexion = new SqlConnection(Conexion);
            await conexion.OpenAsync();

            using var comando = new SqlCommand("SELECT * FROM Eventos", conexion);
            using var lector = await comando.ExecuteReaderAsync();

            while (await lector.ReadAsync())
            {
                lista.Add(new()
                {
                    Id = lector.GetInt32(lector.GetOrdinal("Id")),
                    Nombre = lector.GetString(lector.GetOrdinal("Nombre")),
                    Descri = lector.GetString(lector.GetOrdinal("Descri")),
                    FechaApeIns = lector.GetDateTime(lector.GetOrdinal("FechaApeIns")),
                    FechaCieIns = lector.GetDateTime(lector.GetOrdinal("FechaCieIns")),
                    FechaInicio = lector.GetDateTime(lector.GetOrdinal("FechaInicio")),
                    Provincia = lector.GetInt32(lector.GetOrdinal("Provincia")),
                    CodPostal = lector.GetString(lector.GetOrdinal("CodPostal")),
                    Direccion = lector.GetString(lector.GetOrdinal("Direccion")),
                    Imagen = lector.GetString(lector.GetOrdinal("Imagen")),
                    Precio = lector.GetDecimal(lector.GetOrdinal("Precio")),
                    Usuario = lector.GetInt32(lector.GetOrdinal("Usuario"))
                });
            }

            return lista;
        }

        public async Task<EventoModel?> ObtenerAsync(int id)
        {
            var lista = await ObtenerListaAsync();
            return lista.FirstOrDefault(o => o.Id == id);
        }

        public async Task AgregarAsync(EventoModel evento)
        {
            using var conexion = new SqlConnection(Conexion);
            await conexion.OpenAsync();

            using var comando = new SqlCommand("INSERT INTO Eventos (Nombre, Descri, FechaApeIns, FechaCieIns, FechaInicio, Provincia, CodPostal, Direccion, Imagen, Precio, Usuario)" +
                "VALUES (@Nombre, @Descri, @FechaApeIns, @FechaCieIns, @FechaInicio, @Provincia, @CodPostal, @Direccion, @Imagen, @Precio, @Usuario)", conexion);
            comando.Parameters.AddWithValue("@Nombre", evento.Nombre);
            comando.Parameters.AddWithValue("@Descri", evento.Descri);
            comando.Parameters.AddWithValue("@FechaApeIns", evento.FechaApeIns);
            comando.Parameters.AddWithValue("@FechaCieIns", evento.FechaCieIns);
            comando.Parameters.AddWithValue("@FechaInicio", evento.FechaInicio);
            comando.Parameters.AddWithValue("@Provincia", evento.Provincia);
            comando.Parameters.AddWithValue("@CodPostal", evento.CodPostal);
            comando.Parameters.AddWithValue("@Direccion", evento.Direccion);
            comando.Parameters.AddWithValue("@Imagen", evento.Imagen);
            comando.Parameters.AddWithValue("@Precio", evento.Precio);
            comando.Parameters.AddWithValue("@Usuario", evento.Usuario);

            await comando.ExecuteNonQueryAsync();
        }

        public async Task<bool> EliminarAsync(int id)
        {
            using var conexion = new SqlConnection(Conexion);
            await conexion.OpenAsync();

            using var comando = new SqlCommand("DELETE FROM Eventos WHERE Id = @Id", conexion);
            comando.Parameters.AddWithValue("@Id", id);

            int codigo = await comando.ExecuteNonQueryAsync();

            return codigo > 0;
        }

        public async Task<bool> ModificarAsync(EventoModel evento)
        {
            using var conexion = new SqlConnection(Conexion);
            await conexion.OpenAsync();

            using var comando = new SqlCommand(@"
                UPDATE Eventos
                SET Nombre = @Nombre,
                    Descri = @Descri,
                    FechaApeIns = @FechaApeIns,
                    FechaCieIns = @FechaCieIns,
                    FechaInicio = @FechaInicio,
                    Provincia = @Provincia,
                    CodPostal = @CodPostal,
                    Direccion = @Direccion,
                    Imagen = @Imagen,
                    Precio = @Precio,
                    Usuario = @Usuario
                WHERE Id = @Id", conexion);
            comando.Parameters.AddWithValue("@Id", evento.Id);
            comando.Parameters.AddWithValue("@Nombre", evento.Nombre);
            comando.Parameters.AddWithValue("@Descri", evento.Descri);
            comando.Parameters.AddWithValue("@FechaApeIns", evento.FechaApeIns);
            comando.Parameters.AddWithValue("@FechaCieIns", evento.FechaCieIns);
            comando.Parameters.AddWithValue("@FechaInicio", evento.FechaInicio);
            comando.Parameters.AddWithValue("@Provincia", evento.Provincia);
            comando.Parameters.AddWithValue("@CodPostal", evento.CodPostal);
            comando.Parameters.AddWithValue("@Direccion", evento.Direccion);
            comando.Parameters.AddWithValue("@Imagen", evento.Imagen);
            comando.Parameters.AddWithValue("@Precio", evento.Precio);
            comando.Parameters.AddWithValue("@Usuario", evento.Usuario);

            int codigo = await comando.ExecuteNonQueryAsync();
            return codigo > 0;
        }
    }
}
