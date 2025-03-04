using LimsPrestationService.Dto;
using LimsPrestationService.Models;

namespace LimsPrestationService.Services;

public class EchantillonService : IEchantillonService
{
    public Echantillon FromDtotoEchantillon(string refClient, EchantillonDto echantillonDto, int idPrestation)
    {
        Echantillon echantillon = new Echantillon
        {
            Reference = "ECH" + Guid.NewGuid().ToString(), //TODO : creer la reference 
            ReferenceClient = refClient,
            Note = echantillonDto.Note,
            Provenance = echantillonDto.Provenance,
            DatePrelevement = echantillonDto.DatePrelevement,
            IdTypeEchantillon = echantillonDto.IdTypeEchantillon,
            IdPrestation = idPrestation
        };
        return echantillon;
    }
}