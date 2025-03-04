using LimsPrestationService.Dto;
using LimsPrestationService.Models;
using LimsPrestationService.Services;
using LimsUtils.Api;
using Microsoft.AspNetCore.Mvc;

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

    [HttpGet]
    public async Task<ActionResult<ApiResponse>> GetPrestations()
    {
        VPrestationEtatDecompte[] prestations = await _prestationService.GetPrestations();
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
    

}