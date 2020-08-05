namespace dllArendaDictonary.jDiscount
{
    partial class frmAdd
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btSave = new System.Windows.Forms.Button();
            this.btClose = new System.Windows.Forms.Button();
            this.lTypeDiscont = new System.Windows.Forms.Label();
            this.cmbTypeDicount = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbStab = new System.Windows.Forms.RadioButton();
            this.rbTime = new System.Windows.Forms.RadioButton();
            this.dtpStart = new System.Windows.Forms.DateTimePicker();
            this.dtpEnd = new System.Windows.Forms.DateTimePicker();
            this.lStart = new System.Windows.Forms.Label();
            this.lEnd = new System.Windows.Forms.Label();
            this.lTypeTenant = new System.Windows.Forms.Label();
            this.cmbTypeTenant = new System.Windows.Forms.ComboBox();
            this.lTypeAgreements = new System.Windows.Forms.Label();
            this.cmbTypeAgreements = new System.Windows.Forms.ComboBox();
            this.lPercentDiscount = new System.Windows.Forms.Label();
            this.tbPercentDiscount = new System.Windows.Forms.TextBox();
            this.lDiscountPrice = new System.Windows.Forms.Label();
            this.tbDiscountPrice = new System.Windows.Forms.TextBox();
            this.lPrice = new System.Windows.Forms.Label();
            this.tbPrice = new System.Windows.Forms.TextBox();
            this.lTotalPrice = new System.Windows.Forms.Label();
            this.tbTotalPrice = new System.Windows.Forms.TextBox();
            this.cmbBuilding = new System.Windows.Forms.ComboBox();
            this.cmbObject = new System.Windows.Forms.ComboBox();
            this.lObject = new System.Windows.Forms.Label();
            this.lBuilding = new System.Windows.Forms.Label();
            this.lFloor = new System.Windows.Forms.Label();
            this.cmbFloor = new System.Windows.Forms.ComboBox();
            this.lObjectDiscount = new System.Windows.Forms.Label();
            this.cmbObjectDiscount = new System.Windows.Forms.ComboBox();
            this.chbIsException = new System.Windows.Forms.CheckBox();
            this.lComby = new System.Windows.Forms.Label();
            this.cmbComby = new System.Windows.Forms.ComboBox();
            this.dgvData = new System.Windows.Forms.DataGridView();
            this.cTest = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            this.SuspendLayout();
            // 
            // btSave
            // 
            this.btSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btSave.Image = global::dllArendaDictonary.Properties.Resources.Save;
            this.btSave.Location = new System.Drawing.Point(549, 535);
            this.btSave.Name = "btSave";
            this.btSave.Size = new System.Drawing.Size(32, 32);
            this.btSave.TabIndex = 2;
            this.btSave.UseVisualStyleBackColor = true;
            this.btSave.Click += new System.EventHandler(this.btSave_Click);
            // 
            // btClose
            // 
            this.btClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btClose.Image = global::dllArendaDictonary.Properties.Resources.Exit;
            this.btClose.Location = new System.Drawing.Point(587, 535);
            this.btClose.Name = "btClose";
            this.btClose.Size = new System.Drawing.Size(32, 32);
            this.btClose.TabIndex = 3;
            this.btClose.UseVisualStyleBackColor = true;
            this.btClose.Click += new System.EventHandler(this.btClose_Click);
            // 
            // lTypeDiscont
            // 
            this.lTypeDiscont.AutoSize = true;
            this.lTypeDiscont.Location = new System.Drawing.Point(40, 70);
            this.lTypeDiscont.Name = "lTypeDiscont";
            this.lTypeDiscont.Size = new System.Drawing.Size(65, 13);
            this.lTypeDiscont.TabIndex = 6;
            this.lTypeDiscont.Text = "Тип скидки";
            // 
            // cmbTypeDicount
            // 
            this.cmbTypeDicount.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTypeDicount.FormattingEnabled = true;
            this.cmbTypeDicount.Location = new System.Drawing.Point(117, 66);
            this.cmbTypeDicount.Name = "cmbTypeDicount";
            this.cmbTypeDicount.Size = new System.Drawing.Size(413, 21);
            this.cmbTypeDicount.TabIndex = 7;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbStab);
            this.groupBox1.Controls.Add(this.rbTime);
            this.groupBox1.Location = new System.Drawing.Point(20, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(277, 48);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Временной тип скидки";
            // 
            // rbStab
            // 
            this.rbStab.AutoSize = true;
            this.rbStab.Location = new System.Drawing.Point(136, 19);
            this.rbStab.Name = "rbStab";
            this.rbStab.Size = new System.Drawing.Size(125, 17);
            this.rbStab.TabIndex = 9;
            this.rbStab.Text = "Постоянная скидка";
            this.rbStab.UseVisualStyleBackColor = true;
            this.rbStab.Click += new System.EventHandler(this.rbTime_Click);
            // 
            // rbTime
            // 
            this.rbTime.AutoSize = true;
            this.rbTime.Checked = true;
            this.rbTime.Location = new System.Drawing.Point(6, 19);
            this.rbTime.Name = "rbTime";
            this.rbTime.Size = new System.Drawing.Size(121, 17);
            this.rbTime.TabIndex = 9;
            this.rbTime.TabStop = true;
            this.rbTime.Text = "Временная скидка";
            this.rbTime.UseVisualStyleBackColor = true;
            this.rbTime.Click += new System.EventHandler(this.rbTime_Click);
            // 
            // dtpStart
            // 
            this.dtpStart.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpStart.Location = new System.Drawing.Point(323, 27);
            this.dtpStart.Name = "dtpStart";
            this.dtpStart.Size = new System.Drawing.Size(88, 20);
            this.dtpStart.TabIndex = 9;
            // 
            // dtpEnd
            // 
            this.dtpEnd.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpEnd.Location = new System.Drawing.Point(442, 27);
            this.dtpEnd.Name = "dtpEnd";
            this.dtpEnd.Size = new System.Drawing.Size(88, 20);
            this.dtpEnd.TabIndex = 9;
            // 
            // lStart
            // 
            this.lStart.AutoSize = true;
            this.lStart.Location = new System.Drawing.Point(304, 31);
            this.lStart.Name = "lStart";
            this.lStart.Size = new System.Drawing.Size(13, 13);
            this.lStart.TabIndex = 10;
            this.lStart.Text = "с";
            // 
            // lEnd
            // 
            this.lEnd.AutoSize = true;
            this.lEnd.Location = new System.Drawing.Point(417, 31);
            this.lEnd.Name = "lEnd";
            this.lEnd.Size = new System.Drawing.Size(19, 13);
            this.lEnd.TabIndex = 10;
            this.lEnd.Text = "по";
            // 
            // lTypeTenant
            // 
            this.lTypeTenant.AutoSize = true;
            this.lTypeTenant.Location = new System.Drawing.Point(17, 97);
            this.lTypeTenant.Name = "lTypeTenant";
            this.lTypeTenant.Size = new System.Drawing.Size(88, 13);
            this.lTypeTenant.TabIndex = 6;
            this.lTypeTenant.Text = "Тип арендатора";
            // 
            // cmbTypeTenant
            // 
            this.cmbTypeTenant.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTypeTenant.FormattingEnabled = true;
            this.cmbTypeTenant.Location = new System.Drawing.Point(117, 93);
            this.cmbTypeTenant.Name = "cmbTypeTenant";
            this.cmbTypeTenant.Size = new System.Drawing.Size(413, 21);
            this.cmbTypeTenant.TabIndex = 7;
            // 
            // lTypeAgreements
            // 
            this.lTypeAgreements.AutoSize = true;
            this.lTypeAgreements.Location = new System.Drawing.Point(29, 124);
            this.lTypeAgreements.Name = "lTypeAgreements";
            this.lTypeAgreements.Size = new System.Drawing.Size(76, 13);
            this.lTypeAgreements.TabIndex = 6;
            this.lTypeAgreements.Text = "Тип договора";
            // 
            // cmbTypeAgreements
            // 
            this.cmbTypeAgreements.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTypeAgreements.FormattingEnabled = true;
            this.cmbTypeAgreements.Location = new System.Drawing.Point(117, 120);
            this.cmbTypeAgreements.Name = "cmbTypeAgreements";
            this.cmbTypeAgreements.Size = new System.Drawing.Size(413, 21);
            this.cmbTypeAgreements.TabIndex = 7;
            // 
            // lPercentDiscount
            // 
            this.lPercentDiscount.AutoSize = true;
            this.lPercentDiscount.Location = new System.Drawing.Point(12, 164);
            this.lPercentDiscount.Name = "lPercentDiscount";
            this.lPercentDiscount.Size = new System.Drawing.Size(246, 13);
            this.lPercentDiscount.TabIndex = 11;
            this.lPercentDiscount.Text = "Процент скидки от общей стоимости договора";
            // 
            // tbPercentDiscount
            // 
            this.tbPercentDiscount.Location = new System.Drawing.Point(411, 160);
            this.tbPercentDiscount.MaxLength = 15;
            this.tbPercentDiscount.Name = "tbPercentDiscount";
            this.tbPercentDiscount.Size = new System.Drawing.Size(119, 20);
            this.tbPercentDiscount.TabIndex = 12;
            this.tbPercentDiscount.Text = "0,00";
            this.tbPercentDiscount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbPercentDiscount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbDecimal_KeyPress);
            this.tbPercentDiscount.Validating += new System.ComponentModel.CancelEventHandler(this.tbPercentDiscount_Validating);
            // 
            // lDiscountPrice
            // 
            this.lDiscountPrice.AutoSize = true;
            this.lDiscountPrice.Location = new System.Drawing.Point(12, 190);
            this.lDiscountPrice.Name = "lDiscountPrice";
            this.lDiscountPrice.Size = new System.Drawing.Size(227, 13);
            this.lDiscountPrice.TabIndex = 11;
            this.lDiscountPrice.Text = "Новая цена стоимости 1 квадратного мета";
            // 
            // tbDiscountPrice
            // 
            this.tbDiscountPrice.Location = new System.Drawing.Point(411, 186);
            this.tbDiscountPrice.MaxLength = 15;
            this.tbDiscountPrice.Name = "tbDiscountPrice";
            this.tbDiscountPrice.Size = new System.Drawing.Size(119, 20);
            this.tbDiscountPrice.TabIndex = 12;
            this.tbDiscountPrice.Text = "0,00";
            this.tbDiscountPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbDiscountPrice.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbDecimal_KeyPress);
            this.tbDiscountPrice.Validating += new System.ComponentModel.CancelEventHandler(this.tbPercentDiscount_Validating);
            // 
            // lPrice
            // 
            this.lPrice.AutoSize = true;
            this.lPrice.Location = new System.Drawing.Point(12, 216);
            this.lPrice.Name = "lPrice";
            this.lPrice.Size = new System.Drawing.Size(373, 13);
            this.lPrice.TabIndex = 11;
            this.lPrice.Text = "Цена 1 квадратного метра, при которой договора попадают под скидки";
            // 
            // tbPrice
            // 
            this.tbPrice.Location = new System.Drawing.Point(411, 212);
            this.tbPrice.MaxLength = 15;
            this.tbPrice.Name = "tbPrice";
            this.tbPrice.Size = new System.Drawing.Size(119, 20);
            this.tbPrice.TabIndex = 12;
            this.tbPrice.Text = "0,00";
            this.tbPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbPrice.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbDecimal_KeyPress);
            this.tbPrice.Validating += new System.ComponentModel.CancelEventHandler(this.tbPercentDiscount_Validating);
            // 
            // lTotalPrice
            // 
            this.lTotalPrice.AutoSize = true;
            this.lTotalPrice.Location = new System.Drawing.Point(12, 242);
            this.lTotalPrice.Name = "lTotalPrice";
            this.lTotalPrice.Size = new System.Drawing.Size(394, 13);
            this.lTotalPrice.TabIndex = 11;
            this.lTotalPrice.Text = "Общая Стоимость по договору, при которой договора попадают под скидки";
            // 
            // tbTotalPrice
            // 
            this.tbTotalPrice.Location = new System.Drawing.Point(411, 238);
            this.tbTotalPrice.MaxLength = 15;
            this.tbTotalPrice.Name = "tbTotalPrice";
            this.tbTotalPrice.Size = new System.Drawing.Size(119, 20);
            this.tbTotalPrice.TabIndex = 12;
            this.tbTotalPrice.Text = "0,00";
            this.tbTotalPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbTotalPrice.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbDecimal_KeyPress);
            this.tbTotalPrice.Validating += new System.ComponentModel.CancelEventHandler(this.tbPercentDiscount_Validating);
            // 
            // cmbBuilding
            // 
            this.cmbBuilding.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBuilding.FormattingEnabled = true;
            this.cmbBuilding.Location = new System.Drawing.Point(282, 303);
            this.cmbBuilding.Name = "cmbBuilding";
            this.cmbBuilding.Size = new System.Drawing.Size(148, 21);
            this.cmbBuilding.TabIndex = 15;
            this.cmbBuilding.Visible = false;
            this.cmbBuilding.SelectionChangeCommitted += new System.EventHandler(this.cmbBuilding_SelectionChangeCommitted);
            // 
            // cmbObject
            // 
            this.cmbObject.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbObject.FormattingEnabled = true;
            this.cmbObject.Location = new System.Drawing.Point(67, 303);
            this.cmbObject.Name = "cmbObject";
            this.cmbObject.Size = new System.Drawing.Size(148, 21);
            this.cmbObject.TabIndex = 16;
            this.cmbObject.Visible = false;
            this.cmbObject.SelectionChangeCommitted += new System.EventHandler(this.cmbObject_SelectionChangeCommitted);
            // 
            // lObject
            // 
            this.lObject.AutoSize = true;
            this.lObject.Location = new System.Drawing.Point(11, 307);
            this.lObject.Name = "lObject";
            this.lObject.Size = new System.Drawing.Size(45, 13);
            this.lObject.TabIndex = 13;
            this.lObject.Text = "Объект";
            this.lObject.Visible = false;
            // 
            // lBuilding
            // 
            this.lBuilding.AutoSize = true;
            this.lBuilding.Location = new System.Drawing.Point(227, 307);
            this.lBuilding.Name = "lBuilding";
            this.lBuilding.Size = new System.Drawing.Size(44, 13);
            this.lBuilding.TabIndex = 14;
            this.lBuilding.Text = "Здание";
            this.lBuilding.Visible = false;
            // 
            // lFloor
            // 
            this.lFloor.AutoSize = true;
            this.lFloor.Location = new System.Drawing.Point(437, 307);
            this.lFloor.Name = "lFloor";
            this.lFloor.Size = new System.Drawing.Size(33, 13);
            this.lFloor.TabIndex = 14;
            this.lFloor.Text = "Этаж";
            this.lFloor.Visible = false;
            // 
            // cmbFloor
            // 
            this.cmbFloor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFloor.FormattingEnabled = true;
            this.cmbFloor.Location = new System.Drawing.Point(476, 303);
            this.cmbFloor.Name = "cmbFloor";
            this.cmbFloor.Size = new System.Drawing.Size(148, 21);
            this.cmbFloor.TabIndex = 15;
            this.cmbFloor.Visible = false;
            this.cmbFloor.SelectionChangeCommitted += new System.EventHandler(this.cmbBuilding_SelectionChangeCommitted);
            // 
            // lObjectDiscount
            // 
            this.lObjectDiscount.AutoSize = true;
            this.lObjectDiscount.Location = new System.Drawing.Point(15, 279);
            this.lObjectDiscount.Name = "lObjectDiscount";
            this.lObjectDiscount.Size = new System.Drawing.Size(84, 13);
            this.lObjectDiscount.TabIndex = 14;
            this.lObjectDiscount.Text = "Объект скидки";
            // 
            // cmbObjectDiscount
            // 
            this.cmbObjectDiscount.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbObjectDiscount.FormattingEnabled = true;
            this.cmbObjectDiscount.Location = new System.Drawing.Point(110, 276);
            this.cmbObjectDiscount.Name = "cmbObjectDiscount";
            this.cmbObjectDiscount.Size = new System.Drawing.Size(148, 21);
            this.cmbObjectDiscount.TabIndex = 15;
            this.cmbObjectDiscount.SelectionChangeCommitted += new System.EventHandler(this.cmbObjectDiscount_SelectionChangeCommitted);
            // 
            // chbIsException
            // 
            this.chbIsException.AutoSize = true;
            this.chbIsException.Location = new System.Drawing.Point(203, 544);
            this.chbIsException.Name = "chbIsException";
            this.chbIsException.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chbIsException.Size = new System.Drawing.Size(259, 17);
            this.chbIsException.TabIndex = 17;
            this.chbIsException.Text = "Включения объекта в скидку или исключения";
            this.chbIsException.UseVisualStyleBackColor = true;
            this.chbIsException.Visible = false;
            // 
            // lComby
            // 
            this.lComby.Location = new System.Drawing.Point(6, 545);
            this.lComby.Name = "lComby";
            this.lComby.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lComby.Size = new System.Drawing.Size(109, 18);
            this.lComby.TabIndex = 14;
            this.lComby.Text = ".";
            this.lComby.Visible = false;
            // 
            // cmbComby
            // 
            this.cmbComby.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbComby.FormattingEnabled = true;
            this.cmbComby.Location = new System.Drawing.Point(121, 542);
            this.cmbComby.Name = "cmbComby";
            this.cmbComby.Size = new System.Drawing.Size(76, 21);
            this.cmbComby.TabIndex = 15;
            this.cmbComby.Visible = false;
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
            this.cTest});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvData.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvData.Location = new System.Drawing.Point(12, 330);
            this.dgvData.MultiSelect = false;
            this.dgvData.Name = "dgvData";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvData.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvData.RowHeadersVisible = false;
            this.dgvData.Size = new System.Drawing.Size(612, 199);
            this.dgvData.TabIndex = 18;
            this.dgvData.Visible = false;
            this.dgvData.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvData_CellContentClick);
            // 
            // cTest
            // 
            this.cTest.HeaderText = "test";
            this.cTest.Name = "cTest";
            // 
            // frmAdd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(631, 579);
            this.Controls.Add(this.dgvData);
            this.Controls.Add(this.chbIsException);
            this.Controls.Add(this.cmbComby);
            this.Controls.Add(this.cmbObjectDiscount);
            this.Controls.Add(this.cmbFloor);
            this.Controls.Add(this.lComby);
            this.Controls.Add(this.cmbBuilding);
            this.Controls.Add(this.lObjectDiscount);
            this.Controls.Add(this.cmbObject);
            this.Controls.Add(this.lFloor);
            this.Controls.Add(this.lObject);
            this.Controls.Add(this.lBuilding);
            this.Controls.Add(this.tbTotalPrice);
            this.Controls.Add(this.lTotalPrice);
            this.Controls.Add(this.tbPrice);
            this.Controls.Add(this.lPrice);
            this.Controls.Add(this.tbDiscountPrice);
            this.Controls.Add(this.lDiscountPrice);
            this.Controls.Add(this.tbPercentDiscount);
            this.Controls.Add(this.lPercentDiscount);
            this.Controls.Add(this.lEnd);
            this.Controls.Add(this.lStart);
            this.Controls.Add(this.dtpEnd);
            this.Controls.Add(this.dtpStart);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cmbTypeAgreements);
            this.Controls.Add(this.lTypeAgreements);
            this.Controls.Add(this.cmbTypeTenant);
            this.Controls.Add(this.lTypeTenant);
            this.Controls.Add(this.cmbTypeDicount);
            this.Controls.Add(this.lTypeDiscont);
            this.Controls.Add(this.btSave);
            this.Controls.Add(this.btClose);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAdd";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmAdd";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmAdd_FormClosing);
            this.Load += new System.EventHandler(this.frmAdd_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btClose;
        private System.Windows.Forms.Button btSave;
        private System.Windows.Forms.Label lTypeDiscont;
        private System.Windows.Forms.ComboBox cmbTypeDicount;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbStab;
        private System.Windows.Forms.RadioButton rbTime;
        private System.Windows.Forms.DateTimePicker dtpStart;
        private System.Windows.Forms.DateTimePicker dtpEnd;
        private System.Windows.Forms.Label lStart;
        private System.Windows.Forms.Label lEnd;
        private System.Windows.Forms.Label lTypeTenant;
        private System.Windows.Forms.ComboBox cmbTypeTenant;
        private System.Windows.Forms.Label lTypeAgreements;
        private System.Windows.Forms.ComboBox cmbTypeAgreements;
        private System.Windows.Forms.Label lPercentDiscount;
        private System.Windows.Forms.TextBox tbPercentDiscount;
        private System.Windows.Forms.Label lDiscountPrice;
        private System.Windows.Forms.TextBox tbDiscountPrice;
        private System.Windows.Forms.Label lPrice;
        private System.Windows.Forms.TextBox tbPrice;
        private System.Windows.Forms.Label lTotalPrice;
        private System.Windows.Forms.TextBox tbTotalPrice;
        private System.Windows.Forms.ComboBox cmbBuilding;
        private System.Windows.Forms.ComboBox cmbObject;
        private System.Windows.Forms.Label lObject;
        private System.Windows.Forms.Label lBuilding;
        private System.Windows.Forms.Label lFloor;
        private System.Windows.Forms.ComboBox cmbFloor;
        private System.Windows.Forms.Label lObjectDiscount;
        private System.Windows.Forms.ComboBox cmbObjectDiscount;
        private System.Windows.Forms.CheckBox chbIsException;
        private System.Windows.Forms.Label lComby;
        private System.Windows.Forms.ComboBox cmbComby;
        private System.Windows.Forms.DataGridView dgvData;
        private System.Windows.Forms.DataGridViewTextBoxColumn cTest;
    }
}