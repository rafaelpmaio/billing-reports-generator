using GeradorRelatoriosSolarwelleEnergia.Application.Services;
using GeradorRelatoriosSolarwelleEnergia.ApplicationLayer.Services;
using GeradorRelatoriosSolarwelleEnergia.Domain.DTO;
using GeradorRelatoriosSolarwelleEnergia.Domain.Entities;
using GeradorRelatoriosSolarwelleEnergia.Domain.Services;
using GeradorRelatoriosSolarwelleEnergia.Domain.Utils;
using GeradorRelatoriosSolarwelleEnergia.Dominio.Entidades;
using GeradorRelatoriosSolarwelleEnergia.Infrastructure.Pdf;
using GeradorRelatoriosSolarwelleEnergia.Infrastructure.Readers;

namespace GeradorRelatoriosSolarwelleEnergia
{
    public partial class Frm_GeradorRelatoriosSolarWelle : Form
    {
        private readonly ReportGeneratorAppService _handler;
        public Frm_GeradorRelatoriosSolarWelle()
        {
            InitializeComponent();

            _handler = new ReportGeneratorAppService(
                new TabelaCemigService(),
                new ClienteReader(),
                new ClientEconomyHistoryReader(),
                new ClientReportService(),
                new PdfReportSaver()
                );
        }

        private void txtBox_CaminhoTabelaCemig_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Selecione um arquivo XML ou XLSX";
            openFileDialog.Filter = "Arquivos XML ou Excel (*.xml;*.xlsx)|*.xml;*.xlsx|Todos os arquivos (*.*)|*.*";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                txtBox_CaminhoXmlCemig.Text = openFileDialog.FileName;
                habilitarBotao();
            }
        }

        private void txtBox_CaminhoTabelaClientes_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Selecione um arquivo XLSX";
            openFileDialog.Filter = "Arquivos XLSX (*.xlsx)|*.xlsx";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                txtBox_CaminhoTabelaClientes.Text = openFileDialog.FileName;
                habilitarBotao();
            }
        }

        private void txtBox_ValorKwH_KeyPress(object sender, KeyPressEventArgs e)
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
            habilitarBotao();

        }

        private void btn_GerarRelatorios_Click(object sender, EventArgs e)
        {
            try
            {
                string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                string destinyReportsPath = Path.Combine(desktopPath, "relatorios");

                if (!Directory.Exists(destinyReportsPath)) 
                {
                    Directory.CreateDirectory(destinyReportsPath);
                }

                var input = new ReportGenerationInputDto
                {
                    CemigTablePath = txtBox_CaminhoXmlCemig.Text,
                    ClientsTablePath = txtBox_CaminhoTabelaClientes.Text,
                    KwhValue = float.Parse(txtBox_ValorKwH.Text),
                    DestinyFolder = destinyReportsPath,
                    PdfModelPath = Path.Combine(AppContext.BaseDirectory, "Assets", "modeloapresentacao.pdf"),
                };
                //@"C:\Users\Usuário\Desktop\softwaregordao\relatorios\"

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

        private void habilitarBotao()
        {
            btn_GerarRelatorios.Enabled =
                !string.IsNullOrWhiteSpace(txtBox_CaminhoXmlCemig.Text) &&
                !string.IsNullOrWhiteSpace(txtBox_CaminhoTabelaClientes.Text) &&
                !string.IsNullOrWhiteSpace(txtBox_ValorKwH.Text);
        }
    }
}
