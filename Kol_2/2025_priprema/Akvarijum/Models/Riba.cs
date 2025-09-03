namespace Akvarijum.Models;

public class Riba
{
    public int ID { get; set; }
    public required string Vrsta { get; set; }
    public double Masa { get; set; }
    public double Starost { get; set; }
    public List<RibaURezervoaru>? Rezervoari { get; set; }
}
