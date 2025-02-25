using LimsPrestationService.Data;
using LimsPrestationService.Models;
using Microsoft.EntityFrameworkCore;

namespace LimsPrestationService.Services;

public class PrestationService : IPrestationService
{
    private readonly PrestationServiceContext _dbContext;
    public PrestationService(PrestationServiceContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Prestation> GetPrestation(int id)
    {
        Prestation? prestation = await _dbContext.Prestations
            .Where(p => p.IdPrestation == id)
            .Include(p => p.EtatPrestation)
            .Include(p => p.Client)
            .FirstOrDefaultAsync();
        if(prestation == null)
        {
            throw new ArgumentException("Cette prestation n'existe pas");
        }

        return prestation;
    }

    public async Task<List<Prestation>> GetPrestationsTransmissible()
    {
        List<Prestation> prestations = await _dbContext.Prestations
        .Where(p => p.IdEtatPrestation == 1)
        .Include(p => p.EtatPrestation)
        .Include(p => p.Client)
        .ToListAsync();

        return prestations;
    }

    public async Task<List<Prestation>> GetPrestationsParAnnee(string annee)
    {
        List<Prestation> prestations = await _dbContext.Prestations
        .Where(p => p.DateCloture.Value.Year.ToString() == annee)
        .Include(p => p.EtatPrestation)
        .Include(p => p.Client)
        .ToListAsync();

        return prestations;
    }
}