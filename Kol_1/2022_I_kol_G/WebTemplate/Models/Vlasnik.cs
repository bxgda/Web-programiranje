namespace WebTemplate.Models
{
    public class Vlasnik
    {
        [Key]
        public int ID { get; set; }

        [MaxLength(50)]
        public required string Ime { get; set; }

        [MaxLength(50)]
        public required string Prezime { get; set; }

        public DateTime DatumRodjenja { get; set; }

        public string? MestoRodjenja { get; set; }
    }
}
