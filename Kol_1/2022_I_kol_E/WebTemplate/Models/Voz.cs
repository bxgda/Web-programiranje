using System.Text.Json.Serialization;

namespace WebTemplate.Models;

public class Voz
{
    [Key]
    public int ID { get; set; }

    public required string Naziv { get; set; }

    public uint MaksimalniKapacitet { get; set; }

    public DateTime DatumProizvodnje { get; set; }

    public uint MaksimalnaBrzina { get; set; }

    public uint MaksimalnaTezina { get; set; }

    [JsonIgnore]
    public List<Relacija>? Relacije { get; set; }
}