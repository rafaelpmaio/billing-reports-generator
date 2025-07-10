namespace GeradorRelatoriosSolarwelleEnergia
{
    partial class Frm_GeradorRelatoriosSolarWelle
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lbl_RelatorioCemig = new Label();
            btn_GerarRelatorios = new Button();
            txtBox_CaminhoXmlCemig = new TextBox();
            lbl_ValorKwH = new Label();
            txtBox_ValorKwH = new TextBox();
            txtBox_CaminhoTabelaClientes = new TextBox();
            lbl_TabelaClientes = new Label();
            SuspendLayout();
            // 
            // lbl_RelatorioCemig
            // 
            lbl_RelatorioCemig.AutoSize = true;
            lbl_RelatorioCemig.Location = new Point(12, 21);
            lbl_RelatorioCemig.Name = "lbl_RelatorioCemig";
            lbl_RelatorioCemig.Size = new Size(156, 15);
            lbl_RelatorioCemig.TabIndex = 0;
            lbl_RelatorioCemig.Text = "Selecione o Relatório CEMIG";
            // 
            // btn_GerarRelatorios
            // 
            btn_GerarRelatorios.Enabled = false;
            btn_GerarRelatorios.Location = new Point(243, 175);
            btn_GerarRelatorios.Name = "btn_GerarRelatorios";
            btn_GerarRelatorios.Size = new Size(119, 23);
            btn_GerarRelatorios.TabIndex = 1;
            btn_GerarRelatorios.Text = "gerar relatórios";
            btn_GerarRelatorios.UseVisualStyleBackColor = true;
            btn_GerarRelatorios.Click += btn_GerarRelatorios_Click;
            // 
            // txtBox_CaminhoXmlCemig
            // 
            txtBox_CaminhoXmlCemig.Location = new Point(12, 39);
            txtBox_CaminhoXmlCemig.Name = "txtBox_CaminhoXmlCemig";
            txtBox_CaminhoXmlCemig.ReadOnly = true;
            txtBox_CaminhoXmlCemig.Size = new Size(269, 23);
            txtBox_CaminhoXmlCemig.TabIndex = 2;
            txtBox_CaminhoXmlCemig.Click += txtBox_CaminhoXml_Click;
            // 
            // lbl_ValorKwH
            // 
            lbl_ValorKwH.AutoSize = true;
            lbl_ValorKwH.Location = new Point(12, 132);
            lbl_ValorKwH.Name = "lbl_ValorKwH";
            lbl_ValorKwH.Size = new Size(129, 15);
            lbl_ValorKwH.TabIndex = 3;
            lbl_ValorKwH.Text = "Digite o valor do KW/H";
            // 
            // txtBox_ValorKwH
            // 
            txtBox_ValorKwH.Location = new Point(12, 150);
            txtBox_ValorKwH.Name = "txtBox_ValorKwH";
            txtBox_ValorKwH.Size = new Size(129, 23);
            txtBox_ValorKwH.TabIndex = 4;
            txtBox_ValorKwH.KeyPress += txtBox_ValorKwH_KeyPress;
            // 
            // txtBox_CaminhoTabelaClientes
            // 
            txtBox_CaminhoTabelaClientes.Location = new Point(12, 94);
            txtBox_CaminhoTabelaClientes.Name = "txtBox_CaminhoTabelaClientes";
            txtBox_CaminhoTabelaClientes.ReadOnly = true;
            txtBox_CaminhoTabelaClientes.Size = new Size(269, 23);
            txtBox_CaminhoTabelaClientes.TabIndex = 6;
            txtBox_CaminhoTabelaClientes.Click += txtBox_CaminhoTabelaClientes_Click;
            // 
            // lbl_TabelaClientes
            // 
            lbl_TabelaClientes.AutoSize = true;
            lbl_TabelaClientes.Location = new Point(12, 76);
            lbl_TabelaClientes.Name = "lbl_TabelaClientes";
            lbl_TabelaClientes.Size = new Size(171, 15);
            lbl_TabelaClientes.TabIndex = 5;
            lbl_TabelaClientes.Text = "Selecione a tabela de CLIENTES";
            // 
            // Frm_GeradorRelatoriosSolarWelle
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(374, 210);
            Controls.Add(txtBox_CaminhoTabelaClientes);
            Controls.Add(lbl_TabelaClientes);
            Controls.Add(txtBox_ValorKwH);
            Controls.Add(lbl_ValorKwH);
            Controls.Add(txtBox_CaminhoXmlCemig);
            Controls.Add(btn_GerarRelatorios);
            Controls.Add(lbl_RelatorioCemig);
            Name = "Frm_GeradorRelatoriosSolarWelle";
            Text = "SolarWelle - Gerador de Relatórios";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lbl_RelatorioCemig;
        private Button btn_GerarRelatorios;
        private TextBox txtBox_CaminhoXmlCemig;
        private Label lbl_ValorKwH;
        private TextBox txtBox_ValorKwH;
        private TextBox txtBox_CaminhoTabelaClientes;
        private Label lbl_TabelaClientes;
    }
}
