using LimsPrestationService.Models;

namespace LimsPrestationService.Services;

public interface IPreleveurService
{
    Task<List<Preleveur>> GetPreleveurs();
    Task<Preleveur> GetPreleveur(int id);
}