using LimsPrestationService.Dto;
using LimsPrestationService.Models;

namespace LimsPrestationService.Services;

public interface ITravailService
{
    List<Travail> FromDtotoTravaux(EchantillonDto echantillonDto, Echantillon echantillon);
}