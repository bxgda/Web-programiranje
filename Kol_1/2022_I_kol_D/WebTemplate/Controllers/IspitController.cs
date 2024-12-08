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
    [HttpPost("DodajBanku")]
    public async Task<ActionResult> DodajBanku([FromBody] Banka b)
    {
        try
        {
            await Context.Banke.AddAsync(b);
            await Context.SaveChangesAsync();

            return Ok($"Dodata banka sa id: {b.ID}");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost("DodajKlijenta")]
    public async Task<ActionResult> DodajKlijenta([FromBody] Klijent k)
    {
        try
        {
            await Context.Klijenti.AddAsync(k);
            await Context.SaveChangesAsync();

            return Ok($"Dodat klijent sa id: {k.ID}");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost("DodajRacun/{idKlijenta}/{idBanke}")]
    public async Task<ActionResult> DodajRacun([FromBody] Racun r, int idKlijenta, int idBanke)
    {
        try
        {
            var klijent = await Context.Klijenti.FindAsync(idKlijenta);
            var banka = await Context.Banke.FindAsync(idBanke);

            if (klijent == null || banka == null)
                return BadRequest("Klijent ili banka ne postoje.");

            r.Banka = banka;
            r.Klijent = klijent;

            await Context.Racuni.AddAsync(r);
            await Context.SaveChangesAsync();

            return Ok($"Dodat racun sa id: {r.ID}");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut("IzmeniStanje/{BrojRacuna}/{NovoStanje}")]
    public async Task<ActionResult> IzmeniStanje(int BrojRacuna, double NovoStanje)
    {
        try
        {
            var racun = await Context.Racuni
                        .Where(r => r.BrojRacuna == BrojRacuna)
                        .FirstOrDefaultAsync();

            if (racun == null)
                return BadRequest("Racun ne postoji.");

            if (racun.Sredstva > NovoStanje)
                racun.KolicinaPodignutogNovca += racun.Sredstva - NovoStanje;

            racun.Sredstva = NovoStanje;

            Context.Racuni.Update(racun);
            await Context.SaveChangesAsync();

            return Ok("Racun promenjen");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("UkupnaSumaNovca/{idBanke}")]
    public async Task<ActionResult> UkupnaSumaNovca(int idBanke)
    {
        try
        {
            var suma = await Context.Racuni
                .Where(x => x.Banka!.ID == idBanke)
                .SumAsync(x => x.KolicinaPodignutogNovca + x.Sredstva);

            return Ok(suma);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
