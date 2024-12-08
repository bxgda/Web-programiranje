namespace WebTemplate.Models;

public class Upis
{
    [Key]
    public int ID { get; set; }

    public DateTime DatumUpisa { get; set; }

    public int ESPB { get; set; }

    public Student? Student { get; set; }

    public Smer? Smer { get; set; }

    public Fakultet? Fakultet { get; set; }
}
