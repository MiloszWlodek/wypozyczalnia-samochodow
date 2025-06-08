using CarRentalApp.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace CarRentalApp.Server.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opts) : base(opts) { }

        public DbSet<User> Users { get; set; } = default!;
    }
}
