namespace Arenda.Bank
{
    partial class frmSelectBanks
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSelectBanks));
            this.dgvBank = new System.Windows.Forms.DataGridView();
            this.cBankName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cBankBik = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cBankKS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cBankRS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btExit = new System.Windows.Forms.Button();
            this.btSelect = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBank)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvBank
            // 
            this.dgvBank.AllowUserToAddRows = false;
            this.dgvBank.AllowUserToDeleteRows = false;
            this.dgvBank.AllowUserToResizeRows = false;
            this.dgvBank.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvBank.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvBank.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBank.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.cBankName,
            this.cBankBik,
            this.cBankKS,
            this.cBankRS});
            this.dgvBank.Location = new System.Drawing.Point(12, 12);
            this.dgvBank.MultiSelect = false;
            this.dgvBank.Name = "dgvBank";
            this.dgvBank.ReadOnly = true;
            this.dgvBank.RowHeadersVisible = false;
            this.dgvBank.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvBank.Size = new System.Drawing.Size(710, 403);
            this.dgvBank.TabIndex = 1;
            this.dgvBank.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvBank_CellMouseDoubleClick);
            // 
            // cBankName
            // 
            this.cBankName.DataPropertyName = "cName";
            this.cBankName.HeaderText = "Банк";
            this.cBankName.Name = "cBankName";
            this.cBankName.ReadOnly = true;
            // 
            // cBankBik
            // 
            this.cBankBik.DataPropertyName = "BIC";
            this.cBankBik.HeaderText = "БИК";
            this.cBankBik.Name = "cBankBik";
            this.cBankBik.ReadOnly = true;
            // 
            // cBankKS
            // 
            this.cBankKS.DataPropertyName = "CorrespondentAccount";
            this.cBankKS.HeaderText = "К/С";
            this.cBankKS.Name = "cBankKS";
            this.cBankKS.ReadOnly = true;
            // 
            // cBankRS
            // 
            this.cBankRS.DataPropertyName = "PaymentAccount";
            this.cBankRS.HeaderText = "Р/С";
            this.cBankRS.Name = "cBankRS";
            this.cBankRS.ReadOnly = true;
            // 
            // btExit
            // 
            this.btExit.Image = ((System.Drawing.Image)(resources.GetObject("btExit.Image")));
            this.btExit.Location = new System.Drawing.Point(691, 436);
            this.btExit.Name = "btExit";
            this.btExit.Size = new System.Drawing.Size(32, 32);
            this.btExit.TabIndex = 36;
            this.btExit.UseVisualStyleBackColor = true;
            this.btExit.Click += new System.EventHandler(this.btExit_Click);
            // 
            // btSelect
            // 
            this.btSelect.Image = global::Arenda.Properties.Resources.pict_ok;
            this.btSelect.Location = new System.Drawing.Point(628, 436);
            this.btSelect.Name = "btSelect";
            this.btSelect.Size = new System.Drawing.Size(32, 32);
            this.btSelect.TabIndex = 36;
            this.btSelect.UseVisualStyleBackColor = true;
            this.btSelect.Click += new System.EventHandler(this.btSelect_Click);
            // 
            // frmSelectBanks
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(735, 480);
            this.ControlBox = false;
            this.Controls.Add(this.btSelect);
            this.Controls.Add(this.btExit);
            this.Controls.Add(this.dgvBank);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSelectBanks";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Выбор банка арендодателя";
            this.Load += new System.EventHandler(this.frmSelectBanks_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvBank)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvBank;
        private System.Windows.Forms.DataGridViewTextBoxColumn cBankName;
        private System.Windows.Forms.DataGridViewTextBoxColumn cBankBik;
        private System.Windows.Forms.DataGridViewTextBoxColumn cBankKS;
        private System.Windows.Forms.DataGridViewTextBoxColumn cBankRS;
        private System.Windows.Forms.Button btExit;
        private System.Windows.Forms.Button btSelect;
    }
}