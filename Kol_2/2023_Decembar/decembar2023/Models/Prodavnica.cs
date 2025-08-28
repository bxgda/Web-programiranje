using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace decembar2023.Models
{
    public class Prodavnica
    {
        [Key]
        [JsonIgnore]
        public int ProdavnicaID { get; set; }

        public required string Naziv { get; set; }

        public required string Lokacija { get; set; }

        [MaxLength(9)]
        public required string Telefon { get; set; }

        [JsonIgnore]
        public static int MaxKolicina = 100;

        public List<Proizvod>? ListaProizvoda { get; set; }

        public Prodavnica()
        {
            ListaProizvoda = [];
        }
    }
}