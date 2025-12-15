namespace Sensore.Data
{

    using Microsoft.EntityFrameworkCore;
    using Sensore.Models;

    public class AppDBContext:DbContext
    {
       public AppDBContext (DbContextOptions<AppDBContext> options) : base(options)
        {

        }
        public DbSet<User> users { get; set; }
    }
}
