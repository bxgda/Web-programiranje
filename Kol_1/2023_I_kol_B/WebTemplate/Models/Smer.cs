namespace WebTemplate.Models;

public class Smer
{
    [Key]
    public int ID { get; set; }

    [MaxLength(50)]
    public required string Naziv { get; set; }

    public Fakultet? Fakultet { get; set; }
}
