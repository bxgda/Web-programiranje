namespace Akvarijum.Controllers;

[ApiController]
[Route("[controller]")]
public class AkvarijumController : ControllerBase
{
    public AkvarijumContext Context { get; set; }

    public AkvarijumController(AkvarijumContext context)
    {
        Context = context;
    }

    [HttpGet("PreuzmiRezervoare")]
    public async Task<IActionResult> PreuzmiRezervoare()
    {
        return Ok(await Context.Rezervoari.Include(p => p.Ribe)!.ThenInclude(p => p.Riba)
            .Select(p => new
            {
                p.ID,
                p.Kapacitet,
                p.Sifra,
                Ribe = p.Ribe!.Select(p => new
                {
                    NazivRibe = p.Riba.Vrsta,
                    IDSpoja = p.Riba.ID,
                    BrojKomada = p.BrojJedinki,
                    Masa = p.Masa
                }).ToList()
            })
            .ToListAsync());
    }

    [HttpGet("PreuzmiSveRibe")]
    public async Task<IActionResult> PreuzmiSveRibe()
    {
        var ribe = await Context.Ribe.Select(p => new
        {
            p.ID,
            p.Vrsta
        }).ToListAsync();
        return Ok(ribe);
    }

    // 1. Dodavanje rezervoara i ribe u bazu podataka (različite metode).
    [HttpPost("DodavanjeRezervoara")]
    public async Task<IActionResult> DodavanjeRezervoara([FromBody] Rezervoar rezervoar)
    {
        await Context.Rezervoari.AddAsync(rezervoar);
        await Context.SaveChangesAsync();
        return Ok(rezervoar);
    }

    [HttpPost("DodavanjeRibe")]
    public async Task<IActionResult> DodavanjeRibe([FromBody] Riba riba)
    {
        await Context.Ribe.AddAsync(riba);
        await Context.SaveChangesAsync();
        return Ok(riba);
    }

    // 2. Dodavanje ribe u neki rezervoar.
    [HttpPost("DodavanjeRibeURezervoar/{rezervoarID}/{ribaID}/{brojJedinki}/{datum}/{masa}")]
    public async Task<IActionResult> DodavanjeRibeURezervoar(int rezervoarID, int ribaID, int brojJedinki, DateTime datum, double masa)
    {
        var rezervoar = await Context.Rezervoari.FindAsync(rezervoarID);
        var riba = await Context.Ribe.FindAsync(ribaID);

        var rezSaRibama = await Context.Rezervoari.Include(p => p.Ribe).Where(p => p.ID == rezervoarID).FirstOrDefaultAsync();

        if (rezervoar == null || rezSaRibama == null || rezSaRibama.Ribe == null)
        {
            return BadRequest("Ne postoji rezervoar.");
        }

        if (rezSaRibama.Ribe.Count > 0)
        {
            double masaMin = rezSaRibama.Ribe.Min(p => p.Masa);
            double masaMax = rezSaRibama.Ribe.Max(p => p.Masa);
            int brojJedinkiURez = rezSaRibama.Ribe.Sum(p => p.BrojJedinki);

            if (masa * 10 <= masaMax || masa >= masaMin * 10 || brojJedinki + brojJedinkiURez > rezervoar.Kapacitet)
            {
                return BadRequest("Nemoguće dodati ribu zbog kapaciteta ili mase.");
            }
        }

        if (rezervoar == null || riba == null)
        {
            return BadRequest("Nije pronađen rezervoar ili riba.");
        }

        var rur = new RibaURezervoaru()
        {
            Riba = riba,
            Rezervoar = rezervoar,
            BrojJedinki = brojJedinki,
            DatumDodavanja = datum,
            Masa = masa
        };
        await Context.RibeURezervoarima.AddAsync(rur);
        await Context.SaveChangesAsync();
        return Ok(new
        {
            rur.ID,
            NazivRibe = rur.Riba.Vrsta,
            BrojKomada = rur.BrojJedinki,
            rur.Masa
        });
    }

    // 3. Ažuriranje postojećih jedinki u neki akvarijum, uz izmenu datuma dodavanja.
    [HttpPut("IzmenaJedinke/{ribaURezID}/{broj}/{masa}/{datum}")]
    public async Task<IActionResult> IzmenaJedinke(int ribaURezID, int broj, double masa, DateTime datum)
    {
        var ribaURez = await Context.RibeURezervoarima.FindAsync(ribaURezID);

        if (ribaURez == null)
        {
            return BadRequest("Nemoguće pronaći ribu u rezervoaru!");
        }

        ribaURez.BrojJedinki = broj;
        ribaURez.DatumDodavanja = datum;
        ribaURez.Masa = masa;

        Context.RibeURezervoarima.Update(ribaURez);
        await Context.SaveChangesAsync();

        return Ok(ribaURez);
    }

    // 4. Pronalaženje svih rezervoara koje treba očistiti.
    [HttpGet("PronadjiRezervoareZaCiscenje")]
    public async Task<IActionResult> PronadjiRezervoareZaCiscenje()
    {
        var zaCiscenje = await Context.Rezervoari
            .Where(p => p.PoslednjeCiscenje.AddDays(p.FrekvencijaCiscenja) < DateTime.Now)
            .ToListAsync();
        return Ok(zaCiscenje);
    }

    // 5. Pronalaženje ukupne mase riba u zadatom rezervoaru.
    [HttpGet("MasaRiba/{idRezervoara}")]
    public async Task<IActionResult> MasaRiba(int idRezervoara)
    {
        var rezervoar = await Context.Rezervoari
            .Include(p => p.Ribe)!
            .ThenInclude(p => p.Riba)
            .Where(p => p.ID == idRezervoara)
            .FirstOrDefaultAsync();

        double masa = 0;

        if (rezervoar != null && rezervoar.Ribe != null)
        {
            masa = rezervoar.Ribe.Sum(p => p.Riba.Masa);
        }

        return Ok(masa);
    }
}
