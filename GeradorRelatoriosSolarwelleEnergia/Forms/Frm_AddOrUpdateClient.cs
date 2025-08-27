using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GeradorRelatoriosSolarwelleEnergia.Domain.Entities;
using GeradorRelatoriosSolarwelleEnergia.Dominio.Entidades;
using GeradorRelatoriosSolarwelleEnergia.Infrastructure.Database;
using iText.StyledXmlParser.Jsoup.Nodes;

namespace GeradorRelatoriosSolarwelleEnergia.Forms
{
    public partial class Frm_AddOrUpdateClient : Form
    {
        private Cliente? _client;
        public string NumeroInstalacao { get; set; }
        public Frm_AddOrUpdateClient(Cliente? client = null)
        {
            _client = client;
            InitializeComponent();
            InitializeComboBoxes();
            SetFormTitle();
          

            if (_client != null) ConfigureEditMode();
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
        private void SetFormTitle()
        {
            if (_client != null)
            {
                var name = _client is ClientePessoaFisica pf
                    ? pf.Nome
                    : (_client as ClientePessoaJuridica)?.RazaoSocial ?? "";

                this.Text = $"Editando cliente {name}";
            }
            else
            {
                this.Text = "Adicionando novo cliente";
            }
        }
        private void ConfigureEditMode()
        {
            PopulateFields(_client);
            txtBox_NumeroInstalacao.ReadOnly = true;
            txtBox_NumeroInstalacao.BackColor = SystemColors.Control;
            txtBox_NumeroInstalacao.TabStop = false;
        }
        private void btn_Send_Click(object sender, EventArgs e)
        {
            try
            {
                Cliente client = BuildClientFromForm();

                var repo = new ClientRepository();

                if (_client == null)
                {
                    repo.Insert(client);
                    MessageBox.Show("Cliente adicionado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    client.NumeroInstalacao = _client.NumeroInstalacao;
                    repo.Update(client);
                    MessageBox.Show("Cliente atualizado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
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
            txtBox_NomeOuRazaoSocial.Clear();
            txtBox_CpfOuCnpj.Clear();
            txtBox_Telefone.Clear();
            txtBox_RgOuRepresentanteLegal.Clear();
            txtBox_Email.Clear();
            cmb_EnergyDistributor.SelectedIndex = 0;
            cmb_DiscountRate.SelectedIndex = 0; ;

            txtBox_Logradouro.Clear();
            txtBox_NumeroEndereco.Clear();
            txtBox_ComplementoEndereco.Clear();
            txtBox_Bairro.Clear();
            txtBox_Cidade.Clear();
            txtBox_Estado.Clear();
            txtBox_Cep.Clear();

            txtBox_NumeroInstalacao.Focus();
        }
        private void PopulateFields(Cliente cliente)
        {
            txtBox_NumeroInstalacao.Text = cliente.NumeroInstalacao;
            txtBox_NumeroCliente.Text = cliente.NumeroCliente;
            txtBox_Telefone.Text = cliente.Telefone;
            txtBox_Email.Text = cliente.Email;
            cmb_EnergyDistributor.Text = cliente.DistribuidoraLocal;
            cmb_DiscountRate.Text = $"{cliente.DescontoPercentual}%";

            var endereco = cliente.Endereco ?? new Endereco();

            txtBox_Logradouro.Text = endereco.Logradouro;
            txtBox_NumeroEndereco.Text = endereco.Numero;
            txtBox_ComplementoEndereco.Text = endereco.Complemento;
            txtBox_Bairro.Text = endereco.Bairro;
            txtBox_Cidade.Text = endereco.Cidade;
            txtBox_Estado.Text = endereco.Estado;
            txtBox_Cep.Text = endereco.Cep;

            if (cliente is ClientePessoaJuridica pj)
            {
                txtBox_NomeOuRazaoSocial.Text = pj.RazaoSocial;
                txtBox_CpfOuCnpj.Text = pj.Cnpj;
                txtBox_RgOuRepresentanteLegal.Text = pj.RepresentanteLegal;
                rbtn_PessoaJuridica.Checked = true;

            }
            else if (cliente is ClientePessoaFisica pf)
            {
                txtBox_NomeOuRazaoSocial.Text = pf.Nome;
                txtBox_CpfOuCnpj.Text = pf.Cpf;
                txtBox_RgOuRepresentanteLegal.Text = pf.Rg;
                rbtn_PessoaFisica.Checked = true;
            }
        }       

        private Cliente BuildClientFromForm()
        {
            string numeroInstalacao = txtBox_NumeroInstalacao.Text;
            string numeroCliente = txtBox_NumeroCliente.Text;
            string nomeOuRazaoSocial = txtBox_NomeOuRazaoSocial.Text;
            string cpfOuCnpj = txtBox_CpfOuCnpj.Text;
            string telefone = txtBox_Telefone.Text;
            string rgOuRepresentanteLegal = txtBox_RgOuRepresentanteLegal.Text;
            string email = txtBox_Email.Text;
            string distribuidoraEnergia = cmb_EnergyDistributor.SelectedItem.ToString();
            string descontoCliente = cmb_DiscountRate.SelectedItem.ToString().Replace("%", "");

            string[] partesEndereco = new string[]
            {
                txtBox_Logradouro.Text,
                "Nº " + txtBox_NumeroEndereco.Text,
                txtBox_ComplementoEndereco.Text,
                txtBox_Bairro.Text,
                txtBox_Cidade.Text,
                txtBox_Estado.Text,
                "CEP: " + txtBox_Cep.Text,
            };

            var endereco = new Endereco
            {
                Logradouro = txtBox_Logradouro.Text,
                Numero = txtBox_NumeroEndereco.Text,
                Complemento = txtBox_ComplementoEndereco.Text,
                Bairro = txtBox_Bairro.Text,
                Cidade = txtBox_Cidade.Text,
                Estado = txtBox_Estado.Text,
                Cep = txtBox_Cep.Text,
            };

            Cliente cliente;

            if (rbtn_PessoaFisica.Checked)
            {
                cliente = new ClientePessoaFisica
                {
                    Nome = nomeOuRazaoSocial,
                    Cpf = cpfOuCnpj,
                    Rg = rgOuRepresentanteLegal,
                };
            }
            else if (rbtn_PessoaJuridica.Checked)
            {
                cliente = new ClientePessoaJuridica
                {
                    RazaoSocial = nomeOuRazaoSocial,
                    Cnpj = cpfOuCnpj,
                    RepresentanteLegal = rgOuRepresentanteLegal,
                };
            }
            else
            {
                throw new InvalidOperationException("Tipo de cliente não selecionado.");
            }

            cliente.NumeroInstalacao = numeroInstalacao;
            cliente.NumeroCliente = numeroCliente;
            cliente.Telefone = telefone;
            cliente.Endereco = endereco;
            cliente.Email = email;
            cliente.DistribuidoraLocal = distribuidoraEnergia;
            cliente.DescontoPercentual = descontoCliente;

            return cliente;
        }
    }
}