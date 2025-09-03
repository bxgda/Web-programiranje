namespace WebTemplate.Controllers;

[ApiController]
[Route("[controller]")]
public class RacunController : ControllerBase
{
    public IspitContext Context { get; set; }

    public RacunController(IspitContext context)
    {
        Context = context;
    }

    [HttpPost]
    [Route("DodajRacun/{stanID}/{mesec}/{placen}/{cenaVode}")]
    public async Task<ActionResult> DodajRacun(int stanID, int mesec, string placen, int cenaVode)
    {
        if (mesec < 0 || mesec > 12)
        {
            return BadRequest("Mesec mora biti vrednost izmedju 1 i 12");
        }
        
        if (placen != "Da" && placen != "Ne")
        {
            return BadRequest("Placen mora biti ili 'Da' ili 'Ne'");
        }

        var s = await Context.Stanovi.FindAsync(stanID);
        if (s == null)
        {
            return BadRequest("Stan sa datim Id-em nije pronadjen");
        }

        var racunZaIstiMesec = await Context.Racuni
            .Where(racun => racun.Mesec == mesec)
            .Include(racun => racun.Stan)
            .Where(racunStan => racunStan.Stan.ID == stanID)
            .FirstOrDefaultAsync();
        if (racunZaIstiMesec!=null)
        {
            return BadRequest("Vec postoji racun za taj mesec");
        }
        Racun r = new Racun()
        {
            Stan = s,
            Mesec = mesec,
            CenaVode = cenaVode,
            Placen = placen == "Da"
        };
        await Context.Racuni.AddAsync(r);
        await Context.SaveChangesAsync();
        return Ok("Uspesno dodat racun sa ID-em:" + r.ID);
    }
}