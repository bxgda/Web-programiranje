namespace WebTemplate.Models
{
    [Table("Chat")]
    public class Chat
    {
        [Key]
        public int Id { get; set; }
        public Soba Soba { get; set; }
        public Korisnik Korisnici { get; set; }
        public string? Nadimak { get; set; }
        public string? Boja { get; set; }
    }
}