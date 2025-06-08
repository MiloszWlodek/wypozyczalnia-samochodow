using System.ComponentModel.DataAnnotations;

namespace CarRentalApp.Server.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required, MinLength(3)]
        public string Username { get; set; } = default!;

        [Required, EmailAddress]
        public string Email { get; set; } = default!;

        [Required]
        public string PasswordHash { get; set; } = default!;

        public int LoyaltyPoints { get; set; }
    }
}
