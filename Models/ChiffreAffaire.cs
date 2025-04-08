namespace LimsPrestationService.Models;

public class ChiffreAffaire
{
    public int? Annee { get; set;}
    public int? Mois { get; set;}
    public int? Jour { get; set; }
    public decimal? Montant { get; set; }

    // Tri par departement
    public int? IdDepartement { get; set; }
    // Designation departement
    public string? Designation{ get; set; }
}