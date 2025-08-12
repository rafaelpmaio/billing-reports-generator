using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeradorRelatoriosSolarwelleEnergia.Domain.DTO;
using GeradorRelatoriosSolarwelleEnergia.Domain.Entities;
using GeradorRelatoriosSolarwelleEnergia.Domain.Services;
using GeradorRelatoriosSolarwelleEnergia.Domain.Utils;
using GeradorRelatoriosSolarwelleEnergia.Infrastructure.Pdf;
using GeradorRelatoriosSolarwelleEnergia.Infrastructure.Readers;

namespace GeradorRelatoriosSolarwelleEnergia.ApplicationLayer.Services
{
    internal class ReportGeneratorAppService
    {
        private readonly TabelaCemigService _cemigService;
        private readonly IClientReader _clientReader;
        private readonly IClientEconomyHistoryReader _historyReader;
        private readonly IClientReportService _reportService;
        private readonly IPdfReportSaver _pdfGenerator;

        public ReportGeneratorAppService(
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

        public void Generate(ReportGenerationInputDto input)
        {
            var extension = Path.GetExtension(input.CemigTablePath).ToLower();
            List<TabelaCemig> cemigTableList = extension switch
            {
                ".xml" => _cemigService.LerTabelaXml(input.CemigTablePath),
                ".xlsx" => _cemigService.LerTabelaExcel(input.CemigTablePath),
                _ => throw new NotSupportedException("Formato de arquivo da Tabela CEMIG não suportado.")
            };

            var clientsList = _clientReader.LerTabelaExcel(input.ClientsTablePath);
            var economyHistory = _historyReader.Load();
            var relatorios = _reportService.MontarTabelaDeRelatorios(
                cemigTableList,
                clientsList,
                input.KwhValue,
                economyHistory
                );

            foreach (var relatorio in relatorios)
            {
                _pdfGenerator.Generate(relatorio, input.PdfModelPath, input.DestinyFolder);
            }
        }
    }
}
