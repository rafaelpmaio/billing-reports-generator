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

        private const int COL_MODALIDADE = 1;
        private const int COL_INSTALACAO = 2;
        private const int COL_PERIODO = 3;
        private const int COL_QUOTA = 4;
        private const int COL_POSTO_HORARIO = 5;
        private const int COL_SALDO_ANTERIOR = 6;
        private const int COL_SALDO_EXPIRADO = 7;
        private const int COL_QTD_CONSUMO = 8;
        private const int COL_QTD_GERACAO = 9;
        private const int COL_QTD_COMPENSACAO = 10;
        private const int COL_QTD_TRANSFERENCIA = 11;
        private const int COL_QTD_RECEBIMENTO = 12;
        private const int COL_SALDO_ATUAL = 13;
        private const int COL_QTD_SALDO_EXPIRAR = 14;
        private const int COL_PERIODO_SALDO_EXPIRAR = 15;
        public TabelaCemigBuilder FromExcelRow(IXLRow row)
        {
            _tabela.Modalidade = row.Cell(COL_MODALIDADE).GetString();
            _tabela.NumeroInstalacao = row.Cell(COL_INSTALACAO).GetString();
            _tabela.Periodo = ParsePeriodo(row.Cell(COL_PERIODO).GetString());
            _tabela.Quota = row.Cell(COL_QUOTA).GetString();
            _tabela.PostoHorario = row.Cell(COL_POSTO_HORARIO).GetString();
            _tabela.SaldoAnterior = row.Cell(COL_SALDO_ANTERIOR).GetString();
            _tabela.SaldoExpirado = row.Cell(COL_SALDO_EXPIRADO).GetString();
            _tabela.QtdConsumo = row.Cell(COL_QTD_CONSUMO).GetString();
            _tabela.QtdGeracao = row.Cell(COL_QTD_GERACAO).GetString();
            _tabela.QtdCompensacao = row.Cell(COL_QTD_COMPENSACAO).GetString();
            _tabela.QtdTransfencia = row.Cell(COL_QTD_TRANSFERENCIA).GetString();
            _tabela.QtdRecebimento = row.Cell(COL_QTD_RECEBIMENTO).GetString();
            _tabela.SaldoAtual = row.Cell(COL_SALDO_ATUAL).GetString();
            _tabela.QtdSaldoExpirar = row.Cell(COL_QTD_SALDO_EXPIRAR).GetString();
            _tabela.PeriodoSaldoExpirar = row.Cell(COL_PERIODO_SALDO_EXPIRAR).GetString();

            return this;
        }

        private const string ELM_PERIODO = "Periodo";
        private const string ELM_MODALIDADE = "Modalidade";
        private const string ELM_INSTALACAO = "Instalacao";
        private const string ELM_QUOTA = "Quota";
        private const string ELM_POSTO_HORARIO = "Posto_horario";
        private const string ELM_QTD_CONSUMO = "Qtd_consumo";
        private const string ELM_QTD_GERACAO = "Qtd_geracao";
        private const string ELM_QTD_COMPENSACAO = "Qtd_compensacao";
        private const string ELM_QTD_SALDO_ANT = "Qtd_saldo_ant";
        private const string ELM_QTD_TRANSFERENCIA = "Qtd_transferencia";
        private const string ELM_QTD_RECEBIMENTO = "Qtd_recebimento";
        private const string ELM_QTD_SALDO_ATUAL = "Qtd_saldo_atual";
        private const string ELM_QTD_SALDO_EXP = "Qtd_saldo_exp";
        private const string ELM_PER_PROX_SALDO_EXP = "Per_prox_saldo_exp";
        public TabelaCemigBuilder FromXmlElement(XElement element)
        {

            _tabela.Periodo = ParsePeriodo((string)element.Element(ELM_PERIODO));
            _tabela.Modalidade = (string)element.Element(ELM_MODALIDADE);
            _tabela.NumeroInstalacao = (string)element.Element(ELM_INSTALACAO);
            _tabela.Quota = (string)element.Element(ELM_QUOTA);
            _tabela.PostoHorario = (string)element.Element(ELM_POSTO_HORARIO);
            _tabela.QtdConsumo = (string)element.Element(ELM_QTD_CONSUMO);
            _tabela.QtdGeracao = (string)element.Element(ELM_QTD_GERACAO);
            _tabela.QtdCompensacao = (string)element.Element(ELM_QTD_COMPENSACAO);
            _tabela.SaldoAnterior = (string)element.Element(ELM_QTD_SALDO_ANT);
            _tabela.QtdTransfencia = (string)element.Element(ELM_QTD_TRANSFERENCIA);
            _tabela.QtdRecebimento = (string)element.Element(ELM_QTD_RECEBIMENTO);
            _tabela.SaldoAtual = (string)element.Element(ELM_QTD_SALDO_ATUAL);
            _tabela.SaldoExpirado = (string)element.Element(ELM_QTD_SALDO_EXP);
            _tabela.QtdSaldoExpirar = (string)element.Element(ELM_QTD_SALDO_EXP);
            _tabela.PeriodoSaldoExpirar = (string)element.Element(ELM_PER_PROX_SALDO_EXP);

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
