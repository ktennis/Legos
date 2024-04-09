using Microsoft.EntityFrameworkCore;

namespace Legos.Models
{
    public class LegosContext : DbContext
    {
        public LegosContext(DbContextOptions<LegosContext> options): base(options) { }

        public DbSet<Product> Products { get; set; }
    }
}
