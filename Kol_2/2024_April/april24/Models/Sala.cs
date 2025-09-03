
namespace WebTemplate.Models;

[Table("Sala")]
public class Sala
{
    [Key]
    public int Id { get; set; }
    public string? Naziv { get; set; }
    public int BrojRedova { get; set; }
    public int BrojSedistaURedu { get; set; }

}