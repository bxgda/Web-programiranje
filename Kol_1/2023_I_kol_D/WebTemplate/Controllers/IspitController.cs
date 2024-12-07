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

    [HttpPost("DodajProizvod")]
    public async Task<IActionResult> DodajProizvod([FromBody] Proizvod p)
    {
        try
        {
            if (!p.Identifikator.All(char.IsDigit))
                return BadRequest("Identifikator se mora sastojati samo od cifara.");

            await Context.Proizvodi.AddAsync(p);
            await Context.SaveChangesAsync();

            return Ok($"Uspesno dodat proizvod sa ID: {p.ID}.");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost("DodajProdavnicu")]
    public async Task<IActionResult> DodajProdavnicu([FromBody] Prodavnica p)
    {
        try
        {
            if (!string.IsNullOrEmpty(p.BrojTelefona) && !p.BrojTelefona.All(char.IsDigit))
                return BadRequest("Broj telefona se mora sastojati samo iz cifara.");

            await Context.Prodavnice.AddAsync(p);
            await Context.SaveChangesAsync();

            return Ok($"Uspesno dodata prodavnica sa ID: {p.ID}.");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost("DodajProizvodUProdavnicu/{idProizvoda}/{idProdavnice}/{cena}/{brojDostupnih}")]
    public async Task<IActionResult> DodajProizvodUProdavnicu(
        int idProizvoda,
        int idProdavnice,
        double cena,
        int brojDostupnih
    )
    {
        try
        {
            var proizvod = await Context.Proizvodi.FindAsync(idProizvoda);
            var prodavnica = await Context.Prodavnice.FindAsync(idProdavnice);

            if (proizvod == null || prodavnica == null)
                return NotFound("Proizvod ili prodavnica ne postoje.");

            var magacin = new Magacin()
            {
                Cena = cena,
                BrojDostupnih = brojDostupnih,
                Prodavnica = prodavnica,
                Proizvod = proizvod,
            };

            await Context.Magacin.AddAsync(magacin);
            await Context.SaveChangesAsync();

            return Ok($"Proizvod {proizvod!.Naziv} dodat u prodavnicu {prodavnica!.Naziv}.");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut(
        "IzmeniProizvodUProdavnici/{idProizvoda}/{idProdavnice}/{novaCena}/{noviBrojDostupnih}"
    )]
    public async Task<IActionResult> IzmeniProizvodUProdavnici(
        int idProizvoda,
        int idProdavnice,
        double novaCena,
        int noviBrojDostupnih
    )
    {
        try
        {
            var stavkaUMagacinu = await Context
                .Magacin.Include(m => m.Proizvod)
                .Include(m => m.Prodavnica)
                .Where(m => m.Prodavnica!.ID == idProdavnice && m.Proizvod!.ID == idProizvoda)
                .FirstOrDefaultAsync();

            if (stavkaUMagacinu == null)
                return NotFound("Nije pronadjen proizvod u prodavnici.");

            stavkaUMagacinu.Cena = novaCena;
            stavkaUMagacinu.BrojDostupnih = noviBrojDostupnih;

            Context.Magacin.Update(stavkaUMagacinu);
            await Context.SaveChangesAsync();

            return Ok("Uspesno izmenjeno.");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    // ovde ne kaze kolko se kupuje tkd moze i da se smanji samo za jedan al ajde...
    [HttpPut("KupovinaProizvodaIzProdavnice/{idProizvoda}/{idProdavnice}/{kolicina}")]
    public async Task<IActionResult> KupovinaProizvodaIzProdavnice(
        int idProizvoda,
        int idProdavnice,
        int kolicina
    )
    {
        try
        {
            var stavkaUMagacinu = await Context
                .Magacin.Include(m => m.Proizvod)
                .Include(m => m.Prodavnica)
                .Where(m => m.Prodavnica!.ID == idProdavnice && m.Proizvod!.ID == idProizvoda)
                .FirstOrDefaultAsync();

            if (stavkaUMagacinu == null)
                return BadRequest("Nije moguce kupiti proizvod.");

            if (stavkaUMagacinu.BrojDostupnih - kolicina <= 0)
                return BadRequest("Trazena kolicina nije dostupna u prodavnici.");

            stavkaUMagacinu.BrojDostupnih -= kolicina;

            Context.Magacin.Update(stavkaUMagacinu);
            await Context.SaveChangesAsync();

            return Ok("Proizvod kupljen.");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("ProizvodiKojimaJeIstekaoRok")]
    public async Task<IActionResult> ProizvodiKojimaJeIstekaoRok(
        [FromQuery] DateTime datum,
        [FromQuery] int[] idProdavnica
    )
    {
        try
        {
            var proizvodi = await Context
                .Magacin.Include(m => m.Prodavnica)
                .Include(m => m.Proizvod)
                .Where(m =>
                    idProdavnica.Contains(m.Prodavnica!.ID) && m.Proizvod!.DatumIstekaRoka < datum
                )
                .Select(m => m.Proizvod)
                .ToListAsync();

            return Ok(proizvodi);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
