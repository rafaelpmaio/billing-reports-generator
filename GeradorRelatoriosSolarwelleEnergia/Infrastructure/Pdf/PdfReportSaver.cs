using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeradorRelatoriosSolarwelleEnergia.Application.Services;
using GeradorRelatoriosSolarwelleEnergia.Domain.Entities;

namespace GeradorRelatoriosSolarwelleEnergia.Infrastructure.Pdf
{
    internal class PdfReportSaver : IPdfReportSaver
    {
        public void Generate(RelatorioCliente report, string pdfModel, string destinyFolder)
        {
            string nomeBase = report.RazaoSocialOuNome.Trim();
            string nomeArquivo = nomeBase + ".pdf";
            string caminhoPdfFormatado = Path.Combine(destinyFolder, nomeArquivo);

            //Contador adc ao nome do arquivo em caso de haver mais de um relatório por cliente
            int contador = 2;
            while (File.Exists(caminhoPdfFormatado))
            {
                nomeArquivo = $"{nomeBase}({contador}).pdf";
                caminhoPdfFormatado = Path.Combine(destinyFolder, nomeArquivo);
                contador++;
            }

            var formatadorPdf = new PdfReportBuilder(pdfModel, caminhoPdfFormatado);
            formatadorPdf.Gerar(report);
        }
    }
}
