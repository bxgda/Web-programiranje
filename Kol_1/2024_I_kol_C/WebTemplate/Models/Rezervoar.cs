namespace WebTemplate.Models
{
    public class Rezervoar
    {
        [Key]
        public int ID { get; set; }

        [StringLength(6, MinimumLength = 6, ErrorMessage = "sifra mora da ima 6 karaktera")]
        public required string Sifra { get; set; }

        public required double Zapremina { get; set; }

        public required double Temperatura { get; set; }

        public DateTime DatumPoslednjegCiscenja { get; set; }

        public int? FrekvencijaCiscenja { get; set; }

        public required int Kapacitet { get; set; }

        public int BrojTrenutnihRiba { get; set; } = 0;
    }
}