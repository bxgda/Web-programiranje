namespace WebTemplate.Models
{
    public class Ustanova
    {
        [Key]
        public int ID { get; set; }

        public required string Naziv { get; set; }

        public required string Adresa { get; set; }

        public string? BrojTelefona { get; set; }

        public string? EmailAdresa { get; set; }
    }
}
