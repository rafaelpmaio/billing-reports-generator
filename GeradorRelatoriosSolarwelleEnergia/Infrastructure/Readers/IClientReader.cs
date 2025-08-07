using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeradorRelatoriosSolarwelleEnergia.Dominio.Entidades;

namespace GeradorRelatoriosSolarwelleEnergia.Infrastructure.Readers
{
    internal interface IClientReader
    {
        List<Cliente> LerTabelaExcel(string filePath);
    }
}
