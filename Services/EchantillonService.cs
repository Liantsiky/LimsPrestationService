using LimsPrestationService.Dto;
using LimsPrestationService.Models;
using System;
using System.Drawing;
using System.IO;
using QRCoder;
using System.Text;
using System.Diagnostics;
using LimsUtils.Utility;
using LimsPrestationService.Data;
using Microsoft.EntityFrameworkCore;

namespace LimsPrestationService.Services;

public class EchantillonService : IEchantillonService
{
    private readonly PdfService pdfService;
    private readonly PrestationServiceContext _dbContext;
    public EchantillonService(PdfService pdfService, PrestationServiceContext dbContext)
    {
        this.pdfService = pdfService;
        _dbContext = dbContext;
    } 
    public Echantillon FromDtotoEchantillon(string refClient, EchantillonDto echantillonDto, int idPrestation)
    {
        Echantillon echantillon = new Echantillon
        {
            Reference = "ECH" + Guid.NewGuid().ToString(), //TODO : creer la reference 
            ReferenceClient = refClient,
            Note = echantillonDto.Note,
            Provenance = echantillonDto.Provenance,
            DatePrelevement = echantillonDto.DatePrelevement,
            IdTypeEchantillon = echantillonDto.IdTypeEchantillon,
            IdPrestation = idPrestation
        };
        return echantillon;
    }

    public byte[] GenerateEchantillonQr(string reference)
    {
        string content = FileUtils.ReadFile("template/Echantillon.omnis");
        string qrCode = GenerateQrCode(reference);
        content = content.Replace("#QrCode#", qrCode).Replace("#Reference#", reference);
        return pdfService.GeneratePdfAFive(content);
    }

    public string GenerateQrCode(string reference)
    {
        QRCodeGenerator qrGenerator = new QRCodeGenerator();
        QRCodeData qrCodeData = qrGenerator.CreateQrCode(reference, QRCodeGenerator.ECCLevel.Q);
        PngByteQRCode qrCode = new PngByteQRCode(qrCodeData);
        byte[] qrCodeAsPngByteArr = qrCode.GetGraphic(20);
        string base64String = Convert.ToBase64String(qrCodeAsPngByteArr, 0, qrCodeAsPngByteArr.Length);

        return base64String;
    }

    public async Task<Echantillon> GetEchantillon(int id)
    {
        Echantillon? result = await _dbContext.Echantillons
        .Where(e => e.IdEchantillon == id)
        .FirstOrDefaultAsync(e => e.IdEchantillon == id);
        if(result == null)
        {
            throw new Exception("Cette echnatillon n'existe pas dans la base");
        }
        return result;

    }

    public async Task<Echantillon> GetEchantillonByReference(string reference)
    {
        Echantillon? result = await _dbContext.Echantillons
        .Where(e => e.Reference == reference)
        .Include(e => e.DetailsEchantillons)
        .Include(e => e.TypeEchantillon)
        .FirstOrDefaultAsync();
        if(result == null)
        {
            throw new Exception("Cette echantillon n'existe pas dans la base");
        }
        return result;
    }
}