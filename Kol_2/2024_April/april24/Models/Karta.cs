
namespace WebTemplate.Models;

[Table("Karta")]
public class Karta
{
    [Key]
    public int Id { get; set; }
    public Projekcija? Projekcija { get; set; }
    public int Red { get; set; }
    public int Sediste { get; set; }
    public double Cena { get; set; }
}