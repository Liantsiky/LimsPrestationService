using LimsPrestationService.Data;
using LimsPrestationService.Sequences;
using LimsUtils.Utility;


namespace LimsPrestationService.Services;

public class FicheTravailSequenceService : IFicheTravailSequenceService
{
    public readonly PrestationServiceContext _dbContext;
    public FicheTravailSequenceService(PrestationServiceContext dbContext)
    {
        _dbContext = dbContext;
    }
    public string CreateFicheTravailSequence()
    {
        int annee = DateUtils.GetCurrentYear();
        string shortYear = annee.ToString().Substring(2, 2);
        FicheTravailSequence ficheTravailSequence = GetFicheTravailSequenceService(annee);
        ficheTravailSequence.DerniereValeur++;
        string result = ficheTravailSequence.DerniereValeur + "/" + shortYear;
        this.UpdateFicheTravailSequence(ficheTravailSequence);
        return result;
    }

    public FicheTravailSequence GetFicheTravailSequenceService(int annee)
    {
        FicheTravailSequence? result = _dbContext.FicheTravailSequences.FirstOrDefault(s => s.Annee == annee);
        if (result == null){
            FicheTravailSequence nouveau = new FicheTravailSequence
                {
                    Annee = DateUtils.GetCurrentYear(),
                    DerniereValeur = 1  // Initial value
                }
            ;
            _dbContext.FicheTravailSequences.Add(nouveau);
            _dbContext.SaveChanges();
            result = nouveau;  // Get the newly created record to fetch the sequence value.
        }
        return result;
    }

    public void UpdateFicheTravailSequence(FicheTravailSequence ficheTravailSequence)
    {
        _dbContext.FicheTravailSequences.Update(ficheTravailSequence);
        _dbContext.SaveChanges();
    }
}