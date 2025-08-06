using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeradorRelatoriosSolarwelleEnergia.Domain.Entities;
using GeradorRelatoriosSolarwelleEnergia.Infrastructure.Pdf;

namespace GeradorRelatoriosSolarwelleEnergia.Domain.Utils
{
    internal class PdfMapperReport
    {
        public static List<PdfField> Map(RelatorioCliente relatorio)
        {
            var economiaTotal = (relatorio.HistoricoEconomia.Sum(kvp => kvp.Value) + relatorio.ValorEconomizadoNoMes).ToString("F2");
            var endereco = TextBreaker.EmLinhas(relatorio.Endereco, 60);
            var enderecoLinha1 = endereco.ElementAtOrDefault(0);
            var enderecoLinha2 = endereco.ElementAtOrDefault(1);

            return new List<PdfField>
            { 
                new(relatorio.MesReferenciaBoleto, new float[] {155, 455}, false, 14),
                new(relatorio.NumeroInstalacao, new float[] {50, 385}, false, 16),
                new(relatorio.Vencimento, new float[] {50, 290}, false, 16),
                new($"R$ {relatorio.TotalAPagar.ToString("F2")}", new float[] {50, 190}, false, 16),

                new($"R$ {relatorio.TotalSemDesconto.ToString("F2")}", new float[] {280, 385}, true, 16),
                new($"R$ {relatorio.ValorEconomizadoNoMes.ToString("F2")}", new float[] {280, 290}, true, 16),
                new($"R$ {economiaTotal}", new float[] {280, 190}, true, 16),
        
                new($"CNPJ/CPF: {relatorio.CnpjOuCpf}", new float[] {480, 555}, true, 8),
                new($"RAZÃO SOCIAL/NOME: {relatorio.RazaoSocialOuNome}", new float[] {480, 540}, true, 8),
                new($"EMAIL: {relatorio.Email}", new float[] {480, 525}, true, 8),
                new($"ENDEREÇO: {enderecoLinha1}", new float[] {480, 510}, true, 8),
                new(enderecoLinha2, new float[] {480, 500}, true, 8),

                new($"Economia Anual (valores em R$): R$ {economiaTotal}", new float[] {500, 415}, false, 14),
                new($"R$ {relatorio.QtdCompensacao.ToString("F2")}", new float[] {670, 478}, false, 14),
                new($"R$ {relatorio.QtdCompensacao.ToString("F2")}", new float[] {670, 457}, false, 14),
            };
        }
    }
}
