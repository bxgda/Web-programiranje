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

    [HttpPost("DodajAutora")]
    public async Task<IActionResult> DodajAutora([FromBody] Autor autor)
    {
        try
        {
            await Context.Autori.AddAsync(autor);
            await Context.SaveChangesAsync();

            return Ok($"Dodat autor {autor.Ime} {autor.Prezime} sa ID: {autor.ID}");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost("DodajKnjigu")]
    public async Task<IActionResult> DodajKnjigu([FromBody] KnjigaZaSlanje k)
    {
        try
        {
            if (k.ISBN.Length != 13)
                return BadRequest("ISBN mora da ima tacno 13 karaktera.");

            var autor = await Context.Autori.FindAsync(k.AutorID);

            if (autor == null)
                return NotFound($"Autor sa ID: {k.AutorID} ne postoji.");

            var knjiga = new Knjiga()
            {
                Naziv = k.Naziv,
                DatumObjavljivanja = k.DatumObjavljivanja,
                BrojStranica = k.BrojStranica,
                Zanr = k.Zanr,
                ISBN = k.ISBN,
                Autor = autor,
            };

            await Context.Knjige.AddAsync(knjiga);
            await Context.SaveChangesAsync();

            return Ok(
                $"Dodata knjiga {knjiga.Naziv} ciji je autor {autor.Ime} {autor.Prezime} sa ID: {knjiga.ID}"
            );
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost("DodajIzdavaca/{naziv}/{drzava}")]
    public async Task<IActionResult> DodajIzdavaca(string naziv, string drzava)
    {
        try
        {
            var izdavac = new IzdavackaKuca() { Naziv = naziv, Drzava = drzava };

            await Context.IzdavackeKuce.AddAsync(izdavac);
            await Context.SaveChangesAsync();

            return Ok($"Dodata izdavacka kuca {izdavac.Naziv} iz drzave: {izdavac.Drzava}");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut("IzmeniKnjigu/{idKnjige}")]
    public async Task<IActionResult> IzmeniKnjigu([FromBody] KnjigaZaPromenu k, int idKnjige)
    {
        try
        {
            var staraKnjiga = await Context.Knjige.FindAsync(idKnjige);

            if (staraKnjiga == null)
                return NotFound($"Knjiga sa ID: {idKnjige} nije nadjena.");

            staraKnjiga.Naziv = k.Naziv;
            staraKnjiga.BrojStranica = k.BrojStranica;
            staraKnjiga.Zanr = k.Zanr;
            staraKnjiga.DatumObjavljivanja = k.DatumObjavljivanja;

            Context.Knjige.Update(staraKnjiga);
            await Context.SaveChangesAsync();

            return Ok($"Uspesno izmenjena knjiga sa ID: {idKnjige}");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("ObrisiKnjigu/{idKnjige}")]
    public async Task<IActionResult> ObrisiKnjigu(int idKnjige)
    {
        try
        {
            var knjiga = await Context.Knjige.FindAsync(idKnjige);

            if (knjiga == null)
                return NotFound($"Knjiga sa ID: {idKnjige} ne postoji.");

            Context.Knjige.Remove(knjiga);
            await Context.SaveChangesAsync();

            return Ok($"Uspesno obrisana knjiga sa ID: {idKnjige}.");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost("DodajUgovor")]
    public async Task<IActionResult> DodajUgovor([FromBody] UgovorZaSlanje u)
    {
        try
        {
            var autor = await Context.Autori.FindAsync(u.AutorID);
            var knjiga = await Context.Knjige.FindAsync(u.KnjigaID);
            var izdavackaKuca = await Context.IzdavackeKuce.FindAsync(u.IzdavackaKucaID);

            if (autor == null || knjiga == null || izdavackaKuca == null)
                return NotFound("Autor ili knjiga ili izdavacka kuca nije pronadjen...");

            var ugovor = new Ugovor()
            {
                DatumPotpisivanja = u.DatumPotpisivanja,
                BrojUgovora = u.BrojUgovora,
                Autor = autor,
                Knjiga = knjiga,
                IzdavackaKuca = izdavackaKuca,
            };

            await Context.Ugovori.AddAsync(ugovor);
            await Context.SaveChangesAsync();

            return Ok($"Uspesno dodat ugovor sa ID: {ugovor.ID}");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("VratiKnjigeUPeriodu/{datumOd}/{datumDo}")]
    public async Task<IActionResult> VratiKnjigeUPeriodu(DateTime datumOd, DateTime datumDo)
    {
        try
        {
            var knjige = await Context
                .Ugovori.Include(u => u.Knjiga)
                .Where(u => u.DatumPotpisivanja >= datumOd && u.DatumPotpisivanja <= datumDo)
                .Select(u => new
                {
                    u.Knjiga!.ISBN,
                    u.Knjiga!.Naziv,
                    u.Knjiga!.DatumObjavljivanja,
                    u.Knjiga!.BrojStranica,
                    u.Knjiga!.Zanr,
                })
                .ToListAsync();

            return Ok(knjige);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("VratiPeriod/{idIzdavaca}")]
    public async Task<IActionResult> VratiPeriod(int idIzdavaca)
    {
        try
        {
            var prvi = await Context
                .Ugovori.Where(u => u.IzdavackaKuca!.ID == idIzdavaca)
                .OrderBy(u => u.DatumPotpisivanja)
                .Select(u => u.DatumPotpisivanja)
                .FirstOrDefaultAsync();

            var poslednji = await Context
                .Ugovori.Where(u => u.IzdavackaKuca!.ID == idIzdavaca)
                .OrderByDescending(u => u.DatumPotpisivanja)
                .Select(u => u.DatumPotpisivanja)
                .FirstOrDefaultAsync();

            return Ok(new { PrviDatum = prvi, PoslednjiDatum = poslednji });
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
