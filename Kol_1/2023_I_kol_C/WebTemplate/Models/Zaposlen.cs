namespace WebTemplate.Models
{
    public class Zaposlen
    {
        [Key]
        public int ID { get; set; }

        [StringLength(13, MinimumLength = 13, ErrorMessage = "Mat broj mora da ima 13 cifre")]
        public required string MaticniBroj { get; set; }

        [MaxLength(30)]
        public required string Ime { get; set; }

        [MaxLength(30)]
        public required string Prezime { get; set; }

        public DateTime DatumRodjenja { get; set; }

        public string? BrojTelefona { get; set; }

        public int BrojUstanovaUKojeRadi { get; set; } = 0;
    }
}
