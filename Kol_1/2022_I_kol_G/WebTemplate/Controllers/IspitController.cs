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

    [HttpPost("DodajNekretninu")]
    public async Task<IActionResult> DodajNekretninu([FromBody] Nekretnina nekretnina)
    {
        try
        {
            await Context.Nekretnine.AddAsync(nekretnina);
            await Context.SaveChangesAsync();

            return Ok($"Uspesno dodata nekretnita tipa: {nekretnina.Tip} sa ID: {nekretnina.ID}");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost("DodajVlasnika")]
    public async Task<IActionResult> DodajVlasnika([FromBody] Vlasnik vlasnik)
    {
        try
        {
            await Context.Vlasnici.AddAsync(vlasnik);
            await Context.SaveChangesAsync();

            return Ok($"Uspesno dodat vlasnik {vlasnik.Ime} sa ID: {vlasnik.ID}");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost("Kupi/{idNekretnine}/{idVlasnika}/{brUgovora}/{uplacenaSuma}")]
    public async Task<IActionResult> KupiNekretninu(
        int idNekretnine,
        int idVlasnika,
        int brUgovora,
        double uplacenaSuma
    )
    {
        try
        {
            var nekretnina = await Context.Nekretnine.FindAsync(idNekretnine);

            if (nekretnina == null)
                return NotFound("Ne postoji ta nekretnina");

            if (nekretnina.Vrednost > uplacenaSuma)
                return BadRequest("nema dovoljno pare");

            var vlasnik = await Context.Vlasnici.FindAsync(idVlasnika);

            if (vlasnik == null)
                return NotFound("Ne postoji taj vlasnik");

            var kupovina = await Context
                .Kupovine.Where(k =>
                    k.KoJeKupio!.ID == idVlasnika && k.KupljenaNekretnina!.ID == idNekretnine
                )
                .FirstOrDefaultAsync();

            if (kupovina != null)
                return BadRequest("vec je taj vlasnik kupio to");

            var noviUgovor = new Kupovina()
            {
                KoJeKupio = vlasnik,
                KupljenaNekretnina = nekretnina,
                IsplacenaVrednost = uplacenaSuma,
                DatumKupovime = DateTime.Now,
                BrojUgovora = brUgovora,
            };

            nekretnina.BrojPrethnodnihVlasnika++;

            if (uplacenaSuma > nekretnina.Vrednost)
                nekretnina.Vrednost = uplacenaSuma; // kao ako je uplatio vise onda vise ce da kosta kaoo... nebitno skroz ali ok.

            Context.Nekretnine.Update(nekretnina);

            await Context.Kupovine.AddAsync(noviUgovor);
            await Context.SaveChangesAsync();

            return Ok(
                $"Uspesno kupljena nekretnina {nekretnina.ID} od stane novog vlasnika: {vlasnik.ID}"
            );
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("SveNekretnineKojeNekoIma/{idVlasnika}")]
    public async Task<IActionResult> SveNekretnineKojeNekoIma(int idVlasnika)
    {
        try
        {
            var nekretnine = await Context
                .Kupovine.Include(v => v.KoJeKupio)
                .Where(v => v.KoJeKupio!.ID == idVlasnika)
                .Select(n => n.KupljenaNekretnina)
                .ToListAsync();

            if (nekretnine == null)
                return NotFound("taj vlasnik jos nista nije kupio");

            return Ok(nekretnine);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("ProsecnaVrednostNekretnine/{idNekretnine}")]
    public async Task<IActionResult> ProsecnaVrednostNekretnine(int idNekretnine)
    {
        try
        {
            var prosecnaVrednost = await Context
                .Kupovine.Include(n => n.KupljenaNekretnina)
                .Where(n => n.KupljenaNekretnina!.ID == idNekretnine)
                .Select(n => n.IsplacenaVrednost)
                .AverageAsync();

            return Ok(prosecnaVrednost);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
