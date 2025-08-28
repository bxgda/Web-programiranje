using Microsoft.EntityFrameworkCore;

namespace decembar2023.Models
{
    public class Context : DbContext
    {
        public Context(DbContextOptions options) : base(options) { }

        public required DbSet<Prodavnica> Prodavnice { get; set; }

        public required DbSet<Proizvod> Proizvodi { get; set; }
    }
}