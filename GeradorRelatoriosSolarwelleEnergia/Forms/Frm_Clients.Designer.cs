namespace GeradorRelatoriosSolarwelleEnergia.Forms
{
    partial class Frm_Clients
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
            btn_AddClient = new Button();
            btn_DeleteClient = new Button();
            btn_ImportClients = new Button();
            btn_UpdateClient = new Button();
            btn_ExportClients = new Button();
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
            dataGridView.Size = new Size(800, 390);
            dataGridView.TabIndex = 0;
            // 
            // btn_AddClient
            // 
            btn_AddClient.Location = new Point(551, 415);
            btn_AddClient.Name = "btn_AddClient";
            btn_AddClient.Size = new Size(75, 23);
            btn_AddClient.TabIndex = 1;
            btn_AddClient.Text = "Adicionar";
            btn_AddClient.UseVisualStyleBackColor = true;
            btn_AddClient.Click += btn_AddClient_Click;
            // 
            // btn_DeleteClient
            // 
            btn_DeleteClient.Enabled = false;
            btn_DeleteClient.Location = new Point(632, 415);
            btn_DeleteClient.Name = "btn_DeleteClient";
            btn_DeleteClient.Size = new Size(75, 23);
            btn_DeleteClient.TabIndex = 2;
            btn_DeleteClient.Text = "Deletar";
            btn_DeleteClient.UseVisualStyleBackColor = true;
            btn_DeleteClient.Click += btn_DeleteClient_Click;
            // 
            // btn_ImportClients
            // 
            btn_ImportClients.Location = new Point(12, 415);
            btn_ImportClients.Name = "btn_ImportClients";
            btn_ImportClients.Size = new Size(75, 23);
            btn_ImportClients.TabIndex = 3;
            btn_ImportClients.Text = "Importar";
            btn_ImportClients.UseVisualStyleBackColor = true;
            btn_ImportClients.Click += btn_ImportClients_Click;
            // 
            // btn_UpdateClient
            // 
            btn_UpdateClient.Enabled = false;
            btn_UpdateClient.Location = new Point(713, 415);
            btn_UpdateClient.Name = "btn_UpdateClient";
            btn_UpdateClient.Size = new Size(75, 23);
            btn_UpdateClient.TabIndex = 4;
            btn_UpdateClient.Text = "Editar";
            btn_UpdateClient.UseVisualStyleBackColor = true;
            btn_UpdateClient.Click += btn_UpdateClient_Click;
            // 
            // btn_ExportClients
            // 
            btn_ExportClients.Location = new Point(93, 415);
            btn_ExportClients.Name = "btn_ExportClients";
            btn_ExportClients.Size = new Size(75, 23);
            btn_ExportClients.TabIndex = 5;
            btn_ExportClients.Text = "Exportar";
            btn_ExportClients.UseVisualStyleBackColor = true;
            // 
            // Frm_Clients
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btn_ExportClients);
            Controls.Add(btn_UpdateClient);
            Controls.Add(btn_ImportClients);
            Controls.Add(btn_DeleteClient);
            Controls.Add(btn_AddClient);
            Controls.Add(dataGridView);
            Name = "Frm_Clients";
            Text = "Clientes SollarWelle";
            ((System.ComponentModel.ISupportInitialize)dataGridView).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dataGridView;
        private Button btn_AddClient;
        private Button btn_DeleteClient;
        private Button btn_ImportClients;
        private Button btn_UpdateClient;
        private Button btn_ExportClients;
    }
}