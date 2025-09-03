using System.ComponentModel.DataAnnotations;

namespace Akvarijum.Models;

public class Rezervoar
{
    public int ID { get; set; }
    [MinLength(6)]
    [MaxLength(6)]
    public required string Sifra { get; set; }
    public double Zapremina { get; set; }
    public double Temperatura { get; set; }
    public DateTime PoslednjeCiscenje { get; set; }
    public int FrekvencijaCiscenja { get; set; }
    public int Kapacitet { get; set; }
    public List<RibaURezervoaru>? Ribe { get; set; }
}
