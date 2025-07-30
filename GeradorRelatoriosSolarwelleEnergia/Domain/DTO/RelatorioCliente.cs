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

namespace GeradorRelatoriosSolarwelleEnergia.Dominio.DTO
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
        public double TotalSemDesconto { get; set; }
        public double TotalAPagar { get; set; }
        public Dictionary<string, float> HistoricoEconomia { get; set; } = new Dictionary<string, float>();

        public RelatorioCliente(Cliente cliente, TabelaCemig tabelaCemig, float kwhHora)
        {
            this.NumeroInstalacao = tabelaCemig.NumeroInstalacao;
            DateTime data =  DateTime.ParseExact(tabelaCemig.Periodo, "yyyy/MM", null);
            this.Vencimento = new DateTime(data.Year, data.AddMonths(2).Month,3).ToString("dd/MM/yyyy", new CultureInfo("pt-BR")).ToUpper();
            this.MesReferenciaBoleto = new DateTime(data.Year, data.Month, 1).ToString("MMMM/yyyy", new CultureInfo("pt-BR")).ToUpper();
            this.QtdConsumo = float.Parse(tabelaCemig.QtdConsumo, CultureInfo.InvariantCulture)  ;
            this.QtdCompensacao = float.Parse(tabelaCemig.QtdCompensacao, CultureInfo.InvariantCulture);
            this.Endereco = cliente.Endereco;
            this.Email = cliente.Email;
            this.ValorKwhHora = kwhHora;
            this.TotalSemDesconto = this.QtdConsumo * this.ValorKwhHora;
            this.TotalAPagar = this.TotalSemDesconto * 0.8;

            this.CarregarHistoricoEconomiaAnual();

            if (cliente is ClientePessoaFisica pessoaFisica)
            {
                this.CnpjOuCpf = pessoaFisica.Cpf;
                this.RazaoSocialOuNome = pessoaFisica.Nome;
            }
            else if (cliente is ClientePessoaJuridica pessoaJuridica)
            {
                this.CnpjOuCpf = pessoaJuridica.Cnpj;
                this.RazaoSocialOuNome = pessoaJuridica.RazaoSocial;
            }
        }

        public static List<RelatorioCliente> montarTabelaDeRelatorios(List<TabelaCemig> listaTabelaCemig, List<Cliente> listaClientes, float valorKwhH)
        {
            List<RelatorioCliente> listaRelatorios = new List<RelatorioCliente>();

            foreach (var tabelaCemig in listaTabelaCemig)
            {
                foreach (var cliente in listaClientes)
                {
                    if (cliente.NumeroInstalacoes.Contains(tabelaCemig.NumeroInstalacao))
                    {
                        RelatorioCliente relatorio = new RelatorioCliente(cliente, tabelaCemig, valorKwhH);
                        listaRelatorios.Add(relatorio);
                    }
                }
            }
            return listaRelatorios;
        }

        //Cria a prop HistoricoEconomia
        public void CarregarHistoricoEconomiaAnual()
        {
            string caminhoHistoricoEconomiaJson = Path.Combine(AppContext.BaseDirectory, "Assets", "historicoEconomiaClientes.json");

            // Lê e desserializa o arquivo JSON
            string json = File.ReadAllText(caminhoHistoricoEconomiaJson);
            var dadosJson = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, float>>>(json);

            if (dadosJson.ContainsKey(this.NumeroInstalacao))
            {
                var historico = dadosJson[this.NumeroInstalacao];

                // Adiciona todos os meses ao HistoricoEconomia usando chave composta "mes-ano - numInstalacao"
                foreach (var item in historico)
                {                    
                    HistoricoEconomia[item.Key] = item.Value;
                }
            }
            else
            {
                Console.WriteLine($"Instalação {this.NumeroInstalacao} não encontrada no JSON.");
            }
        }

        public override string ToString()
        {
            return $"Número de Instalação: {NumeroInstalacao}, " +
                   $"Consumo: {QtdConsumo}, " +
                   $"CNPJ/CPF: {CnpjOuCpf}" +
                   $"Razão Social/Nome {RazaoSocialOuNome}";
        }

    }
}

