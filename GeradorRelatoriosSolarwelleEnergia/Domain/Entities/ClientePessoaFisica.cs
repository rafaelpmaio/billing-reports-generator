using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeradorRelatoriosSolarwelleEnergia.Dominio.Entidades
{
    internal class ClientePessoaFisica : Client
    {
        public string Cpf { get; set; }
        public string Nome { get; set; }
        public string Rg { get; set; }
        public string Nacionalidade { get; set; }
    }
}
