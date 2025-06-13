namespace CarRentalApp.Shared.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = default!;
        public string Email { get; set; } = default!;
        public int LoyaltyPoints { get; set; }
    }
}
