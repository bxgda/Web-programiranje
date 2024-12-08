namespace WebTemplate.Models;

public class Restoran
{
    [Key]
    public int ID { get; set; }

    [MaxLength(50)]
    public required string Naziv { get; set; }

    public int MaksBrojGostiju { get; set; }

    public int MaksBrojKuvara { get; set; }

    public string? BrojTelefona { get; set; }

    public List<Zaposlenje>? Zaposlenja { get; set; }
}