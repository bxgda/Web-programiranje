namespace WebTemplate.Models;

public class IspitContext : DbContext
{
    // DbSet kolekcije!
    public required DbSet<Bolnica> Bolnice { get; set; }

    public required DbSet<Ugovor> Ugovori { get; set; }

    public required DbSet<Lekar> Lekari { get; set; }

    public IspitContext(DbContextOptions options)
        : base(options) { }
}
