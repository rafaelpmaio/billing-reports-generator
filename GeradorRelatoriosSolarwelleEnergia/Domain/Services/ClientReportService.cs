using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeradorRelatoriosSolarwelleEnergia.Domain.Builders;
using GeradorRelatoriosSolarwelleEnergia.Domain.Entities;
using GeradorRelatoriosSolarwelleEnergia.Dominio.Entidades;

namespace GeradorRelatoriosSolarwelleEnergia.Domain.Services
{
    internal class ClientReportService : IClientReportService
    {
        public List<RelatorioCliente> MontarTabelaDeRelatorios(List<TabelaCemig> listaTabelaCemig, List<Cliente> listaClientes, float valorKwhH, Dictionary<string, Dictionary<string, float>> historicoEconomia)
        {
            var listaRelatorios = new List<RelatorioCliente>();

            foreach (var tabelaCemig in listaTabelaCemig)
            {
                foreach (var cliente in listaClientes)
                {
                    if (cliente.NumeroInstalacoes.Contains(tabelaCemig.NumeroInstalacao))
                    {
                        var relatorio = RelatorioClienteBuilder.Criar(cliente, tabelaCemig, valorKwhH, historicoEconomia);
                        listaRelatorios.Add(relatorio);
                    }
                }
            }
            return listaRelatorios;
        }
    }
}
