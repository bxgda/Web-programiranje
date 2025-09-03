namespace WebTemplate.Controllers;

[ApiController]
[Route("[controller]")]
public class StanController : ControllerBase
{
    public IspitContext Context { get; set; }

    public StanController(IspitContext context)
    {
        Context = context;
    }

    [HttpPost]
    [Route("DodajStan")]
    public async Task<ActionResult> DodajStan([FromBody] Stan stan)
    {
        if (string.IsNullOrEmpty(stan.Vlasnik) || stan.Vlasnik.Length > 50)
        {
            return BadRequest("Vlasnik stana nije dobro unet");
        }

        await Context.Stanovi.AddAsync(stan);
        await Context.SaveChangesAsync();
        return Ok("Uspesno dodat stan sa id:" + stan.ID);
    }

    [HttpGet]
    [Route("VratiStan/{id}")]
    public async Task<ActionResult> VratiStan(int id)
    {
        var stan = await Context.Stanovi
                .Where(stan => stan.ID == id)
                .FirstOrDefaultAsync();
        if (stan == null)
        {
            return BadRequest("Stan sa datim id-em nije pronadjen");
        }

        var racuni = await Context.Racuni
                .Where(racun => racun.Stan == stan)
                .Select(racun => new
                {
                    racun.Mesec,
                    racun.CenaVode,
                    racun.CenaZajednickeStruje,
                    racun.CenaKomunalija,
                    Placen = racun.Placen ? "Da" : "Ne"
                })
                .ToListAsync();
        return Ok(new
        {
            Stan = stan,
            Racuni = racuni,
        });
    }

    [HttpGet]
    [Route("VratiIDeveSvihStanova")]
    public async Task<ActionResult> VratiIDeveSvihStanova()
    {
        var idevi = Context.Stanovi.Select(stan => stan.ID);
        return Ok(await idevi.ToListAsync());
    }

    [HttpGet]
    [Route("IzracunajUkupnoZaduzenje/{IDStana}")]
    public async Task<ActionResult> IzracunajUkupnoZaduzenje(int IDStana)
    {
        var stan = await Context.Stanovi.FindAsync(IDStana);
        if (stan == null)
        {
            return BadRequest("Ne postoji stan sa datim ID-em");
        }
        var zaduzenja = await Context.Racuni
                .Where(racun => racun.Stan == stan
                    && racun.Placen == false)
                .Select(racun => racun.CenaKomunalija + racun.CenaVode + racun.CenaZajednickeStruje)
                .ToListAsync();
        int ukupnoZaduzenje = 0;
        zaduzenja.ForEach(zaduzenje =>
        {
            ukupnoZaduzenje += zaduzenje;
        });
        return Ok(ukupnoZaduzenje);
    }
}