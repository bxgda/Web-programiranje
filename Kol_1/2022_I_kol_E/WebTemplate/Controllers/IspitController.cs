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

    [HttpPost("DodajGrad")]
    public async Task<IActionResult> DodajGrad([FromBody] Grad g)
    {
        try
        {
            await Context.Gradovi.AddAsync(g);
            await Context.SaveChangesAsync();

            return Ok($"Uspesno dodat grad {g.Naziv}.");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("DodajVoz")]
    public async Task<IActionResult> DodajVoz([FromBody] Voz v)
    {
        try
        {
            await Context.Vozovi.AddAsync(v);
            await Context.SaveChangesAsync();

            return Ok($"Uspesno dodat voz sa ID: {v.ID}.");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("DodajRelaciju")]
    public async Task<IActionResult> DodajRelaciju([FromBody] RelacijaZaSlanje r)
    {
        try
        {
            var voz = await Context.Vozovi.FindAsync(r.idVoz);

            if (voz == null || voz.MaksimalniKapacitet < r.BrojPutnika)
                return BadRequest("Voz ne postoji ili ne moze da primi uneti broj putnika.");

            var polazniGrad = await Context.Gradovi.FindAsync(r.idPolazniGrad);
            var dolazniGrad = await Context.Gradovi.FindAsync(r.idDolazniGrad);

            if (polazniGrad == null || dolazniGrad == null)
                return BadRequest("Ili voz ili neki grad ne postoji.");

            var relacija = new Relacija
            {
                BrojPutnika = r.BrojPutnika,
                CenaKarte = r.CenaKarte,
                Datum = r.Datum,
                Voz = voz,
                PolazniGrad = polazniGrad,
                DolazniGrad = dolazniGrad,
            };

            await Context.Relacije.AddAsync(relacija);
            await Context.SaveChangesAsync();

            return Ok($"Uspesno dodata relacija izmedju "
                + $"{polazniGrad.Naziv} i {dolazniGrad.Naziv} sa vozom {voz.Naziv}({voz.ID}).");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("VratiVozoveNaRelaciji/{idPolaznog}/{idDolaznog}")]
    public async Task<IActionResult> VratiVozoveNaRelaciji(int idPolaznog, int idDolaznog)
    {
        try
        {
            var vozovi = await Context.Relacije
                .Include(r => r.PolazniGrad)
                .Include(r => r.DolazniGrad)
                .Include(r => r.Voz)
                .Where(r => r.PolazniGrad!.ID == idPolaznog || r.DolazniGrad!.ID == idDolaznog)
                .Select(r => r.Voz)
                .Distinct()
                .ToListAsync();

            return Ok(vozovi);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("UkupnaZaradaNaRelaciji/{idPolaznog}/{idDolaznog}/{idVoza}")]
    public async Task<IActionResult> UkupnaZaradaNaRelaciji(int idPolaznog, int idDolaznog, int idVoza)
    {
        try
        {
            var zarada = await Context.Relacije
                .Include(r => r.PolazniGrad)
                .Include(r => r.DolazniGrad)
                .Include(r => r.Voz)
                .Where(r => r.Voz!.ID == idVoza && r.PolazniGrad!.ID == idPolaznog && r.DolazniGrad!.ID == idDolaznog)
                .SumAsync(r => r.BrojPutnika * r.CenaKarte);

            return Ok(zarada);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
