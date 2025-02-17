using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LimsPrestationService.Models;

[Table("Client")]
public class Client
{
    [Key]
    [Column("id_client")]
    public int IdClient { get; set; }
    [Column("nom")]
    public string Nom { get; set; }
    [Column("adresse")]
    public string Adresse { get; set; }
    [Column("cin")]
    public string? Cin { get; set; }
    [Column("passeport")]
    public string? Passeport { get; set; }
    [Column("contact")]
    public string Contact { get; set; }
    [Column("email")]
    public string Email { get; set; }
    [Column("fax")]
    public string? Fax { get; set; }
    [Column("isInterne")]
    public int IsInterne { get; set; }
    [Column("ref_contrat")]
    public string? RefContrat { get; set; }
    [Column("nifstat")]
    public string? NifStat { get; set; }
}