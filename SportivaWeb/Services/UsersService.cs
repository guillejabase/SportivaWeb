using Microsoft.EntityFrameworkCore;
using SportivaWeb.Data;
using SportivaWeb.DTOs;

namespace SportivaWeb.Services
{
    public class UsersService(AppDbContext context)
    {
        public async Task<bool> EmailExistsAsync(string email)
        {
            return await context.Users.AnyAsync(u => u.Email == email);
        }

        public async Task<(bool Success, string? Error)> AddAsync(UserDTO userData)
        {
            try
            {
                if (await EmailExistsAsync(userData.Email!))
                {
                    return (false, "El correo ya está registrado.");
                }

                context.Users.Add(new()
                {
                    Email = userData.Email!,
                    Password = userData.Password!
                });

                await context.SaveChangesAsync();
                return (true, null);
            }
            catch
            {
                return (false, "Error al registrar el usuario.");
            }
        }
    }
}
