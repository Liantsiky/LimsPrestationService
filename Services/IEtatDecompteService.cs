using LimsPrestationService.Dto;
using LimsPrestationService.Models;

namespace LimsPrestationService.Services;

public interface IEtatDecompteService
{
    EtatDecompte FromDtotoEtatDecompte(PrestationDto prestationDto, Prestation prestation);
}