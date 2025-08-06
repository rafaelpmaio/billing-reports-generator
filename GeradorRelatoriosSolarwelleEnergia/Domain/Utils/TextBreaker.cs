using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeradorRelatoriosSolarwelleEnergia.Domain.Utils
{
    internal class TextBreaker
    {
        public static List<string> EmLinhas (string text, int maxCharsPerLine, int maxLines = 2)
        {
            var lines = new List<string>();
            var words = text.Split(' ');
            string currentLine = "";

            foreach (var word in words)
            {
                if ((currentLine + " " + word).Trim().Length <= maxCharsPerLine)
                {
                    currentLine = (currentLine + " " + word).Trim();
                }
                else
                {
                    lines.Add(currentLine);
                    currentLine = word;

                    if (lines.Count == maxLines - 1)
                        break;
                }
            }
            // Se ainda sobrou algo no buffer, adiciona como última linha (mesmo que ultrapasse o limite de caracteres)
            var restante = string.Join(" ", words.Skip(lines.SelectMany(l => l.Split(' ')).Count()));
            if (!string.IsNullOrWhiteSpace(restante))
            {
                lines.Add(restante.Trim());
            }
            else if (!string.IsNullOrWhiteSpace(currentLine) && lines.Count < maxLines)
            {
                lines.Add(currentLine.Trim());
            }
            return lines;

        }
    }
}
