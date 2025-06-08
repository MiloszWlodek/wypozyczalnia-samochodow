namespace CarRentalApp.Server.Models
{
    public class Reservation
    {
        public string Id           { get; set; } = Guid.NewGuid().ToString();
        public string CarId        { get; set; } = "";
        public string UserEmail    { get; set; } = "";
        public DateTime ReservedAt { get; set; }
        public DateTime ReturnDue  { get; set; }
    }
}
