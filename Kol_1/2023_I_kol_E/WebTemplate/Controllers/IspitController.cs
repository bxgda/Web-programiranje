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

    [HttpPost("DodajKorisnika")]
    public async Task<IActionResult> DodajKorisnika([FromBody] Korisnik korisnik)
    {
        try
        {
            await Context.Korisnici.AddAsync(korisnik);
            await Context.SaveChangesAsync();

            return Ok($"Dodat je korisnik sa ID: {korisnik.ID}");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost("DodajKnjigu")]
    public async Task<IActionResult> DodajKnjigu([FromBody] Knjiga knjiga)
    {
        try
        {
            await Context.Knjige.AddAsync(knjiga);
            await Context.SaveChangesAsync();

            return Ok($"Dodata je knjiga sa ID: {knjiga.ID}");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost("Iznajmi/{idKorisnika}/{idKnjige}/{rokZaVracanje}")]
    public async Task<IActionResult> Iznajmi(int idKorisnika, int idKnjige, int rokZaVracanje)
    {
        try
        {
            var korisnik = await Context.Korisnici.FindAsync(idKorisnika);

            if (korisnik == null)
                return NotFound("Ne postoji taj korisnik");

            if (korisnik.BrojIznajmljenih >= 5)
                return BadRequest("Korisnik ne moze vise da iznajmljuje");

            var knjiga = await Context.Knjige.FindAsync(idKnjige);

            if (knjiga == null)
                return NotFound("Ne postoji ta kniga");

            if (knjiga.BrojDostupnih == 0)
                return BadRequest("Nema vise knjiga na stanju");

            var iznajmljivanje = new Iznajmljivanje()
            {
                Korisnik = korisnik,
                Knjiga = knjiga,
                RokZaVracanje = rokZaVracanje,
                DatumIznajmljivanja = DateTime.Now,
                //DatumIznajmljivanja = new DateTime(2010, 10, 10), ---> za proveru
            };

            knjiga.BrojDostupnih--;
            korisnik.BrojIznajmljenih++;

            await Context.Iznajmljivanja.AddAsync(iznajmljivanje);
            await Context.SaveChangesAsync();

            return Ok($"Knjiga sa id {idKnjige} iznajmljena korisniku {idKorisnika}");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut("VratiKnjigu/{idKorisnika}/{idKnjige}")]
    public async Task<IActionResult> VratiKnjigu(int idKorisnika, int idKnjige)
    {
        try
        {
            var iznajmljivanje = await Context
                .Iznajmljivanja.Include(ko => ko.Korisnik)
                .Include(kn => kn.Knjiga)
                .Where(k => k.Korisnik!.ID == idKorisnika && k.Knjiga!.ID == idKnjige)
                .FirstOrDefaultAsync();

            if (iznajmljivanje == null)
                return NotFound("nema to iznajmljivanje");

            iznajmljivanje.Vraceno = true;
            Context.Iznajmljivanja.Update(iznajmljivanje);

            var korisnik = await Context.Korisnici.FindAsync(idKorisnika);
            korisnik!.BrojIznajmljenih--;
            Context.Korisnici.Update(korisnik);

            var knjiga = await Context.Knjige.FindAsync(idKnjige);
            knjiga!.BrojDostupnih++;
            Context.Knjige.Update(knjiga);

            await Context.SaveChangesAsync();

            return Ok($"Knjiga sa id {idKnjige} vracena od korisnika {idKorisnika}");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("KoNijeVratioKnjiguNaVreme")]
    public async Task<IActionResult> KoNijeVratioNaVreme()
    {
        try
        {
            var korisnici = await Context
                .Iznajmljivanja.Where(i =>
                    i.Vraceno == false
                    && i.DatumIznajmljivanja.AddDays(i.RokZaVracanje) <= DateTime.Now
                )
                .Select(k => k.Korisnik)
                .ToListAsync();

            if (korisnici.Count == 0)
                return Ok("niko ne kasni sa vracanjem!");
            else
                return Ok(korisnici);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("KojeKnjigeImaManjeOd5")]
    public async Task<IActionResult> KojeKnjigeImaManjeOd5()
    {
        try
        {
            var knjige = await Context.Knjige.Where(k => k.BrojDostupnih < 5).ToListAsync();

            if (knjige.Count == 0)
                return Ok("sve knjige ima bar jos po 5");
            else
                return Ok(knjige);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
