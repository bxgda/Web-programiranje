namespace WebTemplate.Models;

using System.ComponentModel.DataAnnotations;

public class Knjiga
{
    [Key]
    public int ID { get; set; }

    [MaxLength(50)]
    public required string Naziv { get; set; }

    public DateTime DatumObjavljivanja { get; set; }

    public int BrojStranica { get; set; }

    public string? Zanr { get; set; }

    [StringLength(13, MinimumLength = 13, ErrorMessage = "ISBN mora da ima tacno 13 karaktera")]
    public required string ISBN { get; set; }

    public Autor? Autor { get; set; }

    public Ugovor? Ugovor { get; set; }
}
