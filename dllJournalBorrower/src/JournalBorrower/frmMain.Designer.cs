namespace JournalBorrower
{
    partial class frmMain
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.cmbObject = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbTenant = new System.Windows.Forms.TextBox();
            this.tbAgreements = new System.Windows.Forms.TextBox();
            this.tbPlace = new System.Windows.Forms.TextBox();
            this.btUpdate = new System.Windows.Forms.Button();
            this.btPrint = new System.Windows.Forms.Button();
            this.btExit = new System.Windows.Forms.Button();
            this.dgvData = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbPayDopDoc = new System.Windows.Forms.RadioButton();
            this.rbPayDoc = new System.Windows.Forms.RadioButton();
            this.nameTenant = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cAgreements = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cObject = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cPlace = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cSumMeter = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cSumDoc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cSumPay = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cSumItogSum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cSumOwe = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cPrcOwe = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cDateCloseSection = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmbObject
            // 
            this.cmbObject.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbObject.FormattingEnabled = true;
            this.cmbObject.Location = new System.Drawing.Point(100, 12);
            this.cmbObject.Name = "cmbObject";
            this.cmbObject.Size = new System.Drawing.Size(230, 21);
            this.cmbObject.TabIndex = 7;
            this.cmbObject.SelectionChangeCommitted += new System.EventHandler(this.cmbObject_SelectionChangeCommitted);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(86, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Объект аренды";
            // 
            // tbTenant
            // 
            this.tbTenant.Location = new System.Drawing.Point(12, 65);
            this.tbTenant.Name = "tbTenant";
            this.tbTenant.Size = new System.Drawing.Size(100, 20);
            this.tbTenant.TabIndex = 21;
            this.tbTenant.TextChanged += new System.EventHandler(this.tbTenant_TextChanged);
            // 
            // tbAgreements
            // 
            this.tbAgreements.Location = new System.Drawing.Point(118, 65);
            this.tbAgreements.Name = "tbAgreements";
            this.tbAgreements.Size = new System.Drawing.Size(100, 20);
            this.tbAgreements.TabIndex = 21;
            this.tbAgreements.TextChanged += new System.EventHandler(this.tbTenant_TextChanged);
            // 
            // tbPlace
            // 
            this.tbPlace.Location = new System.Drawing.Point(224, 65);
            this.tbPlace.Name = "tbPlace";
            this.tbPlace.Size = new System.Drawing.Size(100, 20);
            this.tbPlace.TabIndex = 21;
            this.tbPlace.TextChanged += new System.EventHandler(this.tbTenant_TextChanged);
            // 
            // btUpdate
            // 
            this.btUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btUpdate.Image = global::JournalBorrower.Properties.Resources.reload_8055;
            this.btUpdate.Location = new System.Drawing.Point(1005, 11);
            this.btUpdate.Name = "btUpdate";
            this.btUpdate.Size = new System.Drawing.Size(48, 48);
            this.btUpdate.TabIndex = 22;
            this.btUpdate.UseVisualStyleBackColor = true;
            this.btUpdate.Click += new System.EventHandler(this.btUpdate_Click);
            // 
            // btPrint
            // 
            this.btPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btPrint.Image = global::JournalBorrower.Properties.Resources.klpq_2511;
            this.btPrint.Location = new System.Drawing.Point(983, 595);
            this.btPrint.Name = "btPrint";
            this.btPrint.Size = new System.Drawing.Size(32, 32);
            this.btPrint.TabIndex = 23;
            this.btPrint.UseVisualStyleBackColor = true;
            this.btPrint.Click += new System.EventHandler(this.btPrint_Click);
            // 
            // btExit
            // 
            this.btExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btExit.Image = global::JournalBorrower.Properties.Resources.exit_8633;
            this.btExit.Location = new System.Drawing.Point(1021, 595);
            this.btExit.Name = "btExit";
            this.btExit.Size = new System.Drawing.Size(32, 32);
            this.btExit.TabIndex = 24;
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
            this.nameTenant,
            this.cAgreements,
            this.cObject,
            this.cPlace,
            this.cSumMeter,
            this.cSumDoc,
            this.cSumPay,
            this.cSumItogSum,
            this.cSumOwe,
            this.cPrcOwe,
            this.cDateCloseSection});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvData.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvData.Location = new System.Drawing.Point(12, 91);
            this.dgvData.MultiSelect = false;
            this.dgvData.Name = "dgvData";
            this.dgvData.RowHeadersVisible = false;
            this.dgvData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvData.Size = new System.Drawing.Size(1041, 498);
            this.dgvData.TabIndex = 25;
            this.dgvData.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvData_CellFormatting);
            this.dgvData.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dgvData_CellPainting);
            this.dgvData.ColumnWidthChanged += new System.Windows.Forms.DataGridViewColumnEventHandler(this.dgvData_ColumnWidthChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbPayDopDoc);
            this.groupBox1.Controls.Add(this.rbPayDoc);
            this.groupBox1.Location = new System.Drawing.Point(336, 1);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(259, 58);
            this.groupBox1.TabIndex = 26;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Тип долгов";
            // 
            // rbPayDopDoc
            // 
            this.rbPayDopDoc.AutoSize = true;
            this.rbPayDopDoc.Location = new System.Drawing.Point(6, 37);
            this.rbPayDopDoc.Name = "rbPayDopDoc";
            this.rbPayDopDoc.Size = new System.Drawing.Size(211, 17);
            this.rbPayDopDoc.TabIndex = 0;
            this.rbPayDopDoc.Text = "оплата по дополнительным оплатам\r\n";
            this.rbPayDopDoc.UseVisualStyleBackColor = true;
            this.rbPayDopDoc.Click += new System.EventHandler(this.rbPayDoc_Click);
            // 
            // rbPayDoc
            // 
            this.rbPayDoc.AutoSize = true;
            this.rbPayDoc.Checked = true;
            this.rbPayDoc.Location = new System.Drawing.Point(6, 15);
            this.rbPayDoc.Name = "rbPayDoc";
            this.rbPayDoc.Size = new System.Drawing.Size(133, 17);
            this.rbPayDoc.TabIndex = 0;
            this.rbPayDoc.TabStop = true;
            this.rbPayDoc.Text = "оплата по договорам";
            this.rbPayDoc.UseVisualStyleBackColor = true;
            this.rbPayDoc.Click += new System.EventHandler(this.rbPayDoc_Click);
            // 
            // nameTenant
            // 
            this.nameTenant.DataPropertyName = "nameTenant";
            this.nameTenant.HeaderText = "Арендатор";
            this.nameTenant.Name = "nameTenant";
            this.nameTenant.ReadOnly = true;
            // 
            // cAgreements
            // 
            this.cAgreements.DataPropertyName = "Agreement";
            this.cAgreements.HeaderText = "Номер договора";
            this.cAgreements.Name = "cAgreements";
            this.cAgreements.ReadOnly = true;
            this.cAgreements.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // cObject
            // 
            this.cObject.DataPropertyName = "nameObjectLease";
            this.cObject.HeaderText = "Объект аренды";
            this.cObject.Name = "cObject";
            this.cObject.ReadOnly = true;
            this.cObject.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // cPlace
            // 
            this.cPlace.DataPropertyName = "namePlace";
            this.cPlace.HeaderText = "Местоположение места аренды";
            this.cPlace.Name = "cPlace";
            this.cPlace.ReadOnly = true;
            this.cPlace.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // cSumMeter
            // 
            this.cSumMeter.DataPropertyName = "Cost_of_Meter";
            this.cSumMeter.HeaderText = "Стоимость 1м2";
            this.cSumMeter.Name = "cSumMeter";
            this.cSumMeter.ReadOnly = true;
            this.cSumMeter.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // cSumDoc
            // 
            this.cSumDoc.DataPropertyName = "Total_Sum";
            this.cSumDoc.HeaderText = "Сумма по договору";
            this.cSumDoc.Name = "cSumDoc";
            this.cSumDoc.ReadOnly = true;
            this.cSumDoc.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // cSumPay
            // 
            this.cSumPay.HeaderText = "Сумма к оплате";
            this.cSumPay.Name = "cSumPay";
            this.cSumPay.ReadOnly = true;
            this.cSumPay.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // cSumItogSum
            // 
            this.cSumItogSum.HeaderText = "Сумма оплаты";
            this.cSumItogSum.Name = "cSumItogSum";
            this.cSumItogSum.ReadOnly = true;
            this.cSumItogSum.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // cSumOwe
            // 
            this.cSumOwe.HeaderText = "Сумма долга";
            this.cSumOwe.Name = "cSumOwe";
            this.cSumOwe.ReadOnly = true;
            this.cSumOwe.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // cPrcOwe
            // 
            this.cPrcOwe.HeaderText = "% долга";
            this.cPrcOwe.Name = "cPrcOwe";
            this.cPrcOwe.ReadOnly = true;
            this.cPrcOwe.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // cDateCloseSection
            // 
            this.cDateCloseSection.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.cDateCloseSection.DataPropertyName = "DateSeal";
            this.cDateCloseSection.HeaderText = "Дата опечат. секции";
            this.cDateCloseSection.MinimumWidth = 80;
            this.cDateCloseSection.Name = "cDateCloseSection";
            this.cDateCloseSection.ReadOnly = true;
            this.cDateCloseSection.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.cDateCloseSection.Width = 80;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1065, 639);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dgvData);
            this.Controls.Add(this.btPrint);
            this.Controls.Add(this.btExit);
            this.Controls.Add(this.btUpdate);
            this.Controls.Add(this.tbPlace);
            this.Controls.Add(this.tbAgreements);
            this.Controls.Add(this.tbTenant);
            this.Controls.Add(this.cmbObject);
            this.Controls.Add(this.label3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MinimizeBox = false;
            this.Name = "frmMain";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Журнал должников";
            this.Load += new System.EventHandler(this.frmMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbObject;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbTenant;
        private System.Windows.Forms.TextBox tbAgreements;
        private System.Windows.Forms.TextBox tbPlace;
        private System.Windows.Forms.Button btUpdate;
        private System.Windows.Forms.Button btPrint;
        private System.Windows.Forms.Button btExit;
        private System.Windows.Forms.DataGridView dgvData;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbPayDopDoc;
        private System.Windows.Forms.RadioButton rbPayDoc;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameTenant;
        private System.Windows.Forms.DataGridViewTextBoxColumn cAgreements;
        private System.Windows.Forms.DataGridViewTextBoxColumn cObject;
        private System.Windows.Forms.DataGridViewTextBoxColumn cPlace;
        private System.Windows.Forms.DataGridViewTextBoxColumn cSumMeter;
        private System.Windows.Forms.DataGridViewTextBoxColumn cSumDoc;
        private System.Windows.Forms.DataGridViewTextBoxColumn cSumPay;
        private System.Windows.Forms.DataGridViewTextBoxColumn cSumItogSum;
        private System.Windows.Forms.DataGridViewTextBoxColumn cSumOwe;
        private System.Windows.Forms.DataGridViewTextBoxColumn cPrcOwe;
        private System.Windows.Forms.DataGridViewTextBoxColumn cDateCloseSection;
    }
}

