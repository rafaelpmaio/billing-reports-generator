using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using OfficeOpenXml;

namespace GeradorRelatoriosSolarwelleEnergia.Dominio.Entidades
{
    internal class Cliente
    {
        public string NumeroCliente { get; set; }
        public string[] NumeroInstalacoes { get; set; }
        public string Telefone { get; set; }
        public string Endereco { get; set; }
        public string Email { get; set; }
        public string DistribuidoraLocal { get; set; } //pertinente?
        public string DescontoPercentual { get; set; }
             
                
        public override string ToString()
        {
            return $"Número Cliente: {NumeroCliente}, " +
                   $"Número de Instalação: {NumeroInstalacoes}, " +
                   $"Telefone: {Telefone}, " +
                   $"Endereco: {Endereco}, " +
                   $"Email: {Email}, " +
                   $"Distribuidora Local: {DistribuidoraLocal}";
        }
    }
}

