using System.Text.Json.Serialization;

namespace WebTemplate.Models;

public class Stan
{
    [Key]
    public int ID { get; set; }

    [MaxLength(50)]
    public required string Vlasnik { get; set; }

    [Range(0,1000)]
    public int Povrsina { get; set; }

    [Range(0,20)]
    public int BrojClanova { get; set; }

    [JsonIgnore]
    public List<Racun> Racuni { get; set; } = new List<Racun>();
}