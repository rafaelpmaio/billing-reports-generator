using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using GeradorRelatoriosSolarwelleEnergia.Dominio.DTO;
using OfficeOpenXml;

namespace GeradorRelatoriosSolarwelleEnergia.Dominio.Entidades
{
    internal class Cliente
    {
        public string NumeroCliente { get; set; }
        public string[] NumeroInstalacoes { get; set; }
        public string Telefone { get; set; }
        public string Endereco { get; set; }
        public string Email { get; set; }
        public string DistribuidoraLocal { get; set; } //pertinente?
      
        public static List<Cliente> LerTabelaExcel(string filePath)
        {
            var tabela = new List<Cliente>();
            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                ExcelPackage.License.SetNonCommercialPersonal("GeradorRelatorios");

                var worksheet = package.Workbook.Worksheets[0];
                int rowCount = worksheet.Dimension.Rows;

                for (int row = 2; row <= rowCount; row++)
                {
                    int tipoCliente = int.Parse(worksheet.Cells[row, 13].Text);

                    Cliente cliente = tipoCliente == 1 ? new ClientePessoaJuridica() : new ClientePessoaFisica();

                    cliente.NumeroCliente = worksheet.Cells[row, 1].Text;
                    cliente.NumeroInstalacoes = worksheet.Cells[row, 2].Text.Split(',').Select(n => n.Trim()).ToArray();
                    cliente.Telefone = worksheet.Cells[row, 9].Text;
                    cliente.Endereco = worksheet.Cells[row, 10].Text;
                    cliente.Email = worksheet.Cells[row, 11].Text;
                    cliente.DistribuidoraLocal = worksheet.Cells[row, 12].Text;

                    if (cliente is ClientePessoaJuridica pj)
                    {
                        pj.RazaoSocial = worksheet.Cells[row, 3].Text;
                        pj.Cnpj = worksheet.Cells[row, 4].Text;
                        pj.RepresentanteLegal = worksheet.Cells[row, 5].Text;
                    }
                    else if (cliente is ClientePessoaFisica pf)
                    {
                        pf.Nome = worksheet.Cells[row, 6].Text;
                        pf.Cpf = worksheet.Cells[row, 7].Text;
                        pf.Rg = worksheet.Cells[row, 8].Text;
                    }


                    tabela.Add(cliente);
                }

                return tabela;
            }
        }
                
        public override string ToString()
        {
            return $"Número Cliente: {NumeroCliente}, " +
                   $"Número de Instalação: {NumeroInstalacoes}, " +
                   $"Telefone: {Telefone}, " +
                   $"Endereco: {Endereco}, " +
                   $"Email: {Email}, " +
                   $"Distribuidora Local: {DistribuidoraLocal}";
        }
    }
}

