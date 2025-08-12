using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeradorRelatoriosSolarwelleEnergia.Domain.DTO;

namespace GeradorRelatoriosSolarwelleEnergia.Domain.Utils
{
    internal class InputValidator
    {
        public static bool Validate(ReportGenerationInputDto input, out string erro)
        {
            if (string.IsNullOrWhiteSpace(input.CemigTablePath) ||
                string.IsNullOrWhiteSpace(input.ClientsTablePath) ||
                string.IsNullOrWhiteSpace(input.DestinyFolder) ||
                string.IsNullOrWhiteSpace(input.PdfModelPath))
            {
                erro = "Todos os campos devem estar preenchidos.";
                return false;
            }
            if (input.KwhValue <= 0)
            {
                erro = "O valor do kWh deve ser maior que zero.";
                return false;
            }
            erro = null;
            return true;
        }
    }
}
