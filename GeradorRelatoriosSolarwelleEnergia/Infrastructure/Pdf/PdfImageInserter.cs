using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iText.IO.Image;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas;

namespace GeradorRelatoriosSolarwelleEnergia.Infrastructure.Pdf
{
    internal class PdfImageInserter
    {
        public static void Insert(PdfPage page, byte[] image, float x, float y, float width, float heigh)
        {
            var canvas = new PdfCanvas(page);
            ImageData imagemData = ImageDataFactory.Create(image);
            var rectangle = new iText.Kernel.Geom.Rectangle(x, y, width, heigh);
            canvas.AddImageFittedIntoRectangle(imagemData, rectangle, false);
        }
    }
}
