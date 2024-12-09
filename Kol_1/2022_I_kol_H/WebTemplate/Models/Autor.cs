namespace WebTemplate.Models
{
    public class Autor
    {
        [Key]
        public int ID { get; set; }

        public required string Ime { get; set; }

        public required string Prezime { get; set; }

        // [DataType(DataType.Date)]
        public DateTime DatumRodjenja { get; set; }

        public DateTime? DatumPrvogAlbuma { get; set; } // moze da bude null, tj na pocetku je null dok ne izda bar jedan album

        public List<Album>? IzdatiAlbumi { get; set; }
    }
}
