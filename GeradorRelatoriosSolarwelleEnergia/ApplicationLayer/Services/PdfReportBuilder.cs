using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeradorRelatoriosSolarwelleEnergia.ApplicationLayer.Services;
using GeradorRelatoriosSolarwelleEnergia.Domain.Entities;
using GeradorRelatoriosSolarwelleEnergia.Domain.Utils;
using GeradorRelatoriosSolarwelleEnergia.Infrastructure.Pdf;
using iText.Kernel.Pdf;

namespace GeradorRelatoriosSolarwelleEnergia.Application.Services
{
    internal class PdfReportBuilder
    {
        private readonly string _caminhoModelo;
        private readonly string _caminhoDestino;

        public PdfReportBuilder(string caminhoModelo, string caminhoDestino)
        {
            _caminhoModelo = caminhoModelo;
            _caminhoDestino = caminhoDestino;
        }

        public void Gerar (RelatorioCliente relatorio)
        {
            var imagemGrafico = AnnualEconomyGraphicGenerator.Generate(relatorio);
            var campos = PdfMapperReport.Map(relatorio);

            using var reader = new PdfReader(_caminhoModelo);
            using var writer = new PdfWriter(_caminhoDestino);
            PdfDocument pdfDoc = new PdfDocument(reader, writer);

            var pagina = pdfDoc.GetPage(1);

            PdfTemplateFiller.FillFields(pagina, campos);
            PdfImageInserter.Insert(pagina, imagemGrafico, x: 470, y: 160, width: 360, heigh: 250);

            pdfDoc.Close();
        }
    }
}
