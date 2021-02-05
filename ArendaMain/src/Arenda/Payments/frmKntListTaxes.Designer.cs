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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmKntListTaxes));
            this.cmbPlaneDate = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btClose = new System.Windows.Forms.Button();
            this.btSelect = new System.Windows.Forms.Button();
            this.dgvData = new System.Windows.Forms.DataGridView();
            this.cObject = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cTenant = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cNumAgreements = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DateFines = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Summa = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cDesciption = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cSelect = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.picBoxIsConfirmed = new System.Windows.Forms.PictureBox();
            this.chbIsConfirmed = new System.Windows.Forms.CheckBox();
            this.btPrint = new System.Windows.Forms.Button();
            this.tbAgreements = new System.Windows.Forms.TextBox();
            this.tbTenant = new System.Windows.Forms.TextBox();
            this.cmbObject = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbTypeAddPayment = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxIsConfirmed)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbPlaneDate
            // 
            this.cmbPlaneDate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPlaneDate.FormattingEnabled = true;
            this.cmbPlaneDate.Location = new System.Drawing.Point(94, 12);
            this.cmbPlaneDate.Name = "cmbPlaneDate";
            this.cmbPlaneDate.Size = new System.Drawing.Size(186, 21);
            this.cmbPlaneDate.TabIndex = 84;
            this.cmbPlaneDate.SelectionChangeCommitted += new System.EventHandler(this.cmbPlaneDate_SelectionChangeCommitted);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 13);
            this.label2.TabIndex = 83;
            this.label2.Text = "План-отчет за:";
            // 
            // btClose
            // 
            this.btClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btClose.Image = global::Arenda.Properties.Resources.Log_Out_icon1;
            this.btClose.Location = new System.Drawing.Point(993, 412);
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
            this.btSelect.Location = new System.Drawing.Point(955, 412);
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
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvData.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.cObject,
            this.cTenant,
            this.cNumAgreements,
            this.DateFines,
            this.cName,
            this.Summa,
            this.cDesciption,
            this.cSelect});
            this.dgvData.Location = new System.Drawing.Point(10, 67);
            this.dgvData.MultiSelect = false;
            this.dgvData.Name = "dgvData";
            this.dgvData.RowHeadersVisible = false;
            this.dgvData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvData.Size = new System.Drawing.Size(1015, 337);
            this.dgvData.TabIndex = 86;
            this.dgvData.ColumnWidthChanged += new System.Windows.Forms.DataGridViewColumnEventHandler(this.dgvData_ColumnWidthChanged);
            this.dgvData.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgvData_RowPostPaint);
            this.dgvData.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.dgvData_RowPrePaint);
            // 
            // cObject
            // 
            this.cObject.DataPropertyName = "nameObject";
            this.cObject.HeaderText = "Объект";
            this.cObject.Name = "cObject";
            this.cObject.ReadOnly = true;
            // 
            // cTenant
            // 
            this.cTenant.DataPropertyName = "nameTenant";
            this.cTenant.HeaderText = "Арендатор";
            this.cTenant.Name = "cTenant";
            this.cTenant.ReadOnly = true;
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
            // cDesciption
            // 
            this.cDesciption.DataPropertyName = "Comment";
            this.cDesciption.HeaderText = "Примечание";
            this.cDesciption.Name = "cDesciption";
            this.cDesciption.ReadOnly = true;
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
            // picBoxIsConfirmed
            // 
            this.picBoxIsConfirmed.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.picBoxIsConfirmed.BackColor = System.Drawing.Color.Coral;
            this.picBoxIsConfirmed.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picBoxIsConfirmed.Location = new System.Drawing.Point(18, 427);
            this.picBoxIsConfirmed.Name = "picBoxIsConfirmed";
            this.picBoxIsConfirmed.Size = new System.Drawing.Size(16, 16);
            this.picBoxIsConfirmed.TabIndex = 88;
            this.picBoxIsConfirmed.TabStop = false;
            // 
            // chbIsConfirmed
            // 
            this.chbIsConfirmed.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chbIsConfirmed.AutoSize = true;
            this.chbIsConfirmed.Location = new System.Drawing.Point(40, 427);
            this.chbIsConfirmed.Name = "chbIsConfirmed";
            this.chbIsConfirmed.Size = new System.Drawing.Size(115, 17);
            this.chbIsConfirmed.TabIndex = 89;
            this.chbIsConfirmed.Text = "Подтверждённые";
            this.chbIsConfirmed.UseVisualStyleBackColor = true;
            this.chbIsConfirmed.Click += new System.EventHandler(this.chbIsConfirmed_Click);
            // 
            // btPrint
            // 
            this.btPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btPrint.Image = ((System.Drawing.Image)(resources.GetObject("btPrint.Image")));
            this.btPrint.Location = new System.Drawing.Point(884, 412);
            this.btPrint.Name = "btPrint";
            this.btPrint.Size = new System.Drawing.Size(32, 32);
            this.btPrint.TabIndex = 90;
            this.btPrint.UseVisualStyleBackColor = true;
            this.btPrint.Click += new System.EventHandler(this.btPrint_Click);
            // 
            // tbAgreements
            // 
            this.tbAgreements.Location = new System.Drawing.Point(126, 41);
            this.tbAgreements.MaxLength = 150;
            this.tbAgreements.Name = "tbAgreements";
            this.tbAgreements.Size = new System.Drawing.Size(100, 20);
            this.tbAgreements.TabIndex = 92;
            this.tbAgreements.TextChanged += new System.EventHandler(this.tbTenant_TextChanged);
            // 
            // tbTenant
            // 
            this.tbTenant.Location = new System.Drawing.Point(12, 41);
            this.tbTenant.MaxLength = 13;
            this.tbTenant.Name = "tbTenant";
            this.tbTenant.Size = new System.Drawing.Size(100, 20);
            this.tbTenant.TabIndex = 91;
            this.tbTenant.TextChanged += new System.EventHandler(this.tbTenant_TextChanged);
            // 
            // cmbObject
            // 
            this.cmbObject.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbObject.FormattingEnabled = true;
            this.cmbObject.Location = new System.Drawing.Point(368, 12);
            this.cmbObject.Name = "cmbObject";
            this.cmbObject.Size = new System.Drawing.Size(186, 21);
            this.cmbObject.TabIndex = 94;
            this.cmbObject.SelectionChangeCommitted += new System.EventHandler(this.cmbObject_SelectionChangeCommitted);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(314, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 93;
            this.label1.Text = "Объект:";
            // 
            // cmbTypeAddPayment
            // 
            this.cmbTypeAddPayment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTypeAddPayment.FormattingEnabled = true;
            this.cmbTypeAddPayment.Location = new System.Drawing.Point(668, 13);
            this.cmbTypeAddPayment.Name = "cmbTypeAddPayment";
            this.cmbTypeAddPayment.Size = new System.Drawing.Size(186, 21);
            this.cmbTypeAddPayment.TabIndex = 96;
            this.cmbTypeAddPayment.SelectionChangeCommitted += new System.EventHandler(this.cmbObject_SelectionChangeCommitted);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(569, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(93, 13);
            this.label3.TabIndex = 95;
            this.label3.Text = "Тип доп. оплаты:";
            // 
            // frmKntListTaxes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1037, 453);
            this.ControlBox = false;
            this.Controls.Add(this.cmbTypeAddPayment);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmbObject);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbAgreements);
            this.Controls.Add(this.tbTenant);
            this.Controls.Add(this.btPrint);
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
        private System.Windows.Forms.CheckBox chbIsConfirmed;
        private System.Windows.Forms.DataGridViewTextBoxColumn cObject;
        private System.Windows.Forms.DataGridViewTextBoxColumn cTenant;
        private System.Windows.Forms.DataGridViewTextBoxColumn cNumAgreements;
        private System.Windows.Forms.DataGridViewTextBoxColumn DateFines;
        private System.Windows.Forms.DataGridViewTextBoxColumn cName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Summa;
        private System.Windows.Forms.DataGridViewTextBoxColumn cDesciption;
        private System.Windows.Forms.DataGridViewCheckBoxColumn cSelect;
        private System.Windows.Forms.Button btPrint;
        private System.Windows.Forms.TextBox tbAgreements;
        private System.Windows.Forms.TextBox tbTenant;
        private System.Windows.Forms.ComboBox cmbObject;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbTypeAddPayment;
        private System.Windows.Forms.Label label3;
    }
}