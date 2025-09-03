namespace WebTemplate.Controllers;

[ApiController]
[Route("[controller]")]
public class BioskopController : ControllerBase
{
    public BioskopContext Context { get; set; }

    public BioskopController(BioskopContext context)
    {
        Context = context;
    }

    [Route("vratiProjekciju/{id}")]
    [HttpGet]
    public async Task<IActionResult> GetProjekcija([FromRoute] int id)
    {
        var projekcija = await Context.Projekcije
            .Include(p => p.Sala)
            .Where(p => p.Id == id)
            .Select(p => new
            {
                p.Naziv,
                p.VremeProjekcije,
                SalaNaziv = p.Sala != null ? p.Sala.Naziv : "",
            })
            .FirstOrDefaultAsync();
        if (projekcija == null)
        {
            return NotFound();
        }
        return Ok(projekcija.Naziv + ": " + projekcija.VremeProjekcije.ToString("dd.MM.yyyy, HH:mm") + " - " + projekcija.SalaNaziv);
    }

    [Route("vratiBrojRedova/{idProjekcije}")]
    [HttpGet]
    public async Task<IActionResult> GetBrojRedova([FromRoute] int idProjekcije)
    {
        var redova = await Context.Projekcije
            .Include(p => p.Sala)
            .Where(p => p.Id == idProjekcije)
            .Select(p => p.Sala != null ? p.Sala.BrojRedova : 0)
            .FirstOrDefaultAsync();
        if (redova == 0)
        {
            return NotFound();
        }
        return Ok(redova);
    }

    [Route("vratiBrojSedistURedu/{idProjekcije}")]
    [HttpGet]
    public async Task<IActionResult> GetBrojSedistURedu([FromRoute] int idProjekcije)
    {
        var sedista = await Context.Projekcije
            .Include(p => p.Sala)
            .Where(p => p.Id == idProjekcije)
            .Select(p => p.Sala != null ? p.Sala.BrojSedistaURedu : 0)
            .FirstOrDefaultAsync();
        if (sedista == 0)
        {
            return NotFound();
        }
        return Ok(sedista);
    }

    [Route("vratiCenuKarte/{idProjekcije}/{red}/{sediste}")]
    [HttpGet]
    public async Task<IActionResult> VratiCenuKarte([FromRoute] int idProjekcije, [FromRoute] int red, [FromRoute] int sediste)
    {
        var projekcija = await Context.Projekcije
            .Where(p => p.Id == idProjekcije)
            .FirstOrDefaultAsync();
        if (projekcija == null)
        {
            return NotFound("Projekcija nije pronađena.");
        }
        double cena = projekcija.Cena;
        for (int i = 1; i < red; i++)
        {
            cena *= 0.97;
        }
        return Ok(new
        {
            Red = red,
            Sediste = sediste,
            SifraProjekcije = projekcija.Sifra,
            Cena = Math.Round(cena, 2)
        });
    }

    [Route("kupiKartu/{red}/{sediste}/{cena}/{sifraProjekcije}")]
    [HttpPost]
    public async Task<IActionResult> KupiKartu([FromRoute] int red, [FromRoute] int sediste, [FromRoute] double cena, [FromRoute] int sifraProjekcije)
    {
        var projekcija = await Context.Projekcije
            .Where(p => p.Sifra == sifraProjekcije)
            .FirstOrDefaultAsync();
        if (projekcija == null)
            return NotFound("Projekcija nije pronađena.");

        if (Context.Projekcije
            .Where(p => p.Karte.Any(k => k.Red == red && k.Sediste == sediste && k.Projekcija.Id == projekcija.Id))
            .FirstOrDefault() != null)
            return BadRequest("Karta za odabrano mesto već postoji.");

        var karta = new Karta
        {
            Projekcija = projekcija,
            Red = red,
            Sediste = sediste,
            Cena = cena
        };
        try
        {
            Context.Karte.Add(karta);
            await Context.SaveChangesAsync();
            return Ok("Karta uspešno kupljena.");
        }
        catch (Exception ex)
        {
            return BadRequest("Greška prilikom kupovine karte: " + ex.Message);
        }
    }

    [Route("zauzetaSedista/{idProjekcije}")]
    [HttpGet]
    public async Task<IActionResult> ZauzetaSedista([FromRoute] int idProjekcije)
    {
        var zauzetaSedista = await Context.Karte
            .Where(k => k.Projekcija.Id == idProjekcije)
            .Select(k => new { k.Red, k.Sediste })
            .ToListAsync();
        return Ok(zauzetaSedista);
    }

    [Route("buduceProjekcije")]
    [HttpGet] 
    public async Task<IActionResult> BuduceProjekcije()
    {
        var buduceProjekcije = await Context.Projekcije
            .Where(p => p.VremeProjekcije > DateTime.Now)
            .Select(p => new
            {
                p.Id
            })
            .ToListAsync();
        return Ok(buduceProjekcije);
    }
}
