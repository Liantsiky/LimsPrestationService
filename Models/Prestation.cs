using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LimsUtils.Utility;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace LimsPrestationService.Models;

[Table("Prestation")]
public class Prestation
{

    public string LoadFicheTravailContent(string content)
    {
        string result = content;
        result = result.Replace("#FicheDeTravail#", ReferenceFicheTravail)
            .Replace("#Nom#", Client?.Nom)
            .Replace("#Contact#", Client?.Contact)
            .Replace("#Adresse#", Client?.Adresse)
            .Replace("#Email#", Client?.Email)
            .Replace("#cin#", Client?.Cin)
            .Replace("#Fax#", Client?.Fax)
            .Replace("#NifStat#", Client?.Nif ?? Client?.Stat ?? "");
        string typeEchantillons= string.Empty;
        string provenances = string.Empty;
        VPrestationDetails?[] echantillons = PrestationDetails
            .GroupBy(te => te!.TypeEchantillon!.Designation)
            .Select(te => te.First())
            .ToArray();
        foreach(VPrestationDetails? echantillon in  echantillons)
        {
            typeEchantillons += echantillon!.TypeEchantillon!.Designation+", ";
            provenances += echantillon!.Echantillon!.Provenance+", ";
        }
        result = result.Replace("#TypeEchantilon#", typeEchantillons.Substring(0, typeEchantillons.Length - 2));
        result = result.Replace("#Provenance#", provenances.Substring(0, provenances.Length - 2));
        string travaux = string.Empty;
        foreach(VDetailsEtatDecompte travail in EtatDecompte!.DetailsEtatDecomptes)
        {
            travaux += "<p>"+travail.Designation+"</p>";
        }
        result = result.Replace("#Travaux#", travaux);
        result = result.Replace("#DatePrestation#", DatePrestation.ToString());
        
        return result;
    }

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
            cin = Client?.Nif ?? Client?.Stat ?? "";
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