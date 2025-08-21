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

namespace GeradorRelatoriosSolarwelleEnergia.Forms
{
    public partial class Frm_AddOrUpdateClient : Form
    {
        public Frm_AddOrUpdateClient()
        {
            InitializeComponent();
        }

        private void btn_Adicionar_Click(object sender, EventArgs e)
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

            try
            {
                Cliente cliente;

                // Verifica o tipo de cliente
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
                        RepresentanteLegal = rgOuRepresentanteLegal
                    };
                }
                else
                {
                    MessageBox.Show("Selecione o tipo de cliente (Pessoa Física ou Jurídica).");
                    return;
                }

               //logica para tipoCliente ser 1 ou 0
             

                cliente.NumeroInstalacao = numeroInstalacao;
                cliente.NumeroCliente = numeroCliente;
                cliente.Telefone = telefone;
                cliente.Endereco = enderecoCompleto;
                cliente.Email = email;
                cliente.DistribuidoraLocal = distribuidoraEnergia;
                cliente.DescontoPercentual = descontoCliente;

                var repo = new ClientRepository();
                repo.Insert( cliente );

                //lógica LimparCampos();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao salvar cliente: " + ex.Message);
            }
        }
    }
}