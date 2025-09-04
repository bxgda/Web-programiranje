namespace StovaristeServer.Models;

public static class DbInitializer
{
    public async static Task Seed(StovaristeContext context)
    {
        var stovariste1 = new Stovariste()
        {
            Naziv = "Stovarište Niš",
            Adresa = "Mramorska 4",
            BrojTelefona = "018222333"
        };

        var stovariste2 = new Stovariste()
        {
            Naziv = "Građevina",
            Adresa = "Matejevac BB",
            BrojTelefona = "018333444"
        };

        var materijal1 = new Materijal()
        {
            Naziv = "Cigla",
            Datum = DateTime.Now.AddDays(-100)
        };

        var materijal2 = new Materijal()
        {
            Naziv = "Cement",
            Datum = DateTime.Now.AddDays(-140)
        };

        var materijal3 = new Materijal()
        {
            Naziv = "Pesak",
            Datum = DateTime.Now.AddDays(-221)
        };

        var materijal4 = new Materijal()
        {
            Naziv = "Daska",
            Datum = DateTime.Now.AddDays(-411)
        };

        var mns1 = new Magacin()
        {
            Materijal = materijal1,
            Stovariste = stovariste1,
            Cena = 140,
            Kolicina = 21010
        };

        var mns2 = new Magacin()
        {
            Materijal = materijal2,
            Stovariste = stovariste1,
            Cena = 160,
            Kolicina = 10200
        };

        var mns3 = new Magacin()
        {
            Materijal = materijal3,
            Stovariste = stovariste1,
            Cena = 100,
            Kolicina = 51020
        };

        var mns4 = new Magacin()
        {
            Materijal = materijal4,
            Stovariste = stovariste1,
            Cena = 200,
            Kolicina = 34100
        };

        var mns5 = new Magacin()
        {
            Materijal = materijal2,
            Stovariste = stovariste2,
            Cena = 133,
            Kolicina = 42410
        };

        await context.Stovarista.AddAsync(stovariste1);
        await context.Stovarista.AddAsync(stovariste2);
        await context.Materijali.AddAsync(materijal1);
        await context.Materijali.AddAsync(materijal2);
        await context.Materijali.AddAsync(materijal3);
        await context.Materijali.AddAsync(materijal4);
        await context.Magacini.AddAsync(mns1);
        await context.Magacini.AddAsync(mns2);
        await context.Magacini.AddAsync(mns3);
        await context.Magacini.AddAsync(mns4);
        await context.Magacini.AddAsync(mns5);

        await context.SaveChangesAsync();
    }
}
