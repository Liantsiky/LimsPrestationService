using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LimsPrestationService.Models;

[Table("v_prestation_etat_decompte")]
public class VPrestationEtatDecompte
{
    [Key]
    [Column("id_prestation")]
    public int IdPrestation { get; set; }
    [Column("date_prestation")]
    public required DateTime DatePrestation { get; set; }
    [Column("reference")]
    public required string Reference { get; set; }
    [Column("montant")]
    public required double Montant { get; set; }
    [Column("remise")]
    public required double Remise { get; set; }
    [Column("montant_total")]
    public required double MontantTotal { get; set; }
}