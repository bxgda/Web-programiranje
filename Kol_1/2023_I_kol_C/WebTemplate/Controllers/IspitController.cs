namespace WebTemplate.Controllers;

[ApiController]
[Route("[controller]")]
public class IspitController : ControllerBase
{
    public IspitContext Context { get; set; }

    public IspitController(IspitContext context)
    {
        Context = context;
    }

    [HttpPost("DodajZaposlenog")]
    public async Task<IActionResult> DodajZaposlenog([FromBody] Zaposlen zaposlen)
    {
        try
        {
            await Context.Zaposleni.AddAsync(zaposlen);
            await Context.SaveChangesAsync();

            return Ok($"Dodat je zaposlen sa ID: {zaposlen.ID}");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost("DodajUstanovu")]
    public async Task<IActionResult> DodajUstanovu([FromBody] Ustanova ustanova)
    {
        try
        {
            await Context.Ustanove.AddAsync(ustanova);
            await Context.SaveChangesAsync();

            return Ok($"Dodata je ustanova sa ID: {ustanova.ID}");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost("Zaposli/{idRadnika}/{idUstanove}/{brUgovora}")]
    public async Task<IActionResult> Zaposli(int idRadnika, int idUstanove, int brUgovora)
    {
        try
        {
            var ugovor = await Context
                .Ugovori.Include(z => z.ZaposlenRadnik)
                .Include(u => u.UstanovaZaposljava)
                .Where(p =>
                    p.ZaposlenRadnik!.ID == idRadnika && p.UstanovaZaposljava!.ID == idUstanove
                )
                .FirstOrDefaultAsync();

            if (ugovor != null)
                return BadRequest("taj radnik je vec zaposljen tu");

            var radnik = await Context.Zaposleni.FindAsync(idRadnika);

            if (radnik == null)
                return NotFound("taj radnik ne postoji");

            var ustanova = await Context.Ustanove.FindAsync(idUstanove);

            if (ustanova == null)
                return NotFound("nema ta ustanova u bazi");

            var ugovorNovi = new Ugovor()
            {
                ZaposlenRadnik = radnik,
                UstanovaZaposljava = ustanova,
                BrojUgovora = brUgovora,
                DatumPotpisivanja = DateTime.Now,
                // DatumPotpisivanja = new DateTime(2010, 10, 10), ---> za proveru
            };

            radnik.BrojUstanovaUKojeRadi++;

            await Context.Ugovori.AddAsync(ugovorNovi);
            await Context.SaveChangesAsync();

            return Ok($"U ustanovi {idUstanove} je zaposljen {radnik.Ime}");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut("PromeniUgovor/{idUgovora}/{odKad}/{brUgovora}")]
    public async Task<IActionResult> PromeniUgovor(int idUgovora, DateTime odKad, int brUgovora)
    {
        try
        {
            var ugovor = await Context.Ugovori.FindAsync(idUgovora);

            if (ugovor == null)
                return NotFound("nema taj ugovor");

            ugovor.BrojUgovora = brUgovora;
            ugovor.DatumPotpisivanja = odKad;

            Context.Ugovori.Update(ugovor);
            await Context.SaveChangesAsync();

            return Ok($"novi br ugovora je {brUgovora} i novi datum je {odKad}");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("KoRadiNaViseMestaOdDana/{odDana}")]
    public async Task<IActionResult> PromeniUgovor(DateTime odDana)
    {
        try
        {
            var zaposleni = await Context
                .Ugovori.Include(z => z.ZaposlenRadnik)
                .Where(p =>
                    p.ZaposlenRadnik!.BrojUstanovaUKojeRadi > 1 && p.DatumPotpisivanja > odDana
                )
                .Select(z => z.ZaposlenRadnik)
                .Distinct()
                .ToListAsync();

            if (zaposleni == null)
                return NotFound("nema niko takav");

            return Ok(zaposleni);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("UkupanBrojRadnihDana/{idUstanove}")]
    public async Task<IActionResult> UkupanBrojRadnihDana(int idUstanove)
    {
        try
        {        /* ovo nije dobro i daje se 0 poena za ovakvu funkciju jer povlacim celu bazu a trebaju mi samo datumi, pise to i na blanketu */
            var ugovori = await Context
                .Ugovori.Include(z => z.ZaposlenRadnik)
                .Where(p => p.UstanovaZaposljava!.ID == idUstanove)
                .ToListAsync();

            if (ugovori == null)
                return NotFound("jos niko ne radi u ustanovi");

            int ukupnoVreme = 0;
            foreach (var U in ugovori)
                ukupnoVreme += (DateTime.Now - U.DatumPotpisivanja).Days;

            return Ok($"ukupno radnih dana svih radnika je {ukupnoVreme}");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
