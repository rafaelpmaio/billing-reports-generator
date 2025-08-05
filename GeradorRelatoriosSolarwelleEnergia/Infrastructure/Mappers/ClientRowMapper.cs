using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Spreadsheet;
using GeradorRelatoriosSolarwelleEnergia.Dominio.Entidades;
using OfficeOpenXml;

namespace GeradorRelatoriosSolarwelleEnergia.Infrastructure.Mappers
{
    internal class ClientRowMapper
    {
        private const int COL_NUMERO_CLIENTE = 1;
        private const int COL_NUMERO_INSTALACOES = 2;
        private const int COL_RAZAO_SOCIAL = 3;
        private const int COL_CNPJ = 4;
        private const int COL_REPRESENTANTE = 5;
        private const int COL_NOME = 6;
        private const int COL_CPF = 7;
        private const int COL_RG = 8;
        private const int COL_TELEFONE = 9;
        private const int COL_ENDERECO = 10;
        private const int COL_EMAIL = 11;
        private const int COL_DISTRIBUIDORA = 12;
        private const int COL_TIPO_CLIENTE = 13;

        public static Cliente Map(ExcelWorksheet ws, int row)
        {
            int tipoCliente = int.Parse(ws.Cells[row, COL_TIPO_CLIENTE].Text);

            Cliente cliente = tipoCliente == 1 
                ? new ClientePessoaJuridica() 
                : new ClientePessoaFisica();

            cliente.NumeroCliente = ws.Cells[row, COL_NUMERO_CLIENTE].Text;
            cliente.NumeroInstalacoes = ws.Cells[row, COL_NUMERO_INSTALACOES].Text.Split(',').Select(n => n.Trim()).ToArray();
            cliente.Telefone = ws.Cells[row, COL_TELEFONE].Text;
            cliente.Endereco = ws.Cells[row, COL_ENDERECO].Text;
            cliente.Email = ws.Cells[row, COL_EMAIL].Text;
            cliente.DistribuidoraLocal = ws.Cells[row, COL_DISTRIBUIDORA].Text;

            if (cliente is ClientePessoaJuridica pj)
            {
                pj.RazaoSocial = ws.Cells[row, COL_RAZAO_SOCIAL].Text;
                pj.Cnpj = ws.Cells[row, COL_CNPJ].Text;
                pj.RepresentanteLegal = ws.Cells[row, COL_REPRESENTANTE].Text;
            }
            else if (cliente is ClientePessoaFisica pf)
            {
                pf.Nome = ws.Cells[row, COL_NOME].Text;
                pf.Cpf = ws.Cells[row, COL_CPF].Text;
                pf.Rg = ws.Cells[row, COL_RG].Text;
            }
            return cliente;
        }

    }
}
