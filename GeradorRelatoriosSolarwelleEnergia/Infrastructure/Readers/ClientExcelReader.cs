using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeradorRelatoriosSolarwelleEnergia.Domain.DTO;
using GeradorRelatoriosSolarwelleEnergia.Dominio.Entidades;
using GeradorRelatoriosSolarwelleEnergia.Infrastructure.Mappers;
using GeradorRelatoriosSolarwelleEnergia.Infrastructure.Readers.Interface;
using OfficeOpenXml;

namespace GeradorRelatoriosSolarwelleEnergia.Infrastructure.Readers
{
    internal class ClientExcelReader : IEntityReader<ClientDto>
    {
        public List<ClientDto> Read(string filePath)
        {
            var table = new List<ClientDto>();
            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                ExcelPackage.License.SetNonCommercialPersonal("GeradorRelatorios");

                var worksheet = package.Workbook.Worksheets["CLIENTES"];
                int rowCount = worksheet.Dimension.Rows;

                for (int row = 2; row <= rowCount; row++)
                {
                    try
                    {
                        var dto = ClientRowMapper.Map(worksheet, row); 
                        if (dto != null)
                            table.Add(dto);
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
