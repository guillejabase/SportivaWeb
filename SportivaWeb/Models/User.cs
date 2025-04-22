using System.ComponentModel.DataAnnotations;

namespace SportivaWeb.Models
{
    public class User
    {
        public int Id { get; set; }

        [StringLength(32)]
        public required string Email { get; set; }

        [StringLength(16)]
        public required string Password { get; set; }
    }
}
