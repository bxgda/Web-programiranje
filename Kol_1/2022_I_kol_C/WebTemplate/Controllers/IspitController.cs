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

    [HttpPost("DodajBolnicu")]
    public async Task<IActionResult> DodajBolnicu([FromBody] Bolnica bolnica)
    {
        try
        {
            await Context.Bolnice.AddAsync(bolnica);
            await Context.SaveChangesAsync();

            return Ok($"Uspesno dodata bolnica {bolnica.Naziv} sa ID: {bolnica.ID}");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost("DodajLekara")]
    public async Task<IActionResult> DodajLekara([FromBody] Lekar lekar)
    {
        try
        {
            await Context.Lekari.AddAsync(lekar);
            await Context.SaveChangesAsync();

            return Ok($"Uspesno dodata bolnica {lekar.Ime} sa ID: {lekar.ID}");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost("ZaposliLekara/{idLekara}/{idBolnice}/{specijalnost}/{brUgovora}")]
    public async Task<IActionResult> ZaposliLekara(
        int idLekara,
        int idBolnice,
        string specijalnost,
        int brUgovora
    )
    {
        try
        {
            var lekar = await Context.Lekari.FindAsync(idLekara);
            if (lekar == null)
                return NotFound("taj lekar ne postoji");

            var bolnica = await Context.Bolnice.FindAsync(idBolnice);
            if (bolnica == null)
                return NotFound("ta bolnica ne postoji");

            var stariUgovor = await Context
                .Ugovori.Where(u => u.P_Lekar!.ID == idLekara && u.P_Bolnica!.ID == idBolnice)
                .FirstOrDefaultAsync();

            if (stariUgovor != null)
                return BadRequest("taj lekar vec radi tu");

            bolnica.BrojOsoblja++;
            Context.Update(bolnica);

            var noviUgovor = new Ugovor()
            {
                P_Bolnica = bolnica,
                P_Lekar = lekar,
                IDBroj = brUgovora,
                DatumPotpisivanja = DateTime.Now,
                Specijalnost = specijalnost,
            };

            await Context.Ugovori.AddAsync(noviUgovor);
            await Context.SaveChangesAsync();

            return Ok($"uspesno zaposljen {idLekara} u {idBolnice}");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("SviLekariKojiSuSpecijalizovaniZa/{specijalnost}/{idBolnice}")]
    public async Task<IActionResult> SviLekariKojiSuSpecijalizovaniZa(
        string specijalnost,
        int idBolnice
    )
    {
        try
        {
            var lekari = await Context
                .Ugovori.Where(s => s.Specijalnost == specijalnost && s.P_Bolnica!.ID == idBolnice)
                .Select(l => l.P_Lekar)
                .ToListAsync();

            if (lekari == null)
                return NotFound("nema ni jedan lekar sa tom specijalnoscu");

            return Ok(lekari);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("DaLiBolnicaImaLekaraSaNevalidnomLicencom/{idBolnice}")]
    public async Task<IActionResult> DaLiBolnicaImaLekaraSaNevalidnomLicencom(int idBolnice)
    {
        try // ne proveravamo da li ta bolnica postoji, mogli bi ali ajde kao podrazumevao
        {
            var bolnica = await Context
                .Ugovori.Where(l =>
                    l.P_Lekar!.DatumDobijanjaLicence == null && l.P_Bolnica!.ID == idBolnice
                )
                .Select(b => b.P_Bolnica)
                .FirstOrDefaultAsync();

            if (bolnica == null)
                return NotFound("sve regularno");
            else
                return Ok("ima sa nevalidnom licencom");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
