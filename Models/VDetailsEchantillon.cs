using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace LimsPrestationService.Models;

[Table("v_details_echantillon")]
public class VDetailsEchantillon
{
    [Key]
    [Column("id_travail")]
    public int IdTravail { get; set; }
    [Column("id_echantillon")]
    public int IdEchantillon { get; set; }
    [ForeignKey("IdEchantillon")]
    [JsonIgnore]
    public Echantillon? Echantillon { get; set; }
    [Column("travaux")]
    public required string Travaux { get; set; }
    [Column("avancee")]
    public required string Avancee { get; set; }
}