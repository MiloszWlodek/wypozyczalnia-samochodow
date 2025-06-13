using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using CarRentalApp.Server.Data;
using CarRentalApp.Server.Services;
using BCrypt.Net;
using SharedUser = CarRentalApp.Shared.Models.User;
using EntityUser = CarRentalApp.Server.Models.User;
using CarRentalApp.Shared.Models;


namespace CarRentalApp.Server.Controllers
{
    [ApiController]
    [Route("api/auth")]
    [AllowAnonymous]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly TokenService _tokenService;

        public AuthController(AppDbContext db, TokenService tokenService)
        {
            _db = db;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDto dto)
        {
            if (await _db.Users.AnyAsync(u => u.Username == dto.Username))
                return BadRequest("Login zajęty.");

            var userEntity = new EntityUser
            {
                Username = dto.Username,
                Email = dto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                LoyaltyPoints = 0
            };

            _db.Users.Add(userEntity);
            await _db.SaveChangesAsync();
            return Ok(new { message = "Rejestracja udana." });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto dto)
        {
            var userEntity = await _db.Users
                .SingleOrDefaultAsync(u => u.Username == dto.Username);

            if (userEntity == null ||
                !BCrypt.Net.BCrypt.Verify(dto.Password, userEntity.PasswordHash))
            {
                return Unauthorized("Błędny login lub hasło.");
            }

            var token = _tokenService.GenerateToken(userEntity.Username);

            var userDto = new SharedUser
            {
                Id = userEntity.Id,
                Username = userEntity.Username,
                Email = userEntity.Email,
                LoyaltyPoints = userEntity.LoyaltyPoints
            };

            return Ok(new { token, user = userDto });
        }
    }
}