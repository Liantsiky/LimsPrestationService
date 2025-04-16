using System.Text.Json.Serialization;

namespace LimsPrestationService.Models;

public class ChiffreAffaireInterneExterne
{
    [JsonPropertyName("isInterne")]
    public int? isInterne { get; set; }
    [JsonPropertyName("montant")]
    public decimal? Montant { get; set; }
    [JsonPropertyName("annee")]
    public int? Annee { get; set; }
    [JsonPropertyName("mois")]
    public int? Mois { get; set; }
}