using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeradorRelatoriosSolarwelleEnergia.Domain.DTO;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GeradorRelatoriosSolarwelleEnergia.Domain.Utils
{
    internal class InputValidator
    {
        public static bool Validate(ReportGenerationInputDto input, out string error)
        {
            if (string.IsNullOrWhiteSpace(input.CemigTablePath) ||
                string.IsNullOrWhiteSpace(input.DestinyFolder) ||
                string.IsNullOrWhiteSpace(input.PdfModelPath))
            {
                error = "Todos os campos devem estar preenchidos.";
                return false;
            }
            if (input.KwhValue <= 0)
            {
                error = "O valor do kWh deve ser maior que zero.";
                return false;
            }
            if (input.Clients == null || !input.Clients.Any())
            {
                error = "A lista de clientes está vazia.";
                return false;
            }
            error = null;
            return true;
        }
    }
}
