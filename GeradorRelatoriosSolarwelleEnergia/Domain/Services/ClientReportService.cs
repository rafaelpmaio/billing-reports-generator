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
        public List<RelatorioCliente> MontarTabelaDeRelatorios(
            List<TabelaCemig> listaTabelaCemig,
            List<Client> listaClientes,
            List<Instalacao> listaInstalacoes,
            List<Endereco> listaEnderecos,
            float valorKwhH,
            Dictionary<string, Dictionary<string, float>> historicoEconomia
            )
        {
            var listaRelatorios = new List<RelatorioCliente>();

            foreach (var tabelaCemig in listaTabelaCemig)
            {
                foreach (var instalacao in listaInstalacoes)
                {
                    if (instalacao.NumeroInstalacao.Trim() == tabelaCemig.NumeroInstalacao.Trim())
                    {
                        var cliente = listaClientes.FirstOrDefault(c => c.NumeroCliente == instalacao.NumeroCliente);

                        if (cliente != null)
                        {
                            var endereco = listaEnderecos.FirstOrDefault(e => e.Id == cliente.IdEndereco);
                            var relatorio = RelatorioClienteBuilder.Criar(instalacao, cliente, endereco, tabelaCemig, valorKwhH, historicoEconomia);
                            listaRelatorios.Add(relatorio);
                        }
                        else
                        {
                            MessageBox.Show(
                                   $"Cliente não encontrado para a instalação de número {instalacao.NumeroInstalacao}.",
                                   "Cliente não encontrado",
                                   MessageBoxButtons.OK,
                                   MessageBoxIcon.Warning
                                   );
                        }
                    }
                }
            }
            return listaRelatorios;
        }
    }
}
