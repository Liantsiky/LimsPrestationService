using LimsPrestationService.Models;
using LimsPrestationService.Services;
using LimsUtils.Api;
using Microsoft.AspNetCore.Mvc;

namespace LimsPrestationService.Controllers;

[ApiController]
[Route("api/type/echantillon")]
public class TypeEchantillonController : ControllerBase
{
    private readonly ITypeEchantillonService _typeEchantillonService;
    public TypeEchantillonController(ITypeEchantillonService typeEchantillonService)
    {
        _typeEchantillonService = typeEchantillonService;
    }

    [HttpGet]
    public async Task<ActionResult<ApiResponse>> GetTypeEchantillonFrom(int position, int pageSize)
    {
        if (position == 0) position = 1;
        if (pageSize == 0) pageSize = 2;
        Dictionary<string, object> response = new Dictionary<string, object>();
        response["nbrPerPage"] = pageSize;
        int totalTypeEchantillonRows = _typeEchantillonService.CountTypeEchantillon();
        response["TotalCount"] = totalTypeEchantillonRows;
        response["nbrLinks"] = Math.Ceiling((double)totalTypeEchantillonRows / pageSize);

            response["position"] = position;
            int skiped = (position-1) * pageSize;
            List<TypeEchantillon> typeEchantillon = await _typeEchantillonService.GetTypeEchantillonFrom(skiped, pageSize);
            return Ok(new ApiResponse
            {
                Data = typeEchantillon,
                ViewBag = response,
                IsSuccess = true,
                Message = "Datas retrieved successfully.",
                StatusCode = 200
            });
    }

    [HttpGet("all")]
    // [Route("/api/type/echantillon/all")]
    public async Task<ActionResult<ApiResponse>> GetAllTypeEchantillons()
    {
        List<TypeEchantillon> typeEchantillons = await _typeEchantillonService.GetTypeEchantillons();
        return Ok(new ApiResponse
        {
            Data = typeEchantillons,
            ViewBag = null,
            IsSuccess = true,
            Message = "Datas retrieved successfully.",
            StatusCode = 200
        });
    }

    [HttpPost]
    public async Task<ActionResult<ApiResponse>> CreateTypeEchantillon([FromBody] TypeEchantillon typeEchantillon)
    {
        TypeEchantillon result = await _typeEchantillonService.CreateTypeEchantillon(typeEchantillon);
        return 
        Ok(new ApiResponse
        {
            Data = result,
            IsSuccess = true,
            Message = "Type d'echantillon créé avec succès.",
            StatusCode = 201
        });
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse>> GetTypeEchantillon(int id)
    {
        TypeEchantillon typeEchantillon = await _typeEchantillonService.GetTypeEchantillon(id);
        if (typeEchantillon == null)
        {
            return NotFound(new ApiResponse
            {
                IsSuccess = false,
                Message = $"Type d'echantillon avec l'id {id} n'existe pas.",
                StatusCode = 404
            });
        }
        return Ok(new ApiResponse
        {
            Data = typeEchantillon,
            IsSuccess = true,
            Message = "Type d'echantillon récupéré avec succès.",
            StatusCode = 200
        });
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ApiResponse>> UpdateTypeEchantillon(int id, [FromBody] TypeEchantillon typeEchantillon)
    {
        TypeEchantillon result = await _typeEchantillonService.UpdateTypeEchantillon(id, typeEchantillon);
        if (result == null)
        {
            return NotFound();
        }

        return Ok(new ApiResponse
        {
            Data = result,
            IsSuccess = true,
            Message = "Type d'echantillon mis à jour avec succès.",
            StatusCode = 200
        });
    }
}
