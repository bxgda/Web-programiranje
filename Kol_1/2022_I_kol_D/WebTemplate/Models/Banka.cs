using System.Text.Json.Serialization;

namespace WebTemplate.Models
{
    public class Banka
    {
        [Key]
        public int ID { get; set; }

        [MaxLength(13)]
        public required string Naziv { get; set; }

        public string? Lokacija { get; set; }

        public string? BrojTelefona { get; set; }

        public int BrojZaposlenih { get; set; }

        [JsonIgnore]
        public List<Racun>? Racuni { get; set; }
    }
}