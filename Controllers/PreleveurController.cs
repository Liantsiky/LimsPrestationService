using LimsPrestationService.Models;
using LimsPrestationService.Services;
using LimsUtils.Api;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/preleveur")]
public class PreleveurController : Controller
{
    private readonly IPreleveurService _preleveurService;

    public PreleveurController(IPreleveurService preleveurService)
    {
        _preleveurService = preleveurService;
    }

    [HttpGet]
    public async Task<ActionResult<ApiResponse>> GetPreleveurs()
    {
        List<Preleveur> etatsPrestations = await _preleveurService.GetPreleveurs();
        try
        {
            return Ok(
                new ApiResponse
                {
                    Data = etatsPrestations,
                    ViewBag = null,
                    IsSuccess = true,
                    Message = "Preleveur retrieved successfully.",
                    StatusCode = 200
                }
            );
        }
        catch (Exception e)
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

    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse>> GetPreleveur(int id)
    {
        var preleveur = await _preleveurService.GetPreleveur(id);
        try
        {
            return Ok(
                new ApiResponse
                {
                    Data = preleveur,
                    ViewBag = null,
                    IsSuccess = true,
                    Message = "Preleveur retrieved successfully.",
                    StatusCode = 200
                }
            );
        }
        catch (Exception e)
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