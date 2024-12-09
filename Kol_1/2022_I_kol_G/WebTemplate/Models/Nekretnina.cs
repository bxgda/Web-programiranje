namespace WebTemplate.Models
{
    public class Nekretnina
    {
        [Key]
        public int ID { get; set; }

        public required string Tip { get; set; }

        public required double Vrednost { get; set; }

        public int BrojPrethnodnihVlasnika { get; set; } = 0;
    }
}
