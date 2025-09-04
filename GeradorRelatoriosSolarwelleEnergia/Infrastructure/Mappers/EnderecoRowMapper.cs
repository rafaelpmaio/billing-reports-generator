using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeradorRelatoriosSolarwelleEnergia.Domain.Entities;
using GeradorRelatoriosSolarwelleEnergia.Dominio.Entidades;
using OfficeOpenXml;

namespace GeradorRelatoriosSolarwelleEnergia.Infrastructure.Mappers
{
    internal class EnderecoRowMapper
    {
        private const int ID = 1;
        private const int LOGRADOURO = 2;
        private const int NUMERO = 3;
        private const int COMPLEMENTO = 4;
        private const int BAIRRO = 5;
        private const int CIDADE = 6;
        private const int ESTADO = 7;
        private const int CEP = 8;

        public static Endereco Map(ExcelWorksheet ws, int row)
        {

            var endereco = new Endereco();

            if (int.TryParse(ws.Cells[row, ID].Text, out int id))
                endereco.Id = id;
            endereco.Logradouro = ws.Cells[row, LOGRADOURO].Text;
            endereco.Numero = ws.Cells[row, NUMERO].Text;
            endereco.Complemento = ws.Cells[row, COMPLEMENTO].Text;
            endereco.Bairro = ws.Cells[row, BAIRRO].Text;
            endereco.Cidade = ws.Cells[row, CIDADE].Text;
            endereco.Estado = ws.Cells[row, ESTADO].Text;
            endereco.Cep = ws.Cells[row, CEP].Text;

            return endereco;
        }
    }
}
