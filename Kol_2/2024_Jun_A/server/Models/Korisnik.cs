using System.ComponentModel.DataAnnotations;

namespace rent_a_car.Models
{
    public class Korisnik
    {
        [Key]
        public int KorisnikID { get; set; }

        public required string Ime { get; set; }

        public required string Prezime { get; set; }

        [MaxLength(13)]
        public required string JMBG { get; set; }

        [MaxLength(9)]
        public required string BrojVozackeDozvole { get; set; }

        public List<Iznajmljivanje>? UgovorIznajmljivanja { get; set; }

        public Korisnik()
        {
            UgovorIznajmljivanja = [];
        }
    }
}