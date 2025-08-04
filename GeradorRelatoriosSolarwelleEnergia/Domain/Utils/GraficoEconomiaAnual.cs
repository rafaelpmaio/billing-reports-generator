using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeradorRelatoriosSolarwelleEnergia.Domain.Entities;

namespace GeradorRelatoriosSolarwelleEnergia.Domain.Utils
{
    internal class GraficoEconomiaAnual
    {

        private static DateTime ParseMesAno(string chave)
        {
            var meses = new Dictionary<string, int>
    {
        { "jan", 1 }, { "fev", 2 }, { "mar", 3 }, { "abr", 4 },
        { "mai", 5 }, { "jun", 6 }, { "jul", 7 }, { "ago", 8 },
        { "set", 9 }, { "out", 10 }, { "nov", 11 }, { "dez", 12 }
    };

            string[] partes = chave.Split('-');
            if (partes.Length != 2)
                throw new FormatException($"Formato inválido: {chave}");

            string mesAbrev = partes[0].ToLower().Trim().TrimEnd('.');
            int ano = 2000 + int.Parse(partes[1]);

            if (!meses.TryGetValue(mesAbrev, out int mes))
                throw new FormatException($"Mês inválido: {mesAbrev}");

            return new DateTime(ano, mes, 1);
        }

        private static string NormalizeMesAno(string chave)
        {
            return chave?.Trim().ToLower().TrimEnd('.');
        }

        public static byte[] GerarGraficoColunas(RelatorioCliente relatorio)
        {
            int largura = 900;
            int altura = 600;
            var bmp = new Bitmap(largura, altura);
            var g = Graphics.FromImage(bmp);
            g.Clear(Color.White);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // Configurações do gráfico
            float margem = 70;
            int totalMeses = 12;
            float larguraDisponivel = largura - 2 * margem;
            float larguraBarra = larguraDisponivel / (totalMeses * 1.5f);
            float espacoEntre = larguraBarra * 0.5f;
            float profundidade3D = 10;
            float alturaUtil = altura - 2 * margem;

            var listaHistoricoEconomia = new List<KeyValuePair<string, float>>();

            //Adiciona valor economizado no mês atual à tabela
            DateTime mesRef;
            if (DateTime.TryParseExact(relatorio.MesReferenciaBoleto, "MMMM/yyyy", new CultureInfo("pt-BR"), DateTimeStyles.None, out mesRef))
            {
                // Gera chave no formato "set-25"
                string chaveMes = mesRef.ToString("MMM-yy", new CultureInfo("pt-BR")).ToLower().Replace(".", "");

                // Cria dicionário para evitar duplicatas
                var dictHistorico = relatorio.HistoricoEconomia
       .ToDictionary(kvp => NormalizeMesAno(kvp.Key), kvp => kvp.Value);

                // Adiciona o mês atualizado
                dictHistorico[chaveMes] = (float)relatorio.ValorEconomizadoNoMes;

                listaHistoricoEconomia = dictHistorico
        .OrderBy(kvp => ParseMesAno(kvp.Key))
        .ToList();
            }
            else
            {
                // Caso não consiga parse, apenas copia a lista original
                listaHistoricoEconomia = relatorio.HistoricoEconomia.ToList();
            }

            float maxValorReal = listaHistoricoEconomia.Max(kvp => kvp.Value);
            float maxValor = (float)(Math.Ceiling(maxValorReal / 100) * 100);

            float x = margem + espacoEntre;
            float yBase = altura - margem;

            // Estilos
            Pen eixoPen = new Pen(Color.Black, 2);
            Pen gradePen = new Pen(Color.LightGray, 1);

            Color azulPetroleo = Color.FromArgb(0, 153, 153);
            Color azulPetroleoEscuro = Color.FromArgb(0, 102, 102);

            Brush barraFrontal = new SolidBrush(azulPetroleo);
            Brush barraLateral = new SolidBrush(azulPetroleoEscuro);
            Font fonte = new Font("Arial", 12, FontStyle.Bold);

            //Brush barraBrush = new SolidBrush(Color.DodgerBlue);

            // Desenhar eixos
            g.DrawLine(eixoPen, margem, margem, margem, yBase); // eixo Y
            g.DrawLine(eixoPen, margem, yBase, largura - margem, yBase); // eixo X

            // Grades horizontais
            int numLinhasGrade = 5;
            for (int i = 1; i <= numLinhasGrade; i++)
            {
                float yGrade = yBase - alturaUtil / numLinhasGrade * i;
                g.DrawLine(gradePen, margem, yGrade, largura - margem, yGrade);

                float valorGrade = maxValor / numLinhasGrade * i;
                string label = valorGrade.ToString("0.##");
                SizeF tamanho = g.MeasureString(label, fonte);
                g.DrawString(label, fonte, Brushes.Gray, margem - tamanho.Width - 5, yGrade - tamanho.Height / 2);
            }

            // Desenhar barras 3D
            foreach (var item in listaHistoricoEconomia)
            {
                string mes = item.Key;
                float valor = item.Value;

                float alturaBarra = valor / maxValor * alturaUtil;
                float y = yBase - alturaBarra;

                // Frente da barra
                g.FillRectangle(barraFrontal, x, y, larguraBarra, alturaBarra);

                // Lateral direita (simula 3D)
                Point[] lateralDireita =
                {
                    new Point((int)(x + larguraBarra), (int)y),
                    new Point((int)(x + larguraBarra + profundidade3D), (int)(y - profundidade3D)),
                    new Point((int)(x + larguraBarra + profundidade3D), (int)(y + alturaBarra - profundidade3D)),
                    new Point((int)(x + larguraBarra), (int)(y + alturaBarra))
                };
                g.FillPolygon(barraLateral, lateralDireita);

                // Topo da barra (simula profundidade no topo)
                Point[] topo =
                {
                    new Point((int)x, (int)y),
                    new Point((int)(x + profundidade3D), (int)(y - profundidade3D)),
                    new Point((int)(x + larguraBarra + profundidade3D), (int)(y - profundidade3D)),
                    new Point((int)(x + larguraBarra), (int)y)
                };
                g.FillPolygon(Brushes.LightBlue, topo);

                // Nome do mês
                g.DrawString(mes, fonte, Brushes.Black, x, yBase + 5);

                // Valor acima da barra
                string valorTexto = valor.ToString("0.##");
                SizeF tamanhoTexto = g.MeasureString(valorTexto, fonte);
                float posXValor = x + (larguraBarra - tamanhoTexto.Width) / 2;
                float posYValor = y - tamanhoTexto.Height - 2;
                g.DrawString(valorTexto, fonte, Brushes.Black, posXValor, posYValor);

                x += larguraBarra + espacoEntre;
            }

            using var ms = new MemoryStream();
            bmp.Save(ms, ImageFormat.Png);
            return ms.ToArray();
        }
    }
}

