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
            btn_ImportDB = new Button();
            groupBox1 = new GroupBox();
            btn_ExportDB = new Button();
            groupBox1.SuspendLayout();
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
            btn_GenerateReports.Location = new Point(12, 129);
            btn_GenerateReports.Name = "btn_GenerateReports";
            btn_GenerateReports.Size = new Size(119, 23);
            btn_GenerateReports.TabIndex = 1;
            btn_GenerateReports.Text = "Gerar Relatórios";
            btn_GenerateReports.UseVisualStyleBackColor = true;
            btn_GenerateReports.Click += btn_GenerateReports_Click;
            // 
            // txtBox_CemigTablePath
            // 
            txtBox_CemigTablePath.Location = new Point(12, 39);
            txtBox_CemigTablePath.Name = "txtBox_CemigTablePath";
            txtBox_CemigTablePath.ReadOnly = true;
            txtBox_CemigTablePath.Size = new Size(156, 23);
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
            txtBox_KwhValue.Size = new Size(156, 23);
            txtBox_KwhValue.TabIndex = 4;
            txtBox_KwhValue.KeyPress += txtBox_KwhValue_KeyPress;
            // 
            // btn_Clients
            // 
            btn_Clients.Location = new Point(6, 80);
            btn_Clients.Name = "btn_Clients";
            btn_Clients.Size = new Size(75, 23);
            btn_Clients.TabIndex = 7;
            btn_Clients.Text = "Clientes";
            btn_Clients.UseVisualStyleBackColor = true;
            btn_Clients.Click += btn_Clients_Click;
            // 
            // btn_Instalacoes
            // 
            btn_Instalacoes.Location = new Point(6, 109);
            btn_Instalacoes.Name = "btn_Instalacoes";
            btn_Instalacoes.Size = new Size(75, 23);
            btn_Instalacoes.TabIndex = 8;
            btn_Instalacoes.Text = "Instalações";
            btn_Instalacoes.UseVisualStyleBackColor = true;
            btn_Instalacoes.Click += btn_Instalacoes_Click;
            // 
            // btn_ImportDB
            // 
            btn_ImportDB.Location = new Point(6, 22);
            btn_ImportDB.Name = "btn_ImportDB";
            btn_ImportDB.Size = new Size(113, 23);
            btn_ImportDB.TabIndex = 9;
            btn_ImportDB.Text = "Importar BD";
            btn_ImportDB.UseVisualStyleBackColor = true;
            btn_ImportDB.Click += btn_ImportDB_Click;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(btn_ExportDB);
            groupBox1.Controls.Add(btn_Clients);
            groupBox1.Controls.Add(btn_ImportDB);
            groupBox1.Controls.Add(btn_Instalacoes);
            groupBox1.Location = new Point(190, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(133, 140);
            groupBox1.TabIndex = 10;
            groupBox1.TabStop = false;
            groupBox1.Text = "Base de Dados";
            // 
            // btn_ExportDB
            // 
            btn_ExportDB.Location = new Point(6, 51);
            btn_ExportDB.Name = "btn_ExportDB";
            btn_ExportDB.Size = new Size(113, 23);
            btn_ExportDB.TabIndex = 10;
            btn_ExportDB.Text = "Exportar BD";
            btn_ExportDB.UseVisualStyleBackColor = true;
            btn_ExportDB.Click += btn_ExportDB_Click;
            // 
            // Frm_SolarWelleReportsGenerator
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(339, 164);
            Controls.Add(groupBox1);
            Controls.Add(txtBox_KwhValue);
            Controls.Add(lbl_ValorKwH);
            Controls.Add(txtBox_CemigTablePath);
            Controls.Add(btn_GenerateReports);
            Controls.Add(lbl_RelatorioCemig);
            Name = "Frm_SolarWelleReportsGenerator";
            Text = "SolarWelle - Gerador de Relatórios";
            groupBox1.ResumeLayout(false);
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
        private Button btn_ImportDB;
        private GroupBox groupBox1;
        private Button btn_ExportDB;
    }
}
