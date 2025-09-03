using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Spreadsheet;
using GeradorRelatoriosSolarwelleEnergia.Domain.Entities;
using GeradorRelatoriosSolarwelleEnergia.Dominio.Entidades;
using OfficeOpenXml;

namespace GeradorRelatoriosSolarwelleEnergia.Infrastructure.Mappers
{
    internal class ClientRowMapper
    {
        private const int COL_NUMERO_CLIENTE = 1;
        private const int COL_INSTALACOES = 2;
        private const int COL_RAZAO_SOCIAL_NOME = 3;
        private const int COL_CNPJ_CPF = 4;
        private const int COL_REPRESENTANTE = 5;
        private const int COL_RG = 6;
        private const int COL_TELEFONE = 7;
        private const int COL_ID_ENDERECO = 8;
        private const int COL_EMAIL = 9;
        private const int COL_TIPO_CLIENTE = 10;
        private const int COL_ATIVO = 11;

        public static Cliente Map(ExcelWorksheet ws, int row)
        {
            int tipoCliente = int.TryParse(ws.Cells[row, COL_TIPO_CLIENTE].Text, out var tipo) ? tipo : 0;

            Cliente cliente = tipoCliente == 1
                ? new ClientePessoaJuridica()
                : new ClientePessoaFisica();

            cliente.TipoCliente = tipoCliente;
            cliente.NumeroCliente = ws.Cells[row, COL_NUMERO_CLIENTE].Text;
            string nomeOuRazaoSocial = ws.Cells[row, COL_RAZAO_SOCIAL_NOME].Text;
            string cpfOuCnpj = ws.Cells[row, COL_CNPJ_CPF].Text;
            cliente.Telefone = ws.Cells[row, COL_TELEFONE].Text;
            cliente.Email = ws.Cells[row, COL_EMAIL].Text;
            cliente.Ativo = ws.Cells[row, COL_ATIVO].Text.Trim() == "1";

            //Endereco
            if (int.TryParse(ws.Cells[row, COL_ID_ENDERECO].Text, out int idEndereco))
                cliente.IdEndereco = idEndereco;

            //Instalacoes
            string instalacoesRaw = ws.Cells[row, COL_INSTALACOES].Text;
            cliente.Instalacoes = string.IsNullOrWhiteSpace(instalacoesRaw)
                ? Array.Empty<string>()
                : instalacoesRaw.Split(',').Select(inst => inst.Trim()).ToArray();

            if (cliente is ClientePessoaJuridica pj)
            {
                pj.RazaoSocial = nomeOuRazaoSocial;
                pj.Cnpj = cpfOuCnpj;
                pj.RepresentanteLegal = ws.Cells[row, COL_REPRESENTANTE].Text;
            }
            else if (cliente is ClientePessoaFisica pf)
            {
                pf.Nome = nomeOuRazaoSocial;
                pf.Cpf = cpfOuCnpj;
                pf.Rg = ws.Cells[row, COL_RG].Text;
            }
            return cliente;
        }

    }
}
