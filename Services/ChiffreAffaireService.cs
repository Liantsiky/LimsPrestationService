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

    public async Task<ChiffreAffaire[]> GetChiffreAffaireAnnuel(ChiffreAffaire chiffreAffaire)
    {
        int anneeDebut = chiffreAffaire!.Mois!.Value;
        int anneeFin = chiffreAffaire!.Annee!.Value;
        var cas = await _dbContext.Database.SqlQuery<ChiffreAffaire>(
            @$"SELECT annee, 0 as mois, 0 as jour, montant FROM v_chiffre_affaire_annuel
                where annee >= {anneeDebut}
                AND annee <= {anneeFin}")
            .ToArrayAsync();

        return cas;
    }

    public async Task<ChiffreAffaire[]> GetChiffreAffaireMensuel(ChiffreAffaire chiffreAffaire)
    {
        var cas = await _dbContext.Database.SqlQuery<ChiffreAffaire>(
            $"SELECT annee, mois, montant, 0 as jour FROM v_chiffre_affaire_mensuel where annee = {chiffreAffaire!.Annee}")
            .ToArrayAsync();
        
        return cas;
    }

    public async Task<ChiffreAffaire[]> GetChiffreAffaireJournalier(ChiffreAffaire chiffreAffaire)
    {
        var cas = await _dbContext.Database.SqlQuery<ChiffreAffaire>(
            @$"SELECT annee, mois, montant, 0 as jour FROM v_chiffre_affaire_journalier 
                where annee = {chiffreAffaire!.Annee}
                and mois = {chiffreAffaire.Mois}")
            .ToArrayAsync();
        
        return cas;
    }
}