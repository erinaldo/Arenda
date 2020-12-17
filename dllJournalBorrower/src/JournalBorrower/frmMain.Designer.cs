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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.cmbObject = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbTenant = new System.Windows.Forms.TextBox();
            this.tbAgreements = new System.Windows.Forms.TextBox();
            this.tbPlace = new System.Windows.Forms.TextBox();
            this.dgvData = new System.Windows.Forms.DataGridView();
            this.nameTenant = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cAgreements = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cObject = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cPlace = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cNameBuild = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cFloor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cSection = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cSumMeter = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cSumDoc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cSumPay = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cSumItogSum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cSumOwe = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cPrcOwe = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cDateCloseSection = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbPayDopDoc = new System.Windows.Forms.RadioButton();
            this.rbPayDoc = new System.Windows.Forms.RadioButton();
            this.btPrint = new System.Windows.Forms.Button();
            this.btExit = new System.Windows.Forms.Button();
            this.btUpdate = new System.Windows.Forms.Button();
            this.cmbTypeDoc = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbBuild = new System.Windows.Forms.TextBox();
            this.tbFloor = new System.Windows.Forms.TextBox();
            this.tbSection = new System.Windows.Forms.TextBox();
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
            this.tbPlace.Visible = false;
            this.tbPlace.TextChanged += new System.EventHandler(this.tbTenant_TextChanged);
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
            this.cNameBuild,
            this.cFloor,
            this.cSection,
            this.cSumMeter,
            this.cSumDoc,
            this.cSumPay,
            this.cSumItogSum,
            this.cSumOwe,
            this.cPrcOwe,
            this.cDateCloseSection});
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvData.DefaultCellStyle = dataGridViewCellStyle6;
            this.dgvData.Location = new System.Drawing.Point(12, 91);
            this.dgvData.MultiSelect = false;
            this.dgvData.Name = "dgvData";
            this.dgvData.RowHeadersVisible = false;
            this.dgvData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvData.Size = new System.Drawing.Size(1041, 498);
            this.dgvData.TabIndex = 25;
            this.dgvData.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvData_CellMouseClick);
            this.dgvData.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvData_CellMouseDoubleClick);
            this.dgvData.ColumnWidthChanged += new System.Windows.Forms.DataGridViewColumnEventHandler(this.dgvData_ColumnWidthChanged);
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
            // 
            // cObject
            // 
            this.cObject.DataPropertyName = "nameObjectLease";
            this.cObject.HeaderText = "Объект аренды";
            this.cObject.Name = "cObject";
            this.cObject.ReadOnly = true;
            // 
            // cPlace
            // 
            this.cPlace.DataPropertyName = "namePlace";
            this.cPlace.HeaderText = "Местоположение места аренды";
            this.cPlace.Name = "cPlace";
            this.cPlace.ReadOnly = true;
            this.cPlace.Visible = false;
            // 
            // cNameBuild
            // 
            this.cNameBuild.DataPropertyName = "buildName";
            this.cNameBuild.HeaderText = "Здание";
            this.cNameBuild.Name = "cNameBuild";
            this.cNameBuild.ReadOnly = true;
            // 
            // cFloor
            // 
            this.cFloor.DataPropertyName = "floorName";
            this.cFloor.HeaderText = "Этаж";
            this.cFloor.Name = "cFloor";
            this.cFloor.ReadOnly = true;
            // 
            // cSection
            // 
            this.cSection.DataPropertyName = "sectionName";
            this.cSection.HeaderText = "Секция";
            this.cSection.Name = "cSection";
            this.cSection.ReadOnly = true;
            // 
            // cSumMeter
            // 
            this.cSumMeter.DataPropertyName = "Cost_of_Meter";
            this.cSumMeter.HeaderText = "Стоимость 1м2";
            this.cSumMeter.Name = "cSumMeter";
            this.cSumMeter.ReadOnly = true;
            // 
            // cSumDoc
            // 
            this.cSumDoc.DataPropertyName = "Total_Sum";
            dataGridViewCellStyle2.Format = "N2";
            dataGridViewCellStyle2.NullValue = null;
            this.cSumDoc.DefaultCellStyle = dataGridViewCellStyle2;
            this.cSumDoc.HeaderText = "Сумма по договору";
            this.cSumDoc.Name = "cSumDoc";
            this.cSumDoc.ReadOnly = true;
            // 
            // cSumPay
            // 
            dataGridViewCellStyle3.Format = "N2";
            dataGridViewCellStyle3.NullValue = null;
            this.cSumPay.DefaultCellStyle = dataGridViewCellStyle3;
            this.cSumPay.HeaderText = "Сумма к оплате";
            this.cSumPay.Name = "cSumPay";
            this.cSumPay.ReadOnly = true;
            // 
            // cSumItogSum
            // 
            dataGridViewCellStyle4.Format = "N2";
            dataGridViewCellStyle4.NullValue = null;
            this.cSumItogSum.DefaultCellStyle = dataGridViewCellStyle4;
            this.cSumItogSum.HeaderText = "Сумма оплаты";
            this.cSumItogSum.Name = "cSumItogSum";
            this.cSumItogSum.ReadOnly = true;
            // 
            // cSumOwe
            // 
            dataGridViewCellStyle5.Format = "N2";
            dataGridViewCellStyle5.NullValue = null;
            this.cSumOwe.DefaultCellStyle = dataGridViewCellStyle5;
            this.cSumOwe.HeaderText = "Сумма долга";
            this.cSumOwe.Name = "cSumOwe";
            this.cSumOwe.ReadOnly = true;
            // 
            // cPrcOwe
            // 
            this.cPrcOwe.HeaderText = "% долга";
            this.cPrcOwe.Name = "cPrcOwe";
            this.cPrcOwe.ReadOnly = true;
            // 
            // cDateCloseSection
            // 
            this.cDateCloseSection.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.cDateCloseSection.DataPropertyName = "DateSeal";
            this.cDateCloseSection.HeaderText = "Дата опечат. секции";
            this.cDateCloseSection.MinimumWidth = 80;
            this.cDateCloseSection.Name = "cDateCloseSection";
            this.cDateCloseSection.ReadOnly = true;
            this.cDateCloseSection.Width = 80;
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
            // cmbTypeDoc
            // 
            this.cmbTypeDoc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTypeDoc.FormattingEnabled = true;
            this.cmbTypeDoc.Location = new System.Drawing.Point(100, 39);
            this.cmbTypeDoc.Name = "cmbTypeDoc";
            this.cmbTypeDoc.Size = new System.Drawing.Size(230, 21);
            this.cmbTypeDoc.TabIndex = 28;
            this.cmbTypeDoc.SelectionChangeCommitted += new System.EventHandler(this.cmbObject_SelectionChangeCommitted);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 13);
            this.label1.TabIndex = 27;
            this.label1.Text = "Тип договора";
            // 
            // tbBuild
            // 
            this.tbBuild.Location = new System.Drawing.Point(375, 65);
            this.tbBuild.Name = "tbBuild";
            this.tbBuild.Size = new System.Drawing.Size(100, 20);
            this.tbBuild.TabIndex = 29;
            this.tbBuild.TextChanged += new System.EventHandler(this.tbTenant_TextChanged);
            // 
            // tbFloor
            // 
            this.tbFloor.Location = new System.Drawing.Point(481, 65);
            this.tbFloor.Name = "tbFloor";
            this.tbFloor.Size = new System.Drawing.Size(100, 20);
            this.tbFloor.TabIndex = 29;
            this.tbFloor.TextChanged += new System.EventHandler(this.tbTenant_TextChanged);
            // 
            // tbSection
            // 
            this.tbSection.Location = new System.Drawing.Point(587, 65);
            this.tbSection.Name = "tbSection";
            this.tbSection.Size = new System.Drawing.Size(100, 20);
            this.tbSection.TabIndex = 29;
            this.tbSection.TextChanged += new System.EventHandler(this.tbTenant_TextChanged);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1065, 639);
            this.Controls.Add(this.tbSection);
            this.Controls.Add(this.tbFloor);
            this.Controls.Add(this.tbBuild);
            this.Controls.Add(this.cmbTypeDoc);
            this.Controls.Add(this.label1);
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
            this.ShowInTaskbar = false;
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
        private System.Windows.Forms.ComboBox cmbTypeDoc;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameTenant;
        private System.Windows.Forms.DataGridViewTextBoxColumn cAgreements;
        private System.Windows.Forms.DataGridViewTextBoxColumn cObject;
        private System.Windows.Forms.DataGridViewTextBoxColumn cPlace;
        private System.Windows.Forms.DataGridViewTextBoxColumn cNameBuild;
        private System.Windows.Forms.DataGridViewTextBoxColumn cFloor;
        private System.Windows.Forms.DataGridViewTextBoxColumn cSection;
        private System.Windows.Forms.DataGridViewTextBoxColumn cSumMeter;
        private System.Windows.Forms.DataGridViewTextBoxColumn cSumDoc;
        private System.Windows.Forms.DataGridViewTextBoxColumn cSumPay;
        private System.Windows.Forms.DataGridViewTextBoxColumn cSumItogSum;
        private System.Windows.Forms.DataGridViewTextBoxColumn cSumOwe;
        private System.Windows.Forms.DataGridViewTextBoxColumn cPrcOwe;
        private System.Windows.Forms.DataGridViewTextBoxColumn cDateCloseSection;
        private System.Windows.Forms.TextBox tbBuild;
        private System.Windows.Forms.TextBox tbFloor;
        private System.Windows.Forms.TextBox tbSection;
    }
}

