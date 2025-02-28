using LimsPrestationService.Models;

namespace LimsPrestationService.Services;

public interface IPrestationService
{
    Task<Prestation> GetPrestation(int id);
    Task<VPrestationEtatDecompte[]> GetPrestations();
}