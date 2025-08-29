using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeradorRelatoriosSolarwelleEnergia.Domain.Entities
{
    public class Instalacao
    {
        public string NumeroInstalacao { get; set; }
        public string NumeroCliente { get; set; }
        public string DistribuidoraLocal { get; set; }
        public string DescontoPercentual { get; set; }
        public bool Ativo {  get; set; }
    }
}
