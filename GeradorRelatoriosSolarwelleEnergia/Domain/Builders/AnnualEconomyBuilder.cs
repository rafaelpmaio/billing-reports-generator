using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeradorRelatoriosSolarwelleEnergia.Domain.Entities;
using GeradorRelatoriosSolarwelleEnergia.Domain.Utils;

namespace GeradorRelatoriosSolarwelleEnergia.Domain.Builders
{
    internal class AnnualEconomyBuilder
    {
        public static List<KeyValuePair<string, float>> Build(RelatorioCliente relatorio)
        {
            if (DateTime.TryParseExact(
                relatorio.MesReferenciaBoleto,
                "MMMM/yyyy",
                new CultureInfo("pt-BR"),
                DateTimeStyles.None,
                out DateTime monthRef))
            {
                // Gera chave no formato "set-25"
                string keyMonth = monthRef.ToString("MMM-yy", new CultureInfo("pt-BR")).ToLower().Replace(".", "");

                // Cria dicionário para evitar duplicatas
                var dictHistorico = relatorio.HistoricoEconomia
                    .ToDictionary(kvp => MonthYearParser.Normalize(kvp.Key), kvp => kvp.Value);                              
                                
                // Adiciona o mês atualizado
                dictHistorico[keyMonth] = (float)relatorio.ValorEconomizadoNoMes;

                return dictHistorico
                    .OrderBy(kvp => MonthYearParser.Parse(kvp.Key))
                    .ToList();
            }
            return relatorio.HistoricoEconomia.ToList();
        }
    }
}
