namespace WebTemplate.Models;

public class IspitContext : DbContext
{
    // DbSet kolekcije!
    public required DbSet<Ugovor> Ugovori { get; set; }

    public required DbSet<Zaposlen> Zaposleni { get; set; }

    public required DbSet<Ustanova> Ustanove { get; set; }

    public IspitContext(DbContextOptions options)
        : base(options) { }
}
