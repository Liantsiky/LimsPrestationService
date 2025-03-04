using LimsPrestationService.Dto;
using LimsPrestationService.Models;

namespace LimsPrestationService.Services;

public interface IEchantillonService
{
    Echantillon FromDtotoEchantillon(string refClient, EchantillonDto echantillonDto, int idPrestation);
}