using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GeradorRelatoriosSolarwelleEnergia.Domain.DTO;
using GeradorRelatoriosSolarwelleEnergia.Domain.Entities;
using GeradorRelatoriosSolarwelleEnergia.Infrastructure.Database;

namespace GeradorRelatoriosSolarwelleEnergia.Forms
{
    public partial class Frm_AddOrUpdateInstalacao : Form
    {
        private Instalacao? _instalacao;
        public Frm_AddOrUpdateInstalacao(Instalacao? instalacao = null)
        {
            _instalacao = instalacao;
            InitializeComponent();
            InitializeComboBoxes();
            SetFormTitle();
        }

        private void SetFormTitle()
        {
            if (_instalacao != null)
            {
                this.Text = $"Editando cliente {_instalacao.NumeroCliente}";
            }
            else
            {
                this.Text = "Adicionando novo cliente";
            }
        }
        private void InitializeComboBoxes()
        {
            cmb_DiscountRate.Items.Clear();
            cmb_DiscountRate.Items.AddRange(new object[] { "0%", "5%", "10%", "15%", "20%", "25%", "30%" });
            cmb_DiscountRate.SelectedIndex = 0;

            cmb_EnergyDistributor.Items.Clear();
            cmb_EnergyDistributor.Items.AddRange(new object[]
            {
                "ELETROBRAS",
                "CPFL ENERGIA",
                "LIGHT",
                "ENEL DISTRIBUIÇÃO SÃO PAULO",
                "ENEL DISTRIBUIÇÃO RIO",
                "EQUATORIAL ENERGIA",
                "CEMIG",
                "NEOENERGIA",
                "CELESC",
                "CIA ENERGÉTICA DE MINAS GERAIS",
                "COELBA",
                "CELG-D",
                "COSERN",
                "ENERGISA",
                "CEEE",
                "COPEL"
            });
            cmb_EnergyDistributor.SelectedIndex = 0;
        }
        private Instalacao BuildInstalacaoFromForm()
        {
            string numeroInstalacao = txtBox_NumeroInstalacao.Text;
            string numeroCliente = txtBox_NumeroCliente.Text;
            string distribuidora = cmb_EnergyDistributor.SelectedItem.ToString();
            string desconto = cmb_DiscountRate.SelectedItem.ToString().Replace("%", "");

            return new Instalacao
            {
                NumeroInstalacao = numeroInstalacao,
                NumeroCliente = numeroCliente,
                DistribuidoraLocal = distribuidora,
                DescontoPercentual = desconto
            };
        }
        private void btn_Send_Click(object sender, EventArgs e)
        {
            try
            {
                var instalacao = BuildInstalacaoFromForm();
                var instalacaoRepo = new InstalacaoRepository();
                instalacaoRepo.Insert(instalacao);

                MessageBox.Show("Instalação salva com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
                ClearFormFields();
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao salvar cliente: " + ex.Message);
            }
        }
        private void ClearFormFields()
        {
            txtBox_NumeroInstalacao.Clear();
            txtBox_NumeroCliente.Clear();
            cmb_EnergyDistributor.SelectedIndex = 0;
            cmb_DiscountRate.SelectedIndex = 0;
            chk_Ativo.Checked = true;

            txtBox_NumeroInstalacao.Focus();
        }
        private void PopulateFields(Instalacao instalacao)
        {
            txtBox_NumeroInstalacao.Text = instalacao.NumeroInstalacao;
            txtBox_NumeroCliente.Text = instalacao.NumeroCliente;
            cmb_EnergyDistributor.Text = instalacao.DistribuidoraLocal;
            cmb_DiscountRate.Text = $"{instalacao.DescontoPercentual}%";
            chk_Ativo.Checked = instalacao.Ativo;
        }
                
    }
}
