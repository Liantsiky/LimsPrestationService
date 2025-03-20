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
    public EchantillonController(IEchantillonService echantillonService)
    {
        _echantillonService = echantillonService;
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