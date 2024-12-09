namespace WebTemplate.Models
{
    public class Numera
    {
        [Key]
        public int ID { get; set; }

        [MaxLength(50)]
        public required string Naziv { get; set; }

        public double? Duzina { get; set; }

        public string? Zanr { get; set; }

        public int BrojUmetnika { get; set; }

        public Album? Album { get; set; }
    }
}
