using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using GeradorRelatoriosSolarwelleEnergia.Dominio.DTO;
using iText.Kernel.Pdf.Canvas.Parser.Listener;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas;
using iText.IO.Image;

namespace GeradorRelatoriosSolarwelleEnergia.Dominio.Utils
{
    internal class FormatadorPdf
    {
        private string CaminhoPdfModelo;
        private string CaminhoPdfFormatado;

        public FormatadorPdf(string caminhoPdfModelo, string caminhoPdfFormatado)
        {
            this.CaminhoPdfModelo = caminhoPdfModelo;
            this.CaminhoPdfFormatado = caminhoPdfFormatado;
        }

        public void gerarRelatorio(RelatorioCliente relatorioCliente)
        {
            byte[] imagemGraficoAnual = GraficoEconomiaAnual.GerarGraficoColunas(relatorioCliente);
            //Cria a lista com os campos que serão inseridos no pdf
            var listaCamposPdf = new List<(string valor, float[] coordenadas, bool negritoEBranco, int tamanhoFonte)>
        {            
            //Dados referentes ao mês
            (relatorioCliente.MesReferenciaBoleto, new float[] {155,455}, false, 14),
            //DateTime.Now.AddMonths(-1).ToString("MMMM/yyyy").ToUpper()
            (relatorioCliente.NumeroInstalacao,new float[] { 50, 385 }, false, 16),
            (relatorioCliente.Vencimento ,new float[] { 50, 290 }, false, 16),
            ($"R$ {relatorioCliente.TotalAPagar.ToString("F2")}", new float[] { 50, 190 }, false, 16),

            ($"R$ {relatorioCliente.TotalSemDesconto.ToString("F2")}", new float[] { 280, 385 }, true, 16),
            ($"R$ {(relatorioCliente.TotalSemDesconto - relatorioCliente.TotalAPagar).ToString("F2")}",new float[] { 280, 290 }, true, 16),
            ($"R$ {relatorioCliente.HistoricoEconomia.Sum(kvp => kvp.Value).ToString("F2")}",new float[] { 280, 190 }, true, 16),

            //Dados do cliente
            ($"CNPJ/CPF: {relatorioCliente.CnpjOuCpf}", new float[] {480, 550}, true, 8),
            ($"RAZÃO SOCIAL/NOME: {relatorioCliente.RazaoSocialOuNome}", new float[] {480, 535}, true, 8),
            ($"ENDEREÇO: {relatorioCliente.Endereco}", new float[] {480, 520}, true, 8),
            ($"EMAIL: {relatorioCliente.Email}", new float[] {480, 505}, true, 8),    
            
            //Área do Gráfico
            ($"Economia Anual (valores em R$):", new float[] {500, 415}, false, 14),
            ($"R$ {relatorioCliente.QtdCompensacao.ToString("F2")}", new float[] {670, 478}, false, 14),
            ($"R$ {relatorioCliente.QtdConsumo.ToString("F2")}", new float[] {670, 457}, false, 14),
        };

            PdfReader reader = new PdfReader(this.CaminhoPdfModelo);
            PdfWriter writer = new PdfWriter(this.CaminhoPdfFormatado);
            PdfDocument pdfDoc = new PdfDocument(reader, writer);

            PdfPage pagina = pdfDoc.GetPage(1);
            preencheCampos(pagina, listaCamposPdf);

            var canvas = new PdfCanvas(pagina);
            ImageData imagemData = ImageDataFactory.Create(imagemGraficoAnual);

            // Define posição e tamanho da imagem
            float x = 470; // posição horizontal
            float y = 160; // posição vertical
            float largura = 360;
            float altura = 250;

            var rectangle = new iText.Kernel.Geom.Rectangle(x, y, largura, altura);
            canvas.AddImageFittedIntoRectangle(imagemData, rectangle, false);

            pdfDoc.Close();
        }
        public void preencheCampos(PdfPage pagina, List<(string valor, float[] coordenadas, bool negritoEBranco, int tamanhoFonte)> campos)
        {
            PdfCanvas canvas = new PdfCanvas(pagina);

            float pageWidth = pagina.GetPageSize().GetWidth();
            float pageHeight = pagina.GetPageSize().GetHeight();

            foreach (var campo in campos)
            {
                float x = campo.coordenadas[0];
                float y = pageHeight - campo.coordenadas[1];

                if (x >= 0 && x <= pageWidth && y >= 0 && y <= pageHeight)
                {
                    var fonte = campo.negritoEBranco
                        ? iText.Kernel.Font.PdfFontFactory.CreateFont("Helvetica-Bold")
                        : iText.Kernel.Font.PdfFontFactory.CreateFont("Helvetica");

                    var corTexto = campo.negritoEBranco
                        ? iText.Kernel.Colors.ColorConstants.WHITE
                        : iText.Kernel.Colors.ColorConstants.BLACK;

                    canvas.BeginText();
                    canvas.SetFontAndSize(fonte, campo.tamanhoFonte);
                    canvas.SetFillColor(corTexto);
                    canvas.MoveText(campo.coordenadas[0], campo.coordenadas[1]);
                    canvas.ShowText(campo.valor);
                    canvas.EndText();
                }
                else
                {
                    Console.WriteLine("As coordenadas estão fora da página");
                }
            }
        }
    }
}
