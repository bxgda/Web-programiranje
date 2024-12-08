namespace WebTemplate.Models;

public class IspitContext : DbContext
{
    // DbSet kolekcije!
    public required DbSet<Kuvar> Kuvari { get; set; }

    public required DbSet<Restoran> Restorani { get; set; }

    public required DbSet<Zaposlenje> Zaposlenja { get; set; }

    public IspitContext(DbContextOptions options) : base(options)
    {

    }
}
