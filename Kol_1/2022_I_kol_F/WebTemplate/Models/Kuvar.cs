using System.Text.Json.Serialization;

namespace WebTemplate.Models;

public class Kuvar
{
    [Key]
    public int ID { get; set; }

    [MaxLength(25)]
    public required string Ime { get; set; }

    [MaxLength(25)]
    public required string Prezime { get; set; }

    public DateTime DatumRodjenja { get; set; }

    public string? StrucnaSprema { get; set; }
    [JsonIgnore]
    public List<Zaposlenje>? Zaposlenja { get; set; }
}