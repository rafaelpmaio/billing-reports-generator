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
using GeradorRelatoriosSolarwelleEnergia.Dominio.Entidades;
using GeradorRelatoriosSolarwelleEnergia.Infrastructure.Database;
using iText.StyledXmlParser.Jsoup.Nodes;

namespace GeradorRelatoriosSolarwelleEnergia.Forms
{
    public partial class Frm_AddOrUpdateClient : Form
    {
        private ClientDto? _clientDto;
        private Endereco? _endereco;
        public string NumeroInstalacao { get; set; }
        public Frm_AddOrUpdateClient(ClientDto? clientDto = null)
        {
            _clientDto = clientDto;

            InitializeComponent();
            SetFormTitle();

            if (clientDto != null) ConfigureEditMode();
        }
        private void SetFormTitle()
        {
            if (_clientDto != null)
            {
                this.Text = $"Editando cliente {_clientDto.RazaoSocialOuNome}";
            }
            else
            {
                this.Text = "Adicionando novo cliente";
            }
        }
        private void ConfigureEditMode()
        {
            var enderecoRepo = new EnderecoRepository();
            _endereco = enderecoRepo.GetById(_clientDto.IdEndereco);

            if (_endereco == null)
            {
                MessageBox.Show("Endereço não encontrado.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            PopulateFields(_clientDto, _endereco);
            txtBox_NumeroCliente.ReadOnly = true;
            txtBox_NumeroCliente.BackColor = SystemColors.Control;
            txtBox_NumeroCliente.TabStop = false;

            txtBox_Instalacoes.ReadOnly = true;
            txtBox_Instalacoes.BackColor = SystemColors.Control;
            txtBox_Instalacoes.TabStop = false;
        }
        private void btn_Send_Click(object sender, EventArgs e)
        {
            try
            {
                var enderecoRepo = new EnderecoRepository();
                var clienteRepo = new ClientRepository();

                var endereco = BuildEnderecoFromForm();
                int enderecoId;

                if (_clientDto != null)
                {
                    if (_clientDto.IdEndereco > 0)
                    {
                        endereco.Id = _clientDto.IdEndereco;
                        enderecoRepo.Update(endereco);
                        enderecoId = endereco.Id;
                    }
                    else
                    {
                        enderecoId = enderecoRepo.Insert(endereco);
                    }

                    var cliente = BuildClientDtoFromForm(enderecoId);
                    cliente.NumeroCliente = _clientDto.NumeroCliente;
                    clienteRepo.Update(cliente);
                }
                else
                {
                    enderecoId = enderecoRepo.Insert(endereco);

                    var cliente = BuildClientDtoFromForm(enderecoId);
                    clienteRepo.Insert(cliente);
                }
                MessageBox.Show("Cliente salvo com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            txtBox_NumeroCliente.Clear();
            txtBox_Instalacoes.Clear();
            txtBox_NomeOuRazaoSocial.Clear();
            txtBox_CpfOuCnpj.Clear();
            txtBox_Telefone.Clear();
            txtBox_RgOuRepresentanteLegal.Clear();
            txtBox_Email.Clear();

            txtBox_Logradouro.Clear();
            txtBox_NumeroEndereco.Clear();
            txtBox_ComplementoEndereco.Clear();
            txtBox_Bairro.Clear();
            txtBox_Cidade.Clear();
            txtBox_Estado.Clear();
            txtBox_Cep.Clear();

            txtBox_NumeroCliente.Focus();
        }
        private void PopulateFields(ClientDto clientDto, Endereco endereco)
        {
            txtBox_NumeroCliente.Text = clientDto.NumeroCliente;
            txtBox_Instalacoes.Text = clientDto.Instalacoes;
            txtBox_NomeOuRazaoSocial.Text = clientDto.RazaoSocialOuNome;
            txtBox_CpfOuCnpj.Text = clientDto.CnpjOuCpf;
            txtBox_RgOuRepresentanteLegal.Text = clientDto.RepresentanteLegal;
            txtBox_Telefone.Text = clientDto.Telefone;
            txtBox_Email.Text = clientDto.Email;

            txtBox_Logradouro.Text = endereco.Logradouro;
            txtBox_NumeroEndereco.Text = endereco.Numero;
            txtBox_ComplementoEndereco.Text = endereco.Complemento;
            txtBox_Bairro.Text = endereco.Bairro;
            txtBox_Cidade.Text = endereco.Cidade;
            txtBox_Estado.Text = endereco.Estado;
            txtBox_Cep.Text = endereco.Cep;

            if (clientDto.TipoCliente == 1)
            {
                rbtn_PessoaJuridica.Checked = true;
            }
            else
            {
                rbtn_PessoaFisica.Checked = true;
            }
        }
        private ClientDto BuildClientDtoFromForm(int enderecoId)
        {
            int tipoCliente;
            if (rbtn_PessoaFisica.Checked)
                tipoCliente = 0;
            else if (rbtn_PessoaJuridica.Checked)
                tipoCliente = 1;
            else
                throw new InvalidOperationException("Tipo de cliente não selecionado");

            return new ClientDto
            {
                //Construir um IdFactory para parar de usar txtBox_NumeroCliente.Text
                NumeroCliente = _clientDto.NumeroCliente ?? txtBox_NumeroCliente.Text,
                Instalacoes = _clientDto.Instalacoes ?? "",
                RazaoSocialOuNome = txtBox_NomeOuRazaoSocial.Text,
                CnpjOuCpf = txtBox_CpfOuCnpj.Text,
                RepresentanteLegal = txtBox_RgOuRepresentanteLegal.Text,
                Rg = txtBox_RgOuRepresentanteLegal.Text,
                Telefone = txtBox_Telefone.Text,
                IdEndereco = enderecoId,
                Email = txtBox_Email.Text,
                TipoCliente = tipoCliente,
                Ativo = _clientDto != null ? _clientDto.Ativo : true
            };


        }
        private Endereco BuildEnderecoFromForm()
        {
            return new Endereco
            {
                Logradouro = txtBox_Logradouro.Text,
                Numero = txtBox_NumeroEndereco.Text,
                Complemento = txtBox_ComplementoEndereco.Text,
                Bairro = txtBox_Bairro.Text,
                Cidade = txtBox_Cidade.Text,
                Estado = txtBox_Estado.Text,
                Cep = txtBox_Cep.Text
            };
        }
    }
}