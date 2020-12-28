namespace dllJournalPlaneReport
{
    partial class frmReportPlane
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
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpEnd = new System.Windows.Forms.DateTimePicker();
            this.dtpStart = new System.Windows.Forms.DateTimePicker();
            this.btUpdate = new System.Windows.Forms.Button();
            this.btPrint = new System.Windows.Forms.Button();
            this.btExit = new System.Windows.Forms.Button();
            this.btAcceptD = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.chbCongressAccept = new System.Windows.Forms.CheckBox();
            this.dgvData = new System.Windows.Forms.DataGridView();
            this.cPeriod = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nameObject = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cConfirmed = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cDateConfirm = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tbFioEdit = new System.Windows.Forms.TextBox();
            this.tbDateEdit = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btAdd = new System.Windows.Forms.Button();
            this.btEdit = new System.Windows.Forms.Button();
            this.btDel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbObject
            // 
            this.cmbObject.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbObject.FormattingEnabled = true;
            this.cmbObject.Location = new System.Drawing.Point(454, 11);
            this.cmbObject.Name = "cmbObject";
            this.cmbObject.Size = new System.Drawing.Size(230, 21);
            this.cmbObject.TabIndex = 9;
            this.cmbObject.SelectionChangeCommitted += new System.EventHandler(this.cmbObject_SelectionChangeCommitted);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(362, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(86, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Объект аренды";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(153, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(19, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "по";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Период с";
            // 
            // dtpEnd
            // 
            this.dtpEnd.CustomFormat = "MM.yyyy";
            this.dtpEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpEnd.Location = new System.Drawing.Point(178, 11);
            this.dtpEnd.Name = "dtpEnd";
            this.dtpEnd.Size = new System.Drawing.Size(67, 20);
            this.dtpEnd.TabIndex = 4;
            this.dtpEnd.CloseUp += new System.EventHandler(this.dtpStart_CloseUp);
            this.dtpEnd.ValueChanged += new System.EventHandler(this.dtpEnd_ValueChanged);
            this.dtpEnd.Leave += new System.EventHandler(this.dtpStart_Leave);
            // 
            // dtpStart
            // 
            this.dtpStart.CustomFormat = "MM.yyyy";
            this.dtpStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpStart.Location = new System.Drawing.Point(74, 11);
            this.dtpStart.Name = "dtpStart";
            this.dtpStart.Size = new System.Drawing.Size(67, 20);
            this.dtpStart.TabIndex = 5;
            this.dtpStart.CloseUp += new System.EventHandler(this.dtpStart_CloseUp);
            this.dtpStart.ValueChanged += new System.EventHandler(this.dtpStart_ValueChanged);
            this.dtpStart.Leave += new System.EventHandler(this.dtpStart_Leave);
            // 
            // btUpdate
            // 
            this.btUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btUpdate.Image = global::dllJournalPlaneReport.Properties.Resources.reload_8055;
            this.btUpdate.Location = new System.Drawing.Point(1241, 5);
            this.btUpdate.Name = "btUpdate";
            this.btUpdate.Size = new System.Drawing.Size(32, 32);
            this.btUpdate.TabIndex = 11;
            this.btUpdate.UseVisualStyleBackColor = true;
            this.btUpdate.Click += new System.EventHandler(this.btUpdate_Click);
            // 
            // btPrint
            // 
            this.btPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btPrint.Image = global::dllJournalPlaneReport.Properties.Resources.klpq_2511;
            this.btPrint.Location = new System.Drawing.Point(1203, 600);
            this.btPrint.Name = "btPrint";
            this.btPrint.Size = new System.Drawing.Size(32, 32);
            this.btPrint.TabIndex = 12;
            this.btPrint.UseVisualStyleBackColor = true;
            this.btPrint.Click += new System.EventHandler(this.btPrint_Click);
            // 
            // btExit
            // 
            this.btExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btExit.Image = global::dllJournalPlaneReport.Properties.Resources.exit_8633;
            this.btExit.Location = new System.Drawing.Point(1241, 600);
            this.btExit.Name = "btExit";
            this.btExit.Size = new System.Drawing.Size(32, 32);
            this.btExit.TabIndex = 13;
            this.btExit.UseVisualStyleBackColor = true;
            this.btExit.Click += new System.EventHandler(this.btExit_Click);
            // 
            // btAcceptD
            // 
            this.btAcceptD.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btAcceptD.Image = global::dllJournalPlaneReport.Properties.Resources.like;
            this.btAcceptD.Location = new System.Drawing.Point(1165, 600);
            this.btAcceptD.Name = "btAcceptD";
            this.btAcceptD.Size = new System.Drawing.Size(32, 32);
            this.btAcceptD.TabIndex = 14;
            this.btAcceptD.UseVisualStyleBackColor = true;
            this.btAcceptD.Click += new System.EventHandler(this.btAcceptD_Click);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(255)))), ((int)(((byte)(153)))));
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Location = new System.Drawing.Point(14, 608);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(19, 19);
            this.panel2.TabIndex = 17;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(153)))));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Location = new System.Drawing.Point(14, 583);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(19, 19);
            this.panel1.TabIndex = 18;
            this.panel1.Visible = false;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(39, 586);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 13);
            this.label4.TabIndex = 16;
            this.label4.Text = "- подтверждённые";
            this.label4.Visible = false;
            // 
            // chbCongressAccept
            // 
            this.chbCongressAccept.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chbCongressAccept.AutoSize = true;
            this.chbCongressAccept.Location = new System.Drawing.Point(42, 609);
            this.chbCongressAccept.Name = "chbCongressAccept";
            this.chbCongressAccept.Size = new System.Drawing.Size(159, 17);
            this.chbCongressAccept.TabIndex = 15;
            this.chbCongressAccept.Text = "- скрыть подтверждённые";
            this.chbCongressAccept.UseVisualStyleBackColor = true;
            this.chbCongressAccept.Click += new System.EventHandler(this.chbCongressAccept_Click);
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
            this.cPeriod,
            this.nameObject,
            this.cConfirmed,
            this.cDateConfirm});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvData.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvData.Location = new System.Drawing.Point(12, 50);
            this.dgvData.MultiSelect = false;
            this.dgvData.Name = "dgvData";
            this.dgvData.ReadOnly = true;
            this.dgvData.RowHeadersVisible = false;
            this.dgvData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvData.Size = new System.Drawing.Size(1261, 527);
            this.dgvData.TabIndex = 19;
            this.dgvData.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvData_CellMouseDoubleClick);
            this.dgvData.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgvData_RowPostPaint);
            this.dgvData.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.dgvData_RowPrePaint);
            this.dgvData.SelectionChanged += new System.EventHandler(this.dgvData_SelectionChanged);
            // 
            // cPeriod
            // 
            this.cPeriod.DataPropertyName = "PeriodMonthPlan";
            this.cPeriod.HeaderText = "Период плана";
            this.cPeriod.Name = "cPeriod";
            this.cPeriod.ReadOnly = true;
            // 
            // nameObject
            // 
            this.nameObject.DataPropertyName = "nameObject";
            this.nameObject.HeaderText = "Объект аренды";
            this.nameObject.Name = "nameObject";
            this.nameObject.ReadOnly = true;
            // 
            // cConfirmed
            // 
            this.cConfirmed.DataPropertyName = "FIOConfirmed";
            this.cConfirmed.HeaderText = "Подтвердил";
            this.cConfirmed.Name = "cConfirmed";
            this.cConfirmed.ReadOnly = true;
            // 
            // cDateConfirm
            // 
            this.cDateConfirm.DataPropertyName = "DateСonfirmed";
            this.cDateConfirm.HeaderText = "Дата подтверждения";
            this.cDateConfirm.Name = "cDateConfirm";
            this.cDateConfirm.ReadOnly = true;
            // 
            // tbFioEdit
            // 
            this.tbFioEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tbFioEdit.Location = new System.Drawing.Point(454, 586);
            this.tbFioEdit.Name = "tbFioEdit";
            this.tbFioEdit.ReadOnly = true;
            this.tbFioEdit.Size = new System.Drawing.Size(354, 20);
            this.tbFioEdit.TabIndex = 20;
            // 
            // tbDateEdit
            // 
            this.tbDateEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tbDateEdit.Location = new System.Drawing.Point(454, 612);
            this.tbDateEdit.Name = "tbDateEdit";
            this.tbDateEdit.ReadOnly = true;
            this.tbDateEdit.Size = new System.Drawing.Size(354, 20);
            this.tbDateEdit.TabIndex = 20;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(366, 590);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(82, 13);
            this.label5.TabIndex = 16;
            this.label5.Text = "Редактировал.";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(362, 616);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(86, 13);
            this.label6.TabIndex = 16;
            this.label6.Text = "Дата редактир.";
            // 
            // btAdd
            // 
            this.btAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btAdd.Image = global::dllJournalPlaneReport.Properties.Resources.Add;
            this.btAdd.Location = new System.Drawing.Point(998, 600);
            this.btAdd.Name = "btAdd";
            this.btAdd.Size = new System.Drawing.Size(32, 32);
            this.btAdd.TabIndex = 21;
            this.btAdd.UseVisualStyleBackColor = true;
            this.btAdd.Click += new System.EventHandler(this.btAdd_Click);
            // 
            // btEdit
            // 
            this.btEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btEdit.Image = global::dllJournalPlaneReport.Properties.Resources.Edit;
            this.btEdit.Location = new System.Drawing.Point(1036, 600);
            this.btEdit.Name = "btEdit";
            this.btEdit.Size = new System.Drawing.Size(32, 32);
            this.btEdit.TabIndex = 21;
            this.btEdit.UseVisualStyleBackColor = true;
            this.btEdit.Click += new System.EventHandler(this.btEdit_Click);
            // 
            // btDel
            // 
            this.btDel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btDel.Image = global::dllJournalPlaneReport.Properties.Resources.Trash;
            this.btDel.Location = new System.Drawing.Point(1074, 600);
            this.btDel.Name = "btDel";
            this.btDel.Size = new System.Drawing.Size(32, 32);
            this.btDel.TabIndex = 21;
            this.btDel.UseVisualStyleBackColor = true;
            this.btDel.Click += new System.EventHandler(this.btDel_Click);
            // 
            // frmReportPlane
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1285, 644);
            this.Controls.Add(this.btDel);
            this.Controls.Add(this.btEdit);
            this.Controls.Add(this.btAdd);
            this.Controls.Add(this.tbDateEdit);
            this.Controls.Add(this.tbFioEdit);
            this.Controls.Add(this.dgvData);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.chbCongressAccept);
            this.Controls.Add(this.btPrint);
            this.Controls.Add(this.btExit);
            this.Controls.Add(this.btAcceptD);
            this.Controls.Add(this.btUpdate);
            this.Controls.Add(this.cmbObject);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dtpEnd);
            this.Controls.Add(this.dtpStart);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MinimizeBox = false;
            this.Name = "frmReportPlane";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Журнал План-Отчётов";
            this.Load += new System.EventHandler(this.frmReportMonth_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbObject;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpEnd;
        private System.Windows.Forms.DateTimePicker dtpStart;
        private System.Windows.Forms.Button btUpdate;
        private System.Windows.Forms.Button btPrint;
        private System.Windows.Forms.Button btExit;
        private System.Windows.Forms.Button btAcceptD;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox chbCongressAccept;
        private System.Windows.Forms.DataGridView dgvData;
        private System.Windows.Forms.TextBox tbFioEdit;
        private System.Windows.Forms.TextBox tbDateEdit;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridViewTextBoxColumn cPeriod;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameObject;
        private System.Windows.Forms.DataGridViewTextBoxColumn cConfirmed;
        private System.Windows.Forms.DataGridViewTextBoxColumn cDateConfirm;
        private System.Windows.Forms.Button btAdd;
        private System.Windows.Forms.Button btEdit;
        private System.Windows.Forms.Button btDel;
    }
}

