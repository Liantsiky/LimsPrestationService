using System.Text.Json.Serialization;

namespace LimsPrestationService.Dto;
public class PrestationDto
{
    [JsonPropertyName("datePrestation")]
    public DateOnly DatePrestation { get; set; }
    [JsonPropertyName("idClient")]
    public int IdClient { get; set; }
    [JsonPropertyName("echantillons")]
    public Dictionary<string,EchantillonDto> Echantillons {get; set;} = new Dictionary<string,EchantillonDto>();
    [JsonPropertyName("travaux")]
    public Dictionary<string,List<int>> Travaux { get; set; } = new Dictionary<string,List<int>>();
}