using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeradorRelatoriosSolarwelleEnergia.Domain.Entities;
using GeradorRelatoriosSolarwelleEnergia.Domain.Services;
using GeradorRelatoriosSolarwelleEnergia.Infrastructure.Pdf;
using GeradorRelatoriosSolarwelleEnergia.Infrastructure.Readers;

namespace GeradorRelatoriosSolarwelleEnergia.ApplicationLayer.Services
{
    internal class ReportGeneratorHandler
    {
        private readonly TabelaCemigService _cemigService;
        private readonly IClientReader _clientReader;
        private readonly IClientEconomyHistoryReader _historyReader;
        private readonly IClientReportService _reportService;
        private readonly IPdfReportSaver _pdfGenerator;

        public ReportGeneratorHandler(
            TabelaCemigService cemigService,
            IClientReader clientReader,
            IClientEconomyHistoryReader historyReader, 
            IClientReportService reportService,
            IPdfReportSaver pdfGenerator)
        {
            _cemigService = cemigService;
            _clientReader = clientReader;
            _historyReader = historyReader;
            _reportService = reportService;
            _pdfGenerator = pdfGenerator;
        }

        public void Generate(string cemigPath, string clientsPath, float kwhValue, string destinyPath, string modelPath)
        {
            var extension = Path.GetExtension(cemigPath).ToLower();
            List<TabelaCemig> cemigTableList = extension switch
            {
                ".xml" => _cemigService.LerTabelaXml(cemigPath),
                ".xlsx" => _cemigService.LerTabelaExcel(cemigPath),
                _ => throw new NotSupportedException("Formato de arquivo da Tabela CEMIG não suportado.")
            };

            var clientsList = _clientReader.LerTabelaExcel(clientsPath);
            var economyHistory = _historyReader.Load();
            var relatorios = _reportService.MontarTabelaDeRelatorios(cemigTableList, clientsList, kwhValue, economyHistory);

            foreach (var relatorio in relatorios)
            {
                _pdfGenerator.Generate(relatorio, modelPath, destinyPath);
            }
        }
    }
}
