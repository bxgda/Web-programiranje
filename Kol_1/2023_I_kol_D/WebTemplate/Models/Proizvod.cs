namespace WebTemplate.Models;

public class Proizvod
{
    [Key]
    public int ID { get; set; }

    [MaxLength(50)]
    public required string Naziv { get; set; }

    [MaxLength(13)]
    [MinLength(13)]
    public required string Identifikator { get; set; }

    public DateTime DatumProizvodnje { get; set; }

    public DateTime DatumIstekaRoka { get; set; }

    public List<Magacin>? Magacin { get; set; }
}
