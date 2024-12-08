namespace WebTemplate.Models
{
    public class Racun
    {
        [Key]
        public int ID { get; set; }

        public int BrojRacuna { get; set; }

        public DateTime DatumOtvaranja { get; set; }

        public double Sredstva { get; set; }

        public double KolicinaPodignutogNovca { get; set; }

        public Banka? Banka { get; set; }

        public Klijent? Klijent { get; set; }
    }
}