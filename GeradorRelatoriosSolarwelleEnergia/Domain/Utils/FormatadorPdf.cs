using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using iText.Kernel.Pdf.Canvas.Parser.Listener;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas;
using iText.IO.Image;
using GeradorRelatoriosSolarwelleEnergia.Domain.Utils;
using GeradorRelatoriosSolarwelleEnergia.Domain.Entities;

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
            byte[] imagemGraficoAnual = GraficoEconomiaAnualGenerator.GerarGraficoColunas(relatorioCliente);
            var economiaTotal = (relatorioCliente.HistoricoEconomia.Sum(kvp => kvp.Value) + relatorioCliente.ValorEconomizadoNoMes).ToString("F2");
            //Quebra endereço em 2 linhas
            var linhasEndereco = QuebrarTextoEmLinhas(relatorioCliente.Endereco, 60);
            var enderecoLinha1 = linhasEndereco.ElementAtOrDefault(0);
            var enderecoLinha2 = linhasEndereco.ElementAtOrDefault(1);
            //Cria a lista com os campos que serão inseridos no pdf
            var listaCamposPdf = new List<(string valor, float[] coordenadas, bool negritoEBranco, int tamanhoFonte)>
        {            
            //Dados referentes ao mês
            (relatorioCliente.MesReferenciaBoleto, new float[] {155,455}, false, 14),
            (relatorioCliente.NumeroInstalacao,new float[] { 50, 385 }, false, 16),
            (relatorioCliente.Vencimento ,new float[] { 50, 290 }, false, 16),
            ($"R$ {relatorioCliente.TotalAPagar.ToString("F2")}", new float[] { 50, 190 }, false, 16),

            ($"R$ {relatorioCliente.TotalSemDesconto.ToString("F2")}", new float[] { 280, 385 }, true, 16),
            ($"R$ {(relatorioCliente.ValorEconomizadoNoMes).ToString("F2")}",new float[] { 280, 290 }, true, 16),
            ($"R$ {economiaTotal}",new float[] { 280, 190 }, true, 16),

            //Dados do cliente
            ($"CNPJ/CPF: {relatorioCliente.CnpjOuCpf}", new float[] {480, 555}, true, 8),
            ($"RAZÃO SOCIAL/NOME: {relatorioCliente.RazaoSocialOuNome}", new float[] {480, 540}, true, 8),
            ($"EMAIL: {relatorioCliente.Email}", new float[] {480, 525}, true, 8),    
            ($"ENDEREÇO: {enderecoLinha1}", new float[] {480, 510}, true, 8),
            (enderecoLinha2, new float[] {480, 500}, true, 8),
            
            //Área do Gráfico
            ($"Economia Anual (valores em R$): R${economiaTotal}", new float[] {500, 415}, false, 14),
            ($"R$ {relatorioCliente.QtdCompensacao.ToString("F2")}", new float[] {670, 478}, false, 14),
            ($"R$ {relatorioCliente.QtdCompensacao.ToString("F2")}", new float[] {670, 457}, false, 14),
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
        private List<string> QuebrarTextoEmLinhas(string texto, int maxCaracteresPorLinha, int maxLinhas = 2)
        {
            var linhas = new List<string>();
            var palavras = texto.Split(' ');

            string linhaAtual = "";

            foreach (var palavra in palavras)
            {
                if ((linhaAtual + " " + palavra).Trim().Length <= maxCaracteresPorLinha)
                {
                    linhaAtual = (linhaAtual + " " + palavra).Trim();
                }
                else
                {
                    linhas.Add(linhaAtual);
                    linhaAtual = palavra;

                    if (linhas.Count == maxLinhas - 1)
                        break;
                }
            }
            // Se ainda sobrou algo no buffer, adiciona como última linha (mesmo que ultrapasse o limite de caracteres)
            var restante = string.Join(" ", palavras.Skip(linhas.SelectMany(l => l.Split(' ')).Count()));
            if (!string.IsNullOrWhiteSpace(restante))
            {
                linhas.Add(restante.Trim());
            }
            else if (!string.IsNullOrWhiteSpace(linhaAtual) && linhas.Count < maxLinhas)
            {
                linhas.Add(linhaAtual.Trim());
            }

            return linhas;

        }

    }
}
