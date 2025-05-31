using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace LimsPrestationService.Models;

[Table("Travail")]
public class Travail
{   
    [Key]
    [Column("id_travail")]
    public int IdTravail { get; set; }
    [Column("prix_unitaire")]
    public decimal PrixUnitaire { get; set; } = 0; //TODO : Trigger a prepare mais d'abord s'occuper de l'insertion
    [Column("id_type_travaux")]
    public required int IdTypeTravaux { get; set; }
    [Column("id_avancee_travail")]
    public int IdAvanceeTravail { get; set; } = 1;
    [ForeignKey("IdAvanceeTravail")]
    public AvanceeTravail? AvanceeTravail { get; set; }
    [Column("id_echantillon")]
    public required int IdEchantillon { get; set; }
    [ForeignKey("IdEchantillon")]
    [JsonIgnore]
    public Echantillon? Echantillon { get; set; }
}