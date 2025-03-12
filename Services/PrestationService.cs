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
            //insert prestation
            Prestation prestation = this.FromDtoToPrestation(prestationDto);
            _dbContext.Prestations.Add(prestation);
            _dbContext.SaveChanges();

            //insert echantillons
            Dictionary<string,Echantillon> echantillons = new Dictionary<string,Echantillon>();
            foreach (KeyValuePair<string, EchantillonDto> echantillonDto in prestationDto.Echantillons)
            {
                Echantillon echantillon = _echantillonService.FromDtotoEchantillon(echantillonDto.Key,echantillonDto.Value,prestation.IdPrestation);
                _dbContext.Echantillons.Add(echantillon);
                _dbContext.SaveChanges();
                echantillons.Add(echantillonDto.Key, echantillon);
            }
            
            //insert travaux
            foreach (KeyValuePair<string,List<int>> travaux in prestationDto.Travaux)
            {
                Echantillon echantillon = echantillons[travaux.Key];
                foreach (int idTypeTravaux in travaux.Value)
                {
                    Travail travail = new Travail
                        {
                            IdTypeTravaux = idTypeTravaux,
                            IdEchantillon = echantillon.IdEchantillon //TODO : control that we can do this type of travaux with this type of echantillon
                        };
                    _dbContext.Travails.Add(travail);
                    _dbContext.SaveChanges();
                    //TODO : Add a Etat_decompte obeject and insert it (need it for the trigger the Etat Decompte - Id_prestation)
                }
            }

            //insert Etat Decompte
            EtatDecompte etatDecompte = new EtatDecompte
                {
                    IdPrestation = prestation.IdPrestation,
                    Reference = prestation.ReferenceFicheTravail,
                    Remise = prestationDto.Remise,
                    DateEtatDecompte = prestationDto.DatePrestation
                };
            _dbContext.EtatDecomptes.Add(etatDecompte);
            _dbContext.SaveChanges();
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