namespace Arenda.Payments
{
    partial class frmKntListTaxes
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.cmbPlaneDate = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btClose = new System.Windows.Forms.Button();
            this.btSelect = new System.Windows.Forms.Button();
            this.dgvData = new System.Windows.Forms.DataGridView();
            this.picBoxIsConfirmed = new System.Windows.Forms.PictureBox();
            this.cNumAgreements = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DateFines = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Summa = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cSelect = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.chbIsConfirmed = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxIsConfirmed)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbPlaneDate
            // 
            this.cmbPlaneDate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPlaneDate.FormattingEnabled = true;
            this.cmbPlaneDate.Location = new System.Drawing.Point(92, 12);
            this.cmbPlaneDate.Name = "cmbPlaneDate";
            this.cmbPlaneDate.Size = new System.Drawing.Size(186, 21);
            this.cmbPlaneDate.TabIndex = 84;
            this.cmbPlaneDate.SelectionChangeCommitted += new System.EventHandler(this.cmbPlaneDate_SelectionChangeCommitted);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 13);
            this.label2.TabIndex = 83;
            this.label2.Text = "План-отчет за:";
            // 
            // btClose
            // 
            this.btClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btClose.Image = global::Arenda.Properties.Resources.Log_Out_icon1;
            this.btClose.Location = new System.Drawing.Point(756, 406);
            this.btClose.Name = "btClose";
            this.btClose.Size = new System.Drawing.Size(32, 32);
            this.btClose.TabIndex = 21;
            this.btClose.UseVisualStyleBackColor = true;
            this.btClose.Click += new System.EventHandler(this.btClose_Click);
            // 
            // btSelect
            // 
            this.btSelect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btSelect.Image = global::Arenda.Properties.Resources.saveHS;
            this.btSelect.Location = new System.Drawing.Point(718, 406);
            this.btSelect.Name = "btSelect";
            this.btSelect.Size = new System.Drawing.Size(32, 32);
            this.btSelect.TabIndex = 85;
            this.btSelect.UseVisualStyleBackColor = true;
            this.btSelect.Click += new System.EventHandler(this.btSelect_Click);
            // 
            // dgvData
            // 
            this.dgvData.AllowUserToAddRows = false;
            this.dgvData.AllowUserToDeleteRows = false;
            this.dgvData.AllowUserToResizeRows = false;
            this.dgvData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvData.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvData.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.cNumAgreements,
            this.DateFines,
            this.cName,
            this.Summa,
            this.cSelect});
            this.dgvData.Location = new System.Drawing.Point(12, 41);
            this.dgvData.MultiSelect = false;
            this.dgvData.Name = "dgvData";
            this.dgvData.RowHeadersVisible = false;
            this.dgvData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvData.Size = new System.Drawing.Size(772, 359);
            this.dgvData.TabIndex = 86;
            this.dgvData.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgvData_RowPostPaint);
            this.dgvData.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.dgvData_RowPrePaint);
            // 
            // picBoxIsConfirmed
            // 
            this.picBoxIsConfirmed.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.picBoxIsConfirmed.BackColor = System.Drawing.Color.Coral;
            this.picBoxIsConfirmed.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picBoxIsConfirmed.Location = new System.Drawing.Point(19, 408);
            this.picBoxIsConfirmed.Name = "picBoxIsConfirmed";
            this.picBoxIsConfirmed.Size = new System.Drawing.Size(12, 12);
            this.picBoxIsConfirmed.TabIndex = 88;
            this.picBoxIsConfirmed.TabStop = false;
            // 
            // cNumAgreements
            // 
            this.cNumAgreements.DataPropertyName = "Agreement";
            this.cNumAgreements.HeaderText = "Номер договора";
            this.cNumAgreements.Name = "cNumAgreements";
            this.cNumAgreements.ReadOnly = true;
            // 
            // DateFines
            // 
            this.DateFines.DataPropertyName = "DateFines";
            this.DateFines.HeaderText = "Дата выписки";
            this.DateFines.Name = "DateFines";
            this.DateFines.ReadOnly = true;
            // 
            // cName
            // 
            this.cName.DataPropertyName = "cName";
            this.cName.HeaderText = "Тип доп. оплаты";
            this.cName.Name = "cName";
            this.cName.ReadOnly = true;
            // 
            // Summa
            // 
            this.Summa.DataPropertyName = "Summa";
            this.Summa.HeaderText = "Сумма к оплате";
            this.Summa.Name = "Summa";
            this.Summa.ReadOnly = true;
            // 
            // cSelect
            // 
            this.cSelect.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.cSelect.DataPropertyName = "isSelect";
            this.cSelect.HeaderText = "V";
            this.cSelect.MinimumWidth = 45;
            this.cSelect.Name = "cSelect";
            this.cSelect.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.cSelect.Width = 45;
            // 
            // chbIsConfirmed
            // 
            this.chbIsConfirmed.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chbIsConfirmed.AutoSize = true;
            this.chbIsConfirmed.Location = new System.Drawing.Point(37, 406);
            this.chbIsConfirmed.Name = "chbIsConfirmed";
            this.chbIsConfirmed.Size = new System.Drawing.Size(115, 17);
            this.chbIsConfirmed.TabIndex = 89;
            this.chbIsConfirmed.Text = "Подтверждённые";
            this.chbIsConfirmed.UseVisualStyleBackColor = true;
            this.chbIsConfirmed.Click += new System.EventHandler(this.chbIsConfirmed_Click);
            // 
            // frmKntListTaxes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.ControlBox = false;
            this.Controls.Add(this.chbIsConfirmed);
            this.Controls.Add(this.picBoxIsConfirmed);
            this.Controls.Add(this.dgvData);
            this.Controls.Add(this.btSelect);
            this.Controls.Add(this.cmbPlaneDate);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btClose);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmKntListTaxes";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Журнал счетов доп. оплат";
            this.Load += new System.EventHandler(this.frmKntListTaxes_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxIsConfirmed)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btClose;
        private System.Windows.Forms.ComboBox cmbPlaneDate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btSelect;
        private System.Windows.Forms.DataGridView dgvData;
        private System.Windows.Forms.PictureBox picBoxIsConfirmed;
        private System.Windows.Forms.DataGridViewTextBoxColumn cNumAgreements;
        private System.Windows.Forms.DataGridViewTextBoxColumn DateFines;
        private System.Windows.Forms.DataGridViewTextBoxColumn cName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Summa;
        private System.Windows.Forms.DataGridViewCheckBoxColumn cSelect;
        private System.Windows.Forms.CheckBox chbIsConfirmed;
    }
}