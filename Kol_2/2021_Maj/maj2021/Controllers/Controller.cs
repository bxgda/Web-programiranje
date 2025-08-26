using maj2021.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace maj2021.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Controller : ControllerBase
    {
        private readonly Context context;

        public Controller(Context context_)
        {
            context = context_;
        }

        [HttpPut("IzmeniKolicinu/{fabrika}/{oznaka}/{novaKolicina}")]
        public async Task<ActionResult> IzmeniKolicinu(string fabrika, string oznaka, int novaKolicina)
        {
            try
            {
                var factory = await context.Fabrike
                .Include(f => f.ListaSilosa)
                .Where(f => f.Naziv.ToLower().Trim() == fabrika.ToLower().Trim())
                .FirstOrDefaultAsync();

                if (factory == null)
                {
                    return BadRequest(new {
                        error = $"Ne postoji fabrika {fabrika} u bazi!",
                    });
                }

                var silos = factory.ListaSilosa!.Find(s => s.Oznaka.Trim().ToLower() == oznaka.Trim().ToLower());

                if (silos == null)
                {
                    return BadRequest(new {
                        error = $"Ne postoji silos {oznaka} u bazi",
                    });
                }

                if (silos.TrenutnaKolicina + novaKolicina > silos.MaxKapacitet)
                {
                    return BadRequest(new {
                        error = "Nije moguce dodati navedenu kolicinu!",
                    });
                }

                silos.TrenutnaKolicina += novaKolicina;
                await context.SaveChangesAsync();

                return Ok(new { novaKolicina = silos.TrenutnaKolicina });
            }
            catch (Exception e)
            {
                return BadRequest(new {
                    error = $"Greska -> {e.Message}",
                });
            }
        }

        [HttpGet("VratiSilose/{fabrika}")]
        public async Task<ActionResult> VratiSilose(string fabrika)
        {
            try
            {
                var silosi = await context.Fabrike
                .Where(f => f.Naziv.ToLower().Trim() == fabrika.ToLower().Trim())
                .Select(f => new
                {
                    Silosi = f.ListaSilosa
                })
                .FirstOrDefaultAsync();

                if (silosi == null)
                {
                    return BadRequest($"Ne postoji fabrika {fabrika}!");
                }

                return Ok(silosi);
            }
            catch (Exception e)
            {
                return BadRequest($"Greska -> {e.Message}");
            }
        }

        [HttpGet("VratiFabrike")]
        public async Task<ActionResult> VratiFabrike()
        {
            try
            {
                var fabrike = await context.Fabrike.Include(f => f.ListaSilosa).ToListAsync();

                return Ok(fabrike);
            }
            catch (Exception e)
            {
                return BadRequest($"Greska -> {e.Message}");
            }
        }
    }
}