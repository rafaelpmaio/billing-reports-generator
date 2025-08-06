using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Wordprocessing;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas;

namespace GeradorRelatoriosSolarwelleEnergia.Infrastructure.Pdf
{
    internal class PdfTemplateFiller
    {
        public static void FillFields(PdfPage pagina, IEnumerable<PdfField> fields)
        {
            PdfCanvas canvas = new PdfCanvas(pagina);
            float pageHeight = pagina.GetPageSize().GetHeight();
            float pageWidth = pagina.GetPageSize().GetWidth();

            foreach (var field in fields)
            {
                float x = field.Coordinates[0];
                float y = field.Coordinates[1];

                var fonte = field.Bold
                    ? iText.Kernel.Font.PdfFontFactory.CreateFont("Helvetica-Bold")
                    : iText.Kernel.Font.PdfFontFactory.CreateFont("Helvetica");

                var corTexto = field.Bold
                    ? iText.Kernel.Colors.ColorConstants.WHITE
                    : iText.Kernel.Colors.ColorConstants.BLACK;

                canvas.BeginText();
                canvas.SetFontAndSize(fonte, field.FontSize);
                canvas.SetFillColor(corTexto);
                canvas.MoveText(x, y);
                canvas.ShowText(field.Value);
                canvas.EndText();
            }
        }
    }
}
