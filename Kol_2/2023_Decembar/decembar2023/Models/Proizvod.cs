using System.ComponentModel.DataAnnotations;

namespace decembar2023.Models
{
    public class Proizvod
    {
        [Key]
        public int ProizodID { get; set; }

        public required string Naziv { get; set; }

        public required string Kategorija { get; set; }

        public required int Cena { get; set; }

        public required int DostupnaKolicina { get; set; }
    }
}