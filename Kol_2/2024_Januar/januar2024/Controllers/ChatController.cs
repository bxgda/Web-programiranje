namespace WebTemplate.Controllers;

[ApiController]
[Route("[controller]")]
public class ChatController : ControllerBase
{
    public ChatContext Context { get; set; }

    public ChatController(ChatContext context)
    {
        Context = context;
    }

    [Route("VratiSobu/{nazivSobe}")]
    [HttpGet]
    public async Task<ActionResult<Soba>> vratiSobu(string nazivSobe)
    {
        var soba = await Context.Sobe.Where(p => p.Naziv == nazivSobe)
            .Select(p => new
            {
                p.Id,
                p.Naziv,
                p.Kapacitet
            }).FirstOrDefaultAsync();
        if (soba == null)
            return BadRequest("ne postoji");
        return Ok(soba);
    }

    [Route("VratiKorisnika/{nazivSobe}/{idKorisnika}")]
    [HttpGet]
    public async Task<ActionResult<Soba>> vratiKorisnika(string nazivSobe, int idKorisnika)
    {
        var soba = await Context.Chatovi
            .Include(p => p.Soba)
            .Where(p => p.Soba.Naziv == nazivSobe)
            .Include(p => p.Korisnici)
            .Where(p => p.Korisnici.Id == idKorisnika)
            .Select(p => new
            {
                p.Korisnici.Id,
                p.Korisnici.KorisnickoIme,
                p.Nadimak,
                p.Boja,
            }).FirstOrDefaultAsync();
        return Ok(soba);
    }

    [Route("VratiKorisnike")]
    [HttpGet]
    public async Task<ActionResult> vratiKorisnike()
    {
        var korisnici = await Context.Korisnici
            .Select(p => new
            {
                p.Id,
                p.KorisnickoIme
            })
            .ToListAsync();
        return Ok(korisnici);
    }

    [Route("VratiSobe")]
    [HttpGet]
    public async Task<ActionResult> vratiSobe()
    {
        var sobe = await Context.Sobe
            .Select(p => new
            {
                p.Id,
                p.Naziv
            })
            .ToListAsync();
        return Ok(sobe);
    }


    [Route("VratiClanoveSobe/{idSobe}")]
    [HttpGet]
    public async Task<ActionResult> vratiClanove(int idSobe)
    {
        if (Context.Sobe.Where(p => p.Id == idSobe).FirstOrDefault() == null)
            return BadRequest("ne postoji soba s ovim id-jem");
        var clanovi = await Context.Chatovi
            .Include(p => p.Korisnici)
            .Where(p => p.Soba.Id == idSobe)
            .Select(p => new
            {
                p.Korisnici.Id,
                p.Korisnici.KorisnickoIme,
                p.Nadimak,
                p.Boja
            })
            .ToListAsync();

        return Ok(clanovi);
    }

    [Route("DodajSobu/{sobaNaziv}")]
    [HttpPost]
    public async Task<ActionResult> DodajSobu(string sobaNaziv)
    {
        var soba = Context.Sobe.Where(p => p.Naziv == sobaNaziv).FirstOrDefault();
        if (soba != null)
            return BadRequest("sOBA SA OVIM IMENOM VEC POSTOJI");
        try
        {
            soba = new Soba { Naziv = sobaNaziv, Kapacitet = 5 };
            Context.Sobe.Add(soba);
            await Context.SaveChangesAsync();
            return Ok("soba je dodata");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [Route("UbaciKorisnikaUSobu/{sobaNaziv}/{idkorisnika}/{nadimak}/{boja}")]
    [HttpPost]
    public async Task<ActionResult> Chatuj(string sobaNaziv, int idkorisnika, string nadimak, string boja)
    {
        var soba = Context.Sobe.Where(p => p.Naziv == sobaNaziv).FirstOrDefault();
        var korisnik = Context.Korisnici.Where(p => p.Id == idkorisnika).FirstOrDefault();
        if (soba == null || korisnik == null)
            return BadRequest($"nesto ne valja {sobaNaziv} ili {idkorisnika}");

        int brClanova = Context.Chatovi
            .Where(p => p.Soba.Id == soba.Id)
            .Count();
        if (brClanova >= soba.Kapacitet)
            return BadRequest("soba je puna");
        try
        {
            Context.Chatovi.Add(new Chat { Soba = soba, Korisnici = korisnik, Nadimak = nadimak, Boja = boja });
            await Context.SaveChangesAsync();
            return Ok("bravo");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [Route("PrebrojJedinstvene/{sobaId}")]
    [HttpGet]
    public async Task<IActionResult> PrebrojJedinstvene(int sobaId)
    {
        var korisnici = await Context.Chatovi
            .Where(p => p.Soba.Id == sobaId)
            .Include(p => p.Korisnici)
            .Select(p => p.Korisnici.Id)
            .ToListAsync();
        int brJedinstvenih = 0;
        foreach (var korisnik in korisnici)
            if (Context.Chatovi.Where(p => p.Korisnici.Id == korisnik).Count() == 1)
                brJedinstvenih++;
        return Ok(brJedinstvenih);
    }
}
