namespace Akvarijum.Models;

public static class DbInitializer
{
    public static void Seed(AkvarijumContext context)
    {
        var rezervoar1 = new Rezervoar()
        {
            Sifra = "RV001",
            FrekvencijaCiscenja = 10,
            Kapacitet = 100,
            PoslednjeCiscenje = DateTime.Now.AddDays(-20),
            Temperatura = 20
        };

        var rezervoar2 = new Rezervoar()
        {
            Sifra = "RV002",
            FrekvencijaCiscenja = 100,
            Kapacitet = 1000,
            PoslednjeCiscenje = DateTime.Now.AddDays(-50),
            Temperatura = 23
        };

        context.Rezervoari.Add(rezervoar1);
        context.Rezervoari.Add(rezervoar2);

        var guppy = new Riba()
        {
            Vrsta = "Guppy",
            Masa = 0.3,
            Starost = 1
        };

        var angle = new Riba()
        {
            Vrsta = "Anglefish",
            Masa = 4.5,
            Starost = 0.5
        };

        var zebra = new Riba()
        {
            Vrsta = "Zebra Danio",
            Masa = 0.5,
            Starost = 2
        };
        var bettas = new Riba()
        {
            Vrsta = "Bettas",
            Masa = 3,
            Starost = 1
        };

        context.Ribe.Add(guppy);
        context.Ribe.Add(angle);
        context.Ribe.Add(zebra);
        context.Ribe.Add(bettas);

        context.RibeURezervoarima.Add(new RibaURezervoaru()
        {
            Rezervoar = rezervoar1,
            Riba = guppy,
            BrojJedinki = 4,
            DatumDodavanja = DateTime.Now.AddDays(-34),
            Masa = 0.5
        });

        context.RibeURezervoarima.Add(new RibaURezervoaru()
        {
            Rezervoar = rezervoar1,
            Riba = angle,
            BrojJedinki = 6,
            DatumDodavanja = DateTime.Now.AddDays(-55),
            Masa = 1.5
        });

        context.RibeURezervoarima.Add(new RibaURezervoaru()
        {
            Rezervoar = rezervoar1,
            Riba = zebra,
            BrojJedinki = 12,
            DatumDodavanja = DateTime.Now.AddDays(-62),
            Masa = 0.5
        });

        context.RibeURezervoarima.Add(new RibaURezervoaru()
        {
            Rezervoar = rezervoar1,
            Riba = bettas,
            BrojJedinki = 2,
            DatumDodavanja = DateTime.Now.AddDays(-62),
            Masa = 3
        });

        context.SaveChanges();
    }
}
