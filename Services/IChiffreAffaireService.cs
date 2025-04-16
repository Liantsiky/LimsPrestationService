using LimsPrestationService.Dto;
using LimsPrestationService.Models;

namespace LimsPrestationService.Services;

public interface IChiffreAffaireService
{
    Task<ChiffreAffaire[]> GetChiffreAffaireMensuel(ChiffreAffaire chiffreAffaire);
    Task<ChiffreAffaire[]> GetChiffreAffaireAnnuel(ChiffreAffaire chiffreAffaire);
    Task<ChiffreAffaire[]> GetChiffreAffaireJournalier(ChiffreAffaire chiffreAffaire);
    Task<ChiffreAffaireDepartementDto[]> GetChiffreAffaireParDepartementMensuel(ChiffreAffaire chiffreAffaire); 
    Task<ChiffreAffaireDepartementDto[]> GetChiffreAffaireParDepartementAnnuel(ChiffreAffaire chiffreAffaire);
    Task<ChiffreAffaireDepartementDto[]> GetChiffreAffaireParDepartementJournalier(ChiffreAffaire chiffreAffaire);
    Task<ChiffreAffaireInterneExterne[]> GetChiffreAffaireInterneExterneAnnuel(ChiffreAffaireInterneExterne chiffreAffaireInterneExterne);
    Task<ChiffreAffaireInterneExterne[]> GetChiffreAffaireInterneExterneMensuel(ChiffreAffaireInterneExterne chiffreAffaireInterneExterne);
}