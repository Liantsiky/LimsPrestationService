using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LimsPrestationService.Models;

[Table("v_details_etat_decompte")]
public class VDetailsEtatDecompte
{
    [Key]
    [Column("id_details_etat_decompte")]
    public int IdDetailsEtatDecompte { get; set; }
    [Column("designation")]
    public required string Designation { get; set; }
    [Column("nombre")]
    public int Nombre { get; set; }
    [Column("prix_unitaire")]
    public required decimal PrixUnitaire { get; set; }
    [Column("montant_total")]
    public required decimal MontantTotal { get; set; }
    [Column("id_etat_decompte")]
    public int IdEtatDecompte { get; set; }
    [ForeignKey("IdEtatDecompte")]
    public EtatDecompte? EtatDecompte { get; set; }
}