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
            txtBox_CaminhoTabelaCemig = new TextBox();
            lbl_ValorKwH = new Label();
            txtBox_ValorKwH = new TextBox();
            clientes = new Button();
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
            btn_GerarRelatorios.Location = new Point(243, 116);
            btn_GerarRelatorios.Name = "btn_GerarRelatorios";
            btn_GerarRelatorios.Size = new Size(119, 23);
            btn_GerarRelatorios.TabIndex = 1;
            btn_GerarRelatorios.Text = "gerar relatórios";
            btn_GerarRelatorios.UseVisualStyleBackColor = true;
            btn_GerarRelatorios.Click += btn_GerarRelatorios_Click;
            // 
            // txtBox_CaminhoTabelaCemig
            // 
            txtBox_CaminhoTabelaCemig.Location = new Point(12, 39);
            txtBox_CaminhoTabelaCemig.Name = "txtBox_CaminhoTabelaCemig";
            txtBox_CaminhoTabelaCemig.ReadOnly = true;
            txtBox_CaminhoTabelaCemig.Size = new Size(269, 23);
            txtBox_CaminhoTabelaCemig.TabIndex = 2;
            txtBox_CaminhoTabelaCemig.Click += txtBox_CaminhoTabelaCemig_Click;
            // 
            // lbl_ValorKwH
            // 
            lbl_ValorKwH.AutoSize = true;
            lbl_ValorKwH.Location = new Point(12, 73);
            lbl_ValorKwH.Name = "lbl_ValorKwH";
            lbl_ValorKwH.Size = new Size(129, 15);
            lbl_ValorKwH.TabIndex = 3;
            lbl_ValorKwH.Text = "Digite o valor do KW/H";
            // 
            // txtBox_ValorKwH
            // 
            txtBox_ValorKwH.Location = new Point(12, 91);
            txtBox_ValorKwH.Name = "txtBox_ValorKwH";
            txtBox_ValorKwH.Size = new Size(129, 23);
            txtBox_ValorKwH.TabIndex = 4;
            txtBox_ValorKwH.KeyPress += txtBox_ValorKwH_KeyPress;
            // 
            // clientes
            // 
            clientes.Location = new Point(162, 116);
            clientes.Name = "clientes";
            clientes.Size = new Size(75, 23);
            clientes.TabIndex = 7;
            clientes.Text = "clientes";
            clientes.UseVisualStyleBackColor = true;
            clientes.Click += btn_Clientes_Click;
            // 
            // Frm_GeradorRelatoriosSolarWelle
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(374, 149);
            Controls.Add(clientes);
            Controls.Add(txtBox_ValorKwH);
            Controls.Add(lbl_ValorKwH);
            Controls.Add(txtBox_CaminhoTabelaCemig);
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
        private TextBox txtBox_CaminhoTabelaCemig;
        private Label lbl_ValorKwH;
        private TextBox txtBox_ValorKwH;
        private Button clientes;
    }
}
