using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LimsPrestationService.Models;

[Table("Etat_decompte")]
public class EtatDecompte
{
    [Key]
    [Column("id_etat_decompte")]
    public int IdEtatDecompte {get; set;}
    [Column("reference")]
    public required string Reference {get; set;}
    [Column("date_etat_decompte")]
    public required DateOnly DateEtatDecompte {get; set;}
    [Column("id_prestation")]
    public int IdPrestation {get; set;}
    [Column("total_montant")]
    public decimal TotalMontant {get; set;} = 0;
    [Column("remise")]
    public double Remise {get;set;} = 0;
    [Column("total_montant_remise")]
    public decimal TotalMontantRemise {get; set;} = 0;
}