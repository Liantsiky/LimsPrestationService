using LimsPrestationService.Dto;
using LimsPrestationService.Models;

namespace LimsPrestationService.Services;

public interface IEchantillonService
{
    Task<Echantillon> GetEchantillon(int id); 
    Echantillon FromDtotoEchantillon(string refClient, EchantillonDto echantillonDto, int idPrestation);
    string GenerateQrCode(string reference);

    byte[] GenerateEchantillonQr(string reference);
}