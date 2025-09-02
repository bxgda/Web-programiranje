namespace WebTemplate.Models
{
    [Table("Soba")]
    public class Soba
    {
        [Key]
        public int Id { get; set; }
        public int Kapacitet { get; set; }
        public string? Naziv { get; set; }

        // public Soba(string naziv, int kapacitet=5)
        // {
        //     this.Naziv = naziv;
        //     this.Kapacitet = kapacitet;
        // }
    }
}