using LimsPrestationService.Models;
using LimsPrestationService.Services;
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

}