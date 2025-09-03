namespace Akvarijum.Models;

public class AkvarijumContext : DbContext
{
    public DbSet<Rezervoar> Rezervoari { get; set; }
    public DbSet<Riba> Ribe { get; set; }
    public DbSet<RibaURezervoaru> RibeURezervoarima { get; set; }

    public AkvarijumContext(DbContextOptions options) : base(options)
    {
        
    }
}
