using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClosedXML.Excel;
using GeradorRelatoriosSolarwelleEnergia.Domain.Entities;
using System.Xml.Linq;

namespace GeradorRelatoriosSolarwelleEnergia.Domain.Builders
{
    internal class TabelaCemigBuilder
    {
        private TabelaCemig _tabela = new TabelaCemig();

        public TabelaCemigBuilder FromExcelRow(IXLRow row)
        {
            _tabela.Modalidade = row.Cell(1).GetString();
            _tabela.NumeroInstalacao = row.Cell(2).GetString();
            _tabela.Periodo = ParsePeriodo(row.Cell(3).GetString());
            _tabela.Quota = row.Cell(4).GetString();
            _tabela.PostoHorario = row.Cell(5).GetString();
            _tabela.SaldoAnterior = row.Cell(6).GetString();
            _tabela.SaldoExpirado = row.Cell(7).GetString();
            _tabela.QtdConsumo = row.Cell(8).GetString();
            _tabela.QtdGeracao = row.Cell(9).GetString();
            _tabela.QtdCompensacao = row.Cell(10).GetString();
            _tabela.QtdTransfencia = row.Cell(11).GetString();
            _tabela.QtdRecebimento = row.Cell(12).GetString();
            _tabela.SaldoAtual = row.Cell(13).GetString();
            _tabela.QtdSaldoExpirar = row.Cell(14).GetString();
            _tabela.PeriodoSaldoExpirar = row.Cell(15).GetString();

            return this;
        }

        public TabelaCemigBuilder FromXmlElement(XElement element)
        {

            _tabela.Periodo = ParsePeriodo((string)element.Element("Periodo"));
            _tabela.Modalidade = (string)element.Element("Modalidade");
            _tabela.NumeroInstalacao = (string)element.Element("Instalacao");
            _tabela.Quota = (string)element.Element("Quota");
            _tabela.PostoHorario = (string)element.Element("Posto_horario");
            _tabela.QtdConsumo = (string)element.Element("Qtd_consumo");
            _tabela.QtdGeracao = (string)element.Element("Qtd_geracao");
            _tabela.QtdCompensacao = (string)element.Element("Qtd_compensacao");
            _tabela.SaldoAnterior = (string)element.Element("Qtd_saldo_ant");
            _tabela.QtdTransfencia = (string)element.Element("Qtd_transferencia");
            _tabela.QtdRecebimento = (string)element.Element("Qtd_recebimento");
            _tabela.SaldoAtual = (string)element.Element("Qtd_saldo_atual");
            _tabela.SaldoExpirado = (string)element.Element("Qtd_saldo_exp");
            _tabela.QtdSaldoExpirar = (string)element.Element("Qtd_saldo_exp");
            _tabela.PeriodoSaldoExpirar = (string)element.Element("Per_prox_saldo_exp");

            return this;
        }

        public TabelaCemig Build() => _tabela;

        private static string ParsePeriodo(string periodo)
        {
            // Se estiver no formato MM/yyyy, converte para yyyy/MM para ficar padrão
            if (DateTime.TryParseExact(periodo, "MM/yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime dt))
            {
                return dt.ToString("yyyy/MM");
            }
            return periodo;
        }

    }
}
