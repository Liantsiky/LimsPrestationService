using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LimsUtils.Utility;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace LimsPrestationService.Models;

[Table("Prestation")]
public class Prestation
{

    public string LoadEtatDecompteContent(string content)
    {
        string result = content;
        string travaux = string.Empty;
        string devis = string.Empty;
        string cin = Client?.Cin ?? "";
        if(EtatDecompte == null) throw new Exception("Etat de décompte n'existe pas");
        foreach(VDetailsEtatDecompte typetravail in EtatDecompte.DetailsEtatDecomptes)
        {
            travaux += "<p>"+typetravail.Designation+"</p>";
            devis += $"<tr> <td>{typetravail.Designation}</td> <td>UN</td>";
            devis += $"<td>{typetravail.Nombre}</td>  <td>{ ViewUtils.RenderMoney(typetravail.PrixUnitaire)}</td>";
            devis += $" <td>{ ViewUtils.RenderMoney(typetravail.MontantTotal)}</td> </tr>";
        }
        result = content.Replace("#Travaux#", travaux);
        result = result.Replace("#Devis#", devis);

        if(string.IsNullOrEmpty(Client?.Cin))
        {
            cin = Client?.NifStat ?? "";
        }
        result = result.Replace("#Cin#", cin);

        return result;
    }

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
    public EtatDecompte? EtatDecompte { get; set; }

    public ICollection<VPrestationDetails> PrestationDetails { get; set; } = new List<VPrestationDetails>();
    
}