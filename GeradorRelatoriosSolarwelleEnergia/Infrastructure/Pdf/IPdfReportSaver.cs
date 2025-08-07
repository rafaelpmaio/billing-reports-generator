using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeradorRelatoriosSolarwelleEnergia.Domain.Entities;

namespace GeradorRelatoriosSolarwelleEnergia.Infrastructure.Pdf
{
    internal interface IPdfReportSaver
    {
        void Generate(RelatorioCliente report, string pdfModel, string destinyFolder);
    }
}
