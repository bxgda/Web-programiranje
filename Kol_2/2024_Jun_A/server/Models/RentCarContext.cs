using Microsoft.EntityFrameworkCore;

namespace rent_a_car.Models
{
    public class RentCarContext : DbContext
    {
        public RentCarContext(DbContextOptions option) : base(option) { }

        public required DbSet<Korisnik> Korisnici { get; set; }

        public required DbSet<Automobil> Automobili { get; set; }

        public required DbSet<Iznajmljivanje> UgovoriIznajmljivanja { get; set; }
    }
}