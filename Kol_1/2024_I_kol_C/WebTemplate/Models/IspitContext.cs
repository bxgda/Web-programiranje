namespace WebTemplate.Models;

public class IspitContext : DbContext
{
    // DbSet kolekcije!
    public required DbSet<Akvarijum> Akvarijumi { get; set; }

    public required DbSet<Rezervoar> Rezervoari { get; set; }

    public required DbSet<Riba> Ribe { get; set; }

    public IspitContext(DbContextOptions options) : base(options)
    {
        
    }
}
