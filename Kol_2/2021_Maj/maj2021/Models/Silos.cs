using System.ComponentModel.DataAnnotations;

namespace maj2021.Models
{
    public class Silos
    {
        [Key]
        public int SilosID { get; set; }

        public required string Oznaka { get; set; }

        public required int MaxKapacitet { get; set; }

        public required int TrenutnaKolicina { get; set; }
    }
}