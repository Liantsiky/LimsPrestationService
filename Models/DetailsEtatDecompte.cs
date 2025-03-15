using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LimsPrestationService.Models;

[Table("Details_etat_decompte")]
public class DetailsEtatDecompte
{
    [Key]
    [Column("id_details_etat_decompte")]
    public int IdDetailsEtatDecompte { get; set; }
    [Column("nombre")]
    public int Nombre { get; set; }
    [Column("prix_unitaire")]
    public decimal PrixUnitaire { get; set; }
    [Column("montant_total")]
    public decimal MontantTotal { get; set; }
    [Column("id_type_travaux")]
    public int IdTypeTravaux { get; set; }
    [ForeignKey("IdTypeTravaux")]
    public TypeTravaux? TypeTravaux { get; set; }
    [Column("id_etat_decompte")]
    public int IdEtatDecompte { get; set; }
    [ForeignKey("IdEtatDecompte")]
    public EtatDecompte? EtatDecompte { get; set; }
}