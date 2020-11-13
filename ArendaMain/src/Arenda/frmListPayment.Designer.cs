namespace Arenda
{
    partial class frmListPayment
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmListPayment));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lblNum = new System.Windows.Forms.Label();
            this.lblDate = new System.Windows.Forms.Label();
            this.lblTenant = new System.Windows.Forms.Label();
            this.lblSum = new System.Windows.Forms.Label();
            this.txtNum = new System.Windows.Forms.TextBox();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.txtTenant = new System.Windows.Forms.TextBox();
            this.txtSum = new System.Windows.Forms.TextBox();
            this.lblRub = new System.Windows.Forms.Label();
            this.grdPayments = new System.Windows.Forms.DataGridView();
            this.lblItog = new System.Windows.Forms.Label();
            this.txtItog = new System.Windows.Forms.TextBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnDel = new System.Windows.Forms.Button();
            this.btnQuit = new System.Windows.Forms.Button();
            this.btnExcel = new System.Windows.Forms.Button();
            this.lblEditor = new System.Windows.Forms.Label();
            this.lblDateEdit = new System.Windows.Forms.Label();
            this.txtEditor = new System.Windows.Forms.TextBox();
            this.txtDateEdit = new System.Windows.Forms.TextBox();
            this.PaymentDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cPlaneDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PaymentSum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sign = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cTypePay = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cIsTypeCash = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cTypeOperation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id_payment = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id_Agreements = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.isPayment = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DateEdit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id_Editor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Editor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tbType = new System.Windows.Forms.TextBox();
            this.tbSumm = new System.Windows.Forms.TextBox();
            this.tbMonth = new System.Windows.Forms.TextBox();
            this.tbDateCreate = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.grdPayments)).BeginInit();
            this.SuspendLayout();
            // 
            // lblNum
            // 
            this.lblNum.AutoSize = true;
            this.lblNum.Location = new System.Drawing.Point(12, 7);
            this.lblNum.Name = "lblNum";
            this.lblNum.Size = new System.Drawing.Size(71, 13);
            this.lblNum.TabIndex = 0;
            this.lblNum.Text = "№ договора:";
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.Location = new System.Drawing.Point(195, 7);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(86, 13);
            this.lblDate.TabIndex = 1;
            this.lblDate.Text = "Дата договора:";
            // 
            // lblTenant
            // 
            this.lblTenant.AutoSize = true;
            this.lblTenant.Location = new System.Drawing.Point(12, 32);
            this.lblTenant.Name = "lblTenant";
            this.lblTenant.Size = new System.Drawing.Size(64, 13);
            this.lblTenant.TabIndex = 2;
            this.lblTenant.Text = "Арендатор:";
            // 
            // lblSum
            // 
            this.lblSum.AutoSize = true;
            this.lblSum.Location = new System.Drawing.Point(12, 61);
            this.lblSum.Name = "lblSum";
            this.lblSum.Size = new System.Drawing.Size(47, 13);
            this.lblSum.TabIndex = 3;
            this.lblSum.Text = "Аренда:";
            // 
            // txtNum
            // 
            this.txtNum.Location = new System.Drawing.Point(89, 4);
            this.txtNum.Name = "txtNum";
            this.txtNum.ReadOnly = true;
            this.txtNum.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtNum.Size = new System.Drawing.Size(100, 20);
            this.txtNum.TabIndex = 4;
            this.txtNum.TabStop = false;
            // 
            // dtpDate
            // 
            this.dtpDate.Enabled = false;
            this.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDate.Location = new System.Drawing.Point(287, 4);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(91, 20);
            this.dtpDate.TabIndex = 5;
            this.dtpDate.TabStop = false;
            // 
            // txtTenant
            // 
            this.txtTenant.Location = new System.Drawing.Point(89, 29);
            this.txtTenant.Name = "txtTenant";
            this.txtTenant.ReadOnly = true;
            this.txtTenant.Size = new System.Drawing.Size(289, 20);
            this.txtTenant.TabIndex = 6;
            this.txtTenant.TabStop = false;
            // 
            // txtSum
            // 
            this.txtSum.Location = new System.Drawing.Point(89, 58);
            this.txtSum.Name = "txtSum";
            this.txtSum.ReadOnly = true;
            this.txtSum.Size = new System.Drawing.Size(256, 20);
            this.txtSum.TabIndex = 7;
            this.txtSum.TabStop = false;
            this.txtSum.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblRub
            // 
            this.lblRub.AutoSize = true;
            this.lblRub.Location = new System.Drawing.Point(351, 61);
            this.lblRub.Name = "lblRub";
            this.lblRub.Size = new System.Drawing.Size(27, 13);
            this.lblRub.TabIndex = 8;
            this.lblRub.Text = "руб.";
            // 
            // grdPayments
            // 
            this.grdPayments.AllowUserToAddRows = false;
            this.grdPayments.AllowUserToDeleteRows = false;
            this.grdPayments.AllowUserToResizeColumns = false;
            this.grdPayments.AllowUserToResizeRows = false;
            this.grdPayments.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdPayments.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.grdPayments.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grdPayments.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.grdPayments.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdPayments.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.PaymentDate,
            this.cPlaneDate,
            this.PaymentSum,
            this.sign,
            this.cTypePay,
            this.cIsTypeCash,
            this.cTypeOperation,
            this.id_payment,
            this.id_Agreements,
            this.isPayment,
            this.DateEdit,
            this.id_Editor,
            this.Editor});
            this.grdPayments.Location = new System.Drawing.Point(15, 84);
            this.grdPayments.MultiSelect = false;
            this.grdPayments.Name = "grdPayments";
            this.grdPayments.ReadOnly = true;
            this.grdPayments.RowHeadersVisible = false;
            this.grdPayments.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdPayments.Size = new System.Drawing.Size(661, 258);
            this.grdPayments.TabIndex = 1;
            this.grdPayments.SelectionChanged += new System.EventHandler(this.grdPayments_SelectionChanged);
            // 
            // lblItog
            // 
            this.lblItog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblItog.AutoSize = true;
            this.lblItog.Location = new System.Drawing.Point(498, 351);
            this.lblItog.Name = "lblItog";
            this.lblItog.Size = new System.Drawing.Size(40, 13);
            this.lblItog.TabIndex = 33;
            this.lblItog.Text = "Итого:";
            // 
            // txtItog
            // 
            this.txtItog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtItog.Location = new System.Drawing.Point(544, 348);
            this.txtItog.Name = "txtItog";
            this.txtItog.ReadOnly = true;
            this.txtItog.Size = new System.Drawing.Size(132, 20);
            this.txtItog.TabIndex = 34;
            this.txtItog.TabStop = false;
            this.txtItog.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.Image = ((System.Drawing.Image)(resources.GetObject("btnAdd.Image")));
            this.btnAdd.Location = new System.Drawing.Point(538, 428);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(30, 30);
            this.btnAdd.TabIndex = 2;
            this.toolTip1.SetToolTip(this.btnAdd, "Добавить");
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEdit.Image = ((System.Drawing.Image)(resources.GetObject("btnEdit.Image")));
            this.btnEdit.Location = new System.Drawing.Point(487, 428);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(30, 30);
            this.btnEdit.TabIndex = 3;
            this.toolTip1.SetToolTip(this.btnEdit, "Редактировать");
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Visible = false;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnDel
            // 
            this.btnDel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDel.Image = ((System.Drawing.Image)(resources.GetObject("btnDel.Image")));
            this.btnDel.Location = new System.Drawing.Point(610, 428);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(30, 30);
            this.btnDel.TabIndex = 4;
            this.toolTip1.SetToolTip(this.btnDel, "Удалить");
            this.btnDel.UseVisualStyleBackColor = true;
            this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
            // 
            // btnQuit
            // 
            this.btnQuit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnQuit.Image = ((System.Drawing.Image)(resources.GetObject("btnQuit.Image")));
            this.btnQuit.Location = new System.Drawing.Point(646, 428);
            this.btnQuit.Name = "btnQuit";
            this.btnQuit.Size = new System.Drawing.Size(30, 30);
            this.btnQuit.TabIndex = 5;
            this.toolTip1.SetToolTip(this.btnQuit, "Выход");
            this.btnQuit.UseVisualStyleBackColor = true;
            this.btnQuit.Click += new System.EventHandler(this.btnQuit_Click);
            // 
            // btnExcel
            // 
            this.btnExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExcel.Image = global::Arenda.Properties.Resources.pict_excel;
            this.btnExcel.Location = new System.Drawing.Point(574, 428);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(30, 30);
            this.btnExcel.TabIndex = 39;
            this.toolTip1.SetToolTip(this.btnExcel, "Список оплат по договору");
            this.btnExcel.UseVisualStyleBackColor = true;
            this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
            // 
            // lblEditor
            // 
            this.lblEditor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblEditor.AutoSize = true;
            this.lblEditor.Location = new System.Drawing.Point(418, 380);
            this.lblEditor.Name = "lblEditor";
            this.lblEditor.Size = new System.Drawing.Size(49, 13);
            this.lblEditor.TabIndex = 35;
            this.lblEditor.Text = "Редакт.:";
            // 
            // lblDateEdit
            // 
            this.lblDateEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDateEdit.AutoSize = true;
            this.lblDateEdit.Location = new System.Drawing.Point(407, 406);
            this.lblDateEdit.Name = "lblDateEdit";
            this.lblDateEdit.Size = new System.Drawing.Size(60, 13);
            this.lblDateEdit.TabIndex = 36;
            this.lblDateEdit.Text = "Дата ред.:";
            // 
            // txtEditor
            // 
            this.txtEditor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtEditor.Location = new System.Drawing.Point(473, 376);
            this.txtEditor.Name = "txtEditor";
            this.txtEditor.ReadOnly = true;
            this.txtEditor.Size = new System.Drawing.Size(203, 20);
            this.txtEditor.TabIndex = 37;
            this.txtEditor.TabStop = false;
            // 
            // txtDateEdit
            // 
            this.txtDateEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDateEdit.Location = new System.Drawing.Point(473, 402);
            this.txtDateEdit.Name = "txtDateEdit";
            this.txtDateEdit.ReadOnly = true;
            this.txtDateEdit.Size = new System.Drawing.Size(203, 20);
            this.txtDateEdit.TabIndex = 38;
            this.txtDateEdit.TabStop = false;
            // 
            // PaymentDate
            // 
            this.PaymentDate.DataPropertyName = "PaymentDate";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.PaymentDate.DefaultCellStyle = dataGridViewCellStyle8;
            this.PaymentDate.HeaderText = "Дата оплаты";
            this.PaymentDate.Name = "PaymentDate";
            this.PaymentDate.ReadOnly = true;
            // 
            // cPlaneDate
            // 
            this.cPlaneDate.DataPropertyName = "planedate";
            this.cPlaneDate.HeaderText = "План";
            this.cPlaneDate.Name = "cPlaneDate";
            this.cPlaneDate.ReadOnly = true;
            // 
            // PaymentSum
            // 
            this.PaymentSum.DataPropertyName = "PaymentSum";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle9.Format = "N2";
            dataGridViewCellStyle9.NullValue = "0";
            this.PaymentSum.DefaultCellStyle = dataGridViewCellStyle9;
            this.PaymentSum.HeaderText = "Сумма оплаты";
            this.PaymentSum.Name = "PaymentSum";
            this.PaymentSum.ReadOnly = true;
            // 
            // sign
            // 
            this.sign.DataPropertyName = "sign";
            this.sign.HeaderText = "Признак";
            this.sign.Name = "sign";
            this.sign.ReadOnly = true;
            this.sign.Visible = false;
            // 
            // cTypePay
            // 
            this.cTypePay.DataPropertyName = "namePaymentType";
            this.cTypePay.HeaderText = "Тип платежа";
            this.cTypePay.Name = "cTypePay";
            this.cTypePay.ReadOnly = true;
            // 
            // cIsTypeCash
            // 
            this.cIsTypeCash.DataPropertyName = "typeCash";
            this.cIsTypeCash.HeaderText = "Нал./Безнал.";
            this.cIsTypeCash.Name = "cIsTypeCash";
            this.cIsTypeCash.ReadOnly = true;
            // 
            // cTypeOperation
            // 
            this.cTypeOperation.DataPropertyName = "typeTenant";
            this.cTypeOperation.HeaderText = "Тип операции";
            this.cTypeOperation.Name = "cTypeOperation";
            this.cTypeOperation.ReadOnly = true;
            // 
            // id_payment
            // 
            this.id_payment.DataPropertyName = "id";
            this.id_payment.HeaderText = "id_payment";
            this.id_payment.Name = "id_payment";
            this.id_payment.ReadOnly = true;
            this.id_payment.Visible = false;
            // 
            // id_Agreements
            // 
            this.id_Agreements.DataPropertyName = "id_Agreements";
            this.id_Agreements.HeaderText = "id_Agreements";
            this.id_Agreements.Name = "id_Agreements";
            this.id_Agreements.ReadOnly = true;
            this.id_Agreements.Visible = false;
            // 
            // isPayment
            // 
            this.isPayment.DataPropertyName = "isPayment";
            this.isPayment.HeaderText = "isPayment";
            this.isPayment.Name = "isPayment";
            this.isPayment.ReadOnly = true;
            this.isPayment.Visible = false;
            // 
            // DateEdit
            // 
            this.DateEdit.DataPropertyName = "DateEdit";
            this.DateEdit.HeaderText = "DateEdit";
            this.DateEdit.Name = "DateEdit";
            this.DateEdit.ReadOnly = true;
            this.DateEdit.Visible = false;
            // 
            // id_Editor
            // 
            this.id_Editor.DataPropertyName = "id_Editor";
            this.id_Editor.HeaderText = "id_Editor";
            this.id_Editor.Name = "id_Editor";
            this.id_Editor.ReadOnly = true;
            this.id_Editor.Visible = false;
            // 
            // Editor
            // 
            this.Editor.DataPropertyName = "Editor";
            this.Editor.HeaderText = "Editor";
            this.Editor.Name = "Editor";
            this.Editor.ReadOnly = true;
            this.Editor.Visible = false;
            // 
            // tbType
            // 
            this.tbType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tbType.Location = new System.Drawing.Point(113, 351);
            this.tbType.Name = "tbType";
            this.tbType.ReadOnly = true;
            this.tbType.Size = new System.Drawing.Size(168, 20);
            this.tbType.TabIndex = 40;
            this.tbType.TabStop = false;
            // 
            // tbSumm
            // 
            this.tbSumm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tbSumm.Location = new System.Drawing.Point(113, 380);
            this.tbSumm.Name = "tbSumm";
            this.tbSumm.ReadOnly = true;
            this.tbSumm.Size = new System.Drawing.Size(88, 20);
            this.tbSumm.TabIndex = 41;
            this.tbSumm.TabStop = false;
            // 
            // tbMonth
            // 
            this.tbMonth.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tbMonth.Location = new System.Drawing.Point(113, 408);
            this.tbMonth.Name = "tbMonth";
            this.tbMonth.ReadOnly = true;
            this.tbMonth.Size = new System.Drawing.Size(88, 20);
            this.tbMonth.TabIndex = 42;
            this.tbMonth.TabStop = false;
            // 
            // tbDateCreate
            // 
            this.tbDateCreate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tbDateCreate.Location = new System.Drawing.Point(113, 438);
            this.tbDateCreate.Name = "tbDateCreate";
            this.tbDateCreate.ReadOnly = true;
            this.tbDateCreate.Size = new System.Drawing.Size(88, 20);
            this.tbDateCreate.TabIndex = 43;
            this.tbDateCreate.TabStop = false;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(64, 354);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 44;
            this.label1.Text = "Тип:";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(63, 383);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 45;
            this.label2.Text = "Сумма:";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(64, 411);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 13);
            this.label3.TabIndex = 46;
            this.label3.Text = "Месяц:";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(20, 441);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(87, 13);
            this.label4.TabIndex = 47;
            this.label4.Text = "Дата создания:";
            // 
            // frmListPayment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(688, 470);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbDateCreate);
            this.Controls.Add(this.tbMonth);
            this.Controls.Add(this.tbSumm);
            this.Controls.Add(this.tbType);
            this.Controls.Add(this.btnExcel);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnDel);
            this.Controls.Add(this.btnQuit);
            this.Controls.Add(this.txtDateEdit);
            this.Controls.Add(this.txtEditor);
            this.Controls.Add(this.lblDateEdit);
            this.Controls.Add(this.lblEditor);
            this.Controls.Add(this.txtItog);
            this.Controls.Add(this.lblItog);
            this.Controls.Add(this.grdPayments);
            this.Controls.Add(this.lblRub);
            this.Controls.Add(this.txtSum);
            this.Controls.Add(this.txtTenant);
            this.Controls.Add(this.dtpDate);
            this.Controls.Add(this.txtNum);
            this.Controls.Add(this.lblSum);
            this.Controls.Add(this.lblTenant);
            this.Controls.Add(this.lblDate);
            this.Controls.Add(this.lblNum);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "frmListPayment";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Список оплат договора";
            this.Load += new System.EventHandler(this.frmListPayment_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmListPayment_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.grdPayments)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblNum;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.Label lblTenant;
        private System.Windows.Forms.Label lblSum;
        private System.Windows.Forms.TextBox txtNum;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.TextBox txtTenant;
        private System.Windows.Forms.TextBox txtSum;
        private System.Windows.Forms.Label lblRub;
        private System.Windows.Forms.DataGridView grdPayments;
        private System.Windows.Forms.Label lblItog;
        private System.Windows.Forms.TextBox txtItog;
        private System.Windows.Forms.Button btnQuit;
        private System.Windows.Forms.Button btnDel;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label lblEditor;
        private System.Windows.Forms.Label lblDateEdit;
        private System.Windows.Forms.TextBox txtEditor;
        private System.Windows.Forms.TextBox txtDateEdit;
        private System.Windows.Forms.Button btnExcel;
        private System.Windows.Forms.DataGridViewTextBoxColumn PaymentDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn cPlaneDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn PaymentSum;
        private System.Windows.Forms.DataGridViewTextBoxColumn sign;
        private System.Windows.Forms.DataGridViewTextBoxColumn cTypePay;
        private System.Windows.Forms.DataGridViewTextBoxColumn cIsTypeCash;
        private System.Windows.Forms.DataGridViewTextBoxColumn cTypeOperation;
        private System.Windows.Forms.DataGridViewTextBoxColumn id_payment;
        private System.Windows.Forms.DataGridViewTextBoxColumn id_Agreements;
        private System.Windows.Forms.DataGridViewTextBoxColumn isPayment;
        private System.Windows.Forms.DataGridViewTextBoxColumn DateEdit;
        private System.Windows.Forms.DataGridViewTextBoxColumn id_Editor;
        private System.Windows.Forms.DataGridViewTextBoxColumn Editor;
        private System.Windows.Forms.TextBox tbType;
        private System.Windows.Forms.TextBox tbSumm;
        private System.Windows.Forms.TextBox tbMonth;
        private System.Windows.Forms.TextBox tbDateCreate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
    }
}