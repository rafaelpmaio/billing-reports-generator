using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeradorRelatoriosSolarwelleEnergia.Domain.Builders;
using GeradorRelatoriosSolarwelleEnergia.Domain.Entities;
using GeradorRelatoriosSolarwelleEnergia.Infrastructure.Pdf;

namespace GeradorRelatoriosSolarwelleEnergia.ApplicationLayer.Services
{
    internal class AnnualEconomyGraphicGenerator
    {
        public static byte[] Generate(RelatorioCliente report)
        {
            var data = AnnualEconomyBuilder.Build(report);
            return AnnualEconomyGraphicDrawer.Draw(data);
        }
    }
}
