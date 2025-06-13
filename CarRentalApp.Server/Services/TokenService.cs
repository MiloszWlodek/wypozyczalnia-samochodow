using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace CarRentalApp.Server.Services
{
    public class TokenService
    {
        private const string SecretKey = "tutaj_znacznie_trudniejszy_i_dłuższy_klucz";
        private static readonly SymmetricSecurityKey SigningKey =
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));

        public string GenerateToken(string username)
        {
            var claims = new[] {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, "User")
            };

            var creds = new SigningCredentials(SigningKey, SecurityAlgorithms.HmacSha256);

            var jwt = new JwtSecurityToken(
                issuer: "CarRentalApp",
                audience: "CarRentalApp",
                claims: claims,
                expires: DateTime.UtcNow.AddHours(3),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }

        public static TokenValidationParameters GetValidationParameters()
        {
            return new TokenValidationParameters
            {
                ValidateIssuer           = true,
                ValidIssuer              = "CarRentalApp",
                ValidateAudience         = true,
                ValidAudience            = "CarRentalApp",
                ValidateIssuerSigningKey = true,
                IssuerSigningKey         = SigningKey,
                ValidateLifetime         = true,
                ClockSkew                = TimeSpan.Zero
            };
        }
    }
}
