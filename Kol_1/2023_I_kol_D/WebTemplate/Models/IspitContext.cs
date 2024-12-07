namespace WebTemplate.Models;

public class IspitContext : DbContext
{
    // DbSet kolekcije!
    public required DbSet<Proizvod> Proizvodi { get; set; }

    public required DbSet<Prodavnica> Prodavnice { get; set; }

    public required DbSet<Magacin> Magacin { get; set; }

    public IspitContext(DbContextOptions options)
        : base(options) { }
}
