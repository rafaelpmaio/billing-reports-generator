using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using GeradorRelatoriosSolarwelleEnergia.Domain.Entities;
using OfficeOpenXml;

namespace GeradorRelatoriosSolarwelleEnergia.Dominio.Entidades
{
    public class Cliente
    {
        public string NumeroCliente { get; set; }
        public string[] Instalacoes { get; set; }
        public string Telefone { get; set; }
        public int IdEndereco { get; set; }
        public string Email { get; set; }
        public int TipoCliente { get; set; }
        public bool Ativo {  get; set; }
             
        public string NumeroInstalacao { get; set; }
        public string DescontoPercentual { get; set; }
        public string DistribuidoraLocal { get; set; } 
        public Endereco Endereco = new Endereco();
                
        public override string ToString()
        {
            return $"Número Cliente: {NumeroCliente}, " +
                   $"Número de Instalação: {NumeroInstalacao}, " +
                   $"Telefone: {Telefone}, " +
                   $"Endereco: {Endereco}, " +
                   $"Email: {Email}, " +
                   $"Distribuidora Local: {DistribuidoraLocal}";
        }
    }
}

