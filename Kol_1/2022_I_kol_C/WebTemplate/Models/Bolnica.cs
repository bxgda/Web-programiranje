namespace WebTemplate.Models
{
    public class Bolnica
    {
        [Key]
        public int ID { get; set; }

        [MaxLength(50)]
        public required string Naziv { get; set; }

        public string? Lokacija { get; set; }

        public required int BrojOdeljenja { get; set; }

        public int BrojOsoblja { get; set; } = 0;

        public string? BrojTelefona { get; set; }
    }
}
