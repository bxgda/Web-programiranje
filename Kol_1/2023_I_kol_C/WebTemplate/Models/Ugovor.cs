namespace WebTemplate.Models
{
    public class Ugovor
    {
        [Key]
        public int ID { get; set; }

        public required int BrojUgovora { get; set; }

        public DateTime DatumPotpisivanja { get; set; }

        public required Zaposlen ZaposlenRadnik { get; set; }

        public required Ustanova UstanovaZaposljava { get; set; }
    }
}
