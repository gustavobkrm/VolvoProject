using Microsoft.EntityFrameworkCore;
using VolvoProject.Models;

namespace VolvoProject.Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Vehicle> Vehicles { get; set; }

    }
}
