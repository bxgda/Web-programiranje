namespace WebTemplate.Models
{
    public class Kupovina
    {
        [Key]
        public int ID { get; set; }

        public required int BrojUgovora { get; set; }

        public DateTime DatumKupovime { get; set; }

        public double? IsplacenaVrednost { get; set; }

        public Nekretnina? KupljenaNekretnina { get; set; }

        public Vlasnik? KoJeKupio { get; set; }
    }
}
