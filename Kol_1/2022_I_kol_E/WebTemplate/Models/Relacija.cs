namespace WebTemplate.Models;

public class Relacija
{
    [Key]
    public int ID { get; set; }

    public uint BrojPutnika { get; set; }

    public uint CenaKarte { get; set; }

    public DateTime Datum { get; set; }

    public Voz? Voz { get; set; }

    // dve veze 1 na N -> mora onModelCreating -> vidi IspitContext
    public Grad? PolazniGrad { get; set; }

    public Grad? DolazniGrad { get; set; }
}
