using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeradorRelatoriosSolarwelleEnergia.Domain.Entities;
using OfficeOpenXml;

namespace GeradorRelatoriosSolarwelleEnergia.Infrastructure.Mappers
{
    internal class InstalacaoRowMapper
    {
        private const int NUMERO_INSTALACAO = 1;
        private const int NUMERO_CLIENTE = 2;
        private const int DISTRIBUIDORA_LOCAL = 3;
        private const int DESCONTO_PERCENTUAL = 4;
        private const int ATIVO = 5;

        public static Instalacao Map(ExcelWorksheet ws, int row)
        {

            var instalacao = new Instalacao();

            instalacao.NumeroInstalacao = ws.Cells[row, NUMERO_INSTALACAO].Text;
            instalacao.NumeroCliente = ws.Cells[row, NUMERO_CLIENTE].Text;
            instalacao.DistribuidoraLocal = ws.Cells[row, DISTRIBUIDORA_LOCAL].Text;
            instalacao.DescontoPercentual = ws.Cells[row, DESCONTO_PERCENTUAL].Text;
            instalacao.Ativo = ws.Cells[row, DESCONTO_PERCENTUAL].Text.Trim() == "1";

            return instalacao;
        }
    }
}
