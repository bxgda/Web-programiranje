using Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure;

namespace WebTemplate.Models;

public class Student
{
    [Key]
    public int ID { get; set; }

    public required int BrojIndeksa { get; set; }

    [MaxLength(25)]
    public required string Ime { get; set; }

    [MaxLength(25)]
    public required string Prezime { get; set; }

    public int GodinaRodjenja { get; set; }

    public string? SrednjaSkola { get; set; }

    public Fakultet? Fakultet { get; set; }

    public Smer? Smer { get; set; }
}
