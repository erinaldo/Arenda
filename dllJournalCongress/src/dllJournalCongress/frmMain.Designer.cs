namespace dllJournalCongress
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
            this.dtpStart = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpEnd = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbObject = new System.Windows.Forms.ComboBox();
            this.tbLandLord = new System.Windows.Forms.TextBox();
            this.tbTenant = new System.Windows.Forms.TextBox();
            this.tbAgreement = new System.Windows.Forms.TextBox();
            this.tbNamePlace = new System.Windows.Forms.TextBox();
            this.dgvData = new System.Windows.Forms.DataGridView();
            this.nameLandLord = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nameTenant = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nameObject = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Agreement = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.namePlace = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Cost_of_Meter = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DateDocument = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Date_of_Departure = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.failComment = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chbCongressAccept = new System.Windows.Forms.CheckBox();
            this.chbDropAgreements = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btUpdate = new System.Windows.Forms.Button();
            this.btPrint = new System.Windows.Forms.Button();
            this.btExit = new System.Windows.Forms.Button();
            this.btAcceptD = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            this.SuspendLayout();
            // 
            // dtpStart
            // 
            this.dtpStart.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpStart.Location = new System.Drawing.Point(69, 10);
            this.dtpStart.Name = "dtpStart";
            this.dtpStart.Size = new System.Drawing.Size(92, 20);
            this.dtpStart.TabIndex = 0;
            this.dtpStart.CloseUp += new System.EventHandler(this.dtpStart_CloseUp);
            this.dtpStart.ValueChanged += new System.EventHandler(this.dtpStart_ValueChanged);
            this.dtpStart.Leave += new System.EventHandler(this.dtpStart_Leave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Период с";
            // 
            // dtpEnd
            // 
            this.dtpEnd.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpEnd.Location = new System.Drawing.Point(192, 10);
            this.dtpEnd.Name = "dtpEnd";
            this.dtpEnd.Size = new System.Drawing.Size(92, 20);
            this.dtpEnd.TabIndex = 0;
            this.dtpEnd.CloseUp += new System.EventHandler(this.dtpStart_CloseUp);
            this.dtpEnd.ValueChanged += new System.EventHandler(this.dtpEnd_ValueChanged);
            this.dtpEnd.Leave += new System.EventHandler(this.dtpStart_Leave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(167, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(19, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "по";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(357, 14);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(86, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Объект аренды";
            // 
            // cmbObject
            // 
            this.cmbObject.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbObject.FormattingEnabled = true;
            this.cmbObject.Location = new System.Drawing.Point(449, 10);
            this.cmbObject.Name = "cmbObject";
            this.cmbObject.Size = new System.Drawing.Size(230, 21);
            this.cmbObject.TabIndex = 3;
            this.cmbObject.SelectionChangeCommitted += new System.EventHandler(this.cmbObject_SelectionChangeCommitted);
            // 
            // tbLandLord
            // 
            this.tbLandLord.Location = new System.Drawing.Point(12, 36);
            this.tbLandLord.Name = "tbLandLord";
            this.tbLandLord.Size = new System.Drawing.Size(100, 20);
            this.tbLandLord.TabIndex = 4;
            this.tbLandLord.TextChanged += new System.EventHandler(this.tbLandLord_TextChanged);
            // 
            // tbTenant
            // 
            this.tbTenant.Location = new System.Drawing.Point(118, 36);
            this.tbTenant.Name = "tbTenant";
            this.tbTenant.Size = new System.Drawing.Size(100, 20);
            this.tbTenant.TabIndex = 4;
            this.tbTenant.TextChanged += new System.EventHandler(this.tbLandLord_TextChanged);
            // 
            // tbAgreement
            // 
            this.tbAgreement.Location = new System.Drawing.Point(224, 36);
            this.tbAgreement.Name = "tbAgreement";
            this.tbAgreement.Size = new System.Drawing.Size(100, 20);
            this.tbAgreement.TabIndex = 4;
            this.tbAgreement.TextChanged += new System.EventHandler(this.tbLandLord_TextChanged);
            // 
            // tbNamePlace
            // 
            this.tbNamePlace.Location = new System.Drawing.Point(330, 36);
            this.tbNamePlace.Name = "tbNamePlace";
            this.tbNamePlace.Size = new System.Drawing.Size(100, 20);
            this.tbNamePlace.TabIndex = 4;
            this.tbNamePlace.TextChanged += new System.EventHandler(this.tbLandLord_TextChanged);
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
            this.nameObject,
            this.Agreement,
            this.namePlace,
            this.Cost_of_Meter,
            this.DateDocument,
            this.Date_of_Departure,
            this.failComment});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvData.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvData.Location = new System.Drawing.Point(12, 62);
            this.dgvData.MultiSelect = false;
            this.dgvData.Name = "dgvData";
            this.dgvData.ReadOnly = true;
            this.dgvData.RowHeadersVisible = false;
            this.dgvData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvData.Size = new System.Drawing.Size(1012, 420);
            this.dgvData.TabIndex = 5;
            this.dgvData.ColumnWidthChanged += new System.Windows.Forms.DataGridViewColumnEventHandler(this.dgvData_ColumnWidthChanged);
            this.dgvData.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgvData_RowPostPaint);
            this.dgvData.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.dgvData_RowPrePaint);
            this.dgvData.SelectionChanged += new System.EventHandler(this.dgvData_SelectionChanged);
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
            // nameObject
            // 
            this.nameObject.DataPropertyName = "nameObject";
            this.nameObject.HeaderText = "Объект аренды";
            this.nameObject.Name = "nameObject";
            this.nameObject.ReadOnly = true;
            // 
            // Agreement
            // 
            this.Agreement.DataPropertyName = "Agreement";
            this.Agreement.HeaderText = "Номер договора";
            this.Agreement.Name = "Agreement";
            this.Agreement.ReadOnly = true;
            // 
            // namePlace
            // 
            this.namePlace.DataPropertyName = "namePlace";
            this.namePlace.HeaderText = "Местоположение места аренды";
            this.namePlace.Name = "namePlace";
            this.namePlace.ReadOnly = true;
            // 
            // Cost_of_Meter
            // 
            this.Cost_of_Meter.DataPropertyName = "Cost_of_Meter";
            this.Cost_of_Meter.HeaderText = "Стоимость 1м2";
            this.Cost_of_Meter.Name = "Cost_of_Meter";
            this.Cost_of_Meter.ReadOnly = true;
            // 
            // DateDocument
            // 
            this.DateDocument.DataPropertyName = "DateDocument";
            this.DateDocument.HeaderText = "Дата подачи заявления";
            this.DateDocument.Name = "DateDocument";
            this.DateDocument.ReadOnly = true;
            // 
            // Date_of_Departure
            // 
            this.Date_of_Departure.DataPropertyName = "Date_of_Departure";
            this.Date_of_Departure.HeaderText = "Планируемая дата съезда";
            this.Date_of_Departure.Name = "Date_of_Departure";
            this.Date_of_Departure.ReadOnly = true;
            // 
            // failComment
            // 
            this.failComment.DataPropertyName = "failComment";
            this.failComment.HeaderText = "Примечание";
            this.failComment.Name = "failComment";
            this.failComment.ReadOnly = true;
            // 
            // chbCongressAccept
            // 
            this.chbCongressAccept.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chbCongressAccept.AutoSize = true;
            this.chbCongressAccept.Location = new System.Drawing.Point(40, 516);
            this.chbCongressAccept.Name = "chbCongressAccept";
            this.chbCongressAccept.Size = new System.Drawing.Size(133, 17);
            this.chbCongressAccept.TabIndex = 6;
            this.chbCongressAccept.Text = "- съезд подтвержден";
            this.chbCongressAccept.UseVisualStyleBackColor = true;
            this.chbCongressAccept.CheckedChanged += new System.EventHandler(this.chbDropAgreements_CheckedChanged);
            // 
            // chbDropAgreements
            // 
            this.chbDropAgreements.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chbDropAgreements.AutoSize = true;
            this.chbDropAgreements.Location = new System.Drawing.Point(40, 541);
            this.chbDropAgreements.Name = "chbDropAgreements";
            this.chbDropAgreements.Size = new System.Drawing.Size(329, 17);
            this.chbDropAgreements.TabIndex = 6;
            this.chbDropAgreements.Text = "- аннуляция подтверждена\\имеется расторжение договора";
            this.chbDropAgreements.UseVisualStyleBackColor = true;
            this.chbDropAgreements.CheckedChanged += new System.EventHandler(this.chbDropAgreements_CheckedChanged);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(37, 493);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(161, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "- имеется аннуляция на съезд";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(153)))));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Location = new System.Drawing.Point(12, 490);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(19, 19);
            this.panel1.TabIndex = 8;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(255)))), ((int)(((byte)(153)))));
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Location = new System.Drawing.Point(12, 515);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(19, 19);
            this.panel2.TabIndex = 8;
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(204)))), ((int)(((byte)(255)))));
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Location = new System.Drawing.Point(12, 540);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(19, 19);
            this.panel3.TabIndex = 8;
            // 
            // btUpdate
            // 
            this.btUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btUpdate.Image = global::dllJournalCongress.Properties.Resources.reload_8055;
            this.btUpdate.Location = new System.Drawing.Point(976, 8);
            this.btUpdate.Name = "btUpdate";
            this.btUpdate.Size = new System.Drawing.Size(48, 48);
            this.btUpdate.TabIndex = 10;
            this.btUpdate.UseVisualStyleBackColor = true;
            this.btUpdate.Click += new System.EventHandler(this.btUpdate_Click);
            // 
            // btPrint
            // 
            this.btPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btPrint.Image = global::dllJournalCongress.Properties.Resources.klpq_2511;
            this.btPrint.Location = new System.Drawing.Point(954, 526);
            this.btPrint.Name = "btPrint";
            this.btPrint.Size = new System.Drawing.Size(32, 32);
            this.btPrint.TabIndex = 9;
            this.btPrint.UseVisualStyleBackColor = true;
            this.btPrint.Click += new System.EventHandler(this.btPrint_Click);
            // 
            // btExit
            // 
            this.btExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btExit.Image = global::dllJournalCongress.Properties.Resources.exit_8633;
            this.btExit.Location = new System.Drawing.Point(992, 526);
            this.btExit.Name = "btExit";
            this.btExit.Size = new System.Drawing.Size(32, 32);
            this.btExit.TabIndex = 9;
            this.btExit.UseVisualStyleBackColor = true;
            this.btExit.Click += new System.EventHandler(this.btExit_Click);
            // 
            // btAcceptD
            // 
            this.btAcceptD.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btAcceptD.Image = global::dllJournalCongress.Properties.Resources.like;
            this.btAcceptD.Location = new System.Drawing.Point(540, 499);
            this.btAcceptD.Name = "btAcceptD";
            this.btAcceptD.Size = new System.Drawing.Size(48, 48);
            this.btAcceptD.TabIndex = 9;
            this.btAcceptD.UseVisualStyleBackColor = true;
            this.btAcceptD.Click += new System.EventHandler(this.btAcceptD_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1036, 573);
            this.Controls.Add(this.btUpdate);
            this.Controls.Add(this.btPrint);
            this.Controls.Add(this.btExit);
            this.Controls.Add(this.btAcceptD);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.chbDropAgreements);
            this.Controls.Add(this.chbCongressAccept);
            this.Controls.Add(this.dgvData);
            this.Controls.Add(this.tbNamePlace);
            this.Controls.Add(this.tbAgreement);
            this.Controls.Add(this.tbTenant);
            this.Controls.Add(this.tbLandLord);
            this.Controls.Add(this.cmbObject);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dtpEnd);
            this.Controls.Add(this.dtpStart);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Журнал съездов";
            this.Load += new System.EventHandler(this.frmMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtpStart;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpEnd;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbObject;
        private System.Windows.Forms.TextBox tbLandLord;
        private System.Windows.Forms.TextBox tbTenant;
        private System.Windows.Forms.TextBox tbAgreement;
        private System.Windows.Forms.TextBox tbNamePlace;
        private System.Windows.Forms.DataGridView dgvData;
        private System.Windows.Forms.CheckBox chbCongressAccept;
        private System.Windows.Forms.CheckBox chbDropAgreements;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btAcceptD;
        private System.Windows.Forms.Button btExit;
        private System.Windows.Forms.Button btPrint;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameLandLord;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameTenant;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameObject;
        private System.Windows.Forms.DataGridViewTextBoxColumn Agreement;
        private System.Windows.Forms.DataGridViewTextBoxColumn namePlace;
        private System.Windows.Forms.DataGridViewTextBoxColumn Cost_of_Meter;
        private System.Windows.Forms.DataGridViewTextBoxColumn DateDocument;
        private System.Windows.Forms.DataGridViewTextBoxColumn Date_of_Departure;
        private System.Windows.Forms.DataGridViewTextBoxColumn failComment;
        private System.Windows.Forms.Button btUpdate;
    }
}

