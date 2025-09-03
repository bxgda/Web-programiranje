namespace Akvarijum.Models;

public class RibaURezervoaru
{
    public int ID { get; set; }
    public required Rezervoar Rezervoar { get; set; }
    public required Riba Riba { get; set; }
    public DateTime DatumDodavanja { get; set; }
    public int BrojJedinki { get; set; }
    public double Masa { get; set; }
}
