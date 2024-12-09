namespace WebTemplate.Models
{
    public class Album
    {
        [Key]
        public int ID { get; set; }

        [MaxLength(50)]
        public required string Naziv { get; set; }

        public int GodinaIzdavanja { get; set; }

        public required string IzdavackaKuca { get; set; }

        public Autor? AutorAlbuma { get; set; }

        public List<Numera>? NumereAlbuma { get; set; }
    }
}
