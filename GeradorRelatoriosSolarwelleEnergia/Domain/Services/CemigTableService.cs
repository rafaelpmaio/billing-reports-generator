using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClosedXML.Excel;
using GeradorRelatoriosSolarwelleEnergia.Domain.Entities;
using GeradorRelatoriosSolarwelleEnergia.Domain.Utils;
using System.Xml.Linq;
using GeradorRelatoriosSolarwelleEnergia.Domain.Builders;

namespace GeradorRelatoriosSolarwelleEnergia.Domain.Services
{
    internal class CemigTableService
    {
        public List<TabelaCemig> ReadExcelTable(string filePath)
        {
            var tabela = new List<TabelaCemig>();

            using var workbook = new XLWorkbook(filePath);
            var worksheet = workbook.Worksheets.Worksheet(1);
            int rowCount = worksheet.LastRowUsed().RowNumber();
            
            var builder = new TabelaCemigBuilder();

            for (int row = 2; row <= rowCount; row++)
            {
                var item = builder
                    .FromExcelRow(worksheet.Row(row))
                    .Build();

                tabela.Add(item);
                builder = new TabelaCemigBuilder();
            }

            return tabela;
        }
        public List<TabelaCemig> LerTabelaXml(string filePath)
        {
            var doc = XDocument.Load(filePath);

            var builder = new TabelaCemigBuilder();

            var tabela = doc.Descendants("Linha")
                .Select(elemento => builder
                    .FromXmlElement(elemento)
                    .Build())
                .ToList();

            return tabela;
        }
    }
}
