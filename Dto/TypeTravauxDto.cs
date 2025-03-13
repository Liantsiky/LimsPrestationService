using System.Text.Json.Serialization;

namespace LimsPrestationService.Dto;

public class TypeTravauxDto
{
    [JsonPropertyName("idTypeTravaux")]
    public int IdTypeTravaux {get; set;}
    [JsonPropertyName("designation")]
    public string? Designation {get; set;}
}