namespace WebTemplate.Models
{
    public class Iznajmljivanje
    {
        [Key]
        public int ID { get; set; }

        public DateTime DatumIznajmljivanja { get; set; }

        public int RokZaVracanje { get; set; }

        public bool Vraceno { get; set; } = false;

        public Knjiga? Knjiga { get; set; }

        public Korisnik? Korisnik { get; set; }
    }
}
