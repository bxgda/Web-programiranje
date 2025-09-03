using System.Text.Json.Serialization;

public class Racun
{
    private const int OsnovicaZaStruju = 150;
    private const int OsnovicaZaKomunalije = 100;

    [Key]
    public int ID { get; set; }

    public required Stan Stan { get; set; }

    [Range(1,12)]
    public int Mesec { get; set; }

    public bool Placen { get; set; }

    public int CenaVode { get; set; }

    public int CenaZajednickeStruje
    {
        get =>
            Stan.BrojClanova * OsnovicaZaStruju;
    }
    public int CenaKomunalija {
        get =>
            Stan.BrojClanova * OsnovicaZaKomunalije;
    }
}