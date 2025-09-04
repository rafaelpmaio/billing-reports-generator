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
    public partial class Frm_Instalacao : Form
    {
        public Frm_Instalacao()
        {
            InitializeComponent();
        }

        public void LoadInstalacoes()
        {
            dataGridView.DataSource = GetInstalacoesAsDataTable();
            dataGridView.ClearSelection();
        }
        private void Frm_Clients_Load(object? sender, EventArgs e)
        {
            LoadInstalacoes();
        }
        private void btn_AddClient_Click(object sender, EventArgs e)
        {
            using (var form = new Frm_AddOrUpdateClient())
            {
                var result = form.ShowDialog();
                if (result == DialogResult.OK)
                {
                    LoadInstalacoes();
                }
            }
        }

        private DataTable GetInstalacoesAsDataTable()
        {
            var repo = new InstalacaoRepository();
            var instalacoes = repo.GetInstalacoes();

            DataTable table = new DataTable();

            table.Columns.Add("NumeroInstalacao", typeof(string));
            table.Columns.Add("NumeroCliente", typeof(string));
            table.Columns.Add("DistribuidoraLocal", typeof(string));
            table.Columns.Add("DescontoPercentual", typeof(string));
            table.Columns.Add("Ativo", typeof(int));

            foreach (var instalacao in instalacoes)
            {
                string nome = "", doc = "", representante = "", rg = "";

                table.Rows.Add(
                    instalacao.NumeroInstalacao,
                    instalacao.NumeroCliente,
                    instalacao.DistribuidoraLocal,
                    instalacao.DescontoPercentual,
                    instalacao.Ativo ? 1 : 0
                );
            }
            return table;
        }

        private void btn_ImportInstalacoes_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Arquivos Excel (*.xlsx)|*.xlsx|Todos os arquivos (*.*)|*.*";
            openFileDialog.Title = "Selecione a planilha de clientes";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;

                var importer = new InstalacaoImporter();
                importer.ImportFromExcelToDb(filePath);
                LoadInstalacoes();

                MessageBox.Show("Clientes importados com sucesso!", "Importação", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
