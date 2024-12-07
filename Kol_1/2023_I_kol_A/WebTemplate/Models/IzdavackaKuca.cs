using System.Text.Json.Serialization;

namespace WebTemplate.Models;

public class IzdavackaKuca
{
    [Key]
    public int ID { get; set; }

    public required string Naziv { get; set; }

    public string? Drzava { get; set; }

    [JsonIgnore]
    public List<Ugovor>? Ugovori { get; set; }
}
