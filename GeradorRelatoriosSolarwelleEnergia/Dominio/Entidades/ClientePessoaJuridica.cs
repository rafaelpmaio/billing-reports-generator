using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeradorRelatoriosSolarwelleEnergia.Dominio.Entidades
{
    internal class ClientePessoaJuridica : Cliente
    {
        public string Cnpj { get; set; }
        public string RazaoSocial { get; set; }
        public string RepresentanteLegal { get; set; }
    }
}
