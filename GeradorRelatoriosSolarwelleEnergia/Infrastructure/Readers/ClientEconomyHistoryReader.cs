using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GeradorRelatoriosSolarwelleEnergia.Infrastructure.Readers
{
    internal class ClientEconomyHistoryReader : IClientEconomyHistoryReader
    {
        public Dictionary<string, Dictionary<string, float>> Load()
        {
            string path = Path.Combine(AppContext.BaseDirectory, "Assets", "historicoEconomiaClientes.json");
            string json = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, float>>>(json) ?? new();
        }
    }
}
