using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeradorRelatoriosSolarwelleEnergia.Domain.Builders;
using GeradorRelatoriosSolarwelleEnergia.Domain.DTO;
using GeradorRelatoriosSolarwelleEnergia.Domain.Entities;
using GeradorRelatoriosSolarwelleEnergia.Dominio.Entidades;
using OfficeOpenXml;

namespace GeradorRelatoriosSolarwelleEnergia.Infrastructure.Exporters
{
    internal class ExcelExporter
    {
        public void ExportToExcel(string filePath, List<Endereco> enderecos, List<Client> clientes, List<Instalacao> instalacoes)
        {
            ExcelPackage.License.SetNonCommercialPersonal("GeradorRelatorios");

            using (var package = new ExcelPackage())
            {
                var clientsDtos = clientes.Select(c =>
                {
                    return ClientDtoBuilder.FromClient(c);
                });

                // Aba ENDERECOS
                var wsEnd = package.Workbook.Worksheets.Add("ENDERECOS");
                wsEnd.Cells["A1"].LoadFromCollection(enderecos, true);

                // Aba CLIENTES
                var wsCli = package.Workbook.Worksheets.Add("CLIENTES");
                wsCli.Cells["A1"].LoadFromCollection(clientsDtos, true);

                // Aba INSTALACOES
                var wsIns = package.Workbook.Worksheets.Add("INSTALACOES");
                wsIns.Cells["A1"].LoadFromCollection(instalacoes, true);

                // Salvar o arquivo
                package.SaveAs(new FileInfo(filePath));
            }
        }
    }
}
