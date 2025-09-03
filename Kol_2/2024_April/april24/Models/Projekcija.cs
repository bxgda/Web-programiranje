
namespace WebTemplate.Models;

[Table("Projekcija")]
public class Projekcija
{
    [Key]
    public int Id { get; set; }
    public string? Naziv { get; set; }
    public DateTime VremeProjekcije { get; set; }
    public Sala? Sala { get; set; }
    public int Sifra { get; set; }
    public double Cena { get; set; }
    public List<Karta>? Karte { get; set; }
}