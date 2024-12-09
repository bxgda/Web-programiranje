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

    [HttpPost("DodajNumeru")]
    public async Task<IActionResult> DodajNumeru([FromBody] Numera numera)
    {
        try
        {
            await Context.Numere.AddAsync(numera);
            await Context.SaveChangesAsync();

            return Ok($"Uspesno dodata numera {numera.Naziv} sa ID: {numera.ID}");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost("DodajAutora")]
    public async Task<IActionResult> DodajAutora([FromBody] Autor autor)
    {
        try
        {
            await Context.Autori.AddAsync(autor);
            await Context.SaveChangesAsync();

            return Ok($"Uspesno dodat autor {autor.Ime} sa ID: {autor.ID}");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost("DodajAlbum")]
    public async Task<IActionResult> DodajAlbum(
        [FromBody] Album album,
        [FromQuery] int idAutora,
        [FromQuery] int[] idNumera
    )
    {
        try
        {
            var autor = await Context.Autori.FindAsync(idAutora);

            if (autor == null)
                return NotFound("Autor ne postoji."); // 404

            if (autor.DatumPrvogAlbuma == null)
                autor.DatumPrvogAlbuma = new DateTime(album.GodinaIzdavanja, 1, 1);

            album.AutorAlbuma = autor;

            var numere = await Context.Numere.Where(n => idNumera.Contains(n.ID)).ToListAsync();
            foreach (Numera n in numere)
            {
                if (n.Album == null) // preskoci numere koje su vec na nekom drugom albumu
                {
                    n.Album = album;
                    Context.Numere.Update(n);
                }
            }

            await Context.Albumi.AddAsync(album);
            await Context.SaveChangesAsync();

            return Ok($"Uspesno dodat album sa ID: {album.ID}.");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("VratiAutore/{viseOdN}")]
    public async Task<IActionResult> VratiAutore(int viseOdN)
    {
        try
        {
            var autori = await Context
                .Autori.Where(a => (DateTime.Now.Year - a.DatumPrvogAlbuma!.Value.Year) > viseOdN)
                .ToListAsync();

            if (autori == null)
                return NotFound("Nema takvih");

            return Ok(autori);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("ObrisiAlbum/{idAlbuma}")]
    public async Task<IActionResult> ObrisiAlbum(int idAlbuma)
    {
        try
        {
            var album = await Context
                .Albumi.Include(a => a.NumereAlbuma)
                .Include(a => a.AutorAlbuma)
                .Where(a => a.ID == idAlbuma)
                .FirstOrDefaultAsync();

            if (album == null)
                return NotFound("Album nije pronadjen.");

            // Otkaci albume od numera
            foreach (Numera n in album.NumereAlbuma!)
            {
                n.Album = null;
                Context.Numere.Update(n);
            }

            // zapamti autora
            var autorAlbuma = album.AutorAlbuma;

            // brisi album
            // mora save changes jer remove samo markira a ne brise odma
            Context.Albumi.Remove(album);
            await Context.SaveChangesAsync();

            // sad nadjemo godinu izdavanja prvog albuma
            var godina = await Context
                .Albumi.Include(a => a.AutorAlbuma)
                .Where(a => a.AutorAlbuma!.ID == autorAlbuma!.ID)
                .OrderBy(a => a.GodinaIzdavanja)
                .Select(a => a.GodinaIzdavanja)
                .FirstOrDefaultAsync();

            autorAlbuma!.DatumPrvogAlbuma = new DateTime(godina, 1, 1);
            Context.Autori.Update(autorAlbuma);

            await Context.SaveChangesAsync();

            return Ok($"Uspesno obrisan album sa ID: {album.ID}.");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
