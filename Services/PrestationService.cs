using LimsPrestationService.Data;
using LimsPrestationService.Dto;
using LimsPrestationService.Models;
using Microsoft.EntityFrameworkCore;
using LimsUtils.Utility;
using System.Threading.Tasks;
using System.Text.Json;

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

    public async Task<Prestation> TransmissionPrestation(int idPrestation)
    {
        Prestation? prestation = await _dbContext.Prestations
            .Where(p => p.IdPrestation == idPrestation)
            .Include(p => p.EtatPrestation)
            .Include(p => p.Client)
            .Include(p => p.PrestationDetails)
            .FirstOrDefaultAsync();
        prestation.IdEtatPrestation = 4;
        _dbContext.Prestations.Update(prestation);
        await _dbContext.SaveChangesAsync();
        return await this.GetPrestation(prestation.IdPrestation);
    }

    public async Task<Prestation> GetPrestation(int idPrestation)
    {
        Prestation? prestation = await _dbContext.Prestations
            .Where(p => p.IdPrestation == idPrestation)
            .Include(p => p.EtatPrestation)
            .Include(p => p.Client)
            .Include(p => p.PrestationDetails)
            .ThenInclude(pd => pd.Travail)
            .ThenInclude(t => t.AvanceeTravail)
            .FirstOrDefaultAsync();
        if(prestation == null)
        {
            throw new ArgumentException("Cette prestation n'existe pas");
        }

        return prestation;
    }

    public async Task<VPrestationEtatDecompte[]> GetPrestations(SortPrestationDto sorter)
    {
        var query =  _dbContext.VPrestationEtatDecomptes.AsQueryable();
        if(!string.IsNullOrEmpty(sorter.ReferenceFicheTravail))
        {
            query = query.Where(p => p.Reference == sorter.ReferenceFicheTravail);
        }
        if(sorter.IdEtatPrestation != null)
        {
            query = query.Where(p => p.IdEtatPrestation == sorter.IdEtatPrestation);
        }
        VPrestationEtatDecompte[] prestations = await query
        .Where(p => p.DatePrestation.Year == sorter.AnneeExercice)
        .ToArrayAsync();

        return prestations;
    }

    public  Prestation CreatePrestation(PrestationDto prestationDto)
    {
        Console.WriteLine(JsonSerializer.Serialize(prestationDto));
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
                Console.WriteLine(JsonSerializer.Serialize(travaux));

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
            .ThenInclude(ed => ed!.DetailsEtatDecomptes) // Excpected to be not null at run time (!)
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

    public async Task<byte[]> FicheTravailToPdf(int id)
    {
        string content = FileUtils.ReadFile("template/FicheTravail.omnis");
        Prestation? prestation = await _dbContext.Prestations
            .Where(p => p.IdPrestation == id)
            .Include(p => p.Client)
            .Include(p => p.EtatDecompte)
            .ThenInclude(ed => ed!.DetailsEtatDecomptes) // Excpected to be not null at run time (!)
            .Include(p => p.PrestationDetails)
            .ThenInclude(pd => pd.TypeEchantillon)
            .Include(p => p.PrestationDetails)
            .ThenInclude(pd => pd.Echantillon)
            .FirstOrDefaultAsync();
        if (prestation == null)
        {
            throw new Exception("Cet état de décompte n'existe pas");
        }

        content = prestation.LoadFicheTravailContent(content);

        return pdfService.GeneratePdf(content);
    }

    public async Task<Prestation> UpdateTravailAndCheck(int idTravail, int idPrestation)
    {
        Prestation? prestation = await _dbContext.Prestations
            .Where(p => p.IdPrestation == idPrestation)
            .Include(p => p.EtatPrestation)
            .Include(p => p.Client)
            .Include(p => p.PrestationDetails)
            .FirstOrDefaultAsync();
        Travail travail = await _travailService.ProgressionTravail(idTravail);
        Boolean isFinish = await this.IsFinished(idPrestation);
        Console.WriteLine("is Finish 1.0 :" + isFinish);
        if (isFinish == true)
        {
            prestation.IdEtatPrestation = 5;
            _dbContext.Prestations.Update(prestation);
            await _dbContext.SaveChangesAsync();
        }
        return prestation;
    }
    public async Task<Boolean> IsFinished(int idPrestation)
    {
        Boolean isFinish = true;
        Prestation? prestation = await _dbContext.Prestations
            .Where(p => p.IdPrestation == idPrestation)
            .Include(p => p.PrestationDetails)
            .ThenInclude(pd => pd.Travail)
            .ThenInclude(t => t.AvanceeTravail)
            .FirstOrDefaultAsync();
        foreach (VPrestationDetails travaux in prestation.PrestationDetails)
        {
            if (travaux?.Travail?.AvanceeTravail?.IdAvanceeTravail == 1)
            {
                isFinish = false;
                break;
            }
        }
        return isFinish;
    }
    // public async Task<List<Travail>> GetTotalTravauxPrestation(int idPrestation)
    // {
    //     List<Travail> travauxPrestation = new List<Travail>();
    //     Prestation? prestation = await _dbContext.Prestations
    //         .Where(p => p.IdPrestation == idPrestation)
    //         .Include(p => p.PrestationDetails)
    //         .ThenInclude(pd => pd.Travail)
    //         .FirstOrDefaultAsync();
    //     foreach (VPrestationDetails prestationDetails in prestation.PrestationDetails)
    //     {
    //         travauxPrestation.Add(prestationDetails.Travail);
    //     }
    //     return travauxPrestation;
    // } 
}