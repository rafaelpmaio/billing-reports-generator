using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeradorRelatoriosSolarwelleEnergia.Dominio.Entidades;

namespace GeradorRelatoriosSolarwelleEnergia.Domain.DTO
{
    internal class ClientDto
    {
        public string NumeroCliente { get; set; }
        public string Instalacoes { get; set; } 
        public string RazaoSocialOuNome { get; set; }
        public string CnpjOuCpf { get; set; }
        public string RepresentanteLegal { get; set; }
        public string Rg { get; set; } 
        public string Telefone { get; set; }
        public int IdEndereco { get; set; }
        public string Email { get; set; }
        public int TipoCliente { get; set; } 
        public bool Ativo { get; set; }
               
    }
}
