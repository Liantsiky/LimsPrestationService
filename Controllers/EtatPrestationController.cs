using LimsPrestationService.Models;
using LimsPrestationService.Services;
using LimsUtils.Api;
using Microsoft.AspNetCore.Mvc;

namespace LimsPrestationService.Controllers;

[ApiController]
[Route("/api/etat/prestation")]
public class EtatPrestationController : Controller
{
    private readonly IEtatPrestationService _etatPrestationService;

    public EtatPrestationController(IEtatPrestationService etatPrestationService)
    {
        _etatPrestationService = etatPrestationService;
    }

    [HttpGet]
    public async Task<ActionResult<ApiResponse>> GetEtatPrestations()
    {
        List<EtatPrestation> etatsPrestations = await _etatPrestationService.GetEtatPrestations();
        return Ok(
            new ApiResponse
            {
                Data = etatsPrestations,
                ViewBag = null,
                IsSuccess = true,
                Message = "Etats de prestation retrieved successfully.",
                StatusCode = 200
            }
        );
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse>> GetEtatPrestation(int id)
    {
        try
        {
            EtatPrestation etatPrestation = await _etatPrestationService.GetEtatPrestation(id);
            return Ok(
                new ApiResponse
                {
                    Data = etatPrestation,
                    ViewBag = null,
                    IsSuccess = true,
                    Message = "Etat de prestation retrieved successfully.",
                    StatusCode = 200
                }
            );
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
    public async Task<ActionResult<ApiResponse>> CreateEtatPrestation([FromBody] EtatPrestation etatPrestation)
    {
        try
        {
            EtatPrestation result = await _etatPrestationService.CreateEtatPrestation(etatPrestation);
            return Ok(
                new ApiResponse
                {
                    Data = result,
                    ViewBag = null,
                    IsSuccess = true,
                    Message = "Etat de prestation created successfully.",
                    StatusCode = 201
                }
            );
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
}