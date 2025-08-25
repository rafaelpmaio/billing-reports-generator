using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
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
            InitializeComponent();
            _client = client;
            if (_client != null)
            {
                this.Text = $"Editando cliente {(_client is ClientePessoaFisica pf? pf.Nome : (_client as ClientePessoaJuridica)?.RazaoSocial ?? "")}";
                PopulateFields(_client);
                txtBox_NumeroInstalacao.ReadOnly = true;
                txtBox_NumeroInstalacao.BackColor = SystemColors.Control;
                txtBox_NumeroInstalacao.TabStop = false;
            }
            else
            {
                this.Text = "Adicionando novo cliente";
            }
        }

        private void btn_Enviar_Click(object sender, EventArgs e)
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
            txtBox_DistribuidoraEnergia.Clear();
            txtBox_DescontoCliente.Clear();

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
            //txtEndereco.Text = cliente.Endereco;
            txtBox_Email.Text = cliente.Email;
            txtBox_DistribuidoraEnergia.Text = cliente.DistribuidoraLocal;
            txtBox_DescontoCliente.Text = cliente.DescontoPercentual;

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
            string distribuidoraEnergia = txtBox_DistribuidoraEnergia.Text;
            string descontoCliente = txtBox_DescontoCliente.Text;

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

            string enderecoCompleto = string.Join(",", partesEndereco.Where(p => !string.IsNullOrWhiteSpace(p)));

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
            cliente.Endereco = enderecoCompleto;
            cliente.Email = email;
            cliente.DistribuidoraLocal = distribuidoraEnergia;
            cliente.DescontoPercentual = descontoCliente;

            return cliente;
        }

    }
}