using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GeradorRelatoriosSolarwelleEnergia.ApplicationLayer.Services;
using GeradorRelatoriosSolarwelleEnergia.Dominio.Entidades;
using GeradorRelatoriosSolarwelleEnergia.Infrastructure.Database;

namespace GeradorRelatoriosSolarwelleEnergia.Forms
{
    public partial class Frm_Clients : Form
    {
        public Frm_Clients()
        {
            InitializeComponent();
            this.Load += Frm_Clients_Load;
        }

        public void LoadClients()
        {
            var repo = new ClientRepository();
            var clients = repo.GetClients();

            DataTable table = new DataTable();

            table.Columns.Add("NumeroCLiente", typeof(string));
            table.Columns.Add("NumeroInstalacao", typeof(string));
            table.Columns.Add("RazaoSocialOuNome", typeof(string));
            table.Columns.Add("CnpjOuCpf", typeof(string));
            table.Columns.Add("RepresentanteLegal", typeof(string));
            table.Columns.Add("RG", typeof(string));
            table.Columns.Add("Telefone", typeof(string));
            table.Columns.Add("Endereco", typeof(string));
            table.Columns.Add("Email", typeof(string));
            table.Columns.Add("DistribuidoraLocal", typeof(string));
            table.Columns.Add("DescontoPercentual", typeof(string));
            table.Columns.Add("TipoCliente", typeof(int));

            foreach (var client in clients)
            {
                string nome = "", doc = "", representante = "", rg = "";

                if (client is ClientePessoaJuridica pj)
                {
                    nome = pj.RazaoSocial;
                    doc = pj.Cnpj;
                    representante = pj.RepresentanteLegal;
                }
                else if (client is ClientePessoaFisica pf)
                {
                    nome = pf.Nome;
                    doc = pf.Cpf;
                    rg = pf.Rg;
                }

                table.Rows.Add(
                    client.NumeroCliente,
                    client.NumeroInstalacao,
                    nome,
                    doc,
                    representante,
                    rg,
                    client.Telefone,
                    client.Endereco,
                    client.Email,
                    client.DistribuidoraLocal,
                    client.DescontoPercentual,
                    client is ClientePessoaJuridica ? 1 : 0
                );
            }

            dataGridView1.DataSource = table;
        }

        private void Frm_Clients_Load(object? sender, EventArgs e)
        {
            LoadClients();
        }

        private void btn_AddClient_Click(object sender, EventArgs e)
        {

        }

        private void btn_DeleteClient_Click(object sender, EventArgs e)
        {

        }

        private void btn_ImportClients_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Arquivos Excel (*.xlsx)|*.xlsx|Todos os arquivos (*.*)|*.*";
            openFileDialog.Title = "Selecione a planilha de clientes";

            if(openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;

                var importer = new ClientImporter();
                importer.ImportFromExcelToDb(filePath);

                MessageBox.Show("Clientes importados com sucesso!", "Importação", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
