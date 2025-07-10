using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using OfficeOpenXml;

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
            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                var worksheet = package.Workbook.Worksheets[0];
                int rowCount = worksheet.Dimension.Rows;

                for (int row = 2; row <= rowCount; row++)
                {
                    var item = new TabelaCemig
                    {
                        Modalidade = worksheet.Cells[row, 1].Text,
                        NumeroInstalacao = worksheet.Cells[row, 2].Text,
                        Periodo = worksheet.Cells[row, 3].Text,
                        Quota = worksheet.Cells[row, 4].Text,
                        PostoHorario = worksheet.Cells[row, 5].Text,
                        SaldoAnterior = worksheet.Cells[row, 6].Text,
                        SaldoExpirado = worksheet.Cells[row, 7].Text,
                        QtdConsumo = worksheet.Cells[row, 8].Text,
                        QtdGeracao = worksheet.Cells[row, 9].Text,
                        QtdCompensacao = worksheet.Cells[row, 10].Text,
                        QtdTransfencia = worksheet.Cells[row, 11].Text,
                        QtdRecebimento = worksheet.Cells[row, 12].Text,
                        SaldoAtual = worksheet.Cells[row, 13].Text,
                        QtdSaldoExpirar = worksheet.Cells[row, 14].Text,
                        PeriodoSaldoExpirar = worksheet.Cells[row, 15].Text,
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
                    Periodo = (string)campo.Element("Periodo"),
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

