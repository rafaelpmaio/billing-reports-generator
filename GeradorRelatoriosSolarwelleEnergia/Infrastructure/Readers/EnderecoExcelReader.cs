using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeradorRelatoriosSolarwelleEnergia.Domain.Entities;
using GeradorRelatoriosSolarwelleEnergia.Infrastructure.Mappers;
using GeradorRelatoriosSolarwelleEnergia.Infrastructure.Readers.Interface;
using OfficeOpenXml;

namespace GeradorRelatoriosSolarwelleEnergia.Infrastructure.Readers
{
    internal class EnderecoExcelReader : IEntityReader<Endereco>
    {
        public List<Endereco> Read(string filePath)
        {
            var table = new List<Endereco>();
            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                ExcelPackage.License.SetNonCommercialPersonal("GeradorRelatorios");

                var worksheet = package.Workbook.Worksheets[0];
                int rowCount = worksheet.Dimension.Rows;

                for (int row = 2; row <= rowCount; row++)
                {
                    try
                    {
                        Endereco endereco = EnderecoRowMapper.Map(worksheet, row);
                        if (endereco != null)
                            table.Add(endereco);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Erro na linha {row}: {ex.Message}");
                    }
                }
            }
            return table;
        }
    }
}
