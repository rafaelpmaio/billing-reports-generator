namespace GeradorRelatoriosSolarwelleEnergia
{
    partial class Frm_SolarWelleReportsGenerator
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
            btn_GenerateReports = new Button();
            txtBox_CemigTablePath = new TextBox();
            lbl_ValorKwH = new Label();
            txtBox_KwhValue = new TextBox();
            btn_Clients = new Button();
            btn_Instalacoes = new Button();
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
            // btn_GenerateReports
            // 
            btn_GenerateReports.Enabled = false;
            btn_GenerateReports.Location = new Point(394, 116);
            btn_GenerateReports.Name = "btn_GenerateReports";
            btn_GenerateReports.Size = new Size(119, 23);
            btn_GenerateReports.TabIndex = 1;
            btn_GenerateReports.Text = "gerar relatórios";
            btn_GenerateReports.UseVisualStyleBackColor = true;
            btn_GenerateReports.Click += btn_GenerateReports_Click;
            // 
            // txtBox_CemigTablePath
            // 
            txtBox_CemigTablePath.Location = new Point(12, 39);
            txtBox_CemigTablePath.Name = "txtBox_CemigTablePath";
            txtBox_CemigTablePath.ReadOnly = true;
            txtBox_CemigTablePath.Size = new Size(269, 23);
            txtBox_CemigTablePath.TabIndex = 2;
            txtBox_CemigTablePath.Click += txtBox_CemigTablePath_Click;
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
            // txtBox_KwhValue
            // 
            txtBox_KwhValue.Location = new Point(12, 91);
            txtBox_KwhValue.Name = "txtBox_KwhValue";
            txtBox_KwhValue.Size = new Size(129, 23);
            txtBox_KwhValue.TabIndex = 4;
            txtBox_KwhValue.KeyPress += txtBox_KwhValue_KeyPress;
            // 
            // btn_Clients
            // 
            btn_Clients.Location = new Point(313, 116);
            btn_Clients.Name = "btn_Clients";
            btn_Clients.Size = new Size(75, 23);
            btn_Clients.TabIndex = 7;
            btn_Clients.Text = "clientes";
            btn_Clients.UseVisualStyleBackColor = true;
            btn_Clients.Click += btn_Clients_Click;
            // 
            // btn_Instalacoes
            // 
            btn_Instalacoes.Location = new Point(232, 116);
            btn_Instalacoes.Name = "btn_Instalacoes";
            btn_Instalacoes.Size = new Size(75, 23);
            btn_Instalacoes.TabIndex = 8;
            btn_Instalacoes.Text = "Instalações";
            btn_Instalacoes.UseVisualStyleBackColor = true;
            btn_Instalacoes.Click += btn_Instalacoes_Click;
            // 
            // Frm_SolarWelleReportsGenerator
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(525, 149);
            Controls.Add(btn_Instalacoes);
            Controls.Add(btn_Clients);
            Controls.Add(txtBox_KwhValue);
            Controls.Add(lbl_ValorKwH);
            Controls.Add(txtBox_CemigTablePath);
            Controls.Add(btn_GenerateReports);
            Controls.Add(lbl_RelatorioCemig);
            Name = "Frm_SolarWelleReportsGenerator";
            Text = "SolarWelle - Gerador de Relatórios";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lbl_RelatorioCemig;
        private Button btn_GenerateReports;
        private TextBox txtBox_CemigTablePath;
        private Label lbl_ValorKwH;
        private TextBox txtBox_KwhValue;
        private Button btn_Clients;
        private Button btn_Instalacoes;
    }
}
