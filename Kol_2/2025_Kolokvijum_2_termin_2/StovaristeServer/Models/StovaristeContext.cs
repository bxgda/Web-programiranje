namespace StovaristeServer.Models;

public class StovaristeContext : DbContext
{
    public DbSet<Stovariste> Stovarista { get; set; }
    public DbSet<Magacin> Magacini { get; set; }
    public DbSet<Materijal> Materijali { get; set; }

    public StovaristeContext(DbContextOptions options) : base(options)
    {

    }
}
