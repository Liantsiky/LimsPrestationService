using System.Text.Json.Serialization;
using LimsUtils.Utility;

namespace LimsPrestationService.Dto;

public class SortPrestationDto
{
    [JsonPropertyName("referenceFicheTravail")]
    public string? ReferenceFicheTravail { get; set; } = string.Empty;
    [JsonPropertyName("idEtatPrestation")]
    public int? IdEtatPrestation { get; set; }
    [JsonPropertyName("anneeExercice")]
    public int? AnneeExercice { get; set; }

    public void ApplyDefaultValues()
    {
        ReferenceFicheTravail ??= string.Empty;
        AnneeExercice ??= DateUtils.GetCurrentYear();
    }
}