using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LimsPrestationService.Models;

[Table("Prestation")]
public class Prestation
{
    [Key]
    [Column("id_prestation")]
    public int IdPrestation { get; set; }
    [Column("reference_fiche_travail")]
    public required string ReferenceFicheTravail { get; set; }
    [Column("total_montant")]
    public required double TotalMontant { get; set; }
    [Column("remise")]
    public required double Remise { get; set; }
    [Column("date_prestation")]
    public required DateTime DatePrestation { get; set; }
    [Column("date_cloture")]
    public DateTime? DateCloture { get; set; }
    [Column("id_etat_prestation")]
    public int IdEtatPrestation { get; set; }
    [ForeignKey("IdEtatPrestation")]
    public EtatPrestation? EtatPrestation { get; set; }
    [Column("id_client")]
    public int IdClient { get; set; }
    [ForeignKey("IdClient")]
    public Client? Client { get; set; }
    [Column("statut_paiement")]
    public int StatutPaiement { get; set; }
    
}