namespace StovaristeServer.Models;

public class Magacin
{
    public int ID { get; set; }
    public required Materijal Materijal { get; set; }
    public required Stovariste Stovariste { get; set; }
    public double Cena { get; set; }
    public double Kolicina { get; set; }
}
