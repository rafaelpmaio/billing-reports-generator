namespace GeradorRelatoriosSolarwelleEnergia.Forms
{
    partial class Frm_Instalacoes
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
            dataGridView = new DataGridView();
            btn_UpdateInstalacao = new Button();
            btn_DeleteInstalacao = new Button();
            btn_AddInstalacao = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView).BeginInit();
            SuspendLayout();
            // 
            // dataGridView
            // 
            dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView.Dock = DockStyle.Top;
            dataGridView.Location = new Point(0, 0);
            dataGridView.Name = "dataGridView";
            dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView.Size = new Size(800, 392);
            dataGridView.TabIndex = 0;
            // 
            // btn_UpdateInstalacao
            // 
            btn_UpdateInstalacao.Location = new Point(713, 415);
            btn_UpdateInstalacao.Name = "btn_UpdateInstalacao";
            btn_UpdateInstalacao.Size = new Size(75, 23);
            btn_UpdateInstalacao.TabIndex = 2;
            btn_UpdateInstalacao.Text = "Editar";
            btn_UpdateInstalacao.UseVisualStyleBackColor = true;
            // 
            // btn_DeleteInstalacao
            // 
            btn_DeleteInstalacao.Location = new Point(632, 415);
            btn_DeleteInstalacao.Name = "btn_DeleteInstalacao";
            btn_DeleteInstalacao.Size = new Size(75, 23);
            btn_DeleteInstalacao.TabIndex = 3;
            btn_DeleteInstalacao.Text = "Deletar";
            btn_DeleteInstalacao.UseVisualStyleBackColor = true;
            // 
            // btn_AddInstalacao
            // 
            btn_AddInstalacao.Location = new Point(551, 415);
            btn_AddInstalacao.Name = "btn_AddInstalacao";
            btn_AddInstalacao.Size = new Size(75, 23);
            btn_AddInstalacao.TabIndex = 4;
            btn_AddInstalacao.Text = "Adicionar";
            btn_AddInstalacao.UseVisualStyleBackColor = true;
            // 
            // Frm_Instalacoes
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btn_AddInstalacao);
            Controls.Add(btn_DeleteInstalacao);
            Controls.Add(btn_UpdateInstalacao);
            Controls.Add(dataGridView);
            Name = "Frm_Instalacoes";
            Text = "Frm_Instalacao";
            ((System.ComponentModel.ISupportInitialize)dataGridView).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dataGridView;
        private Button btn_UpdateInstalacao;
        private Button btn_DeleteInstalacao;
        private Button btn_AddInstalacao;
    }
}