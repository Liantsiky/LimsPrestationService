using LimsPrestationService.Data;
using LimsPrestationService.Dto;
using LimsPrestationService.Models;
using Microsoft.EntityFrameworkCore;

namespace LimsPrestationService.Services;

public class PrestationService : IPrestationService
{
    private readonly PrestationServiceContext _dbContext;
    private readonly IEchantillonService _echantillonService;
    private readonly IFicheTravailSequenceService _ficheTravailSequenceService;
    public PrestationService(PrestationServiceContext dbContext, IEchantillonService echantillonService,IFicheTravailSequenceService ficheTravailSequenceService)
    {
        _dbContext = dbContext;
        _echantillonService = echantillonService;
        _ficheTravailSequenceService = ficheTravailSequenceService;
    }

    public async Task<Prestation> GetPrestation(int id)
    {
        Prestation? prestation = await _dbContext.Prestations
            .Where(p => p.IdPrestation == id)
            .Include(p => p.EtatPrestation)
            .Include(p => p.Client)
            .Include(p => p.PrestationDetails)
            .FirstOrDefaultAsync();
        if(prestation == null)
        {
            throw new ArgumentException("Cette prestation n'existe pas");
        }

        return prestation;
    }

    public async Task<VPrestationEtatDecompte[]> GetPrestations()
    {
        VPrestationEtatDecompte[] prestations = await _dbContext.VPrestationEtatDecomptes
        .ToArrayAsync();

        return prestations;
    }

    public  Prestation CreatePrestation(PrestationDto prestationDto)
    {

        using var transaction =  _dbContext.Database.BeginTransaction();
        try
        {
            Prestation prestation = this.FromDtoToPrestation(prestationDto);
            _dbContext.Prestations.Add(prestation);
            _dbContext.SaveChanges();


            foreach (KeyValuePair<string, EchantillonDto> echantillonDto in prestationDto.Echantillons)
            {
                Echantillon echantillon = _echantillonService.FromDtotoEchantillon(echantillonDto.Key,echantillonDto.Value,prestation.IdPrestation);
                Console.Write(echantillon);
                _dbContext.Echantillons.Add(echantillon);
                echantillonDto.Value.IdEchantillon = echantillon.IdPrestation;
                 _dbContext.SaveChanges();
            }
            // _dbContext.SaveChanges();

            transaction.Commit();

            return this.GetPrestation(prestation.IdPrestation).Result;

        } catch (Exception)
        {
            transaction.Rollback();
            throw;
        }
    }

    public Prestation FromDtoToPrestation(PrestationDto prestationDto)
    {
        Prestation prestation = new Prestation
        {
            DatePrestation = prestationDto.DatePrestation,
            IdClient = prestationDto.IdClient,
            ReferenceFicheTravail = _ficheTravailSequenceService.CreateFicheTravailSequence(),
            DateCloture = null
        };
        return prestation;
    }
}