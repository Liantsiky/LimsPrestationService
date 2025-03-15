using DinkToPdf;
using DinkToPdf.Contracts;
using LimsPrestationService.Models;

namespace LimsPrestationService.Services;

public class PdfService
{
    private readonly IConverter _converter;

    public PdfService(IConverter converter)
    {
        _converter = converter;
    }

    public byte[] GeneratePdf(string htmlContent)
    {
        var doc = new HtmlToPdfDocument()
        {
            GlobalSettings = new GlobalSettings { PaperSize = PaperKind.A4 },
            Objects = { new ObjectSettings { HtmlContent = htmlContent } }
        };

        return _converter.Convert(doc);
    }
}