using LimsPrestationService.Data;
using LimsPrestationService.Models;
using Microsoft.EntityFrameworkCore;

namespace LimsPrestationService.Services;

public class ChiffreAffaireService : IChiffreAffaireService
{

    private readonly PrestationServiceContext _dbContext;

    public ChiffreAffaireService(PrestationServiceContext dbContext)
    {
        this._dbContext = dbContext;
    }

    // FIXME : Define
    public Task<ChiffreAffaire[]> GetChiffreAffaireJournalier(int annee)
    {
        throw new NotImplementedException();
    }

    public async Task<ChiffreAffaire[]> GetChiffreAffaireMensuel(ChiffreAffaire chiffreAffaire)
    {
        var cas = await _dbContext.Database.SqlQuery<ChiffreAffaire>(
            $"SELECT annee, mois, montant, 0 as jour FROM v_chiffre_affaire_mensuel where annee = {chiffreAffaire!.Annee}")
            .ToArrayAsync();
        
        return cas;
    }
}