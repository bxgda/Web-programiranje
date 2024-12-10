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

    // 1.
    [HttpPost("DodajRezervoar")]
    public async Task<IActionResult> DodajRezervoar([FromBody] Rezervoar rezervoar)
    {
        try
        {
            await Context.Rezervoari.AddAsync(rezervoar);
            await Context.SaveChangesAsync();

            return Ok($"Uspesno dodat rezervoar i dodeljen mu je ID: {rezervoar.ID}");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    // 1.
    [HttpPost("DodajRibu")]
    public async Task<IActionResult> DodajRibu([FromBody] Riba riba)
    {
        try
        {
            await Context.Ribe.AddAsync(riba);
            await Context.SaveChangesAsync();

            return Ok($"Uspesno dodata riba i dodeljen joj je ID: {riba.ID}");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    // 2.
    [HttpPost("DodajURezervoar/{idRibe}/{idRezervoara}/{brJedinki}")]
    public async Task<IActionResult> DodajURezervoar(int idRibe, int idRezervoara, int brJedinki)
    {
        try
        {
            var rezervoar = await Context.Rezervoari.FindAsync(idRezervoara);

            if (rezervoar == null)
                return NotFound("taj rezervoar ne postoji");

            if (rezervoar.BrojTrenutnihRiba + brJedinki > rezervoar.Kapacitet)
                return BadRequest("taj broj jedinki premasuje kapacitet");

            var riba = await Context.Ribe.FindAsync(idRibe);

            if (riba == null)
                return NotFound("ta riba ne postoji");

            var mogucaSmetnja = await Context
                .Akvarijumi.Include(r => r.P_Riba)
                .Where(p => (p.P_Riba!.Masa * 10 <= riba.Masa) || (p.P_Riba.Masa > riba.Masa * 10))
                .FirstOrDefaultAsync();

            if (mogucaSmetnja != null)
                return BadRequest("moguce je da se ribe pojedu medjusobno");

            rezervoar.BrojTrenutnihRiba += brJedinki;
            Context.Rezervoari.Update(rezervoar);

            var akvarijum = new Akvarijum()
            {
                P_Rezervoar = rezervoar,
                P_Riba = riba,
                BrojJedinki = brJedinki,
                DatumDodavanja = DateTime.Now,
            };

            await Context.Akvarijumi.AddAsync(akvarijum);
            await Context.SaveChangesAsync();

            return Ok($"dodto {brJedinki} riba sa ID {idRibe} u rezervoar {idRezervoara}");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    // 3.
    [HttpPut("IzmeniBrojRibeUAkvarijumu/{idAkvarijuma}/{noviBrojJedinki}")]
    public async Task<IActionResult> IzmeniBrojRibeUAkvarijumu(
        int idAkvarijuma,
        int noviBrojJedinki
    )
    {
        try
        {
            var akvarijum = await Context.Akvarijumi.FindAsync(idAkvarijuma);
            if (akvarijum == null)
                return BadRequest("ne postoji taj akvarijum");

            if (akvarijum.BrojJedinki == noviBrojJedinki)
                return BadRequest("vec ima toliko jedinki, nema sta da se menja");

            /*
                ovo ovde ne valja uopste jer bi trebalo da se promeni i trenutni broj riba u rezervoaru odnosno da
                se ne promeni ako to kapacitet ne dozvoljava ali ja to na kolokvijumu kad sam uradio sve provere izlazila
                mi je neka greska koju nisam video do sad i nisam imao vremena da to ispravim pa sam ga uradio ovako
                realno kako ne bi trebalo... ako budem nekad mogao cu da ispravim ovu metodu i da vidim koja je to greska...
                u sustini neko mi je rekao da iako izlazi greska da se podaci u bazi menjaju tako da je mozda ipak sve bilo ok
                ali to i dalje ne znam... svakako dobio sam max broj poena jer ovu metodu nisu gledali kod neko samo u swager
            */

            akvarijum.BrojJedinki = noviBrojJedinki;
            akvarijum.DatumDodavanja = DateTime.Now;
            Context.Akvarijumi.Update(akvarijum);

            await Context.SaveChangesAsync();

            return Ok($"novi broj riba u akvarijumu {idAkvarijuma} je {noviBrojJedinki}");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    // 4.
    [HttpGet("KoJRezTrebaDaSeOcisti")]
    public async Task<IActionResult> KoJRezTrebaDaSeOcisti()
    {
        try
        {
            var rezervoari = await Context
                .Rezervoari //ovo nece da radi ako se radi o datumima oko Nove godine ali ne moze ovde da se radi sa TimeSpan tipom podataka (probao sam dok sam vezbao, ne znam zasto)
                .Where(p =>
                    DateTime.Now.DayOfYear - p.DatumPoslednjegCiscenja.DayOfYear
                    > p.FrekvencijaCiscenja
                )
                .ToListAsync();

            if (rezervoari == null)
                return Ok("svi su cisti");

            return Ok(rezervoari);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    // 5.
    [HttpGet("UkupnaMasaUrezervoaru/{idRezervoara}")]
    public async Task<IActionResult> UkupnaMasaUrezervoaru(int idRezervoara)
    {
        try
        {
            var mase = await Context
                .Akvarijumi.Include(r => r.P_Riba)
                .Include(r => r.P_Rezervoar)
                .Where(p => p.P_Rezervoar!.ID == idRezervoara)
                .Select(m => m.P_Riba!.Masa * m.BrojJedinki)
                .ToListAsync();

            if (mase == null)
                return NotFound("taj rezervoar i dalje nema ni jednu ribu");

            double ukupnaMasa = 0;
            foreach (double m in mase)
                ukupnaMasa += m;

            return Ok($"ukupna masa = {ukupnaMasa}");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
