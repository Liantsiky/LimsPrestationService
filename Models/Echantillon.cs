using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LimsPrestationService.Models;

[Table("Echantillon")]
public class Echantillon
{
    [Key]
    [Column("id_echantillon")]
    public int IdEchantillon { get; set; }
    [Column("reference")]
    public required string Reference { get; set; }
    [Column("reference_client")]
    public required string ReferenceClient { get; set;}
    [Column("note")]
    public string? Note { get; set; }
    [Column("id_prestation")]
    public int IdPrestation { get; set; }
    [ForeignKey("IdPrestation")]
    public Prestation? Prestation { get; set; }
    [Column("id_type_echantillon")]
    public int IdTypeEchantillon { get; set; }
    [ForeignKey("IdTypeEchantillon")]
    public TypeEchantillon? TypeEchantillon { get; set; }
    [Column("provenance")]
    public string? Provenance { get; set; }
    [Column("date_prelevement")]
    public DateOnly? DatePrelevement { get; set; } 
    public ICollection<Travail> Travails { get; set; } = new List<Travail>();

    // For details Echantillon
    public ICollection<VDetailsEchantillon> DetailsEchantillons { get; set; } = new List<VDetailsEchantillon>();
    
}