namespace dllJournalLoad1C
{
    partial class frmJournalLoad1C
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.cmbObject = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbTypeDoc = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvData = new System.Windows.Forms.DataGridView();
            this.cDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cObject = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cLandLord = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nameTenant = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cAgreements = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cTypeContract = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cPlace = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cAgreement1C = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cDate1C = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cTypePay = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tbPlace = new System.Windows.Forms.TextBox();
            this.tbAgreements = new System.Windows.Forms.TextBox();
            this.tbTenant = new System.Windows.Forms.TextBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.dtpStart = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.dtpEnd = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tbTypePay = new System.Windows.Forms.TextBox();
            this.tbAgreement1C = new System.Windows.Forms.TextBox();
            this.tbLandLord = new System.Windows.Forms.TextBox();
            this.btSendMail = new System.Windows.Forms.Button();
            this.btExcel = new System.Windows.Forms.Button();
            this.btExit = new System.Windows.Forms.Button();
            this.btUpdate = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbObject
            // 
            this.cmbObject.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbObject.FormattingEnabled = true;
            this.cmbObject.Location = new System.Drawing.Point(102, 38);
            this.cmbObject.Name = "cmbObject";
            this.cmbObject.Size = new System.Drawing.Size(220, 21);
            this.cmbObject.TabIndex = 9;
            this.cmbObject.SelectionChangeCommitted += new System.EventHandler(this.cmbObject_SelectionChangeCommitted);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 41);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(86, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Объект аренды";
            // 
            // cmbTypeDoc
            // 
            this.cmbTypeDoc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTypeDoc.FormattingEnabled = true;
            this.cmbTypeDoc.Location = new System.Drawing.Point(433, 12);
            this.cmbTypeDoc.Name = "cmbTypeDoc";
            this.cmbTypeDoc.Size = new System.Drawing.Size(230, 21);
            this.cmbTypeDoc.TabIndex = 30;
            this.cmbTypeDoc.SelectionChangeCommitted += new System.EventHandler(this.cmbLandLord_SelectionChangeCommitted);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(341, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 13);
            this.label1.TabIndex = 29;
            this.label1.Text = "Тип договора";
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
            this.cObject,
            this.cLandLord,
            this.nameTenant,
            this.cAgreements,
            this.cTypeContract,
            this.cPlace,
            this.cAgreement1C,
            this.cDate1C,
            this.cTypePay});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvData.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvData.Location = new System.Drawing.Point(12, 98);
            this.dgvData.Name = "dgvData";
            this.dgvData.RowHeadersVisible = false;
            this.dgvData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvData.Size = new System.Drawing.Size(1122, 423);
            this.dgvData.TabIndex = 32;
            this.dgvData.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvData_CellDoubleClick);
            this.dgvData.ColumnWidthChanged += new System.Windows.Forms.DataGridViewColumnEventHandler(this.dgvData_ColumnWidthChanged);
            this.dgvData.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgvData_RowPostPaint);
            this.dgvData.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.dgvData_RowPrePaint);
            // 
            // cDate
            // 
            this.cDate.DataPropertyName = "DateLoad";
            this.cDate.HeaderText = "Дата загрузки счета";
            this.cDate.Name = "cDate";
            this.cDate.ReadOnly = true;
            // 
            // cObject
            // 
            this.cObject.DataPropertyName = "nameObjectLease";
            this.cObject.HeaderText = "Объект";
            this.cObject.Name = "cObject";
            this.cObject.ReadOnly = true;
            // 
            // cLandLord
            // 
            this.cLandLord.DataPropertyName = "nameLandLord";
            this.cLandLord.HeaderText = "Арендодатель";
            this.cLandLord.Name = "cLandLord";
            this.cLandLord.ReadOnly = true;
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
            this.cAgreements.HeaderText = "№ договора";
            this.cAgreements.Name = "cAgreements";
            this.cAgreements.ReadOnly = true;
            // 
            // cTypeContract
            // 
            this.cTypeContract.DataPropertyName = "TypeContract";
            this.cTypeContract.HeaderText = "Тип договора";
            this.cTypeContract.Name = "cTypeContract";
            this.cTypeContract.ReadOnly = true;
            // 
            // cPlace
            // 
            this.cPlace.DataPropertyName = "namePlace";
            this.cPlace.HeaderText = "Местоположение";
            this.cPlace.Name = "cPlace";
            this.cPlace.ReadOnly = true;
            // 
            // cAgreement1C
            // 
            this.cAgreement1C.DataPropertyName = "NumberAccount";
            this.cAgreement1C.HeaderText = "№ счета 1С";
            this.cAgreement1C.Name = "cAgreement1C";
            this.cAgreement1C.ReadOnly = true;
            // 
            // cDate1C
            // 
            this.cDate1C.DataPropertyName = "DateAccount";
            this.cDate1C.HeaderText = "Дата счета 1С";
            this.cDate1C.Name = "cDate1C";
            this.cDate1C.ReadOnly = true;
            // 
            // cTypePay
            // 
            this.cTypePay.DataPropertyName = "TypePayment";
            this.cTypePay.HeaderText = "Назначение платежа";
            this.cTypePay.Name = "cTypePay";
            this.cTypePay.ReadOnly = true;
            // 
            // tbPlace
            // 
            this.tbPlace.Location = new System.Drawing.Point(222, 72);
            this.tbPlace.MaxLength = 250;
            this.tbPlace.Name = "tbPlace";
            this.tbPlace.Size = new System.Drawing.Size(100, 20);
            this.tbPlace.TabIndex = 35;
            this.tbPlace.TextChanged += new System.EventHandler(this.tbTenant_TextChanged);
            // 
            // tbAgreements
            // 
            this.tbAgreements.Location = new System.Drawing.Point(116, 72);
            this.tbAgreements.MaxLength = 250;
            this.tbAgreements.Name = "tbAgreements";
            this.tbAgreements.Size = new System.Drawing.Size(100, 20);
            this.tbAgreements.TabIndex = 36;
            this.tbAgreements.TextChanged += new System.EventHandler(this.tbTenant_TextChanged);
            // 
            // tbTenant
            // 
            this.tbTenant.Location = new System.Drawing.Point(10, 72);
            this.tbTenant.MaxLength = 250;
            this.tbTenant.Name = "tbTenant";
            this.tbTenant.Size = new System.Drawing.Size(100, 20);
            this.tbTenant.TabIndex = 37;
            this.tbTenant.TextChanged += new System.EventHandler(this.tbTenant_TextChanged);
            // 
            // dtpStart
            // 
            this.dtpStart.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpStart.Location = new System.Drawing.Point(102, 12);
            this.dtpStart.Name = "dtpStart";
            this.dtpStart.Size = new System.Drawing.Size(93, 20);
            this.dtpStart.TabIndex = 38;
            this.dtpStart.ValueChanged += new System.EventHandler(this.dtpStart_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Период с";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(201, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(19, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "по";
            // 
            // dtpEnd
            // 
            this.dtpEnd.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpEnd.Location = new System.Drawing.Point(226, 12);
            this.dtpEnd.Name = "dtpEnd";
            this.dtpEnd.Size = new System.Drawing.Size(96, 20);
            this.dtpEnd.TabIndex = 38;
            this.dtpEnd.ValueChanged += new System.EventHandler(this.dtpEnd_ValueChanged);
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(36, 536);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(197, 13);
            this.label5.TabIndex = 39;
            this.label5.Text = "- счет отправлен по e-mail арендатору";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(153)))));
            this.panel1.Location = new System.Drawing.Point(13, 534);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(17, 17);
            this.panel1.TabIndex = 40;
            // 
            // tbTypePay
            // 
            this.tbTypePay.Location = new System.Drawing.Point(556, 72);
            this.tbTypePay.MaxLength = 250;
            this.tbTypePay.Name = "tbTypePay";
            this.tbTypePay.Size = new System.Drawing.Size(100, 20);
            this.tbTypePay.TabIndex = 41;
            this.tbTypePay.TextChanged += new System.EventHandler(this.tbTenant_TextChanged);
            // 
            // tbAgreement1C
            // 
            this.tbAgreement1C.Location = new System.Drawing.Point(450, 72);
            this.tbAgreement1C.MaxLength = 250;
            this.tbAgreement1C.Name = "tbAgreement1C";
            this.tbAgreement1C.Size = new System.Drawing.Size(100, 20);
            this.tbAgreement1C.TabIndex = 42;
            this.tbAgreement1C.TextChanged += new System.EventHandler(this.tbTenant_TextChanged);
            // 
            // tbLandLord
            // 
            this.tbLandLord.Location = new System.Drawing.Point(344, 72);
            this.tbLandLord.MaxLength = 250;
            this.tbLandLord.Name = "tbLandLord";
            this.tbLandLord.Size = new System.Drawing.Size(100, 20);
            this.tbLandLord.TabIndex = 43;
            this.tbLandLord.TextChanged += new System.EventHandler(this.tbTenant_TextChanged);
            // 
            // btSendMail
            // 
            this.btSendMail.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btSendMail.Image = global::dllJournalLoad1C.Properties.Resources.stamp1;
            this.btSendMail.Location = new System.Drawing.Point(1026, 528);
            this.btSendMail.Name = "btSendMail";
            this.btSendMail.Size = new System.Drawing.Size(32, 32);
            this.btSendMail.TabIndex = 33;
            this.btSendMail.UseVisualStyleBackColor = true;
            this.btSendMail.Click += new System.EventHandler(this.btSendMail_Click);
            // 
            // btExcel
            // 
            this.btExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btExcel.Image = global::dllJournalLoad1C.Properties.Resources.klpq_2511;
            this.btExcel.Location = new System.Drawing.Point(1064, 528);
            this.btExcel.Name = "btExcel";
            this.btExcel.Size = new System.Drawing.Size(32, 32);
            this.btExcel.TabIndex = 33;
            this.toolTip1.SetToolTip(this.btExcel, "Сохранить");
            this.btExcel.UseVisualStyleBackColor = true;
            this.btExcel.Click += new System.EventHandler(this.btExcel_Click);
            // 
            // btExit
            // 
            this.btExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btExit.Image = global::dllJournalLoad1C.Properties.Resources.exit_8633;
            this.btExit.Location = new System.Drawing.Point(1102, 528);
            this.btExit.Name = "btExit";
            this.btExit.Size = new System.Drawing.Size(32, 32);
            this.btExit.TabIndex = 34;
            this.toolTip1.SetToolTip(this.btExit, "Выход");
            this.btExit.UseVisualStyleBackColor = true;
            this.btExit.Click += new System.EventHandler(this.btExit_Click);
            // 
            // btUpdate
            // 
            this.btUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btUpdate.Image = global::dllJournalLoad1C.Properties.Resources.reload_8055;
            this.btUpdate.Location = new System.Drawing.Point(1086, 11);
            this.btUpdate.Name = "btUpdate";
            this.btUpdate.Size = new System.Drawing.Size(48, 48);
            this.btUpdate.TabIndex = 31;
            this.toolTip1.SetToolTip(this.btUpdate, "Обновить");
            this.btUpdate.UseVisualStyleBackColor = true;
            this.btUpdate.Click += new System.EventHandler(this.btUpdate_Click);
            // 
            // frmJournalLoad1C
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1146, 572);
            this.Controls.Add(this.tbLandLord);
            this.Controls.Add(this.tbAgreement1C);
            this.Controls.Add(this.tbTypePay);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.dtpEnd);
            this.Controls.Add(this.dtpStart);
            this.Controls.Add(this.tbPlace);
            this.Controls.Add(this.tbAgreements);
            this.Controls.Add(this.tbTenant);
            this.Controls.Add(this.btSendMail);
            this.Controls.Add(this.btExcel);
            this.Controls.Add(this.btExit);
            this.Controls.Add(this.dgvData);
            this.Controls.Add(this.btUpdate);
            this.Controls.Add(this.cmbTypeDoc);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cmbObject);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MinimizeBox = false;
            this.Name = "frmJournalLoad1C";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Журнал загрузки счетов 1С";
            this.Load += new System.EventHandler(this.frmSelectAgreementsTo1C_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ComboBox cmbObject;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbTypeDoc;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btUpdate;
        private System.Windows.Forms.DataGridView dgvData;
        private System.Windows.Forms.Button btExcel;
        private System.Windows.Forms.Button btExit;
        private System.Windows.Forms.TextBox tbPlace;
        private System.Windows.Forms.TextBox tbAgreements;
        private System.Windows.Forms.TextBox tbTenant;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.DateTimePicker dtpStart;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtpEnd;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox tbTypePay;
        private System.Windows.Forms.TextBox tbAgreement1C;
        private System.Windows.Forms.TextBox tbLandLord;
        private System.Windows.Forms.DataGridViewTextBoxColumn cDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn cObject;
        private System.Windows.Forms.DataGridViewTextBoxColumn cLandLord;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameTenant;
        private System.Windows.Forms.DataGridViewTextBoxColumn cAgreements;
        private System.Windows.Forms.DataGridViewTextBoxColumn cTypeContract;
        private System.Windows.Forms.DataGridViewTextBoxColumn cPlace;
        private System.Windows.Forms.DataGridViewTextBoxColumn cAgreement1C;
        private System.Windows.Forms.DataGridViewTextBoxColumn cDate1C;
        private System.Windows.Forms.DataGridViewTextBoxColumn cTypePay;
        private System.Windows.Forms.Button btSendMail;
    }
}

