using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeradorRelatoriosSolarwelleEnergia.Dominio.Entidades;
using GeradorRelatoriosSolarwelleEnergia.Infrastructure.Mappers;
using OfficeOpenXml;

namespace GeradorRelatoriosSolarwelleEnergia.Infrastructure.Readers
{
    internal class ClientExcelReader : IClientReader
    {
        public List<Cliente> ReadClients(string filePath)
        {
            var tabela = new List<Cliente>();
            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                ExcelPackage.License.SetNonCommercialPersonal("GeradorRelatorios");

                var worksheet = package.Workbook.Worksheets[0];
                int rowCount = worksheet.Dimension.Rows;

                for (int row = 2; row <= rowCount; row++)
                {
                    try
                    {
                        Cliente cliente = ClientRowMapper.Map(worksheet, row);
                        if (cliente != null)
                            tabela.Add(cliente);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Erro na linha {row}: {ex.Message}");
                    }
                }
            }
            return tabela;

        }
    }
}
