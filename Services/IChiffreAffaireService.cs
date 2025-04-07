using LimsPrestationService.Models;

namespace LimsPrestationService.Services;

public interface IChiffreAffaireService
{
    Task<ChiffreAffaire[]> GetChiffreAffaireMensuel(ChiffreAffaire chiffreAffaire);
    Task<ChiffreAffaire[]> GetChiffreAffaireAnnuel(ChiffreAffaire chiffreAffaire);
    Task<ChiffreAffaire[]> GetChiffreAffaireJournalier(ChiffreAffaire chiffreAffaire);
}