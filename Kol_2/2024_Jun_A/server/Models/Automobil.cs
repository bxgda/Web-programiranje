using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace rent_a_car.Models
{
    public class Automobil
    {
        [Key]
        public int AutomobilID { get; set; }

        public required string Model { get; set; }

        public required int PredjenaKilometraza { get; set; }

        public required int Godiste { get; set; }

        public required int CenaPoDanu { get; set; }

        public required bool Iznajmljen { get; set; } = false;

        public List<Iznajmljivanje>? UgovorIznajmljivanja { get; set; }

        public Automobil()
        {
            UgovorIznajmljivanja = [];
        }
    }
}