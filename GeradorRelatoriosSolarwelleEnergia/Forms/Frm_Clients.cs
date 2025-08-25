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
            dataGridView.SelectionChanged += DataGridView_SelectionChanged;
        }

        public void LoadClients()
        {
            dataGridView.DataSource = GetClientsAsDataTable();
            dataGridView.ClearSelection();
        }
        private void Frm_Clients_Load(object? sender, EventArgs e)
        {
            LoadClients();
        }
        private void btn_AddClient_Click(object sender, EventArgs e)
        {
            using (var form = new Frm_AddOrUpdateClient())
            {
                var result = form.ShowDialog();
                if (result == DialogResult.OK)
                {
                    LoadClients();
                }
            }

        }
        private void btn_DeleteClient_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show(
                    "Tem certeza que deseja excluir o cliente selecionado?",
                    "Confirmação",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

            if (confirmResult == DialogResult.No)
                return;

            try
            {
                DataGridViewRow selectedRow = dataGridView.SelectedRows[0];
                string numeroInstalacao = Convert.ToString(selectedRow.Cells["NumeroInstalacao"].Value);

                var repo = new ClientRepository();
                repo.Remove(numeroInstalacao);

                MessageBox.Show("Cliente removido com sucesso.", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LoadClients(); // Recarrega a grid
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao excluir cliente: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btn_UpdateClient_Click(object sender, EventArgs e)
        {
            var client = CreateClientFromSelectedRow();
 
            using (var form = new Frm_AddOrUpdateClient(client))
            {
                var result = form.ShowDialog();
                if(result == DialogResult.OK)
                {
                    LoadClients();
                }
            }
        }
        private void btn_ImportClients_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Arquivos Excel (*.xlsx)|*.xlsx|Todos os arquivos (*.*)|*.*";
            openFileDialog.Title = "Selecione a planilha de clientes";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;

                var importer = new ClientImporter();
                importer.ImportFromExcelToDb(filePath);

                MessageBox.Show("Clientes importados com sucesso!", "Importação", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void DataGridView_SelectionChanged(object? sender, EventArgs e)
        {
            bool rowSelected = dataGridView.SelectedRows.Count > 0;
            btn_DeleteClient.Enabled = rowSelected;
            btn_UpdateClient.Enabled = rowSelected;
        }
        private DataTable GetClientsAsDataTable() 
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
            return table;
        }
        private Cliente CreateClientFromSelectedRow()
        {
            DataGridViewRow selectedRow = dataGridView.SelectedRows[0];

            string numeroInstalacao = selectedRow.Cells["NumeroInstalacao"].Value?.ToString();
            string numeroCliente = selectedRow.Cells["NumeroCLiente"].Value?.ToString();
            string nome = selectedRow.Cells["RazaoSocialOuNome"].Value?.ToString();
            string doc = selectedRow.Cells["CnpjOuCpf"].Value?.ToString();
            string representante = selectedRow.Cells["RepresentanteLegal"].Value?.ToString();
            string rg = selectedRow.Cells["RG"].Value?.ToString();
            string telefone = selectedRow.Cells["Telefone"].Value?.ToString();
            string endereco = selectedRow.Cells["Endereco"].Value?.ToString();
            string email = selectedRow.Cells["Email"].Value?.ToString();
            string distribuidora = selectedRow.Cells["DistribuidoraLocal"].Value?.ToString();
            string desconto = selectedRow.Cells["DescontoPercentual"].Value?.ToString();
            int tipo = Convert.ToInt32(selectedRow.Cells["TipoCliente"].Value);

            Cliente cliente;

            if (tipo == 1)
            {
                cliente = new ClientePessoaJuridica
                {
                    RazaoSocial = nome,
                    Cnpj = doc,
                    RepresentanteLegal = representante
                };
            }
            else
            {
                cliente = new ClientePessoaFisica
                {
                    Nome = nome,
                    Cpf = doc,
                    Rg = rg
                };
            }

            cliente.NumeroInstalacao = numeroInstalacao;
            cliente.NumeroCliente = numeroCliente;
            cliente.Telefone = telefone;
            cliente.Endereco = endereco;
            cliente.Email = email;
            cliente.DistribuidoraLocal = distribuidora;
            cliente.DescontoPercentual = desconto;
            return cliente;
        }


    }
}
