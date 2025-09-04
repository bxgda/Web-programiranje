namespace StovaristeServer.Controllers;

[ApiController]
[Route("[controller]")]
public class StovaristeController : ControllerBase
{
    public StovaristeContext Context { get; set; }

    public StovaristeController(StovaristeContext context)
    {
        Context = context;
    }

    [HttpGet("PreuzmiStovarista")]
    public async Task<IActionResult> PreuzmiStovarista()
    {
        return Ok(await Context.Stovarista.ToListAsync());
    }

    [HttpPost("DodavanjeMaterijala/{stovaristeID}/{kolicina}/{cena}")]
    public async Task<IActionResult> DodavanjeMaterijala([FromBody] Materijal materijal, int stovaristeID, double kolicina, double cena)
    {
        await Context.Materijali.AddAsync(materijal);

        var stovariste = await Context.Stovarista.FindAsync(stovaristeID);

        if (stovariste == null)
        {
            return BadRequest("Nije pronađeno stovarište.");
        }

        var magacin = new Magacin()
        {
            Cena = cena,
            Kolicina = kolicina,
            Materijal = materijal,
            Stovariste = stovariste
        };

        await Context.AddAsync(magacin);
        await Context.SaveChangesAsync();
        return Ok(materijal);
    }

    [HttpPut("KupovinaMaterijala/{stovaristeID}/{materijalID}/{kolicina}")]
    public async Task<IActionResult> KupovinaMaterijala(int stovaristeID, int materijalID, double kolicina)
    {
        var materijal = await Context.Materijali.FindAsync(materijalID);

        if (materijal == null)
        {
            return BadRequest("Materijal nije pronađen.");
        }

        string imeMaterijala = materijal.Naziv;

        var stovariste = await Context.Stovarista
            .Include(p => p.MaterijaliUMagacinu)!
            .ThenInclude(p => p.Materijal)
            .Where(p => p.ID == stovaristeID)
            .FirstOrDefaultAsync();

        if (stovariste == null || stovariste.MaterijaliUMagacinu == null)
        {
            return BadRequest("Nije pronađeno stovarište sa prosleđenim materijalom i ID-jem.");
        }

        var materijalNaStovaristu = stovariste.MaterijaliUMagacinu.Where(p => p.Materijal.ID == materijalID).FirstOrDefault();

        if (materijalNaStovaristu != null && materijalNaStovaristu.Kolicina >= kolicina)
        {
            double cena = materijalNaStovaristu.Cena * kolicina;
            materijalNaStovaristu.Kolicina -= kolicina;
            Context.Magacini.Update(materijalNaStovaristu);
            await Context.SaveChangesAsync();
            return Ok($"Kupili ste {kolicina}kg {imeMaterijala}. Cena je: {cena}");
        }
        else
        {
            if (materijalNaStovaristu == null)
            {
                Magacin m = new Magacin
                {
                    Materijal = materijal,
                    Stovariste = stovariste,
                    Cena = Random.Shared.Next(100, 1000),
                    Kolicina = kolicina * 10
                };

                await Context.Magacini.AddAsync(m);
                await Context.SaveChangesAsync();
            }
            else
            {
                materijalNaStovaristu.Kolicina += kolicina * 10;
                Context.Magacini.Update(materijalNaStovaristu);
                await Context.SaveChangesAsync();
            }
        }

        return BadRequest("Neuspela kupovina!");
    }

    [HttpGet("PronadjiMaterijal/{stovaristeID}/{text}")]
    public async Task<IActionResult> PronadjiMaterijal(int stovaristeID, string text)
    {
        var materijaliNaStovaristu = await Context.Stovarista
            .Include(p => p.MaterijaliUMagacinu)!
            .ThenInclude(p => p.Materijal)
            .Where(p => p.ID == stovaristeID)
            .SelectMany(p => p.MaterijaliUMagacinu!)
            .ToListAsync();

        var pretraga = materijaliNaStovaristu.Where(p => p.Materijal.Naziv.Contains(text));

        if (pretraga != null && pretraga.Any())
        {
            return Ok(pretraga);
        }
        else
        {
            return BadRequest("Nije pronađen materijal.");
        }
    }

    [HttpGet("MaterijalUNajvecojKolicini/{stovaristeID}")]
    public async Task<IActionResult> MaterijalUNajvecojKolicini(int stovaristeID)
    {
        var materijali = await Context.Magacini
            .Include(p => p.Materijal)
            .Include(p => p.Stovariste)
            .Where(p => p.Stovariste.ID == stovaristeID)
            .Select(p => new { p.Kolicina, p.Materijal.ID })
            .ToListAsync();

        var idM = materijali.GroupBy(p => p.ID)
            .Select(p => new { ID = p.Key, Kolicina = p.Sum(q => q.Kolicina) })
            .OrderByDescending(p => p.Kolicina)
            .FirstOrDefault()?.ID;

        if (idM != null)
        {
            return Ok(await Context.Materijali.FindAsync(idM));
        }

        return BadRequest("Nije pronađen najzastupljeniji materijal.");
    }
}
