using LimsPrestationService.Dto;
using LimsPrestationService.Models;

namespace LimsPrestationService.Services;

public class EtatDecompteService : IEtatDecompteService 
{
    public EtatDecompte FromDtotoEtatDecompte(PrestationDto prestationDto, Prestation prestation)
    {
        EtatDecompte etatDecompte = new EtatDecompte
            {
                  IdPrestation = prestation.IdPrestation,
                    Reference = prestation.ReferenceFicheTravail,
                    Remise = prestationDto.Remise,
                    DateEtatDecompte = prestationDto.DatePrestation
            };
        // Implementation
        return etatDecompte;
    }
}