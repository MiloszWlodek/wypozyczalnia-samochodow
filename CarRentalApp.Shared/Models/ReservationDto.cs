namespace CarRentalApp.Shared.Models
{
    public class ReservationDto
    {
        public int Id               { get; set; }
        public int UserId           { get; set; }
        public int CarId            { get; set; }
        public DateTime StartDate   { get; set; }
        public DateTime EndDate     { get; set; }
        public bool UseLoyaltyPoints { get; set; }
        public string CarMakeModel  { get; set; } = default!;
    }
}
