using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LimsPrestationService.Models;

[Table("Client")]
[Index(nameof(Email), IsUnique = true)]
[Index(nameof(Cin), IsUnique = true)]
[Index(nameof(Passeport), IsUnique = true)]
[Index(nameof(Contact), IsUnique = true)]
public class Client
{
    [Key]
    [Column("id_client")]
    public int IdClient { get; set; }
    [Column("nom")]
    public required string Nom { get; set; }
    [Column("adresse")]
    public required string Adresse { get; set; }
    [Column("cin")]
    public string? Cin { get; set; }
    [Column("passeport")]
    public string? Passeport { get; set; }
    [Column("contact")]
    public required string Contact { get; set; }
    [Column("email")]
    public required string Email { get; set; }
    [Column("fax")]
    public string? Fax { get; set; }
    [Column("isInterne")]
    public int IsInterne { get; set; }
    [Column("ref_contrat")]
    public string? RefContrat { get; set; }
    [Column("nifstat")]
    public string? NifStat { get; set; }
}