using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeradorRelatoriosSolarwelleEnergia.Infrastructure.Readers.Interface
{
    internal interface IClientEconomyHistoryReader
    {
        public Dictionary<string, Dictionary<string, float>> Load();
    }
}
