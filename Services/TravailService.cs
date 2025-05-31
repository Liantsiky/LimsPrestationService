using LimsPrestationService.Dto;
using LimsPrestationService.Models;
using LimsPrestationService.Data;
using Microsoft.EntityFrameworkCore;


namespace LimsPrestationService.Services;

public class TravailService : ITravailService
{
    private readonly PrestationServiceContext _dbContext;

    public TravailService (PrestationServiceContext dbContext)
    {
        _dbContext = dbContext;
    }
    public List<Travail> FromDtotoTravaux(EchantillonDto echantillonDto,Echantillon echantillon)
    {
        List<Travail> result = new List<Travail>();
        foreach (TypeTravauxDto travaux in echantillonDto.TypeTravaux)
        {
            result.Add(new Travail
            {
                IdTypeTravaux = travaux.IdTypeTravaux,
                IdEchantillon = echantillon.IdEchantillon //TODO : control that we can do this type of travaux with this type of echantillon
            });
        }
        return result;
    }

    public async Task<Travail> ProgressionTravail(int idTravail)
    {
        Travail? travail = await _dbContext.Travails
            .Where(t => t.IdTravail == idTravail)
            .FirstOrDefaultAsync();
        travail.IdAvanceeTravail = 3;
        _dbContext.Travails.Update(travail);
        await _dbContext.SaveChangesAsync();
        return  travail;
    }
}