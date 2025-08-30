using decembar2023.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace decembar2023.Controllers
{
    // prepravljeno da vraca odgovor kao json
  
    [ApiController]
    [Route("[controller]")]
    public class Controller : ControllerBase
    {
        private readonly Context context;

        public Controller(Context context_)
        {
            context = context_;
        }

        [HttpGet("VratiProdavnice")]
        public async Task<ActionResult> VratiProdavnice()
        {
            try
            {
                var prodavnice = await context.Prodavnice.Include(p => p.ListaProizvoda)
                .ToListAsync();

                if (prodavnice == null)
                {
                    return BadRequest("doslo je do greske priliikom izvrsenja upita");
                }

                if (prodavnice.Count == 0)
                {
                    return BadRequest("Nema prodavnica u bazi!");
                }

                return Ok(prodavnice);
            }
            catch (Exception e)
            {
                return BadRequest($"Poruka o gresci: {e.Message}");
            }
        }

        [HttpPut("DodajProizvod/{naziv}/{kategorija}/{cena}/{kolicina}/{nazivProdavnice}")]
        public async Task<ActionResult> DodajProizvod(string naziv, string kategorija, int cena, int kolicina, string nazivProdavnice)
        {
            try
            {
                var prodavnica = await context.Prodavnice.Where(p => p.Naziv.Trim().ToLower() == nazivProdavnice.Trim().ToLower())
                .FirstOrDefaultAsync();

                if (prodavnica != null)
                {
                    if (prodavnica.ListaProizvoda!.Count < Prodavnica.MaxKolicina)
                    {
                        var proizvod = new Proizvod
                        {
                            Naziv = naziv,
                            Kategorija = kategorija,
                            Cena = cena,
                            DostupnaKolicina = kolicina
                        };

                        await context.Proizvodi.AddAsync(proizvod);
                        prodavnica.ListaProizvoda.Add(proizvod);
                        await context.SaveChangesAsync();

                        return Ok($"Uspesno dodat proizvod: ({naziv}) u prodavnici: ({nazivProdavnice})");
                    }
                    else
                    {
                        return BadRequest("Kapacitet prodavnice popunjen!");
                    }
                }

                return BadRequest($"Prodavnica {nazivProdavnice} ne postoji u bazi podataka!");
            }
            catch (Exception e)
            {
                return BadRequest($"Poruka o gresci: {e.Message}");
            }
        }

        [HttpPost("ProdajProizvod/{nazivProdavnice}/{nazivProizvoda}/{kupljenaKolicina}")]
        public async Task<ActionResult> ProdajProizvod(string nazivProdavnice, string nazivProizvoda, int kupljenaKolicina)
        {
            try
            {
                var proizvod = await context.Prodavnice
                .Where(p => p.Naziv.Trim() == nazivProdavnice.Trim())
                .SelectMany(p => p.ListaProizvoda!)
                .FirstOrDefaultAsync(p => p.Naziv.Trim().ToLower() == nazivProizvoda.Trim().ToLower());

                if (proizvod == null)
                {
                    return BadRequest(new { error = $"Ne postoji taj proizvod", });
                }

                if (proizvod.DostupnaKolicina < kupljenaKolicina)
                {
                    return BadRequest(new { error = $"Nema toliko proizvoda", });
                }

                proizvod.DostupnaKolicina -= kupljenaKolicina;
                await context.SaveChangesAsync();

                return Ok(new {kupljenaKolicina = proizvod.DostupnaKolicina});
            }
            catch (Exception e)
            {
                return BadRequest($"Poruka o gresci: {e.Message}");
            }            
        }
    }
}