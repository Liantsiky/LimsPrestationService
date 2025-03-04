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
    [Column("date_prestation")]
    public required DateOnly DatePrestation { get; set; }
    [Column("date_cloture")]
    public DateOnly? DateCloture { get; set; }
    [Column("id_etat_prestation")]
    public int IdEtatPrestation { get; set; } = 1;
    [ForeignKey("IdEtatPrestation")]
    public EtatPrestation? EtatPrestation { get; set; }
    [Column("id_client")]
    public int IdClient { get; set; }
    [ForeignKey("IdClient")]
    public Client? Client { get; set; }
    [Column("statut_paiement")]
    public int StatutPaiement { get; set; } = 1;

    public ICollection<VPrestationDetails> PrestationDetails { get; set; } = new List<VPrestationDetails>();
    
}