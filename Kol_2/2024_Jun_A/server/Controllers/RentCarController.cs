using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using rent_a_car.Models;

namespace rent_a_car.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RentCarController : ControllerBase
    {
        private readonly RentCarContext context;

        public RentCarController(RentCarContext _context) => context = _context;

        [HttpPost("IznajmiAutomobil")]
        public async Task<ActionResult> IznajmiAutomobil([FromBody] IznajmljivanjeAuta ia)
        {
            try
            {
                var k = new Korisnik
                {
                    Ime = ia.Ime,
                    Prezime = ia.Prezime,
                    JMBG = ia.Jmbg,
                    BrojVozackeDozvole = ia.BrVozacke
                };

                if (k == null)
                {
                    return BadRequest("Nevalidan korisnik!");
                }

                var auto = await context.Automobili
                .Where(a => a.Model == ia.Model && a.Godiste == ia.Godiste && a.PredjenaKilometraza == ia.Kilometraza)
                .FirstOrDefaultAsync();

                if (auto == null)
                {
                    return BadRequest("Auto ne postoji!");
                }

                var ugovor = new Iznajmljivanje
                {
                    Automobil = auto,
                    Korisnik = k,
                    BrojDana = ia.BrDana
                };

                await context.Korisnici.AddAsync(k);

                k.UgovorIznajmljivanja!.Add(ugovor);
                auto.UgovorIznajmljivanja!.Add(ugovor);
                auto.Iznajmljen = true;

                await context.UgovoriIznajmljivanja.AddAsync(ugovor);
                await context.SaveChangesAsync();

                return Ok("Automobil uspesno iznajmljen!");
            }
            catch (Exception e)
            {
                return BadRequest("Poruka o gresci " + e.Message);
            }
        }

        [HttpGet("Filtriraj")]
        public async Task<ActionResult> Filtriraj([FromQuery] Filtriranje f)
        {
            try
            {
                var upit = context.Automobili.AsQueryable();

                if (!string.IsNullOrEmpty(f.Model))
                {
                    upit = upit.Where(auto => auto.Model.Trim().ToLower() == f.Model.Trim().ToLower());
                }
                if (f.Godiste > 0)
                {
                    upit = upit.Where(auto => auto.Godiste == f.Godiste);
                }
                if (f.Kilometraza > 0)
                {
                    upit = upit.Where(auto => auto.PredjenaKilometraza == f.Kilometraza);
                }

                var automobili = await upit
                .Select(a => new
                {
                    a.AutomobilID,
                    a.Model,
                    a.PredjenaKilometraza,
                    a.Godiste,
                    a.CenaPoDanu,
                    a.Iznajmljen
                })
                .ToListAsync();

                return Ok(automobili);
            }
            catch (Exception e)
            {
                return BadRequest("Poruka o gresci " + e.Message);
            }
        }

        [HttpGet("PrikaziAutomobile")]
        public async Task<ActionResult> PrikaziAutomobile()
        {
            try
            {
                var automobili = await context.Automobili.ToListAsync();

                return Ok(automobili);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}