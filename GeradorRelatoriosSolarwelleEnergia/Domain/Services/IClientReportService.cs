using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeradorRelatoriosSolarwelleEnergia.Domain.Entities;
using GeradorRelatoriosSolarwelleEnergia.Dominio.Entidades;

namespace GeradorRelatoriosSolarwelleEnergia.Domain.Services
{
    internal interface IClientReportService
    {
        public List<RelatorioCliente> MontarTabelaDeRelatorios(
            List<TabelaCemig> listaTabelaCemig,
            List<Cliente> listaClientes,
            float valorKwhH,
            Dictionary<string,
            Dictionary<string,
            float>> historicoEconomia);
    }
}
