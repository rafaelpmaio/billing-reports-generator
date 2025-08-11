using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeradorRelatoriosSolarwelleEnergia.Infrastructure.Pdf
{
    internal class AnnualEconomyGraphicDrawer
    {
        public static byte[] Draw(List<KeyValuePair<string, float>> data)
        {
            int width = 900;
            int heigh = 600;
            var bmp = new Bitmap(width, heigh);
            var graphic = Graphics.FromImage(bmp);

            graphic.Clear(Color.White);
            graphic.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // Configurações do gráfico
            float margin = 70;
            int monthsTotal = 12;
            float widthAvaileble = width - 2 * margin;
            float barWidth = widthAvaileble / (monthsTotal * 1.5f);
            float spaceBtw = barWidth * 0.5f;
            float depth3D = 10;
            float utilHeigh = heigh - 2 * margin;

            float actualMaxValue = data.Max(kvp => kvp.Value);
            float roundedMaxValue = (float)(Math.Ceiling(actualMaxValue / 100) * 100);

            float x = margin + spaceBtw;
            float yBase = heigh - margin;

            // Estilos
            Pen eixoPen = new Pen(Color.Black, 2);
            Pen gradePen = new Pen(Color.LightGray, 1);
            Font font = new Font("Arial", 12, FontStyle.Bold);

            Color azulPetroleo = Color.FromArgb(0, 153, 153);
            Color azulPetroleoEscuro = Color.FromArgb(0, 102, 102);

            Brush barraFrontal = new SolidBrush(azulPetroleo);
            Brush barraLateral = new SolidBrush(azulPetroleoEscuro);

            // Desenhar eixos
            graphic.DrawLine(eixoPen, margin, margin, margin, yBase); // eixo Y
            graphic.DrawLine(eixoPen, margin, yBase, width - margin, yBase); // eixo X

            // Grades horizontais
            int numLinhasGrade = 5;
            for (int i = 1; i <= numLinhasGrade; i++)
            {
                float yGrade = yBase - utilHeigh / numLinhasGrade * i;
                graphic.DrawLine(gradePen, margin, yGrade, width - margin, yGrade);

                float valorGrade = roundedMaxValue / numLinhasGrade * i;
                string label = valorGrade.ToString("0.##");
                SizeF tamanho = graphic.MeasureString(label, font);
                graphic.DrawString(label, font, Brushes.Gray, margin - tamanho.Width - 5, yGrade - tamanho.Height / 2);
            }

            // Desenhar barras 3D
            foreach (var item in data)
            {
                string mes = item.Key;
                float valor = item.Value;

                float alturaBarra = valor / roundedMaxValue * utilHeigh;
                float y = yBase - alturaBarra;

                // Frente da barra
                graphic.FillRectangle(barraFrontal, x, y, barWidth, alturaBarra);

                // Lateral direita (simula 3D)
                Point[] lateralDireita =
                {
                    new Point((int)(x + barWidth), (int)y),
                    new Point((int)(x + barWidth + depth3D), (int)(y - depth3D)),
                    new Point((int)(x + barWidth + depth3D), (int)(y + alturaBarra - depth3D)),
                    new Point((int)(x + barWidth), (int)(y + alturaBarra))
                };
                graphic.FillPolygon(barraLateral, lateralDireita);

                // Topo da barra (simula profundidade no topo)
                Point[] topo =
                {
                    new Point((int)x, (int)y),
                    new Point((int)(x + depth3D), (int)(y - depth3D)),
                    new Point((int)(x + barWidth + depth3D), (int)(y - depth3D)),
                    new Point((int)(x + barWidth), (int)y)
                };
                graphic.FillPolygon(Brushes.LightBlue, topo);

                // Nome do mês
                graphic.DrawString(mes, font, Brushes.Black, x, yBase + 5);

                // Valor acima da barra
                string valorTexto = valor.ToString("0.##");
                SizeF tamanhoTexto = graphic.MeasureString(valorTexto, font);
                float posXValor = x + (barWidth - tamanhoTexto.Width) / 2;
                float posYValor = y - tamanhoTexto.Height - 2;
                graphic.DrawString(valorTexto, font, Brushes.Black, posXValor, posYValor);

                x += barWidth + spaceBtw;
            }

            using var ms = new MemoryStream();
            bmp.Save(ms, ImageFormat.Png);
            return ms.ToArray();
        }
    }
}
