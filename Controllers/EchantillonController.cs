using LimsPrestationService.Models;
using LimsPrestationService.Services;
using LimsUtils.Api;
using Microsoft.AspNetCore.Mvc;

namespace LimsPrestationService.Controllers;

[ApiController]
[Route("api/echantillon")]
public class EchantillonController : Controller
{
    private readonly IEchantillonService _echantillonService;
    private readonly IPrestationService _prestationService;
    public EchantillonController(IEchantillonService echantillonService, IPrestationService prestationService)
    {
        _echantillonService = echantillonService;
        _prestationService = prestationService;
    }

    [HttpPut]
    public async Task<ActionResult<ApiResponse>> UpdateTravailAndCheck(int idTravail, int idPrestation)
    {
        Prestation prestation = await _prestationService.UpdateTravailAndCheck(idTravail, idPrestation);
        return Ok(
            new ApiResponse
            {
                Data = prestation,
                ViewBag = null,
                IsSuccess = true,
                Message = "Travail mis a jour avec succes.",
                StatusCode = 200
            });
    }

    [HttpPost("qr/{id}")]
    public async Task<IActionResult> GenerateQrCode(int id)
    {
        Echantillon echantillon = await _echantillonService.GetEchantillon(id);
        byte[] qrcode = _echantillonService.GenerateEchantillonQr(echantillon.Reference);
        return File(qrcode, "application/pdf", $"Echantillon_{id}.pdf");
    }

    [HttpGet("{reference}")]
    public async Task<IActionResult> GetEchantillonByReference(string reference)
    {
        Echantillon echantillon = await _echantillonService.GetEchantillonByReference(reference);
        try{
            return Ok(
                new ApiResponse
                {
                    Data = echantillon,
                    ViewBag = null,
                    IsSuccess = true,
                }
            );
        }catch(Exception e){
            return BadRequest(
                new ApiResponse
                {
                    Data = null,
                    ViewBag = null,
                    IsSuccess = false,
                    Message = e.Message,
                    StatusCode = 400
                }
            );
        }
    }

}