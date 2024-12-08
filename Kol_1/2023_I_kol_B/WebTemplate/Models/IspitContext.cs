namespace WebTemplate.Models;

public class IspitContext : DbContext
{
    // DbSet kolekcije!
    public required DbSet<Fakultet> Fakulteti { get; set; }

    public required DbSet<Student> Studenti { get; set; }

    public required DbSet<Smer> Smerovi { get; set; }

    public required DbSet<Upis> Upisi { get; set; }

    public IspitContext(DbContextOptions options)
        : base(options) { }
}
