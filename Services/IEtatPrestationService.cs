using LimsPrestationService.Models;

namespace LimsPrestationService.Services;

public interface IEtatPrestationService
{
    Task<List<EtatPrestation>> GetEtatPrestations();
    Task<EtatPrestation> GetEtatPrestation(int id);
    Task<EtatPrestation> CreateEtatPrestation(EtatPrestation etatPrestation);
}