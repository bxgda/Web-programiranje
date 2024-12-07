namespace WebTemplate.Models;

public class Ugovor
{
    [Key]
    public int ID { get; set; }

    public DateTime DatumPotpisivanja { get; set; }

    public int BrojUgovora { get; set; }

    public Autor? Autor { get; set; }

    [ForeignKey("KnjigaFK")]
    public Knjiga? Knjiga { get; set; }

    public IzdavackaKuca? IzdavackaKuca { get; set; }
}
