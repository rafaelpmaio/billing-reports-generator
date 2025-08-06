using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeradorRelatoriosSolarwelleEnergia.Domain.Utils
{
    internal class MonthYearParser
    {
        private static readonly Dictionary<string, int> Months = new()

    {
        { "jan", 1 }, { "fev", 2 }, { "mar", 3 }, { "abr", 4 },
        { "mai", 5 }, { "jun", 6 }, { "jul", 7 }, { "ago", 8 },
        { "set", 9 }, { "out", 10 }, { "nov", 11 }, { "dez", 12 }
    };

        public static DateTime Parse(string key)
        {

            string[] parts = key.Split('-');
            if (parts.Length != 2)
                throw new FormatException($"Formato inválido: {key}");

            string monthAbrev = parts[0].ToLower().Trim().TrimEnd('.');
            int year = 2000 + int.Parse(parts[1]);

            if (!Months.TryGetValue(monthAbrev, out int month))
                throw new FormatException($"Mês inválido: {monthAbrev}");

            return new DateTime(year, month, 1);
        }
        public static string Normalize(string key)
        {
            return key?.Trim().ToLower().TrimEnd('.');
        }
    }

    }

