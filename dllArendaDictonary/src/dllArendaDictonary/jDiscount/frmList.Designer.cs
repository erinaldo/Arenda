namespace dllArendaDictonary.jDiscount
{
    partial class frmList
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tbLandLord = new System.Windows.Forms.TextBox();
            this.chbNotActive = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pConfirmD = new System.Windows.Forms.Panel();
            this.cmbObject = new System.Windows.Forms.ComboBox();
            this.lObject = new System.Windows.Forms.Label();
            this.cmbTypeContract = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.chbUnlimitedDiscount = new System.Windows.Forms.CheckBox();
            this.lDateEnd = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpEnd = new System.Windows.Forms.DateTimePicker();
            this.dtpStart = new System.Windows.Forms.DateTimePicker();
            this.tbAgreements = new System.Windows.Forms.TextBox();
            this.btDeAcceptD = new System.Windows.Forms.Button();
            this.btConfirmD = new System.Windows.Forms.Button();
            this.btClose = new System.Windows.Forms.Button();
            this.dgvData = new System.Windows.Forms.DataGridView();
            this.cObject = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nameLandLord = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cAgreements = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cTypeDoc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cStartDiscount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cStopDiscount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cTypeDiscount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cSumDiscount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cConfirmD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chbIsAccept = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            this.SuspendLayout();
            // 
            // tbLandLord
            // 
            this.tbLandLord.Location = new System.Drawing.Point(12, 34);
            this.tbLandLord.Name = "tbLandLord";
            this.tbLandLord.Size = new System.Drawing.Size(244, 20);
            this.tbLandLord.TabIndex = 0;
            this.tbLandLord.TextChanged += new System.EventHandler(this.tbName_TextChanged);
            // 
            // chbNotActive
            // 
            this.chbNotActive.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chbNotActive.AutoSize = true;
            this.chbNotActive.Location = new System.Drawing.Point(281, 441);
            this.chbNotActive.Name = "chbNotActive";
            this.chbNotActive.Size = new System.Drawing.Size(138, 17);
            this.chbNotActive.TabIndex = 2;
            this.chbNotActive.Text = "- отклонённые скидки";
            this.chbNotActive.UseVisualStyleBackColor = true;
            this.chbNotActive.CheckedChanged += new System.EventHandler(this.chbNotActive_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(204)))), ((int)(((byte)(255)))));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Location = new System.Drawing.Point(254, 440);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(19, 19);
            this.panel1.TabIndex = 3;
            // 
            // pConfirmD
            // 
            this.pConfirmD.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pConfirmD.BackColor = System.Drawing.Color.Yellow;
            this.pConfirmD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pConfirmD.Location = new System.Drawing.Point(12, 440);
            this.pConfirmD.Name = "pConfirmD";
            this.pConfirmD.Size = new System.Drawing.Size(19, 19);
            this.pConfirmD.TabIndex = 3;
            // 
            // cmbObject
            // 
            this.cmbObject.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbObject.FormattingEnabled = true;
            this.cmbObject.Location = new System.Drawing.Point(66, 7);
            this.cmbObject.Name = "cmbObject";
            this.cmbObject.Size = new System.Drawing.Size(173, 21);
            this.cmbObject.TabIndex = 22;
            this.cmbObject.SelectionChangeCommitted += new System.EventHandler(this.cmbObject_SelectionChangeCommitted_1);
            // 
            // lObject
            // 
            this.lObject.AutoSize = true;
            this.lObject.Location = new System.Drawing.Point(15, 11);
            this.lObject.Name = "lObject";
            this.lObject.Size = new System.Drawing.Size(45, 13);
            this.lObject.TabIndex = 21;
            this.lObject.Text = "Объект";
            // 
            // cmbTypeContract
            // 
            this.cmbTypeContract.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTypeContract.FormattingEnabled = true;
            this.cmbTypeContract.Location = new System.Drawing.Point(347, 7);
            this.cmbTypeContract.Name = "cmbTypeContract";
            this.cmbTypeContract.Size = new System.Drawing.Size(230, 21);
            this.cmbTypeContract.TabIndex = 24;
            this.cmbTypeContract.SelectionChangeCommitted += new System.EventHandler(this.cmbObject_SelectionChangeCommitted_1);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(265, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 13);
            this.label2.TabIndex = 23;
            this.label2.Text = "Тип договора";
            // 
            // chbUnlimitedDiscount
            // 
            this.chbUnlimitedDiscount.AutoSize = true;
            this.chbUnlimitedDiscount.Location = new System.Drawing.Point(967, 10);
            this.chbUnlimitedDiscount.Name = "chbUnlimitedDiscount";
            this.chbUnlimitedDiscount.Size = new System.Drawing.Size(126, 17);
            this.chbUnlimitedDiscount.TabIndex = 95;
            this.chbUnlimitedDiscount.Text = "Постоянная скидка";
            this.chbUnlimitedDiscount.UseVisualStyleBackColor = true;
            this.chbUnlimitedDiscount.CheckedChanged += new System.EventHandler(this.chbUnlimitedDiscount_CheckedChanged);
            // 
            // lDateEnd
            // 
            this.lDateEnd.AutoSize = true;
            this.lDateEnd.Location = new System.Drawing.Point(764, 12);
            this.lDateEnd.Name = "lDateEnd";
            this.lDateEnd.Size = new System.Drawing.Size(89, 13);
            this.lDateEnd.TabIndex = 93;
            this.lDateEnd.Text = "Дата окончания";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(586, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 13);
            this.label3.TabIndex = 94;
            this.label3.Text = "Дата начала";
            // 
            // dtpEnd
            // 
            this.dtpEnd.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpEnd.Location = new System.Drawing.Point(867, 8);
            this.dtpEnd.Name = "dtpEnd";
            this.dtpEnd.Size = new System.Drawing.Size(79, 20);
            this.dtpEnd.TabIndex = 91;
            this.dtpEnd.CloseUp += new System.EventHandler(this.dtpStart_CloseUp);
            this.dtpEnd.ValueChanged += new System.EventHandler(this.dtpEnd_ValueChanged);
            this.dtpEnd.Leave += new System.EventHandler(this.dtpStart_Leave);
            // 
            // dtpStart
            // 
            this.dtpStart.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpStart.Location = new System.Drawing.Point(671, 8);
            this.dtpStart.Name = "dtpStart";
            this.dtpStart.Size = new System.Drawing.Size(79, 20);
            this.dtpStart.TabIndex = 92;
            this.dtpStart.CloseUp += new System.EventHandler(this.dtpStart_CloseUp);
            this.dtpStart.ValueChanged += new System.EventHandler(this.dtpStart_ValueChanged);
            this.dtpStart.Leave += new System.EventHandler(this.dtpStart_Leave);
            // 
            // tbAgreements
            // 
            this.tbAgreements.Location = new System.Drawing.Point(262, 34);
            this.tbAgreements.Name = "tbAgreements";
            this.tbAgreements.Size = new System.Drawing.Size(244, 20);
            this.tbAgreements.TabIndex = 0;
            this.tbAgreements.TextChanged += new System.EventHandler(this.tbName_TextChanged);
            // 
            // btDeAcceptD
            // 
            this.btDeAcceptD.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btDeAcceptD.Image = global::dllArendaDictonary.Properties.Resources.agt_action_fail_2870;
            this.btDeAcceptD.Location = new System.Drawing.Point(904, 433);
            this.btDeAcceptD.Name = "btDeAcceptD";
            this.btDeAcceptD.Size = new System.Drawing.Size(32, 32);
            this.btDeAcceptD.TabIndex = 96;
            this.btDeAcceptD.UseVisualStyleBackColor = true;
            this.btDeAcceptD.Click += new System.EventHandler(this.btDeAcceptD_Click);
            // 
            // btConfirmD
            // 
            this.btConfirmD.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btConfirmD.Image = global::dllArendaDictonary.Properties.Resources.tick;
            this.btConfirmD.Location = new System.Drawing.Point(866, 433);
            this.btConfirmD.Name = "btConfirmD";
            this.btConfirmD.Size = new System.Drawing.Size(32, 32);
            this.btConfirmD.TabIndex = 5;
            this.btConfirmD.UseVisualStyleBackColor = true;
            this.btConfirmD.Click += new System.EventHandler(this.btConfirmD_Click);
            // 
            // btClose
            // 
            this.btClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btClose.Image = global::dllArendaDictonary.Properties.Resources.Exit;
            this.btClose.Location = new System.Drawing.Point(1070, 433);
            this.btClose.Name = "btClose";
            this.btClose.Size = new System.Drawing.Size(32, 32);
            this.btClose.TabIndex = 4;
            this.btClose.UseVisualStyleBackColor = true;
            this.btClose.Click += new System.EventHandler(this.btClose_Click);
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
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvData.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.cObject,
            this.nameLandLord,
            this.cAgreements,
            this.cTypeDoc,
            this.cStartDiscount,
            this.cStopDiscount,
            this.cTypeDiscount,
            this.cSumDiscount,
            this.cConfirmD});
            this.dgvData.Location = new System.Drawing.Point(12, 60);
            this.dgvData.MultiSelect = false;
            this.dgvData.Name = "dgvData";
            this.dgvData.ReadOnly = true;
            this.dgvData.RowHeadersVisible = false;
            this.dgvData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvData.Size = new System.Drawing.Size(1090, 367);
            this.dgvData.TabIndex = 97;
            this.dgvData.ColumnWidthChanged += new System.Windows.Forms.DataGridViewColumnEventHandler(this.dgvData_ColumnWidthChanged);
            this.dgvData.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgvData_RowPostPaint);
            this.dgvData.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.dgvData_RowPrePaint);
            this.dgvData.SelectionChanged += new System.EventHandler(this.dgvData_SelectionChanged);
            // 
            // cObject
            // 
            this.cObject.DataPropertyName = "nameObjectLease";
            this.cObject.HeaderText = "Объект";
            this.cObject.Name = "cObject";
            this.cObject.ReadOnly = true;
            // 
            // nameLandLord
            // 
            this.nameLandLord.DataPropertyName = "nameLandLord";
            this.nameLandLord.HeaderText = "Арендатор";
            this.nameLandLord.Name = "nameLandLord";
            this.nameLandLord.ReadOnly = true;
            // 
            // cAgreements
            // 
            this.cAgreements.DataPropertyName = "Agreement";
            this.cAgreements.HeaderText = "№ договора";
            this.cAgreements.Name = "cAgreements";
            this.cAgreements.ReadOnly = true;
            // 
            // cTypeDoc
            // 
            this.cTypeDoc.DataPropertyName = "TypeContract";
            this.cTypeDoc.HeaderText = "Тип договора";
            this.cTypeDoc.Name = "cTypeDoc";
            this.cTypeDoc.ReadOnly = true;
            // 
            // cStartDiscount
            // 
            this.cStartDiscount.DataPropertyName = "DateStart";
            this.cStartDiscount.HeaderText = "Дата  начала";
            this.cStartDiscount.Name = "cStartDiscount";
            this.cStartDiscount.ReadOnly = true;
            // 
            // cStopDiscount
            // 
            this.cStopDiscount.DataPropertyName = "DateEnd";
            this.cStopDiscount.HeaderText = "Дата окончания";
            this.cStopDiscount.Name = "cStopDiscount";
            this.cStopDiscount.ReadOnly = true;
            // 
            // cTypeDiscount
            // 
            this.cTypeDiscount.DataPropertyName = "nameTypeDiscount";
            this.cTypeDiscount.HeaderText = "Тип скидки";
            this.cTypeDiscount.Name = "cTypeDiscount";
            this.cTypeDiscount.ReadOnly = true;
            // 
            // cSumDiscount
            // 
            this.cSumDiscount.DataPropertyName = "Discount";
            this.cSumDiscount.HeaderText = "Скидка";
            this.cSumDiscount.Name = "cSumDiscount";
            this.cSumDiscount.ReadOnly = true;
            // 
            // cConfirmD
            // 
            this.cConfirmD.DataPropertyName = "nameStatusDiscount";
            this.cConfirmD.HeaderText = "D";
            this.cConfirmD.Name = "cConfirmD";
            this.cConfirmD.ReadOnly = true;
            this.cConfirmD.Visible = false;
            // 
            // chbIsAccept
            // 
            this.chbIsAccept.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chbIsAccept.AutoSize = true;
            this.chbIsAccept.Location = new System.Drawing.Point(39, 441);
            this.chbIsAccept.Name = "chbIsAccept";
            this.chbIsAccept.Size = new System.Drawing.Size(158, 17);
            this.chbIsAccept.TabIndex = 98;
            this.chbIsAccept.Text = "- подтвержденные скидки";
            this.chbIsAccept.UseVisualStyleBackColor = true;
            this.chbIsAccept.CheckedChanged += new System.EventHandler(this.chbNotActive_CheckedChanged);
            // 
            // frmList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1114, 474);
            this.Controls.Add(this.chbIsAccept);
            this.Controls.Add(this.dgvData);
            this.Controls.Add(this.btDeAcceptD);
            this.Controls.Add(this.chbUnlimitedDiscount);
            this.Controls.Add(this.lDateEnd);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dtpEnd);
            this.Controls.Add(this.dtpStart);
            this.Controls.Add(this.cmbTypeContract);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbObject);
            this.Controls.Add(this.lObject);
            this.Controls.Add(this.btConfirmD);
            this.Controls.Add(this.btClose);
            this.Controls.Add(this.pConfirmD);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.chbNotActive);
            this.Controls.Add(this.tbAgreements);
            this.Controls.Add(this.tbLandLord);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmList";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Справочник скидок";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmList_FormClosing);
            this.Load += new System.EventHandler(this.frmList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbLandLord;
        private System.Windows.Forms.CheckBox chbNotActive;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btClose;
        private System.Windows.Forms.Button btConfirmD;
        private System.Windows.Forms.Panel pConfirmD;
        private System.Windows.Forms.ComboBox cmbObject;
        private System.Windows.Forms.Label lObject;
        private System.Windows.Forms.ComboBox cmbTypeContract;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chbUnlimitedDiscount;
        private System.Windows.Forms.Label lDateEnd;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtpEnd;
        private System.Windows.Forms.DateTimePicker dtpStart;
        private System.Windows.Forms.TextBox tbAgreements;
        private System.Windows.Forms.Button btDeAcceptD;
        private System.Windows.Forms.DataGridView dgvData;
        private System.Windows.Forms.CheckBox chbIsAccept;
        private System.Windows.Forms.DataGridViewTextBoxColumn cObject;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameLandLord;
        private System.Windows.Forms.DataGridViewTextBoxColumn cAgreements;
        private System.Windows.Forms.DataGridViewTextBoxColumn cTypeDoc;
        private System.Windows.Forms.DataGridViewTextBoxColumn cStartDiscount;
        private System.Windows.Forms.DataGridViewTextBoxColumn cStopDiscount;
        private System.Windows.Forms.DataGridViewTextBoxColumn cTypeDiscount;
        private System.Windows.Forms.DataGridViewTextBoxColumn cSumDiscount;
        private System.Windows.Forms.DataGridViewTextBoxColumn cConfirmD;
    }
}