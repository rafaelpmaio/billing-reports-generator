namespace GeradorRelatoriosSolarwelleEnergia.Forms
{
    partial class Frm_AddOrUpdateInstalacao
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            cmb_EnergyDistributor = new ComboBox();
            lbl_DistribuidoraEnergia = new Label();
            cmb_DiscountRate = new ComboBox();
            lbl_DescontoCliente = new Label();
            txtBox_NumeroCliente = new TextBox();
            lbl_NumeroCliente = new Label();
            txtBox_NumeroInstalacao = new TextBox();
            lbl_NumeroInstalacao = new Label();
            btn_Send = new Button();
            chk_Ativo = new CheckBox();
            SuspendLayout();
            // 
            // cmb_EnergyDistributor
            // 
            cmb_EnergyDistributor.FormattingEnabled = true;
            cmb_EnergyDistributor.Location = new Point(12, 118);
            cmb_EnergyDistributor.Name = "cmb_EnergyDistributor";
            cmb_EnergyDistributor.Size = new Size(186, 23);
            cmb_EnergyDistributor.TabIndex = 39;
            // 
            // lbl_DistribuidoraEnergia
            // 
            lbl_DistribuidoraEnergia.AutoSize = true;
            lbl_DistribuidoraEnergia.Location = new Point(11, 100);
            lbl_DistribuidoraEnergia.Name = "lbl_DistribuidoraEnergia";
            lbl_DistribuidoraEnergia.Size = new Size(133, 15);
            lbl_DistribuidoraEnergia.TabIndex = 38;
            lbl_DistribuidoraEnergia.Text = "Distribuidora de Energia";
            // 
            // cmb_DiscountRate
            // 
            cmb_DiscountRate.FormattingEnabled = true;
            cmb_DiscountRate.Location = new Point(12, 170);
            cmb_DiscountRate.Name = "cmb_DiscountRate";
            cmb_DiscountRate.Size = new Size(121, 23);
            cmb_DiscountRate.TabIndex = 41;
            // 
            // lbl_DescontoCliente
            // 
            lbl_DescontoCliente.AutoSize = true;
            lbl_DescontoCliente.Location = new Point(12, 152);
            lbl_DescontoCliente.Name = "lbl_DescontoCliente";
            lbl_DescontoCliente.Size = new Size(124, 15);
            lbl_DescontoCliente.TabIndex = 40;
            lbl_DescontoCliente.Text = "Valor do Desconto (%)";
            // 
            // txtBox_NumeroCliente
            // 
            txtBox_NumeroCliente.Location = new Point(12, 74);
            txtBox_NumeroCliente.Name = "txtBox_NumeroCliente";
            txtBox_NumeroCliente.Size = new Size(214, 23);
            txtBox_NumeroCliente.TabIndex = 45;
            // 
            // lbl_NumeroCliente
            // 
            lbl_NumeroCliente.AutoSize = true;
            lbl_NumeroCliente.Location = new Point(12, 56);
            lbl_NumeroCliente.Name = "lbl_NumeroCliente";
            lbl_NumeroCliente.Size = new Size(91, 15);
            lbl_NumeroCliente.TabIndex = 44;
            lbl_NumeroCliente.Text = "Numero Cliente";
            // 
            // txtBox_NumeroInstalacao
            // 
            txtBox_NumeroInstalacao.Location = new Point(12, 30);
            txtBox_NumeroInstalacao.Name = "txtBox_NumeroInstalacao";
            txtBox_NumeroInstalacao.Size = new Size(211, 23);
            txtBox_NumeroInstalacao.TabIndex = 43;
            // 
            // lbl_NumeroInstalacao
            // 
            lbl_NumeroInstalacao.AutoSize = true;
            lbl_NumeroInstalacao.Location = new Point(12, 12);
            lbl_NumeroInstalacao.Name = "lbl_NumeroInstalacao";
            lbl_NumeroInstalacao.Size = new Size(107, 15);
            lbl_NumeroInstalacao.TabIndex = 42;
            lbl_NumeroInstalacao.Text = "Numero Instalação";
            // 
            // btn_Send
            // 
            btn_Send.Location = new Point(171, 264);
            btn_Send.Name = "btn_Send";
            btn_Send.Size = new Size(75, 23);
            btn_Send.TabIndex = 46;
            btn_Send.Text = "Enviar";
            btn_Send.UseVisualStyleBackColor = true;
            btn_Send.Click += btn_Send_Click;
            // 
            // chk_Ativo
            // 
            chk_Ativo.AutoSize = true;
            chk_Ativo.Location = new Point(11, 211);
            chk_Ativo.Name = "chk_Ativo";
            chk_Ativo.Size = new Size(109, 19);
            chk_Ativo.TabIndex = 47;
            chk_Ativo.Text = "Instalação Ativa";
            chk_Ativo.UseVisualStyleBackColor = true;
            // 
            // Frm_AddOrUpdateInstalacao
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(258, 297);
            Controls.Add(chk_Ativo);
            Controls.Add(btn_Send);
            Controls.Add(txtBox_NumeroCliente);
            Controls.Add(lbl_NumeroCliente);
            Controls.Add(txtBox_NumeroInstalacao);
            Controls.Add(lbl_NumeroInstalacao);
            Controls.Add(cmb_DiscountRate);
            Controls.Add(lbl_DescontoCliente);
            Controls.Add(cmb_EnergyDistributor);
            Controls.Add(lbl_DistribuidoraEnergia);
            Name = "Frm_AddOrUpdateInstalacao";
            Text = "Frm_AddOrUpdateInstalacao";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox cmb_EnergyDistributor;
        private Label lbl_DistribuidoraEnergia;
        private ComboBox cmb_DiscountRate;
        private Label lbl_DescontoCliente;
        private TextBox txtBox_NumeroCliente;
        private Label lbl_NumeroCliente;
        private TextBox txtBox_NumeroInstalacao;
        private Label lbl_NumeroInstalacao;
        private Button btn_Send;
        private CheckBox chk_Ativo;
    }
}