using System.Text.Json;
using LimsPrestationService.Data;
using LimsPrestationService.Dto;
using LimsPrestationService.Models;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;

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

        // Interpolation to avoid sqlinjection
        var anneeDebutParam = new MySqlParameter("@anneeDebut", anneeDebut);
        var anneeFinParam = new MySqlParameter("@anneeFin", anneeFin);

        var cas = await _dbContext.ChiffreAffaires.FromSqlRaw(
            @"SELECT annee, 0 as mois, 0 as jour, montant, 0 as idDepartement, 'None' as designation FROM v_chiffre_affaire_annuel
                where annee >= @anneeDebut
                AND annee <= @anneeFin", anneeDebutParam, anneeFinParam)
            .ToArrayAsync();

        return cas;
    }

    public async Task<ChiffreAffaire[]> GetChiffreAffaireMensuel(ChiffreAffaire chiffreAffaire)
    {
        int annee = chiffreAffaire!.Annee!.Value;
        var anneeParam = new MySqlParameter("@annee", annee);

        var cas = await _dbContext.ChiffreAffaires.FromSqlRaw(
            @"SELECT annee, mois, montant, 0 as jour, 0 as idDepartement, 'None' as designation FROM v_chiffre_affaire_mensuel
             where annee =@annee", anneeParam)
            .ToArrayAsync();
        
        return cas;
    }

    public async Task<ChiffreAffaire[]> GetChiffreAffaireJournalier(ChiffreAffaire chiffreAffaire)
    {
        int annee = chiffreAffaire!.Annee!.Value;
        int mois = chiffreAffaire!.Mois!.Value;

        var anneeParam = new MySqlParameter("@annee", annee);
        var moisParam = new MySqlParameter("@mois", mois);

        var cas = await _dbContext.ChiffreAffaires.FromSqlRaw(
            @"SELECT annee, mois, montant, jour, 0 as idDepartement, 'None' as designation FROM v_chiffre_affaire_journalier 
                where annee = @annee
                and mois = @mois", anneeParam, moisParam)
            .ToArrayAsync();
        
        return cas;
    }

    public ChiffreAffaireDepartementDto[] GroupByDepartement(List<ChiffreAffaire> chiffreAffaires)
    {
        ChiffreAffaireDepartementDto[] result = new ChiffreAffaireDepartementDto [1];
        result = chiffreAffaires
            .GroupBy(c => new { c.IdDepartement, c.Designation })
            .Select (ca => new ChiffreAffaireDepartementDto 
            {
                IdDepartement = ca.Key.IdDepartement,
                Designation = ca.Key.Designation,
                ChiffreAffaires = ca.Select(caff => new ChiffreAffaire
                {
                    Montant = caff.Montant,
                    Annee = caff.Annee,
                    Mois = caff.Mois,
                    Jour = caff.Jour
                }).ToList()
            }).ToArray();

        return result;
    }

    public async Task<ChiffreAffaireDepartementDto[]> GetChiffreAffaireParDepartementMensuel(ChiffreAffaire chiffreAffaire)
    {
        ChiffreAffaireDepartementDto[] result = new ChiffreAffaireDepartementDto [1];

        int annee = chiffreAffaire!.Annee!.Value;
        var anneeParam = new MySqlParameter("@annee", annee);

        var cas = await _dbContext.ChiffreAffaires.FromSqlRaw(
            @"SELECT annee, mois, montant, 0 as jour, idDepartement, designation FROM v_chiffre_affaire_par_departement_mensuel 
                where annee = @annee", anneeParam)
            .ToListAsync();

        result = GroupByDepartement(cas);

        return result;
    }

    public async Task<ChiffreAffaireDepartementDto[]> GetChiffreAffaireParDepartementAnnuel(ChiffreAffaire chiffreAffaire)
    {
        ChiffreAffaireDepartementDto[] result = new ChiffreAffaireDepartementDto [1];
        int anneeDebut = chiffreAffaire!.Mois!.Value;
        var anneeDebutParam = new MySqlParameter("@anneeDebut", anneeDebut);
        int anneeFin = chiffreAffaire!.Annee!.Value;
        var anneeFinParam = new MySqlParameter("@anneeFin", anneeFin);

        var cas = await _dbContext.ChiffreAffaires.FromSqlRaw(
            @"SELECT annee, 0 as mois, montant, 0 as jour, idDepartement, designation FROM v_chiffre_affaire_par_departement_annuel 
                where annee >= @anneeDebut AND annee <= @anneeFin", anneeDebutParam, anneeFinParam)
            .ToListAsync();

        result = GroupByDepartement(cas);

        return result;
    }

    public async Task<ChiffreAffaireDepartementDto[]> GetChiffreAffaireParDepartementJournalier(ChiffreAffaire chiffreAffaire)
    {
        ChiffreAffaireDepartementDto[] result = new ChiffreAffaireDepartementDto [1];
        int mois = chiffreAffaire!.Mois!.Value;
        var moisParam = new MySqlParameter("@mois", mois);
        int annee = chiffreAffaire!.Annee!.Value;
        var anneeParam = new MySqlParameter("@annee", annee);

        var cas = await _dbContext.ChiffreAffaires.FromSqlRaw(
            @"SELECT annee, mois, montant, jour, idDepartement, designation FROM v_chiffre_affaire_par_departement_journalier 
                where annee = @annee AND mois = @mois", anneeParam, moisParam)
            .ToListAsync();

        result = GroupByDepartement(cas);

        return result;
    }

    public async Task<ChiffreAffaireInterneExterne[]> GetChiffreAffaireInterneExterneAnnuel(ChiffreAffaireInterneExterne chiffreAffaireInterneExterne)
    {
        int annee = chiffreAffaireInterneExterne!.Annee!.Value;
        var anneeParam = new MySqlParameter("@annee", annee);

        var cas = await _dbContext.ChiffreAffaireInterneExternes.FromSqlRaw(
            @"SELECT annee, 0 as mois, montant, isInterne FROM v_chiffre_affaire_client_annuel 
                where annee = @annee", anneeParam)
            .ToArrayAsync();

        return cas;
    }

    public async Task<ChiffreAffaireInterneExterne[]> GetChiffreAffaireInterneExterneMensuel(ChiffreAffaireInterneExterne chiffreAffaireInterneExterne)
    {
        int annee = chiffreAffaireInterneExterne!.Annee!.Value;
        var anneeParam = new MySqlParameter("@annee", annee);

        int mois = chiffreAffaireInterneExterne!.Mois!.Value;
        var moisParam = new MySqlParameter("@mois", mois);

        var cas = await _dbContext.ChiffreAffaireInterneExternes.FromSqlRaw(
            @"SELECT annee, mois, montant, isInterne FROM v_chiffre_affaire_client_mensuel
                where annee = @annee and mois = @mois", anneeParam, moisParam)
            .ToArrayAsync();

        Console.WriteLine(JsonSerializer.Serialize(cas));

        return cas;
    }
}