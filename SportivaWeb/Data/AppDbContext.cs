using Microsoft.EntityFrameworkCore;
using SportivaWeb.Models;

namespace SportivaWeb.Data
{
    public class AppDbContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasData
                ([
                    new() { Id = 1, Email = "admin@gmail.com", Password = "admin" }
                ]);

            base.OnModelCreating(modelBuilder);
        }
    }
}
