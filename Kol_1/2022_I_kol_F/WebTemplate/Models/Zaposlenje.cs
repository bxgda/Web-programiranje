
namespace WebTemplate.Models;

public class Zaposlenje
{
    [Key]
    public int ID { get; set; }

    public DateTime DatumZaposljenja { get; set; }

    public uint Plata { get; set; }

    public string? Pozicija { get; set; }

    public Restoran? Restoran { get; set; }

    public Kuvar? Kuvar { get; set; }
}
