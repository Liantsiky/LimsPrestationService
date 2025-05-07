using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace LimsPrestationService.Models;

[Table("Etat_decompte")]
public class EtatDecompte
{
    [Key]
    [Column("id_etat_decompte")]
    public int IdEtatDecompte { get; set; }
    [Column("reference")]
    public required string Reference { get; set; }
    [Column("date_etat_decompte")]
    public required DateOnly DateEtatDecompte { get; set; }
    [Column("id_prestation")]
    public int IdPrestation { get; set; }
    [ForeignKey("IdPrestation")]
    [JsonIgnore]
    public Prestation? Prestation { get; set; }
    [Column("total_montant")]
    public decimal TotalMontant { get; set; } = 0;
    [Column("remise")]
    public required double Remise { get; set; }
    [Column("total_montant_remise")]
    public decimal TotalMontantRemise { get; set; } = 0;

    public ICollection<VDetailsEtatDecompte> DetailsEtatDecomptes { get; set; } = new List<VDetailsEtatDecompte>();
}