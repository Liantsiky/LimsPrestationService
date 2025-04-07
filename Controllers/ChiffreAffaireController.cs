using LimsPrestationService.Models;
using LimsPrestationService.Services;
using LimsUtils.Api;
using LimsUtils.Utility;
using Microsoft.AspNetCore.Mvc;

namespace LimsPrestationService.Controllers;

[ApiController]
[Route("/api/chiffre/affaire")]
public class ChiffreAffaireController : Controller
{
    private readonly IChiffreAffaireService _chiffreAffaireService;
    public ChiffreAffaireController(IChiffreAffaireService chiffreAffaireService)
    {
        _chiffreAffaireService = chiffreAffaireService;
    }

    [HttpPost("mensuel")]
    public async Task<ActionResult<ApiResponse>> GetChiffreAffaireMensuel([FromBody] ChiffreAffaire? chiffreAffaire)
    {
        if(chiffreAffaire == null)
        {
            chiffreAffaire = new ChiffreAffaire();
            chiffreAffaire.Annee = DateUtils.GetCurrentYear();
        }
        try
        {
            ChiffreAffaire[] result = await _chiffreAffaireService.GetChiffreAffaireMensuel(chiffreAffaire);
            return Ok(
                new ApiResponse
                {
                    Data = result,
                    ViewBag = null,
                    IsSuccess = true,
                    Message = "Chiffre d'affaire mensuel retourné avec succes.",
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

    [HttpPost("annuel")]
    public async Task<ActionResult<ApiResponse>> GetChiffreAffaireAnnuel([FromBody] ChiffreAffaire? chiffreAffaire)
    {
        if(chiffreAffaire == null)
        {
            chiffreAffaire = new ChiffreAffaire();
            chiffreAffaire.Annee = DateUtils.GetCurrentYear();
            chiffreAffaire.Mois = chiffreAffaire.Annee - 5;
        }
        try
        {
            ChiffreAffaire[] result = await _chiffreAffaireService.GetChiffreAffaireAnnuel(chiffreAffaire);
            return Ok(
                new ApiResponse
                {
                    Data = result,
                    ViewBag = null,
                    IsSuccess = true,
                    Message = "Chiffre d'affaire annuel retourné avec succes.",
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

    [HttpPost("journalier")]
    public async Task<ActionResult<ApiResponse>> GetChiffreAffaireJournalier([FromBody] ChiffreAffaire? chiffreAffaire)
    {
        if(chiffreAffaire == null)
        {
            chiffreAffaire = new ChiffreAffaire();
            chiffreAffaire.Annee = DateUtils.GetCurrentYear();
            chiffreAffaire.Mois = DateTime.Now.Month;
        }
        try
        {
            ChiffreAffaire[] result = await _chiffreAffaireService.GetChiffreAffaireJournalier(chiffreAffaire);
            return Ok(
                new ApiResponse
                {
                    Data = result,
                    ViewBag = null,
                    IsSuccess = true,
                    Message = "Chiffre d'affaire annuel retourné avec succes.",
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
}