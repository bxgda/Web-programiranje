using System.ComponentModel.DataAnnotations;

namespace maj2021.Models
{
    public class Fabrika
    {
        [Key]
        public int FabrikaID { get; set; }

        public required string Naziv { get; set; }

        public List<Silos>? ListaSilosa { get; set; }

        public Fabrika()
        {
            ListaSilosa = [];
        }
    }
}