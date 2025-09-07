using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Spreadsheet;
using GeradorRelatoriosSolarwelleEnergia.Domain.DTO;
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

        public static ClientDto Map(ExcelWorksheet ws, int row)
        {   
            var dto = new ClientDto();

            dto.NumeroCliente = ws.Cells[row, COL_NUMERO_CLIENTE].Text;
            dto.Instalacoes = ws.Cells[row, COL_INSTALACOES].Text;
            dto.RazaoSocialOuNome = ws.Cells[row, COL_RAZAO_SOCIAL_NOME].Text; 
            dto.CnpjOuCpf = ws.Cells[row, COL_CNPJ_CPF].Text;
            dto.RepresentanteLegal = ws.Cells[row, COL_REPRESENTANTE].Text;
            dto.Rg = ws.Cells[row, COL_RG].Text;
            dto.Telefone = ws.Cells[row, COL_TELEFONE].Text;

            if (int.TryParse(ws.Cells[row, COL_ID_ENDERECO].Text, out int idEndereco))
                dto.IdEndereco = idEndereco;

            dto.Email = ws.Cells[row, COL_EMAIL].Text;
            dto.TipoCliente = int.TryParse(ws.Cells[row, COL_TIPO_CLIENTE].Text, out var tipo) ? tipo : 0;
            dto.Ativo = ws.Cells[row, COL_ATIVO].Text.Trim() == "1";
            
            return dto;
        }

    }
}
