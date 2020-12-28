namespace dllJournalPlaneReport
{
    partial class frmViewPayment
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btPrint = new System.Windows.Forms.Button();
            this.btExit = new System.Windows.Forms.Button();
            this.dgvData = new System.Windows.Forms.DataGridView();
            this.cDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cTypePayDoc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cSumma = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cDatePay = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cSummPay = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cTypePay = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tbObject = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbAgreements = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbDate = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            this.SuspendLayout();
            // 
            // btPrint
            // 
            this.btPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btPrint.Image = global::dllJournalPlaneReport.Properties.Resources.klpq_2511;
            this.btPrint.Location = new System.Drawing.Point(1093, 588);
            this.btPrint.Name = "btPrint";
            this.btPrint.Size = new System.Drawing.Size(32, 32);
            this.btPrint.TabIndex = 14;
            this.btPrint.UseVisualStyleBackColor = true;
            this.btPrint.Click += new System.EventHandler(this.btPrint_Click);
            // 
            // btExit
            // 
            this.btExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btExit.Image = global::dllJournalPlaneReport.Properties.Resources.exit_8633;
            this.btExit.Location = new System.Drawing.Point(1131, 588);
            this.btExit.Name = "btExit";
            this.btExit.Size = new System.Drawing.Size(32, 32);
            this.btExit.TabIndex = 15;
            this.btExit.UseVisualStyleBackColor = true;
            this.btExit.Click += new System.EventHandler(this.btExit_Click);
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
            this.cDate,
            this.cTypePayDoc,
            this.cSumma,
            this.cDatePay,
            this.cSummPay,
            this.cTypePay});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvData.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgvData.Location = new System.Drawing.Point(12, 88);
            this.dgvData.MultiSelect = false;
            this.dgvData.Name = "dgvData";
            this.dgvData.ReadOnly = true;
            this.dgvData.RowHeadersVisible = false;
            this.dgvData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvData.Size = new System.Drawing.Size(1151, 490);
            this.dgvData.TabIndex = 20;
            // 
            // cDate
            // 
            this.cDate.DataPropertyName = "DateFines";
            this.cDate.HeaderText = "Дата";
            this.cDate.Name = "cDate";
            this.cDate.ReadOnly = true;
            // 
            // cTypePayDoc
            // 
            this.cTypePayDoc.DataPropertyName = "namePayment";
            this.cTypePayDoc.HeaderText = "Тип доп. оплаты";
            this.cTypePayDoc.Name = "cTypePayDoc";
            this.cTypePayDoc.ReadOnly = true;
            // 
            // cSumma
            // 
            this.cSumma.DataPropertyName = "summaFines";
            dataGridViewCellStyle2.Format = "N2";
            dataGridViewCellStyle2.NullValue = null;
            this.cSumma.DefaultCellStyle = dataGridViewCellStyle2;
            this.cSumma.HeaderText = "Сумма";
            this.cSumma.Name = "cSumma";
            this.cSumma.ReadOnly = true;
            // 
            // cDatePay
            // 
            this.cDatePay.DataPropertyName = "Date";
            this.cDatePay.HeaderText = "Дата оплаты";
            this.cDatePay.Name = "cDatePay";
            this.cDatePay.ReadOnly = true;
            // 
            // cSummPay
            // 
            this.cSummPay.DataPropertyName = "summaPayments";
            dataGridViewCellStyle3.Format = "N2";
            dataGridViewCellStyle3.NullValue = null;
            this.cSummPay.DefaultCellStyle = dataGridViewCellStyle3;
            this.cSummPay.HeaderText = "Сумма оплаты";
            this.cSummPay.Name = "cSummPay";
            this.cSummPay.ReadOnly = true;
            // 
            // cTypePay
            // 
            this.cTypePay.DataPropertyName = "typeCash";
            this.cTypePay.HeaderText = "Тип оплаты";
            this.cTypePay.Name = "cTypePay";
            this.cTypePay.ReadOnly = true;
            // 
            // tbObject
            // 
            this.tbObject.Location = new System.Drawing.Point(135, 62);
            this.tbObject.Name = "tbObject";
            this.tbObject.ReadOnly = true;
            this.tbObject.Size = new System.Drawing.Size(316, 20);
            this.tbObject.TabIndex = 21;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 66);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 13);
            this.label1.TabIndex = 22;
            this.label1.Text = "Объект аренды:";
            // 
            // tbAgreements
            // 
            this.tbAgreements.Location = new System.Drawing.Point(135, 12);
            this.tbAgreements.Name = "tbAgreements";
            this.tbAgreements.ReadOnly = true;
            this.tbAgreements.Size = new System.Drawing.Size(316, 20);
            this.tbAgreements.TabIndex = 21;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 13);
            this.label2.TabIndex = 22;
            this.label2.Text = "Договор:";
            // 
            // tbDate
            // 
            this.tbDate.Location = new System.Drawing.Point(135, 36);
            this.tbDate.Name = "tbDate";
            this.tbDate.ReadOnly = true;
            this.tbDate.Size = new System.Drawing.Size(316, 20);
            this.tbDate.TabIndex = 21;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 40);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(111, 13);
            this.label3.TabIndex = 22;
            this.label3.Text = "Период план-отчёта:";
            // 
            // frmViewPayment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1175, 632);
            this.ControlBox = false;
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbDate);
            this.Controls.Add(this.tbAgreements);
            this.Controls.Add(this.tbObject);
            this.Controls.Add(this.dgvData);
            this.Controls.Add(this.btPrint);
            this.Controls.Add(this.btExit);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmViewPayment";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Просмотр прочих платежей";
            this.Load += new System.EventHandler(this.frmViewPayment_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btPrint;
        private System.Windows.Forms.Button btExit;
        private System.Windows.Forms.DataGridView dgvData;
        private System.Windows.Forms.TextBox tbObject;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbAgreements;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbDate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridViewTextBoxColumn cDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn cTypePayDoc;
        private System.Windows.Forms.DataGridViewTextBoxColumn cSumma;
        private System.Windows.Forms.DataGridViewTextBoxColumn cDatePay;
        private System.Windows.Forms.DataGridViewTextBoxColumn cSummPay;
        private System.Windows.Forms.DataGridViewTextBoxColumn cTypePay;
    }
}