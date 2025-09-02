using System.ComponentModel.DataAnnotations;

namespace rent_a_car.Models
{
    public class Iznajmljivanje
    {
        [Key]
        public int IznajmljivanjeID { get; set; }

        public required int BrojDana { get; set; }

        public Korisnik? Korisnik { get; set; }

        public Automobil? Automobil { get; set; }
    }
}