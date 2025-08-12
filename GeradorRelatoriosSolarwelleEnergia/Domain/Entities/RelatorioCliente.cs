using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeradorRelatoriosSolarwelleEnergia.Dominio.Entidades;
using Newtonsoft.Json;
using System.Globalization;
using GeradorRelatoriosSolarwelleEnergia.Domain.Utils;

namespace GeradorRelatoriosSolarwelleEnergia.Domain.Entities
{
    internal class RelatorioCliente
    {
        public string NumeroInstalacao { get; set; }
        public string Vencimento { get; set; } //todo dia 3, mas podendo ser alterado por input
        public string MesReferenciaBoleto { get; set; }
        public float QtdCompensacao { get; set; }
        public float QtdConsumo { get; set; }
        public string CnpjOuCpf { get; set; }
        public string RazaoSocialOuNome { get; set; }
        public string Endereco { get; set; }
        public string Email { get; set; }
        public float ValorKwhHora { get; set; }
        public float PercentualDesconto { get; set; }
        public float FatorDeDesconto => 1 - (PercentualDesconto / 100);
        public double TotalSemDesconto => QtdCompensacao * ValorKwhHora;
        public double TotalAPagar => TotalSemDesconto * FatorDeDesconto;
        public double ValorEconomizadoNoMes => TotalSemDesconto - TotalAPagar;
        public Dictionary<string, float> HistoricoEconomia { get; set; } = new();

        public override string ToString()
        {
            return $"Número de Instalação: {NumeroInstalacao}, " +
                   $"Consumo: {QtdConsumo}, " +
                   $"CNPJ/CPF: {CnpjOuCpf}" +
                   $"Razão Social/Nome {RazaoSocialOuNome}";
        }
    }
}

