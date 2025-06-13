namespace CarRentalApp.Models
{
    public class Car
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Make { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public string LicensePlate { get; set; } = string.Empty;
        public bool IsAvailable { get; set; } = true;
        public string? ReservedByUserId { get; set; }
    }
}
