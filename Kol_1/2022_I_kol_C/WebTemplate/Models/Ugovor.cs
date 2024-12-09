namespace WebTemplate.Models
{
    public class Ugovor
    {
        [Key]
        public int ID { get; set; }

        public required int IDBroj { get; set; }

        [DataType(DataType.Date)]
        public DateTime DatumPotpisivanja { get; set; }

        public required string Specijalnost { get; set; }

        public Bolnica? P_Bolnica { get; set; }

        public Lekar? P_Lekar { get; set; }
    }
}
