namespace WebTemplate.Models;

public class IspitContext : DbContext
{
    // DbSet kolekcije!
    public required DbSet<Grad> Gradovi { get; set; }

    public required DbSet<Voz> Vozovi { get; set; }

    public required DbSet<Relacija> Relacije { get; set; }

    public IspitContext(DbContextOptions options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Relacija>()
            .HasOne(r => r.PolazniGrad)
            .WithMany(r => r.PolazneRelacija);

        modelBuilder.Entity<Relacija>()
            .HasOne(r => r.DolazniGrad)
            .WithMany(r => r.DolazneRelacije);
    }
}
