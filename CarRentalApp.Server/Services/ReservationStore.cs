using CarRentalApp.Server.Models;

namespace CarRentalApp.Server.Services
{
    public class ReservationStore
    {
        public List<Reservation> Reservations { get; } = new List<Reservation>();
    }
}
