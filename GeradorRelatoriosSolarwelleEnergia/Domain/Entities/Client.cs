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
    public class Client
    {
        public string NumeroCliente { get; set; }
        public string[] Instalacoes { get; set; }
        public string Telefone { get; set; }
        public int IdEndereco { get; set; }
        public string Email { get; set; }
        public int TipoCliente { get; set; }         
        public bool Ativo {  get; set; }

        public string InstalacoesString { get; set; }

        public override string ToString()
        {
            return $"Número Cliente: {NumeroCliente}, " +
                   $"Instalações: {InstalacoesString}, " +
                   $"Telefone: {Telefone}, " +
                   $"IdEndereço: {IdEndereco}, " +
                   $"Email: {Email}, " +
                   $"Tipo do Cliente: {(TipoCliente == 1 ? "PJ" : "PF")}";
        }
    }
}

