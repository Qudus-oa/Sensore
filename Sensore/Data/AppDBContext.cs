using Microsoft.EntityFrameworkCore;
using Sensore.Models;

namespace Sensore.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        // Add other tables if needed
        // public DbSet<Patient> Patients { get; set; }
        // public DbSet<Clinician> Clinicians { get; set; }
    }
}
