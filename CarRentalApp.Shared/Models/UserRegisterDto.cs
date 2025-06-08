using System.ComponentModel.DataAnnotations;

namespace CarRentalApp.Shared.Models
{
    public class UserRegisterDto
    {
        [Required, MinLength(3)]
        public string Username { get; set; } = default!;

        [Required, EmailAddress]
        public string Email { get; set; } = default!;

        [Required, MinLength(6)]
        public string Password { get; set; } = default!;
    }
}
