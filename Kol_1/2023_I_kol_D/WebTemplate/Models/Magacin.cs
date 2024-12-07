using System.Text.Json.Serialization;

namespace WebTemplate.Models;

public class Magacin
{
    [Key]
    public int ID { get; set; }

    public double Cena { get; set; }

    public int BrojDostupnih { get; set; }

    [JsonIgnore]
    public Prodavnica? Prodavnica { get; set; }

    [JsonIgnore]
    public Proizvod? Proizvod { get; set; }
}
