using Microsoft.AspNetCore.Mvc;
using CarRentalApp.Server.Models;
using CarRentalApp.Server.Services;

namespace CarRentalApp.Server.Controllers
{
    [ApiController]
    [Route("api/notify")]
    public class NotificationsController : ControllerBase
    {
        private readonly EmailService _email;
        private readonly ReservationStore _store;

        public NotificationsController(EmailService email, ReservationStore store)
        {
            _email = email;
            _store = store;
        }

        [HttpPost("reservation")]
        public async Task<IActionResult> ReservationConfirmed(
            [FromQuery]string userEmail,
            [FromQuery]string carId,
            [FromQuery]DateTime? returnDue)
        {
            var res = new Reservation {
                CarId       = carId,
                UserEmail   = userEmail,
                ReservedAt  = DateTime.UtcNow,
                ReturnDue   = returnDue ?? DateTime.UtcNow.AddDays(1)
            };

            _store.Reservations.Add(res);

            var html = $"<p>Twoja rezerwacja auta <strong>{carId}</strong> została potwierdzona.<br/>Zwróć do: {res.ReturnDue:yyyy-MM-dd}.</p>";
            await _email.SendAsync(userEmail, "Potwierdzenie rezerwacji", html);

            return CreatedAtAction(null, new { id = res.Id }, res);
        }

        [HttpPost("reminder")]
        public async Task<IActionResult> Reminder(
            [FromQuery]string userEmail,
            [FromQuery]string carId,
            [FromQuery]string returnDate)
        {
            var html = $"<p>Przypomnienie: jutro ({returnDate}) mija termin zwrotu auta <strong>{carId}</strong>.</p>";
            await _email.SendAsync(userEmail, "Przypomnienie o zwrocie", html);
            return Ok();
        }
    }
}