using LimsPrestationService.Data;
using LimsPrestationService.Models;
using Microsoft.EntityFrameworkCore;

namespace LimsPrestationService.Services;

public class EtatPrestationService : IEtatPrestationService
{

    private readonly PrestationServiceContext _dbContext;
    public EtatPrestationService(PrestationServiceContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<EtatPrestation> CreateEtatPrestation(EtatPrestation etatPrestation)
    {
        _dbContext.EtatPrestations.Add(etatPrestation);
        await _dbContext.SaveChangesAsync();
        return await GetEtatPrestation(etatPrestation.IdEtatPrestation);
    }

    public async Task<EtatPrestation> GetEtatPrestation(int id)
    {
        EtatPrestation? result = await _dbContext.EtatPrestations.FindAsync(id);
        if (result == null)
        {
            throw new ArgumentException("Cet état de prestation n'existe pas");
        }
        return result;
    }

    public async Task<List<EtatPrestation>> GetEtatPrestations()
    {
        List<EtatPrestation> results = await _dbContext.EtatPrestations
        .OrderByDescending(ep => ep.IdEtatPrestation)
        .ToListAsync();
        return results;
    }
}