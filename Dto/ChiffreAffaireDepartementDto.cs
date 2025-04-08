using System.Text.Json.Serialization;
using LimsPrestationService.Models;

namespace LimsPrestationService.Dto;

public class ChiffreAffaireDepartementDto
{
    [JsonPropertyName("idDepartement")]
    public int? IdDepartement { get; set; }
    [JsonPropertyName("designation")]
    public string? Designation { get; set; }
    [JsonPropertyName("chiffreAffaires")]
    public ICollection<ChiffreAffaire> ChiffreAffaires { get; set; } = new List<ChiffreAffaire>();
}