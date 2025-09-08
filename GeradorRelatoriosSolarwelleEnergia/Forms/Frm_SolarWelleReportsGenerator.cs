using System.Data;
using System.Windows.Forms;
using GeradorRelatoriosSolarwelleEnergia.Application.Services;
using GeradorRelatoriosSolarwelleEnergia.ApplicationLayer.Services;
using GeradorRelatoriosSolarwelleEnergia.Domain.DTO;
using GeradorRelatoriosSolarwelleEnergia.Domain.Entities;
using GeradorRelatoriosSolarwelleEnergia.Domain.Services;
using GeradorRelatoriosSolarwelleEnergia.Domain.Utils;
using GeradorRelatoriosSolarwelleEnergia.Dominio.Entidades;
using GeradorRelatoriosSolarwelleEnergia.Forms;
using GeradorRelatoriosSolarwelleEnergia.Infrastructure.Database;
using GeradorRelatoriosSolarwelleEnergia.Infrastructure.Exporters;
using GeradorRelatoriosSolarwelleEnergia.Infrastructure.Pdf;
using GeradorRelatoriosSolarwelleEnergia.Infrastructure.Readers;

namespace GeradorRelatoriosSolarwelleEnergia
{
    public partial class Frm_SolarWelleReportsGenerator : Form
    {
        private readonly ReportGeneratorAppService _handler;
        public Frm_SolarWelleReportsGenerator()
        {
            InitializeComponent();

            _handler = new ReportGeneratorAppService(
                new CemigTableService(),
                new ClientEconomyHistoryReader(),
                new ClientReportService(),
                new PdfReportSaver()
                );
        }

        private void txtBox_CemigTablePath_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Selecione um arquivo XML ou XLSX";
            openFileDialog.Filter = "Arquivos XML ou Excel (*.xml;*.xlsx)|*.xml;*.xlsx|Todos os arquivos (*.*)|*.*";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                txtBox_CemigTablePath.Text = openFileDialog.FileName;
                EnableGenerateButton();
            }
        }
        private void txtBox_KwhValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox txt = sender as TextBox;

            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != ',')
            {
                e.Handled = true; // Bloqueia a tecla
            }
            if ((e.KeyChar == ',' || e.KeyChar == '.') &&
                ((txt).Text.Contains(",") ||
                (txt).Text.Contains(".")))
            {
                e.Handled = true;
            }

            // Verificar se já existe ponto/vírgula e limita a 2 dígitos depois
            int separadorPos = txt.Text.IndexOfAny(new[] { '.', ',' });
            if (separadorPos >= 0)
            {
                // Verifica se o cursor está após o separador
                if (txt.SelectionStart > separadorPos)
                {
                    int decimais = txt.Text.Substring(separadorPos + 1).Length;

                    // Se já houver 2 dígitos depois do ponto, bloqueia
                    if (decimais >= 2 && char.IsDigit(e.KeyChar))
                    {
                        e.Handled = true;
                        return;
                    }
                }
            }
            EnableGenerateButton();

        }
        private void btn_GenerateReports_Click(object sender, EventArgs e)
        {
            try
            {
                string destinyReportsPath = GetOrCreateReportsFolder();

                var clientRepo = new ClientRepository();
                var instalacaoRepo = new InstalacaoRepository();
                var enderecoRepo = new EnderecoRepository();

                var clients = clientRepo.GetAll();
                var instalacoes = instalacaoRepo.GetAll();
                var enderecos = enderecoRepo.GetAll();

                var input = new ReportGenerationInputDto
                {
                    CemigTablePath = txtBox_CemigTablePath.Text,
                    Clients = clients,
                    Instalacoes = instalacoes,
                    Enderecos = enderecos,
                    KwhValue = float.Parse(txtBox_KwhValue.Text),
                    DestinyFolder = destinyReportsPath,
                    PdfModelPath = Path.Combine(AppContext.BaseDirectory, "Assets", "modeloapresentacao.pdf"),
                };

                if (!InputValidator.Validate(input, out string error))
                {
                    MessageBox.Show(error, "Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                _handler.Generate(input);

                MessageBox.Show("Relatórios gerados com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao gerar relatórios: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private string GetOrCreateReportsFolder()
        {
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string destinyReportsPath = Path.Combine(desktopPath, "relatorios");

            if (!Directory.Exists(destinyReportsPath))
            {
                Directory.CreateDirectory(destinyReportsPath);
            }

            return destinyReportsPath;
        }
        private void EnableGenerateButton()
        {
            btn_GenerateReports.Enabled =
                !string.IsNullOrWhiteSpace(txtBox_CemigTablePath.Text) &&
                !string.IsNullOrWhiteSpace(txtBox_KwhValue.Text);
        }
        private void btn_Clients_Click(object sender, EventArgs e)
        {
            var clientsForm = new Frm_Clients();
            clientsForm.ShowDialog();
        }
        private void btn_Instalacoes_Click(object sender, EventArgs e)
        {
            var instalacoesForm = new Frm_Instalacoes();
            instalacoesForm.ShowDialog();
        }
        private void btn_ImportDB_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                Filter = "Arquivos Excel (*.xlsx)|*.xlsx|Todos os arquivos (*.*)|*.*",
                Title = "Selecione a planilha de base de dados"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;

                try
                {
                    var enderecoImporter = new EnderecoImporter();
                    enderecoImporter.ImportFromExcelToDb(filePath);

                    var clientEmporter = new ClientImporter();
                    clientEmporter.ImportFromExcelToDb(filePath);

                    var instalacaoImporter = new InstalacaoImporter();
                    instalacaoImporter.ImportFromExcelToDb(filePath);

                    MessageBox.Show("Base de dados importada com sucesso!", "Importação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro durante a importação: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void btn_ExportDB_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Arquivos Excel (*.xlsx)|*.xlsx",
                Title = "Salvar base de dados como Excel",
                FileName = "bd_sollarwelle.xlsx"
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = saveFileDialog.FileName;

                try
                {
                    // 1. Recupera os dados do banco
                    var enderecoRepo = new EnderecoRepository();
                    var clienteRepo = new ClientRepository();
                    var instalacaoRepo = new InstalacaoRepository();

                    var enderecos = enderecoRepo.GetAll();
                    var clientes = clienteRepo.GetAll();
                    var instalacoes = instalacaoRepo.GetAll();

                    // 2. Exporta os dados para Excel
                    var exporter = new ExcelExporter();
                    exporter.ExportToExcel(filePath, enderecos, clientes, instalacoes);

                    MessageBox.Show("Base de dados exportada com sucesso!", "Exportação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro durante a exportação: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
