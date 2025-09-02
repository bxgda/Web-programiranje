namespace WebTemplate.Models;

public class ChatContext : DbContext
{
    public DbSet<Korisnik> Korisnici { get; set; }
    public DbSet<Soba> Sobe { get; set; }
    public DbSet<Chat> Chatovi { get; set; }

    public ChatContext(DbContextOptions options) : base(options)
    {

    }
}
