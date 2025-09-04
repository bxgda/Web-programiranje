namespace StovaristeServer.Models;

public class Stovariste
{
    public int ID { get; set; }
    public required string Naziv { get; set; }
    public required string Adresa { get; set; }
    public required string BrojTelefona { get; set; }
    public List<Magacin>? MaterijaliUMagacinu { get; set; }
}
