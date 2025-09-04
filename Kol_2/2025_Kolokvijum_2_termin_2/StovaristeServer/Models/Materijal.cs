namespace StovaristeServer.Models;

public class Materijal
{
    public int ID { get; set; }
    public required string Naziv { get; set; }
    public DateTime Datum { get; set; }
    public List<Magacin>? MagaciniSaMaterijalom { get; set; }
}
