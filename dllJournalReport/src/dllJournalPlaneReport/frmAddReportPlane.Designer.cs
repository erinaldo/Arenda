namespace dllJournalPlaneReport
{
    partial class frmAddReportPlane
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
            this.label1 = new System.Windows.Forms.Label();
            this.dtpStart = new System.Windows.Forms.DateTimePicker();
            this.tbLandLord = new System.Windows.Forms.TextBox();
            this.tbAgreements = new System.Windows.Forms.TextBox();
            this.tbTenant = new System.Windows.Forms.TextBox();
            this.dgvData = new System.Windows.Forms.DataGridView();
            this.chkHideDocColumnts = new System.Windows.Forms.CheckBox();
            this.cmbTypeContract = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btSave = new System.Windows.Forms.Button();
            this.btCalcData = new System.Windows.Forms.Button();
            this.btPrint = new System.Windows.Forms.Button();
            this.btExit = new System.Windows.Forms.Button();
            this.btAcceptD = new System.Windows.Forms.Button();
            this.btClear = new System.Windows.Forms.Button();
            this.tbSumPlane = new System.Windows.Forms.TextBox();
            this.lSumPlane = new System.Windows.Forms.Label();
            this.nameLandLord = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nameTenant = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TypeContract = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cAgreements = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.timeLimit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cBuild = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cFloor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cSection = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cSquart = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Cost_of_Meter = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cSumDog = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cDiscount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cPlane = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbObject
            // 
            this.cmbObject.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbObject.FormattingEnabled = true;
            this.cmbObject.Location = new System.Drawing.Point(267, 12);
            this.cmbObject.Name = "cmbObject";
            this.cmbObject.Size = new System.Drawing.Size(230, 21);
            this.cmbObject.TabIndex = 11;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(175, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(86, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Объект аренды";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 25;
            this.label1.Text = "Период с";
            // 
            // dtpStart
            // 
            this.dtpStart.CustomFormat = "MM.yyyy";
            this.dtpStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpStart.Location = new System.Drawing.Point(79, 12);
            this.dtpStart.Name = "dtpStart";
            this.dtpStart.Size = new System.Drawing.Size(67, 20);
            this.dtpStart.TabIndex = 24;
            // 
            // tbLandLord
            // 
            this.tbLandLord.Location = new System.Drawing.Point(224, 54);
            this.tbLandLord.Name = "tbLandLord";
            this.tbLandLord.Size = new System.Drawing.Size(100, 20);
            this.tbLandLord.TabIndex = 27;
            this.tbLandLord.TextChanged += new System.EventHandler(this.tbTenant_TextChanged);
            // 
            // tbAgreements
            // 
            this.tbAgreements.Location = new System.Drawing.Point(118, 54);
            this.tbAgreements.Name = "tbAgreements";
            this.tbAgreements.Size = new System.Drawing.Size(100, 20);
            this.tbAgreements.TabIndex = 28;
            this.tbAgreements.TextChanged += new System.EventHandler(this.tbTenant_TextChanged);
            // 
            // tbTenant
            // 
            this.tbTenant.Location = new System.Drawing.Point(12, 54);
            this.tbTenant.Name = "tbTenant";
            this.tbTenant.Size = new System.Drawing.Size(100, 20);
            this.tbTenant.TabIndex = 29;
            this.tbTenant.TextChanged += new System.EventHandler(this.tbTenant_TextChanged);
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
            this.nameLandLord,
            this.nameTenant,
            this.TypeContract,
            this.cAgreements,
            this.timeLimit,
            this.cBuild,
            this.cFloor,
            this.cSection,
            this.cSquart,
            this.Cost_of_Meter,
            this.cSumDog,
            this.cDiscount,
            this.cPlane});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvData.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvData.Location = new System.Drawing.Point(12, 80);
            this.dgvData.MultiSelect = false;
            this.dgvData.Name = "dgvData";
            this.dgvData.RowHeadersVisible = false;
            this.dgvData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvData.Size = new System.Drawing.Size(1227, 458);
            this.dgvData.TabIndex = 30;
            this.dgvData.ColumnWidthChanged += new System.Windows.Forms.DataGridViewColumnEventHandler(this.dgvData_ColumnWidthChanged);
            // 
            // chkHideDocColumnts
            // 
            this.chkHideDocColumnts.AutoSize = true;
            this.chkHideDocColumnts.Location = new System.Drawing.Point(12, 551);
            this.chkHideDocColumnts.Name = "chkHideDocColumnts";
            this.chkHideDocColumnts.Size = new System.Drawing.Size(174, 17);
            this.chkHideDocColumnts.TabIndex = 31;
            this.chkHideDocColumnts.Text = "- скрыть данные по договору";
            this.chkHideDocColumnts.UseVisualStyleBackColor = true;
            this.chkHideDocColumnts.Click += new System.EventHandler(this.chkHideDocColumnts_Click);
            // 
            // cmbTypeContract
            // 
            this.cmbTypeContract.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTypeContract.FormattingEnabled = true;
            this.cmbTypeContract.Location = new System.Drawing.Point(599, 11);
            this.cmbTypeContract.Name = "cmbTypeContract";
            this.cmbTypeContract.Size = new System.Drawing.Size(230, 21);
            this.cmbTypeContract.TabIndex = 33;
            this.cmbTypeContract.SelectionChangeCommitted += new System.EventHandler(this.cmbTypeContract_SelectionChangeCommitted);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(517, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 13);
            this.label2.TabIndex = 32;
            this.label2.Text = "Тип договора";
            // 
            // btSave
            // 
            this.btSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btSave.Enabled = false;
            this.btSave.Image = global::dllJournalPlaneReport.Properties.Resources.Save;
            this.btSave.Location = new System.Drawing.Point(1169, 587);
            this.btSave.Name = "btSave";
            this.btSave.Size = new System.Drawing.Size(32, 32);
            this.btSave.TabIndex = 34;
            this.btSave.UseVisualStyleBackColor = true;
            this.btSave.Click += new System.EventHandler(this.btSave_Click);
            // 
            // btCalcData
            // 
            this.btCalcData.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btCalcData.Image = global::dllJournalPlaneReport.Properties.Resources.calculator;
            this.btCalcData.Location = new System.Drawing.Point(1143, 11);
            this.btCalcData.Name = "btCalcData";
            this.btCalcData.Size = new System.Drawing.Size(45, 45);
            this.btCalcData.TabIndex = 26;
            this.btCalcData.UseVisualStyleBackColor = true;
            this.btCalcData.Click += new System.EventHandler(this.btCalcData_Click);
            // 
            // btPrint
            // 
            this.btPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btPrint.Enabled = false;
            this.btPrint.Image = global::dllJournalPlaneReport.Properties.Resources.klpq_2511;
            this.btPrint.Location = new System.Drawing.Point(1131, 587);
            this.btPrint.Name = "btPrint";
            this.btPrint.Size = new System.Drawing.Size(32, 32);
            this.btPrint.TabIndex = 21;
            this.btPrint.UseVisualStyleBackColor = true;
            this.btPrint.Click += new System.EventHandler(this.btPrint_Click);
            // 
            // btExit
            // 
            this.btExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btExit.Image = global::dllJournalPlaneReport.Properties.Resources.exit_8633;
            this.btExit.Location = new System.Drawing.Point(1207, 587);
            this.btExit.Name = "btExit";
            this.btExit.Size = new System.Drawing.Size(32, 32);
            this.btExit.TabIndex = 22;
            this.btExit.UseVisualStyleBackColor = true;
            this.btExit.Click += new System.EventHandler(this.btExit_Click);
            // 
            // btAcceptD
            // 
            this.btAcceptD.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btAcceptD.Image = global::dllJournalPlaneReport.Properties.Resources.like;
            this.btAcceptD.Location = new System.Drawing.Point(1003, 587);
            this.btAcceptD.Name = "btAcceptD";
            this.btAcceptD.Size = new System.Drawing.Size(32, 32);
            this.btAcceptD.TabIndex = 23;
            this.btAcceptD.UseVisualStyleBackColor = true;
            this.btAcceptD.Click += new System.EventHandler(this.btAcceptD_Click);
            // 
            // btClear
            // 
            this.btClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btClear.Image = global::dllJournalPlaneReport.Properties.Resources.Trash;
            this.btClear.Location = new System.Drawing.Point(1194, 11);
            this.btClear.Name = "btClear";
            this.btClear.Size = new System.Drawing.Size(45, 45);
            this.btClear.TabIndex = 35;
            this.btClear.UseVisualStyleBackColor = true;
            this.btClear.Click += new System.EventHandler(this.btClear_Click);
            // 
            // tbSumPlane
            // 
            this.tbSumPlane.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.tbSumPlane.Location = new System.Drawing.Point(1140, 547);
            this.tbSumPlane.Name = "tbSumPlane";
            this.tbSumPlane.ReadOnly = true;
            this.tbSumPlane.Size = new System.Drawing.Size(100, 20);
            this.tbSumPlane.TabIndex = 36;
            this.tbSumPlane.Text = "0,00";
            this.tbSumPlane.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lSumPlane
            // 
            this.lSumPlane.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lSumPlane.AutoSize = true;
            this.lSumPlane.Location = new System.Drawing.Point(1100, 551);
            this.lSumPlane.Name = "lSumPlane";
            this.lSumPlane.Size = new System.Drawing.Size(37, 13);
            this.lSumPlane.TabIndex = 37;
            this.lSumPlane.Text = "Итого";
            // 
            // nameLandLord
            // 
            this.nameLandLord.DataPropertyName = "nameLandLord";
            this.nameLandLord.HeaderText = "Арендодатель";
            this.nameLandLord.Name = "nameLandLord";
            this.nameLandLord.ReadOnly = true;
            // 
            // nameTenant
            // 
            this.nameTenant.DataPropertyName = "nameTenant";
            this.nameTenant.HeaderText = "Арендатор";
            this.nameTenant.Name = "nameTenant";
            this.nameTenant.ReadOnly = true;
            // 
            // TypeContract
            // 
            this.TypeContract.DataPropertyName = "TypeContract";
            this.TypeContract.HeaderText = "Тип договора";
            this.TypeContract.Name = "TypeContract";
            this.TypeContract.ReadOnly = true;
            // 
            // cAgreements
            // 
            this.cAgreements.DataPropertyName = "Agreement";
            this.cAgreements.HeaderText = "Номер договора";
            this.cAgreements.Name = "cAgreements";
            this.cAgreements.ReadOnly = true;
            this.cAgreements.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // timeLimit
            // 
            this.timeLimit.DataPropertyName = "timeLimit";
            this.timeLimit.HeaderText = "Срок действия";
            this.timeLimit.Name = "timeLimit";
            this.timeLimit.ReadOnly = true;
            // 
            // cBuild
            // 
            this.cBuild.DataPropertyName = "Build";
            this.cBuild.HeaderText = "Здание";
            this.cBuild.Name = "cBuild";
            this.cBuild.ReadOnly = true;
            // 
            // cFloor
            // 
            this.cFloor.DataPropertyName = "Floor";
            this.cFloor.HeaderText = "Этаж";
            this.cFloor.Name = "cFloor";
            this.cFloor.ReadOnly = true;
            // 
            // cSection
            // 
            this.cSection.DataPropertyName = "namePlace";
            this.cSection.HeaderText = "№ секции";
            this.cSection.Name = "cSection";
            this.cSection.ReadOnly = true;
            // 
            // cSquart
            // 
            this.cSquart.DataPropertyName = "Total_Area";
            this.cSquart.HeaderText = "Площадь м2";
            this.cSquart.Name = "cSquart";
            this.cSquart.ReadOnly = true;
            // 
            // Cost_of_Meter
            // 
            this.Cost_of_Meter.DataPropertyName = "Cost_of_Meter";
            this.Cost_of_Meter.HeaderText = "Стоимость м2";
            this.Cost_of_Meter.Name = "Cost_of_Meter";
            this.Cost_of_Meter.ReadOnly = true;
            // 
            // cSumDog
            // 
            this.cSumDog.DataPropertyName = "Total_Sum";
            this.cSumDog.HeaderText = "Сумма по договору";
            this.cSumDog.Name = "cSumDog";
            this.cSumDog.ReadOnly = true;
            // 
            // cDiscount
            // 
            this.cDiscount.DataPropertyName = "discount";
            this.cDiscount.HeaderText = "Скидка";
            this.cDiscount.Name = "cDiscount";
            this.cDiscount.ReadOnly = true;
            // 
            // cPlane
            // 
            this.cPlane.DataPropertyName = "plane";
            this.cPlane.HeaderText = "План";
            this.cPlane.Name = "cPlane";
            this.cPlane.ReadOnly = true;
            // 
            // frmAddReportMonth
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1251, 640);
            this.Controls.Add(this.lSumPlane);
            this.Controls.Add(this.tbSumPlane);
            this.Controls.Add(this.btClear);
            this.Controls.Add(this.btSave);
            this.Controls.Add(this.cmbTypeContract);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.chkHideDocColumnts);
            this.Controls.Add(this.dgvData);
            this.Controls.Add(this.tbLandLord);
            this.Controls.Add(this.tbAgreements);
            this.Controls.Add(this.tbTenant);
            this.Controls.Add(this.btCalcData);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dtpStart);
            this.Controls.Add(this.btPrint);
            this.Controls.Add(this.btExit);
            this.Controls.Add(this.btAcceptD);
            this.Controls.Add(this.cmbObject);
            this.Controls.Add(this.label3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAddReportMonth";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ежемесячный план";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmAddReportMonth_FormClosing);
            this.Load += new System.EventHandler(this.frmAddReportMonth_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbObject;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btPrint;
        private System.Windows.Forms.Button btExit;
        private System.Windows.Forms.Button btAcceptD;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpStart;
        private System.Windows.Forms.Button btCalcData;
        private System.Windows.Forms.TextBox tbLandLord;
        private System.Windows.Forms.TextBox tbAgreements;
        private System.Windows.Forms.TextBox tbTenant;
        private System.Windows.Forms.DataGridView dgvData;
        private System.Windows.Forms.CheckBox chkHideDocColumnts;
        private System.Windows.Forms.ComboBox cmbTypeContract;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btSave;
        private System.Windows.Forms.Button btClear;
        private System.Windows.Forms.TextBox tbSumPlane;
        private System.Windows.Forms.Label lSumPlane;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameLandLord;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameTenant;
        private System.Windows.Forms.DataGridViewTextBoxColumn TypeContract;
        private System.Windows.Forms.DataGridViewTextBoxColumn cAgreements;
        private System.Windows.Forms.DataGridViewTextBoxColumn timeLimit;
        private System.Windows.Forms.DataGridViewTextBoxColumn cBuild;
        private System.Windows.Forms.DataGridViewTextBoxColumn cFloor;
        private System.Windows.Forms.DataGridViewTextBoxColumn cSection;
        private System.Windows.Forms.DataGridViewTextBoxColumn cSquart;
        private System.Windows.Forms.DataGridViewTextBoxColumn Cost_of_Meter;
        private System.Windows.Forms.DataGridViewTextBoxColumn cSumDog;
        private System.Windows.Forms.DataGridViewTextBoxColumn cDiscount;
        private System.Windows.Forms.DataGridViewTextBoxColumn cPlane;
    }
}