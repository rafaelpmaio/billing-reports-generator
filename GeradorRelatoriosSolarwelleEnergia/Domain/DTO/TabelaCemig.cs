using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using OfficeOpenXml;
using ClosedXML.Excel;

namespace GeradorRelatoriosSolarwelleEnergia.Dominio.DTO
{
    internal class TabelaCemig
    {
        public virtual string Modalidade { get; set; }
        public virtual string NumeroInstalacao { get; set; }
        public virtual string Periodo { get; set; }
        public virtual string Quota { get; set; }
        public virtual string PostoHorario { get; set; }
        public virtual string SaldoAnterior { get; set; }
        public virtual string SaldoExpirado { get; set; }
        public virtual string QtdConsumo { get; set; } //talvez nome + sugestivo
        public virtual string QtdGeracao { get; set; } //talvez nome + sugestivo
        public virtual string QtdCompensacao { get; set; } //talvez nome + sugestivo
        public virtual string QtdTransfencia { get; set; } //talvez nome + sugestivo
        public virtual string QtdRecebimento { get; set; } //talvez nome + sugestivo
        public virtual string SaldoAtual { get; set; }
        public virtual string QtdSaldoExpirar { get; set; }
        public virtual string PeriodoSaldoExpirar { get; set; }


        public static List<TabelaCemig> LerTabelaExcel(string filePath)
        {
            var tabela = new List<TabelaCemig>();

            using (var workbook = new XLWorkbook(filePath))
            {

                var worksheet = workbook.Worksheets.Worksheet(1);
                int rowCount = worksheet.LastRowUsed().RowNumber();

                for (int row = 2; row <= rowCount; row++)
                {
                    var item = new TabelaCemig
                    {
                        Modalidade = worksheet.Cell(row, 1).GetString(),
                        NumeroInstalacao = worksheet.Cell(row, 2).GetString(),
                        Periodo = ParsePeriodo(worksheet.Cell(row, 3).GetString()),
                        Quota = worksheet.Cell(row, 4).GetString(),
                        PostoHorario = worksheet.Cell(row, 5).GetString(),
                        SaldoAnterior = worksheet.Cell(row, 6).GetString(),
                        SaldoExpirado = worksheet.Cell(row, 7).GetString(),
                        QtdConsumo = worksheet.Cell(row, 8).GetString(),
                        QtdGeracao = worksheet.Cell(row, 9).GetString(),
                        QtdCompensacao = worksheet.Cell(row, 10).GetString(),
                        QtdTransfencia = worksheet.Cell(row, 11).GetString(),
                        QtdRecebimento = worksheet.Cell(row, 12).GetString(),
                        SaldoAtual = worksheet.Cell(row, 13).GetString(),
                        QtdSaldoExpirar = worksheet.Cell(row, 14).GetString(),
                        PeriodoSaldoExpirar = worksheet.Cell(row, 15).GetString(),
                    };
                    tabela.Add(item);
                }
            }
            return tabela;
        }

        public static List<TabelaCemig> LerTabelaXML(string filePath)
        {
            var doc = XDocument.Load(filePath);

            List<TabelaCemig> tabela = doc.Descendants("Linha")
                .Select(campo => new TabelaCemig
                {
                    Periodo = ParsePeriodo((string)campo.Element("Periodo")),
                    Modalidade = (string)campo.Element("Modalidade"),
                    NumeroInstalacao = (string)campo.Element("Instalacao"),
                    Quota = (string)campo.Element("Quota"),
                    PostoHorario = (string)campo.Element("Posto_horario"),
                    QtdConsumo = (string)campo.Element("Qtd_consumo"),
                    QtdGeracao = (string)campo.Element("Qtd_geracao"),
                    QtdCompensacao = (string)campo.Element("Qtd_compensacao"),
                    SaldoAnterior = (string)campo.Element("Qtd_saldo_ant"),
                    QtdTransfencia = (string)campo.Element("Qtd_transferencia"),
                    QtdRecebimento = (string)campo.Element("Qtd_recebimento"),
                    SaldoAtual = (string)campo.Element("Qtd_saldo_atual"),
                    SaldoExpirado = (string)campo.Element("Qtd_saldo_exp"),
                    QtdSaldoExpirar = (string)campo.Element("Qtd_saldo_exp"),
                    PeriodoSaldoExpirar = (string)campo.Element("Per_prox_saldo_exp"),
                }).ToList();
            
            return tabela;
        }

        private static string ParsePeriodo(string periodo)
        {
            // Se estiver no formato MM/yyyy, converte para yyyy/MM para ficar padrão
            if (DateTime.TryParseExact(periodo, "MM/yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime dt))
            {
                return dt.ToString("yyyy/MM");
            }
            return periodo; // se já estiver no formato esperado, retorna como está
        }

        public override string ToString()
        {
            return $"Modalidade: {Modalidade}, " +
                   $"Número de Instalação: {NumeroInstalacao}, " +
                   $"Período: {Periodo}, " +
                   $"Quota: {Quota}, " +
                   $"Posto Horário: {PostoHorario}, " +
                   $"Saldo Anterior: {SaldoAnterior}, " +
                   $"Saldo Expirado: {SaldoExpirado}, " +
                   $"Consumo: {QtdConsumo}, " +
                   $"Geração: {QtdGeracao}, " +
                   $"Compensação: {QtdCompensacao}, " +
                   $"Transferido: {QtdTransfencia}, " +
                   $"Recebimento: {QtdRecebimento}, " +
                   $"Saldo Atual: {SaldoAtual}, " +
                   $"Quantidade Saldo a Expirar: {QtdSaldoExpirar}, " +
                   $"Período Saldo a Expirar: {PeriodoSaldoExpirar}";
        }
    }
}

