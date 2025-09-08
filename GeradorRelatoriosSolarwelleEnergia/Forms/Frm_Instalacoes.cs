using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GeradorRelatoriosSolarwelleEnergia.ApplicationLayer.Services;
using GeradorRelatoriosSolarwelleEnergia.Domain.DTO;
using GeradorRelatoriosSolarwelleEnergia.Domain.Entities;
using GeradorRelatoriosSolarwelleEnergia.Dominio.Entidades;
using GeradorRelatoriosSolarwelleEnergia.Infrastructure.Database;

namespace GeradorRelatoriosSolarwelleEnergia.Forms
{
    public partial class Frm_Instalacoes : Form
    {
        public Frm_Instalacoes()
        {
            InitializeComponent();
            this.Load += Frm_Instalacoes_Load;
            dataGridView.SelectionChanged += DataGridView_SelectionChanged;
        }
        public void LoadInstalacoes()
        {
            dataGridView.DataSource = GetInstalacoesAsDataTable();
            dataGridView.ClearSelection();
        }
        private void Frm_Instalacoes_Load(object? sender, EventArgs e)
        {
            LoadInstalacoes();
        }
        private void btn_AddInstalacao_Click(object sender, EventArgs e)
        {
            using (var form = new Frm_AddOrUpdateInstalacao())
            {
                var result = form.ShowDialog();
                if (result == DialogResult.OK)
                {
                    LoadInstalacoes();
                }
            }
        }
        private void btn_UpdateInstalacao_Click(object sender, EventArgs e)
        {
            var instalacao = CreateInstalacaoFromSelectedRow();

            using (var form = new Frm_AddOrUpdateInstalacao(instalacao))
            {
                var result = form.ShowDialog();
                if (result == DialogResult.OK)
                {
                    LoadInstalacoes();
                }
            }
        }
        private void btn_DeleteInstalacao_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show(
                    "Tem certeza que deseja excluir a instalação selecionado?",
                    "Confirmação",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

            if (confirmResult == DialogResult.No)
                return;

            try
            {
                DataGridViewRow selectedRow = dataGridView.SelectedRows[0];
                string numeroInstalacao = Convert.ToString(selectedRow.Cells["NumeroInstalacao"].Value);

                var repo = new InstalacaoRepository();
                repo.Remove(numeroInstalacao);

                MessageBox.Show("Instalacao removida com sucesso.", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LoadInstalacoes();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao excluir cliente: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void DataGridView_SelectionChanged(object? sender, EventArgs e)
        {
            bool rowSelected = dataGridView.SelectedRows.Count > 0;
            btn_DeleteInstalacao.Enabled = rowSelected;
            btn_UpdateInstalacao.Enabled = rowSelected;
        }
        private DataTable GetInstalacoesAsDataTable()
        {
            var repo = new InstalacaoRepository();
            var instalacoes = repo.GetAll();

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
        private Instalacao CreateInstalacaoFromSelectedRow()
        {
            DataGridViewRow selectedRow = dataGridView.SelectedRows[0];

            string numeroInstalacao = selectedRow.Cells["NumeroInstalacao"].Value?.ToString();
            string numeroCliente = selectedRow.Cells["NumeroCliente"].Value?.ToString();
            string distribuidoraLocal = selectedRow.Cells["DistribuidoraLocal"].Value?.ToString();
            string descontoPercentual = selectedRow.Cells["DescontoPercentual"].Value?.ToString();
            bool ativo = selectedRow.Cells["Ativo"].Value?.ToString() == "1";

            var instalacao = new Instalacao
            {
                NumeroInstalacao = numeroInstalacao,
                NumeroCliente = numeroCliente,
                DistribuidoraLocal = distribuidoraLocal,
                DescontoPercentual = descontoPercentual,
                Ativo = ativo
            };

            return instalacao;
        }
    }
}
