using LimsPrestationService.Models;

namespace LimsPrestationService.Services;

public interface IPrestationService
{
    Task<List<Prestation>> GetPrestationsTransmissible();
    Task<Prestation> GetPrestation(int id);
    Task<List<Prestation>> GetPrestationsParAnnee(string annee);
}