using LimsPrestationService.Dto;
using LimsPrestationService.Models;
using LimsPrestationService.Services;
using LimsUtils.Api;
using LimsUtils.Utility;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace LimsPrestationService.Controllers;

[ApiController]
[Route("/api/prestation")]
public class PrestationController : Controller
{
    private readonly IPrestationService _prestationService;
    public PrestationController(IPrestationService prestationService)
    {
        _prestationService = prestationService;
    }

    [HttpPut("transmission/{idPrestation}")]
    public async Task<ActionResult<ApiResponse>> TransmissionPrestation(int idPrestation)
    {
        Prestation prestation = _prestationService.TransmissionPrestation(idPrestation).Result;
        return Ok(
            new ApiResponse
            {
                Data = prestation,
                ViewBag = null,
                IsSuccess = true,
                Message = "Prestation transmis avec succes.",
                StatusCode = 200
            });
    }
    [HttpPut("livraison/{idPrestation}")]
    public async Task<ActionResult<ApiResponse>> LivraisonPrestation(int idPrestation)
    {
        Prestation prestation = _prestationService.LivraisonPrestation(idPrestation).Result;
        return Ok(
            new ApiResponse
            {
                Data = prestation,
                ViewBag = null,
                IsSuccess = true,
                Message = "Prestation livree avec succes.",
                StatusCode = 200
            });
    }

    [HttpPost("tri")]
    public async Task<ActionResult<ApiResponse>> GetPrestations([FromBody] SortPrestationDto? sorter)
    {
        if(sorter == null)
        {
            sorter = new SortPrestationDto();
        }
        sorter.ApplyDefaultValues();
        VPrestationEtatDecompte[] prestations = await _prestationService.GetPrestations(sorter);
        return Ok(
            new ApiResponse
            {
                Data = prestations,
                ViewBag = null,
                IsSuccess = true,
                Message = "Prestations transmissibles retrieved successfully.",
                StatusCode = 200
            });
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Prestation>> GetPrestation(int id)
    {
        
        try
        {
            Prestation prestation = await _prestationService.GetPrestation(id);

            return Ok(
                new ApiResponse
                {
                    Data = prestation,
                    ViewBag = null,
                    IsSuccess = true,
                    Message = "Prestation retrieved successfully.",
                    StatusCode = 200
                });
        }catch(Exception e)
        {


            return BadRequest(new ApiResponse
            {
                Data = null,
                ViewBag = null,
                IsSuccess = false,
                Message = e.Message,
                StatusCode = 400
            });
        }
    }
    [HttpPost]
    public ActionResult<Prestation> CreatePrestation([FromBody] PrestationDto prestationDto)
    {
        try
        {
            Prestation result = _prestationService.CreatePrestation(prestationDto);
            Console.WriteLine("hhhh:" + JsonSerializer.Serialize(prestationDto));
            return Ok(
                new ApiResponse
                {
                    Data = result,
                    ViewBag = null,
                    IsSuccess = true,
                    Message = "Prestation cree avec succes",
                    StatusCode = 201
                }
            );
        }catch(Exception e)
        {
            return BadRequest(
                new ApiResponse
                {
                    Data = null,
                    ViewBag = null,
                    IsSuccess = false,
                    Message = e.Message,
                    StatusCode = 400
                });
        }
    }

    [HttpPost("etat/decompte/{id}")]
    public async Task<IActionResult> EtatDeDecompteToPdf(int id)
    {
        byte[] content = await _prestationService.EtatDeDecompteToPdf(id);
        return File(content, "application/pdf", $"EtatDeDecompte_{id}.pdf");
    }

    [HttpPost("fiche/travail/{id}")]
    public async Task<IActionResult> FicheTravaildf(int id)
    {
        byte[] content = await _prestationService.FicheTravailToPdf(id);
        return File(content, "application/pdf", $"FicheTravail_{id}.pdf");
    }
    
}