using LimsPrestationService.Dto;
using LimsPrestationService.Models;

namespace LimsPrestationService.Services;

public interface IPrestationService
{
    Prestation CreatePrestation (PrestationDto prestation);
    Task<Prestation> GetPrestation(int id);
    Task<VPrestationEtatDecompte[]> GetPrestations();
    Task<byte[]> EtatDeDecompteToPdf(int id);
    Task<byte[]> FicheTravailToPdf(int id);
    
}