using LimsPrestationService.Dto;
using LimsPrestationService.Models;

namespace LimsPrestationService.Services;

public interface IPrestationService
{
    Prestation CreatePrestation(PrestationDto prestation);
    Task<Prestation> GetPrestation(int id);
    Task<VPrestationEtatDecompte[]> GetPrestations(SortPrestationDto sorter);
    Task<byte[]> EtatDeDecompteToPdf(int id);
    Task<byte[]> FicheTravailToPdf(int id);
    Task<Prestation> TransmissionPrestation(int id);
    Task<Prestation> UpdateTravailAndCheck(int idTravail, int idPrestation);
    Task<Prestation> LivraisonPrestation(int idPrestation);
    
}