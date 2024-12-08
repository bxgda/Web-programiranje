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

    [HttpPost("DodajFakultet")]
    public async Task<IActionResult> DodajFakultet([FromBody] Fakultet f)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(f.BrojTelefona) || !f.BrojTelefona.All(char.IsDigit))
                return BadRequest("Broj telefona moze sadrzati samo cifre.");

            await Context.AddAsync(f);
            await Context.SaveChangesAsync();

            return Ok($"Uspesno dodat fakultet: {f.Naziv}, sa ID: {f.ID}.");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost("DodajStudenta")]
    public async Task<IActionResult> DodajStudenta([FromBody] StudentZaSlanje s)
    {
        try
        {
            var fakultet = await Context.Fakulteti.FindAsync(s.idFakulteta);

            if (fakultet == null)
                return NotFound("Fakultet ne postoji. Nije moguce dodatni studenta.");

            var student = new Student()
            {
                BrojIndeksa = s.BrojIndeksa,
                Ime = s.Ime,
                Prezime = s.Prezime,
                GodinaRodjenja = s.GodinaRodjenja,
                SrednjaSkola = s.SrednjaSkola,
                Fakultet = fakultet,
            };

            await Context.Studenti.AddAsync(student);
            await Context.SaveChangesAsync();

            return Ok($"Uspesno dodat student sa indeksom: {student.BrojIndeksa}.");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost("DodajSmer")]
    public async Task<IActionResult> DodajSmer(
        [FromQuery] int idFakulteta,
        [FromQuery] string naziv
    )
    {
        try
        {
            var fakultet = await Context.Fakulteti.FindAsync(idFakulteta);

            if (fakultet == null)
                return NotFound("Fakultet ne postoji. Nije moguce dodati smer.");

            var smer = new Smer() { Naziv = naziv, Fakultet = fakultet };

            await Context.Smerovi.AddAsync(smer);
            await Context.SaveChangesAsync();

            return Ok($"Uspesno dodat smer: {smer.Naziv}, sa ID: {smer.ID}.");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost("UpisiStudenta")]
    public async Task<IActionResult> UpisiStudenta([FromBody] UpisZaSlanje u)
    {
        try
        {
            var student = await Context.Studenti.FindAsync(u.idStudenta);

            if (student == null)
                return NotFound("Student ne postoji.");

            var fakultet = await Context.Fakulteti.FindAsync(u.idFakulteta);

            if (fakultet == null)
                return NotFound("Fakultet ne postoji.");

            var smer = await Context.Smerovi.FindAsync(u.idSmera);

            if (smer == null)
                return NotFound("Smer ne postoji.");

            var upis = new Upis()
            {
                DatumUpisa = u.DatumUpisa,
                ESPB = u.ESPB,
                Student = student,
                Smer = smer,
                Fakultet = fakultet,
            };

            student.Smer = smer;

            await Context.Upisi.AddAsync(upis);
            await Context.SaveChangesAsync();

            return Ok(
                $"Student {student.BrojIndeksa} {student.Ime} {student.Prezime} je uspesno upisan na smer {smer.Naziv} na fakultet {fakultet.Naziv}."
            );
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut("IzmeniStudenta/{idStudenta}")]
    public async Task<IActionResult> IzmeniStudenta([FromBody] StudentZaIzmenu s, int idStudenta)
    {
        try
        {
            var stariStudent = await Context.Studenti.FindAsync(idStudenta);

            if (stariStudent == null)
                return NotFound("Student ne postoji.");

            var noviFakultet = await Context.Fakulteti.FindAsync(s.idFakulteta);

            if (noviFakultet == null)
                return NotFound("Fakultet ne postoji.");

            var noviSmer = await Context.Smerovi.FindAsync(s.idSmera);

            if (noviSmer == null)
                return NotFound("Smer ne postoji.");

            stariStudent.Ime = s.Ime;
            stariStudent.Prezime = s.Prezime;
            stariStudent.GodinaRodjenja = s.GodinaRodjenja;
            stariStudent.SrednjaSkola = s.SrednjaSkola;
            stariStudent.Fakultet = noviFakultet;
            stariStudent.Smer = noviSmer;

            await Context.SaveChangesAsync();

            return Ok($"Student sa indeksom {stariStudent.BrojIndeksa} uspesno izmenjen.");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("VratiStudenteSmeraUPeriodu")]
    public async Task<IActionResult> VratiStudenteSmeraUPeriodu(
        [FromQuery] DateTime startDate,
        [FromQuery] DateTime endDate,
        [FromQuery] int idSmera
    )
    {
        try
        {
            var smer = await Context.Smerovi.FindAsync(idSmera);

            if (smer == null)
                return NotFound("Smer ne postoji.");

            var studenti = await Context
                .Upisi.Include(u => u.Smer)
                .Where(u =>
                    u.Smer!.ID == idSmera && u.DatumUpisa >= startDate && u.DatumUpisa <= endDate
                )
                .Select(u => new
                {
                    u.Student!.BrojIndeksa,
                    u.Student.Ime,
                    u.Student.Prezime,
                    u.Student.GodinaRodjenja,
                })
                .ToListAsync();

            return Ok(studenti);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("ProsecniESPBZaSmer")]
    public async Task<IActionResult> ProsecniESPBZaSmer([FromQuery] int idSmera)
    {
        try
        {
            var smer = await Context.Smerovi.FindAsync(idSmera);

            if (smer == null)
                return NotFound("Smer ne postoji.");

            var prosecniESPB = await Context
                .Upisi.Where(u => u.Smer!.ID == idSmera)
                .AverageAsync(u => u.ESPB);

            return Ok(prosecniESPB);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
