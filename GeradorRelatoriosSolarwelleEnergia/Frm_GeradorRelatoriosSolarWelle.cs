using GeradorRelatoriosSolarwelleEnergia.Dominio.DTO;
using GeradorRelatoriosSolarwelleEnergia.Dominio.Entidades;
using GeradorRelatoriosSolarwelleEnergia.Dominio.Utils;

namespace GeradorRelatoriosSolarwelleEnergia
{
    public partial class Frm_GeradorRelatoriosSolarWelle : Form
    {
        public Frm_GeradorRelatoriosSolarWelle()
        {
            InitializeComponent();
        }

        private void txtBox_CaminhoXml_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Selecione um arquivo XML";
            openFileDialog.Filter = "Arquivos XML (*.xml)|*.xml";
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
            string filePathTabelaCemig = txtBox_CaminhoXmlCemig.Text;
            string filePathTabelaClientes = txtBox_CaminhoTabelaClientes.Text;
            float valorKwhH = float.Parse(txtBox_ValorKwH.Text);

            List<TabelaCemig> listaTabelaCemig = TabelaCemig.LerTabelaXML(filePathTabelaCemig);
            List<Cliente> listaClientes = Cliente.LerTabelaExcel(filePathTabelaClientes);
                    
            string caminhoPdfModelo = Path.Combine(AppContext.BaseDirectory, "Assets", "modeloapresentacao.pdf");

            var listaRelatorios = RelatorioCliente.montarTabelaDeRelatorios(listaTabelaCemig, listaClientes, valorKwhH);

            //TRECHO ENGESSADO, AJUSTAR
            string pastaDestino = @"C:\Users\Usuário\Desktop\softwaregordao\relatorios\";

            foreach (var relatorio in listaRelatorios)
            {
                string nomeBase = relatorio.RazaoSocialOuNome.Trim();
                string nomeArquivo = nomeBase + ".pdf";
                string caminhoPdfFormatado = Path.Combine(pastaDestino, nomeArquivo);

                //Contador adc ao nome do arquivo em caso de haver mais de um relatório por cliente
                int contador = 2;
                while (File.Exists(caminhoPdfFormatado))
                {
                    nomeArquivo = $"{nomeBase}({contador}).pdf";
                    caminhoPdfFormatado = Path.Combine(pastaDestino, nomeArquivo);
                    contador++;
                }

                FormatadorPdf formatadorPdf = new FormatadorPdf(caminhoPdfModelo, caminhoPdfFormatado);
                formatadorPdf.gerarRelatorio(relatorio);
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
