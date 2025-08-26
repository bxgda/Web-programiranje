using Microsoft.EntityFrameworkCore;

namespace maj2021.Models
{
    public class Context : DbContext
    {
        public Context(DbContextOptions options) : base(options) { }

        public required DbSet<Silos> Silosi { get; set; }

        public required DbSet<Fabrika> Fabrike { get; set; }
    }
}