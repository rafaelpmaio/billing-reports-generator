using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeradorRelatoriosSolarwelleEnergia.Domain.Entities;
using GeradorRelatoriosSolarwelleEnergia.Dominio.Entidades;

namespace GeradorRelatoriosSolarwelleEnergia.Domain.Builders
{
    internal class RelatorioClienteBuilder
    {
        public static RelatorioCliente Criar(Cliente cliente, TabelaCemig tabelaCemig, float kwhHora, Dictionary<string, Dictionary<string, float>> historicoEconomia)
        {
            DateTime data = DateTime.ParseExact(tabelaCemig.Periodo, "yyyy/MM", null);
            string vencimento = new DateTime(data.Year, data.AddMonths(2).Month, 3).ToString("dd/MM/yyyy", new CultureInfo("pt-BR")).ToUpper();
            string mesRef = new DateTime(data.Year, data.Month, 1).ToString("MMMM/yyyy", new CultureInfo("pt-BR")).ToUpper();

            var relatorio = new RelatorioCliente()
            {
                NumeroInstalacao = tabelaCemig.NumeroInstalacao,
                QtdConsumo = float.Parse(tabelaCemig.QtdConsumo, CultureInfo.InvariantCulture),
                QtdCompensacao = float.Parse(tabelaCemig.QtdCompensacao, CultureInfo.InvariantCulture),
                Endereco = cliente.Endereco?.ToString() ?? "",
                Email = cliente.Email,
                ValorKwhHora = kwhHora,
                Vencimento = vencimento,
                MesReferenciaBoleto = mesRef,
                DescontoPercentual = float.Parse(cliente.DescontoPercentual),
                CnpjOuCpf = cliente is ClientePessoaFisica pf ? pf.Cpf : ((ClientePessoaJuridica)cliente).Cnpj,
                RazaoSocialOuNome = cliente is ClientePessoaFisica pf2 ? pf2.Nome : ((ClientePessoaJuridica)cliente).RazaoSocial,

            };
            if (historicoEconomia.TryGetValue(relatorio.NumeroInstalacao, out var hist)) { }
                relatorio.HistoricoEconomia = hist;

            return relatorio;
        }
    }
}
