using LimsPrestationService.Dto;
using LimsPrestationService.Models;

namespace LimsPrestationService.Services;

public class TravailService : ITravailService
{
    public List<Travail> FromDtotoTravaux(EchantillonDto echantillonDto,Echantillon echantillon)
    {
        List<Travail> result = new List<Travail>();
        foreach (TypeTravauxDto travaux in echantillonDto.TypeTravaux)
        {
            result.Add(new Travail
            {
                IdTypeTravaux = travaux.IdTypeTravaux,
                IdEchantillon = echantillon.IdEchantillon //TODO : control that we can do this type of travaux with this type of echantillon
            });
        }
        return result;
    }
}