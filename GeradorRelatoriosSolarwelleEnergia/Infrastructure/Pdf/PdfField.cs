using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeradorRelatoriosSolarwelleEnergia.Infrastructure.Pdf
{
    internal class PdfField
    {
        public string Value { get; set; }
        public float[] Coordinates { get; set; } = new float[2];
        public bool Bold { get; set; }
        public int FontSize { get; set; }

        public PdfField(string value, float[] coordinates, bool bold, int fontSize)
        {
            Value = value;
            Coordinates = coordinates;
            Bold = bold;
            FontSize = fontSize;
        }
    }
}
