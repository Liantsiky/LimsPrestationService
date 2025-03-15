using LimsPrestationService.Data;
using LimsPrestationService.Dto;
using LimsPrestationService.Models;
using Microsoft.EntityFrameworkCore;
using LimsUtils.Utility;
using System.Threading.Tasks;

namespace LimsPrestationService.Services;

public class PrestationService : IPrestationService
{
    private readonly PrestationServiceContext _dbContext;
    private readonly IEchantillonService _echantillonService;
    private readonly IFicheTravailSequenceService _ficheTravailSequenceService;
    private readonly ITravailService _travailService;
    private readonly IEtatDecompteService _etatDecompteService;
    private readonly PdfService pdfService;
    public PrestationService(PrestationServiceContext dbContext, IEchantillonService echantillonService,
        IFicheTravailSequenceService ficheTravailSequenceService, PdfService pdfService, ITravailService travailService,
        IEtatDecompteService etatDecompteService)
    {
        _dbContext = dbContext;
        _echantillonService = echantillonService;
        _ficheTravailSequenceService = ficheTravailSequenceService;
        _travailService = travailService;
        _etatDecompteService = etatDecompteService;
        this.pdfService = pdfService;
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
            foreach (KeyValuePair<string, EchantillonDto> echantillonDto in prestationDto.Echantillons)
            {
                Echantillon echantillon = _echantillonService.FromDtotoEchantillon(echantillonDto.Key,echantillonDto.Value,prestation.IdPrestation);
                _dbContext.Echantillons.Add(echantillon);
                _dbContext.SaveChanges();
                List<Travail> travaux = _travailService.FromDtotoTravaux(echantillonDto.Value,echantillon);
                _dbContext.Travails.AddRange(travaux);
                _dbContext.SaveChanges();
            }

            //insert Etat Decompte
            _dbContext.EtatDecomptes.Add(_etatDecompteService.FromDtotoEtatDecompte(prestationDto,prestation));
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

    public async Task<byte[]> EtatDeDecompteToPdf(int id)
    {
        string content = FileUtils.ReadFile("template/EtatDecompte.omnis");
        Prestation? prestation = await _dbContext.Prestations
            .Where(p => p.IdPrestation == id)
            .Include(p => p.Client)
            .Include(p => p.EtatDecompte)
            .ThenInclude(ed => ed.DetailsEtatDecomptes)
            .FirstOrDefaultAsync();
        if(prestation == null)
        {
            throw new Exception("Cet état de décompte n'existe pas");
        }

        content = content.Replace("#FicheDeTravail#", prestation.ReferenceFicheTravail)
            .Replace("#Nom#", prestation.Client?.Nom)
            .Replace("#Contact#", prestation.Client?.Contact)
            .Replace("#Adresse#", prestation.Client?.Adresse);

        content = prestation.LoadEtatDecompteContent(content);
        decimal? total = prestation.EtatDecompte?.TotalMontant ?? 0;
        content = content.Replace("#Montant#", ViewUtils.RenderMoney(total));
        
        return pdfService.GeneratePdf(content);
    }
}