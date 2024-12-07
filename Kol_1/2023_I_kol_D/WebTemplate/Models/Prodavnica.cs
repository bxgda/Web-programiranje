namespace WebTemplate.Models;

public class Prodavnica
{
    [Key]
    public int ID { get; set; }

    [MaxLength(50)]
    public required string Naziv { get; set; }

    [MaxLength(50)]
    public required string Adresa { get; set; }

    [MaxLength(11)]
    public string? BrojTelefona { get; set; }

    public string? ImeZaduzenogLica { get; set; }

    public List<Magacin>? Magacin { get; set; }
}
