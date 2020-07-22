namespace Arenda
{
    partial class frmListTaxes
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmListTaxes));
            this.txtTenant = new System.Windows.Forms.TextBox();
            this.txtNum = new System.Windows.Forms.TextBox();
            this.lblTenant = new System.Windows.Forms.Label();
            this.lblNum = new System.Windows.Forms.Label();
            this.grdPayments = new System.Windows.Forms.DataGridView();
            this.TaxDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PaymentName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.penalty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PaymentSum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Debt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.scan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id_tax = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id_Agreements = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Comment = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DateEdit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id_Editor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Editor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PaymentId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblItog = new System.Windows.Forms.Label();
            this.txtPenalty = new System.Windows.Forms.TextBox();
            this.txtPayment = new System.Windows.Forms.TextBox();
            this.txtDebt = new System.Windows.Forms.TextBox();
            this.txtComment = new System.Windows.Forms.TextBox();
            this.txtDateEdit = new System.Windows.Forms.TextBox();
            this.txtEditor = new System.Windows.Forms.TextBox();
            this.lblDateEdit = new System.Windows.Forms.Label();
            this.lblEditor = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btnScan = new System.Windows.Forms.Button();
            this.btnPay = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnDel = new System.Windows.Forms.Button();
            this.btnQuit = new System.Windows.Forms.Button();
            this.lblComment = new System.Windows.Forms.Label();
            this.cboAnotherPay = new System.Windows.Forms.ComboBox();
            this.lblAnotherPay = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.grdPayments)).BeginInit();
            this.SuspendLayout();
            // 
            // txtTenant
            // 
            this.txtTenant.Location = new System.Drawing.Point(89, 31);
            this.txtTenant.Name = "txtTenant";
            this.txtTenant.ReadOnly = true;
            this.txtTenant.Size = new System.Drawing.Size(393, 20);
            this.txtTenant.TabIndex = 10;
            this.txtTenant.TabStop = false;
            // 
            // txtNum
            // 
            this.txtNum.Location = new System.Drawing.Point(89, 6);
            this.txtNum.Name = "txtNum";
            this.txtNum.ReadOnly = true;
            this.txtNum.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtNum.Size = new System.Drawing.Size(156, 20);
            this.txtNum.TabIndex = 9;
            this.txtNum.TabStop = false;
            // 
            // lblTenant
            // 
            this.lblTenant.AutoSize = true;
            this.lblTenant.Location = new System.Drawing.Point(12, 34);
            this.lblTenant.Name = "lblTenant";
            this.lblTenant.Size = new System.Drawing.Size(64, 13);
            this.lblTenant.TabIndex = 8;
            this.lblTenant.Text = "Арендатор:";
            // 
            // lblNum
            // 
            this.lblNum.AutoSize = true;
            this.lblNum.Location = new System.Drawing.Point(12, 9);
            this.lblNum.Name = "lblNum";
            this.lblNum.Size = new System.Drawing.Size(71, 13);
            this.lblNum.TabIndex = 7;
            this.lblNum.Text = "№ договора:";
            // 
            // grdPayments
            // 
            this.grdPayments.AllowUserToAddRows = false;
            this.grdPayments.AllowUserToDeleteRows = false;
            this.grdPayments.AllowUserToResizeColumns = false;
            this.grdPayments.AllowUserToResizeRows = false;
            this.grdPayments.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.grdPayments.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grdPayments.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.grdPayments.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdPayments.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.TaxDate,
            this.PaymentName,
            this.penalty,
            this.PaymentSum,
            this.Debt,
            this.scan,
            this.id_tax,
            this.id_Agreements,
            this.Comment,
            this.DateEdit,
            this.id_Editor,
            this.Editor,
            this.PaymentId});
            this.grdPayments.Location = new System.Drawing.Point(12, 57);
            this.grdPayments.MultiSelect = false;
            this.grdPayments.Name = "grdPayments";
            this.grdPayments.ReadOnly = true;
            this.grdPayments.RowHeadersVisible = false;
            this.grdPayments.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdPayments.Size = new System.Drawing.Size(470, 258);
            this.grdPayments.TabIndex = 11;
            this.grdPayments.SelectionChanged += new System.EventHandler(this.grdPayments_SelectionChanged);
            // 
            // TaxDate
            // 
            this.TaxDate.DataPropertyName = "TaxDate";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.TaxDate.DefaultCellStyle = dataGridViewCellStyle2;
            this.TaxDate.HeaderText = "Дата выписки";
            this.TaxDate.Name = "TaxDate";
            this.TaxDate.ReadOnly = true;
            // 
            // PaymentName
            // 
            this.PaymentName.DataPropertyName = "PaymentName";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.PaymentName.DefaultCellStyle = dataGridViewCellStyle3;
            this.PaymentName.HeaderText = "Доп. оплата";
            this.PaymentName.Name = "PaymentName";
            this.PaymentName.ReadOnly = true;
            // 
            // penalty
            // 
            this.penalty.DataPropertyName = "penalty";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.penalty.DefaultCellStyle = dataGridViewCellStyle4;
            this.penalty.HeaderText = "Сумма к оплате";
            this.penalty.Name = "penalty";
            this.penalty.ReadOnly = true;
            // 
            // PaymentSum
            // 
            this.PaymentSum.DataPropertyName = "PaymentSum";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle5.Format = "N2";
            dataGridViewCellStyle5.NullValue = "0";
            this.PaymentSum.DefaultCellStyle = dataGridViewCellStyle5;
            this.PaymentSum.HeaderText = "Сумма оплаты";
            this.PaymentSum.Name = "PaymentSum";
            this.PaymentSum.ReadOnly = true;
            // 
            // Debt
            // 
            this.Debt.DataPropertyName = "Debt";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Debt.DefaultCellStyle = dataGridViewCellStyle6;
            this.Debt.HeaderText = "Долг";
            this.Debt.Name = "Debt";
            this.Debt.ReadOnly = true;
            // 
            // scan
            // 
            this.scan.DataPropertyName = "scan";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.scan.DefaultCellStyle = dataGridViewCellStyle7;
            this.scan.FillWeight = 40F;
            this.scan.HeaderText = "Скан";
            this.scan.Name = "scan";
            this.scan.ReadOnly = true;
            // 
            // id_tax
            // 
            this.id_tax.DataPropertyName = "id";
            this.id_tax.HeaderText = "id_tax";
            this.id_tax.Name = "id_tax";
            this.id_tax.ReadOnly = true;
            this.id_tax.Visible = false;
            // 
            // id_Agreements
            // 
            this.id_Agreements.DataPropertyName = "id_Agreements";
            this.id_Agreements.HeaderText = "id_Agreements";
            this.id_Agreements.Name = "id_Agreements";
            this.id_Agreements.ReadOnly = true;
            this.id_Agreements.Visible = false;
            // 
            // Comment
            // 
            this.Comment.DataPropertyName = "Comment";
            this.Comment.HeaderText = "Comment";
            this.Comment.Name = "Comment";
            this.Comment.ReadOnly = true;
            this.Comment.Visible = false;
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
            // PaymentId
            // 
            this.PaymentId.DataPropertyName = "PaymentId";
            this.PaymentId.HeaderText = "PaymentId";
            this.PaymentId.Name = "PaymentId";
            this.PaymentId.ReadOnly = true;
            this.PaymentId.Visible = false;
            // 
            // lblItog
            // 
            this.lblItog.AutoSize = true;
            this.lblItog.Location = new System.Drawing.Point(12, 328);
            this.lblItog.Name = "lblItog";
            this.lblItog.Size = new System.Drawing.Size(40, 13);
            this.lblItog.TabIndex = 34;
            this.lblItog.Text = "Итого:";
            // 
            // txtPenalty
            // 
            this.txtPenalty.Location = new System.Drawing.Point(91, 325);
            this.txtPenalty.Name = "txtPenalty";
            this.txtPenalty.ReadOnly = true;
            this.txtPenalty.Size = new System.Drawing.Size(114, 20);
            this.txtPenalty.TabIndex = 35;
            this.txtPenalty.TabStop = false;
            this.txtPenalty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtPayment
            // 
            this.txtPayment.Location = new System.Drawing.Point(211, 325);
            this.txtPayment.Name = "txtPayment";
            this.txtPayment.ReadOnly = true;
            this.txtPayment.Size = new System.Drawing.Size(114, 20);
            this.txtPayment.TabIndex = 36;
            this.txtPayment.TabStop = false;
            this.txtPayment.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtDebt
            // 
            this.txtDebt.Location = new System.Drawing.Point(328, 325);
            this.txtDebt.Name = "txtDebt";
            this.txtDebt.ReadOnly = true;
            this.txtDebt.Size = new System.Drawing.Size(114, 20);
            this.txtDebt.TabIndex = 37;
            this.txtDebt.TabStop = false;
            this.txtDebt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtComment
            // 
            this.txtComment.AllowDrop = true;
            this.txtComment.Location = new System.Drawing.Point(91, 351);
            this.txtComment.Multiline = true;
            this.txtComment.Name = "txtComment";
            this.txtComment.ReadOnly = true;
            this.txtComment.Size = new System.Drawing.Size(351, 66);
            this.txtComment.TabIndex = 62;
            // 
            // txtDateEdit
            // 
            this.txtDateEdit.Location = new System.Drawing.Point(91, 449);
            this.txtDateEdit.Name = "txtDateEdit";
            this.txtDateEdit.ReadOnly = true;
            this.txtDateEdit.Size = new System.Drawing.Size(137, 20);
            this.txtDateEdit.TabIndex = 66;
            this.txtDateEdit.TabStop = false;
            // 
            // txtEditor
            // 
            this.txtEditor.Location = new System.Drawing.Point(91, 423);
            this.txtEditor.Name = "txtEditor";
            this.txtEditor.ReadOnly = true;
            this.txtEditor.Size = new System.Drawing.Size(137, 20);
            this.txtEditor.TabIndex = 65;
            this.txtEditor.TabStop = false;
            // 
            // lblDateEdit
            // 
            this.lblDateEdit.AutoSize = true;
            this.lblDateEdit.Location = new System.Drawing.Point(12, 452);
            this.lblDateEdit.Name = "lblDateEdit";
            this.lblDateEdit.Size = new System.Drawing.Size(60, 13);
            this.lblDateEdit.TabIndex = 64;
            this.lblDateEdit.Text = "Дата ред.:";
            // 
            // lblEditor
            // 
            this.lblEditor.AutoSize = true;
            this.lblEditor.Location = new System.Drawing.Point(12, 426);
            this.lblEditor.Name = "lblEditor";
            this.lblEditor.Size = new System.Drawing.Size(49, 13);
            this.lblEditor.TabIndex = 63;
            this.lblEditor.Text = "Редакт.:";
            // 
            // btnScan
            // 
            this.btnScan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnScan.Image = global::Arenda.Properties.Resources.scanner;
            this.btnScan.Location = new System.Drawing.Point(269, 438);
            this.btnScan.Name = "btnScan";
            this.btnScan.Size = new System.Drawing.Size(30, 30);
            this.btnScan.TabIndex = 73;
            this.toolTip1.SetToolTip(this.btnScan, "Сканирование изображения");
            this.btnScan.UseVisualStyleBackColor = true;
            this.btnScan.Click += new System.EventHandler(this.btnScan_Click);
            // 
            // btnPay
            // 
            this.btnPay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPay.Image = ((System.Drawing.Image)(resources.GetObject("btnPay.Image")));
            this.btnPay.Location = new System.Drawing.Point(305, 438);
            this.btnPay.Name = "btnPay";
            this.btnPay.Size = new System.Drawing.Size(30, 30);
            this.btnPay.TabIndex = 71;
            this.toolTip1.SetToolTip(this.btnPay, "Ввод дополнительной оплаты");
            this.btnPay.UseVisualStyleBackColor = true;
            this.btnPay.Click += new System.EventHandler(this.btnPay_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.Image = ((System.Drawing.Image)(resources.GetObject("btnAdd.Image")));
            this.btnAdd.Location = new System.Drawing.Point(341, 438);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(30, 30);
            this.btnAdd.TabIndex = 67;
            this.toolTip1.SetToolTip(this.btnAdd, "Добавить");
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEdit.Image = ((System.Drawing.Image)(resources.GetObject("btnEdit.Image")));
            this.btnEdit.Location = new System.Drawing.Point(376, 438);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(30, 30);
            this.btnEdit.TabIndex = 68;
            this.toolTip1.SetToolTip(this.btnEdit, "Редактировать");
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnDel
            // 
            this.btnDel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDel.Image = ((System.Drawing.Image)(resources.GetObject("btnDel.Image")));
            this.btnDel.Location = new System.Drawing.Point(412, 438);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(30, 30);
            this.btnDel.TabIndex = 69;
            this.toolTip1.SetToolTip(this.btnDel, "Удалить");
            this.btnDel.UseVisualStyleBackColor = true;
            this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
            // 
            // btnQuit
            // 
            this.btnQuit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnQuit.Image = ((System.Drawing.Image)(resources.GetObject("btnQuit.Image")));
            this.btnQuit.Location = new System.Drawing.Point(448, 438);
            this.btnQuit.Name = "btnQuit";
            this.btnQuit.Size = new System.Drawing.Size(30, 30);
            this.btnQuit.TabIndex = 70;
            this.toolTip1.SetToolTip(this.btnQuit, "Выход");
            this.btnQuit.UseVisualStyleBackColor = true;
            this.btnQuit.Click += new System.EventHandler(this.btnQuit_Click);
            // 
            // lblComment
            // 
            this.lblComment.AutoSize = true;
            this.lblComment.Location = new System.Drawing.Point(12, 354);
            this.lblComment.Name = "lblComment";
            this.lblComment.Size = new System.Drawing.Size(73, 13);
            this.lblComment.TabIndex = 72;
            this.lblComment.Text = "Примечание:";
            // 
            // cboAnotherPay
            // 
            this.cboAnotherPay.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboAnotherPay.FormattingEnabled = true;
            this.cboAnotherPay.Location = new System.Drawing.Point(331, 5);
            this.cboAnotherPay.Name = "cboAnotherPay";
            this.cboAnotherPay.Size = new System.Drawing.Size(151, 21);
            this.cboAnotherPay.TabIndex = 74;
            this.cboAnotherPay.SelectedIndexChanged += new System.EventHandler(this.cboAnotherPay_SelectedIndexChanged);
            // 
            // lblAnotherPay
            // 
            this.lblAnotherPay.AutoSize = true;
            this.lblAnotherPay.Location = new System.Drawing.Point(251, 9);
            this.lblAnotherPay.Name = "lblAnotherPay";
            this.lblAnotherPay.Size = new System.Drawing.Size(74, 13);
            this.lblAnotherPay.TabIndex = 75;
            this.lblAnotherPay.Text = "Доп. оплаты:";
            // 
            // frmListTaxes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(490, 480);
            this.Controls.Add(this.lblAnotherPay);
            this.Controls.Add(this.cboAnotherPay);
            this.Controls.Add(this.btnScan);
            this.Controls.Add(this.lblComment);
            this.Controls.Add(this.btnPay);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnDel);
            this.Controls.Add(this.btnQuit);
            this.Controls.Add(this.txtDateEdit);
            this.Controls.Add(this.txtEditor);
            this.Controls.Add(this.lblDateEdit);
            this.Controls.Add(this.lblEditor);
            this.Controls.Add(this.txtComment);
            this.Controls.Add(this.txtDebt);
            this.Controls.Add(this.txtPayment);
            this.Controls.Add(this.txtPenalty);
            this.Controls.Add(this.lblItog);
            this.Controls.Add(this.grdPayments);
            this.Controls.Add(this.txtTenant);
            this.Controls.Add(this.txtNum);
            this.Controls.Add(this.lblTenant);
            this.Controls.Add(this.lblNum);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "frmListTaxes";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Список дополнительных оплат к договору";
            this.Load += new System.EventHandler(this.frmListTaxes_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmListTaxes_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.grdPayments)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtTenant;
        private System.Windows.Forms.TextBox txtNum;
        private System.Windows.Forms.Label lblTenant;
        private System.Windows.Forms.Label lblNum;
        private System.Windows.Forms.DataGridView grdPayments;
        private System.Windows.Forms.Label lblItog;
        private System.Windows.Forms.TextBox txtPenalty;
        private System.Windows.Forms.TextBox txtPayment;
        private System.Windows.Forms.TextBox txtDebt;
        private System.Windows.Forms.TextBox txtComment;
        private System.Windows.Forms.TextBox txtDateEdit;
        private System.Windows.Forms.TextBox txtEditor;
        private System.Windows.Forms.Label lblDateEdit;
        private System.Windows.Forms.Label lblEditor;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnDel;
        private System.Windows.Forms.Button btnQuit;
        private System.Windows.Forms.Button btnPay;
        private System.Windows.Forms.Label lblComment;
        private System.Windows.Forms.Button btnScan;
        private System.Windows.Forms.DataGridViewTextBoxColumn TaxDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn PaymentName;
        private System.Windows.Forms.DataGridViewTextBoxColumn penalty;
        private System.Windows.Forms.DataGridViewTextBoxColumn PaymentSum;
        private System.Windows.Forms.DataGridViewTextBoxColumn Debt;
        private System.Windows.Forms.DataGridViewTextBoxColumn scan;
        private System.Windows.Forms.DataGridViewTextBoxColumn id_tax;
        private System.Windows.Forms.DataGridViewTextBoxColumn id_Agreements;
        private System.Windows.Forms.DataGridViewTextBoxColumn Comment;
        private System.Windows.Forms.DataGridViewTextBoxColumn DateEdit;
        private System.Windows.Forms.DataGridViewTextBoxColumn id_Editor;
        private System.Windows.Forms.DataGridViewTextBoxColumn Editor;
        private System.Windows.Forms.DataGridViewTextBoxColumn PaymentId;
        private System.Windows.Forms.ComboBox cboAnotherPay;
        private System.Windows.Forms.Label lblAnotherPay;
    }
}