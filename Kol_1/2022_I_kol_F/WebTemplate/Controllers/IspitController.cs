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

    [HttpPost("DodajRestoran")]
    public async Task<IActionResult> DodajRestoran([FromBody] Restoran r)
    {
        try
        {
            if (r.BrojTelefona != null && !r.BrojTelefona.All(char.IsDigit))
                return BadRequest("BrojTelefona moze samo sadrzati cifre.");

            await Context.Restorani.AddAsync(r);
            await Context.SaveChangesAsync();

            return Ok($"Uspesno dodat restoran sa ID: {r.ID}.");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }


    [HttpPost("DodajKuvara")]
    public async Task<IActionResult> DodajKuvara([FromBody] Kuvar k)
    {
        try
        {
            await Context.Kuvari.AddAsync(k);
            await Context.SaveChangesAsync();

            return Ok($"Uspesno dodat kuvar sa ID: {k.ID}.");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost("Zaposli/{idKuvara}/{idRestorana}")]
    public async Task<IActionResult> Zaposli([FromBody] Zaposlenje z, int idKuvara, int idRestorana)
    {
        try
        {
            var kuvar = await Context.Kuvari.FindAsync(idKuvara);
            var restoran = await Context.Restorani.FindAsync(idRestorana);

            if (kuvar == null || restoran == null)
                return BadRequest("Kuvar ili restoran ne postoje.");

            var trBrKuvara = await Context.Restorani
                .Include(r => r.Zaposlenja)
                .Where(r => r.ID == idRestorana)
                .Select(r => r.Zaposlenja!.Count)
                .FirstOrDefaultAsync();

            if (trBrKuvara + 1 > restoran.MaksBrojKuvara)
                return BadRequest("Nema vise mesta da se zaposli.");

            z.Kuvar = kuvar;
            z.Restoran = restoran;

            await Context.Zaposlenja.AddAsync(z);
            await Context.SaveChangesAsync();

            return Ok($"Uspesno zaposljen kuvar sa ID: {kuvar.ID} u restoran sa ID: {restoran.ID}.");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    [HttpGet("PronadjiPlatuKuvara/{KuvarId}")]
    public async Task<ActionResult> PronadjiPlatuKuvara(int KuvarId)
    {
        try
        {
            var suma = await Context.Zaposlenja
                .Include(p => p.Kuvar)
                .Include(p => p.Restoran)
                .Where(p => p.Kuvar!.ID == KuvarId)
                .SumAsync(p => p.Plata);

            return Ok(suma);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }

    }
    [HttpGet("PronadjiKuvara")]
    public async Task<ActionResult> PronadjiKuvara()
    {
        try
        {
            var kuvari = await Context.Kuvari
                .Include(p => p.Zaposlenja)
                .OrderBy(p => p.Zaposlenja!.Count)
                .LastOrDefaultAsync();

            return Ok(kuvari);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
