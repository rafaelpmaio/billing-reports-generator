using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeradorRelatoriosSolarwelleEnergia.Domain.DTO
{
    internal class ReportGenerationInputDto
    {
        public string CemigTablePath { get; set; }
        public string ClientsTablePath {  get; set; }
        public float KwhValue { get; set; }
        public string DestinyFolder {  get; set; }
        public string PdfModelPath { get; set; }

        public bool UseDatabase { get; set; }

    }
}
