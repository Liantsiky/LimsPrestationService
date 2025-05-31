using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace LimsPrestationService.Models;

[Table("v_prestation_details")]
public class VPrestationDetails
{
    [Key]
    [Column("id_travail")]
    public int IdTravail { get; set; }
    [ForeignKey("IdTravail")]
    public Travail? Travail { get; set;}
    [Column("id_prestation")]
    public int IdPrestation { get; set; }
    [JsonIgnore]
    [ForeignKey("IdPrestation")]
    public Prestation? Prestation { get; set; }
    [Column("reference_client")]
    public required string ReferenceClient { get; set; }
    [Column("reference")]
    public required string Reference { get; set; }
    [Column("reference_fiche_travail")]
    public required string ReferenceFicheTravail { get; set; }
    [Column("designation")]
    public required string Designation { get; set; }
    [Column("tarif")]
    public required decimal Tarif { get; set; }
    [Column("id_type_echantillon")]
    public int IdTypeEchantillon { get; set; }
    [ForeignKey("IdTypeEchantillon")]
    public TypeEchantillon? TypeEchantillon { get; set; }
    [Column("id_echantillon")]
    public int IdEchantillon { get; set; }
    [ForeignKey("IdEchantillon")]
    public Echantillon? Echantillon { get; set; }
    
}