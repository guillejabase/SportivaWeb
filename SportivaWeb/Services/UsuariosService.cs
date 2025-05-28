using Microsoft.Data.SqlClient;
using SportivaWeb.Models.DB;
using SportivaWeb.Validations;

namespace SportivaWeb.Services
{
    public interface IUsuariosService
    {
        Task AgregarAsync(UsuarioModel usuario);
        Task<bool> EmailExisteAsync(string email);
        Task<bool> NombreExisteAsync(string nombre);
        Task<List<UsuarioModel>> ObtenerAsync();
        Task<UsuarioValidation> ValidarCredencialesAsync(string nombreOEmail, string contra);
    }

    public class UsuariosService(IConfiguration configuration) : IUsuariosService
    {
        private readonly string? Conexion = configuration.GetConnectionString("DefaultConnection");

        public async Task<List<UsuarioModel>> ObtenerAsync()
        {
            List<UsuarioModel> lista = [];

            using var conexion = new SqlConnection(Conexion);

            await conexion.OpenAsync();

            using var comando = new SqlCommand("SELECT * FROM Usuarios", conexion);
            using var lector = await comando.ExecuteReaderAsync();

            while (await lector.ReadAsync())
            {
                lista.Add(new()
                {
                    Id = lector.GetInt32(lector.GetOrdinal("Id")),
                    Nombre = lector.GetString(lector.GetOrdinal("Nombre")),
                    Email = lector.GetString(lector.GetOrdinal("Email")),
                    Contra = lector.GetString(lector.GetOrdinal("Contra")),
                    Rol = lector.GetInt32(lector.GetOrdinal("Rol"))
                });
            }

            return lista;
        }

        public async Task<bool> NombreExisteAsync(string nombre)
        {
            using var conexion = new SqlConnection(Conexion);

            await conexion.OpenAsync();

            using var comando = new SqlCommand("SELECT COUNT(*) FROM Usuarios WHERE Nombre = @Nombre", conexion);

            comando.Parameters.AddWithValue("@Nombre", nombre);

            var resultado = await comando.ExecuteScalarAsync();

            return Convert.ToInt32(resultado) > 0;
        }

        public async Task<bool> EmailExisteAsync(string email)
        {
            using var conexion = new SqlConnection(Conexion);

            await conexion.OpenAsync();

            using var comando = new SqlCommand("SELECT COUNT(*) FROM Usuarios WHERE Email = @Email", conexion);

            comando.Parameters.AddWithValue("@Email", email);

            var resultado = await comando.ExecuteScalarAsync();

            return Convert.ToInt32(resultado) > 0;
        }

        public async Task AgregarAsync(UsuarioModel usuario)
        {
            using var conexion = new SqlConnection(Conexion);

            await conexion.OpenAsync();

            using var comando = new SqlCommand("INSERT INTO Usuarios (Nombre, Email, Contra) VALUES (@Nombre, @Email, @Contra)", conexion);

            comando.Parameters.AddWithValue("@Nombre", usuario.Nombre);
            comando.Parameters.AddWithValue("@Email", usuario.Email);
            comando.Parameters.AddWithValue("@Contra", usuario.Contra);

            await comando.ExecuteNonQueryAsync();
        }

        public async Task<UsuarioValidation> ValidarCredencialesAsync(string nombreOEmail, string contra)
        {
            using var conexion = new SqlConnection(Conexion);

            await conexion.OpenAsync();

            using var comando = new SqlCommand("SELECT Id, Nombre, Email, Contra, Rol FROM Usuarios WHERE Nombre = @NombreOEmail OR Email = @NombreOEmail", conexion);

            comando.Parameters.AddWithValue("@NombreOEmail", nombreOEmail);

            using var lector = await comando.ExecuteReaderAsync();

            if (!await lector.ReadAsync())
            {
                return new()
                {
                    Usuario = null,
                    Existe = false
                };
            }

            var guardada = lector.GetString(lector.GetOrdinal("Contra"));

            if (contra != guardada)
            {
                return new()
                {
                    Usuario = null,
                    Existe = true
                };
            }

            return new()
            {
                Usuario = new()
                {

                    Id = lector.GetInt32(lector.GetOrdinal("Id")),
                    Nombre = lector.GetString(lector.GetOrdinal("Nombre")),
                    Email = lector.GetString(lector.GetOrdinal("Email")),
                    Contra = guardada,
                    Rol = lector.GetInt32(lector.GetOrdinal("Rol"))
                },
                Existe = true
            };
        }
    }
}
