namespace Arenda
{
    partial class frmTaxPayments
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTaxPayments));
            this.lblComment = new System.Windows.Forms.Label();
            this.txtComment = new System.Windows.Forms.TextBox();
            this.lblRub = new System.Windows.Forms.Label();
            this.txtSum = new System.Windows.Forms.TextBox();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.lblSum = new System.Windows.Forms.Label();
            this.lblDate = new System.Windows.Forms.Label();
            this.grdPayments = new System.Windows.Forms.DataGridView();
            this.TaxPaymentDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PaymentSum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id_Fines = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DateEdit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id_Editor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Editor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtDateEdit = new System.Windows.Forms.TextBox();
            this.txtEditor = new System.Windows.Forms.TextBox();
            this.lblDateEdit = new System.Windows.Forms.Label();
            this.lblEditor = new System.Windows.Forms.Label();
            this.gbxPaymentTax = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtPaymentTax = new System.Windows.Forms.TextBox();
            this.dtpDatePaymentTax = new System.Windows.Forms.DateTimePicker();
            this.lblPaymentTax = new System.Windows.Forms.Label();
            this.lblDatePaymentTax = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btnScan = new System.Windows.Forms.Button();
            this.btnDel = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnQuit = new System.Windows.Forms.Button();
            this.txtPayment = new System.Windows.Forms.TextBox();
            this.lblItog = new System.Windows.Forms.Label();
            this.lblAnotherPay = new System.Windows.Forms.Label();
            this.cboAnotherPay = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.grdPayments)).BeginInit();
            this.gbxPaymentTax.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblComment
            // 
            this.lblComment.AutoSize = true;
            this.lblComment.Location = new System.Drawing.Point(12, 93);
            this.lblComment.Name = "lblComment";
            this.lblComment.Size = new System.Drawing.Size(73, 13);
            this.lblComment.TabIndex = 81;
            this.lblComment.Text = "Примечание:";
            // 
            // txtComment
            // 
            this.txtComment.AllowDrop = true;
            this.txtComment.Location = new System.Drawing.Point(130, 90);
            this.txtComment.MaxLength = 255;
            this.txtComment.Multiline = true;
            this.txtComment.Name = "txtComment";
            this.txtComment.ReadOnly = true;
            this.txtComment.Size = new System.Drawing.Size(294, 53);
            this.txtComment.TabIndex = 80;
            this.txtComment.TabStop = false;
            // 
            // lblRub
            // 
            this.lblRub.AutoSize = true;
            this.lblRub.Location = new System.Drawing.Point(301, 61);
            this.lblRub.Name = "lblRub";
            this.lblRub.Size = new System.Drawing.Size(27, 13);
            this.lblRub.TabIndex = 79;
            this.lblRub.Text = "руб.";
            // 
            // txtSum
            // 
            this.txtSum.Location = new System.Drawing.Point(130, 58);
            this.txtSum.Name = "txtSum";
            this.txtSum.ReadOnly = true;
            this.txtSum.Size = new System.Drawing.Size(165, 20);
            this.txtSum.TabIndex = 78;
            this.txtSum.TabStop = false;
            this.txtSum.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // dtpDate
            // 
            this.dtpDate.Enabled = false;
            this.dtpDate.Location = new System.Drawing.Point(130, 5);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(165, 20);
            this.dtpDate.TabIndex = 77;
            this.dtpDate.TabStop = false;
            // 
            // lblSum
            // 
            this.lblSum.AutoSize = true;
            this.lblSum.Location = new System.Drawing.Point(12, 61);
            this.lblSum.Name = "lblSum";
            this.lblSum.Size = new System.Drawing.Size(108, 13);
            this.lblSum.TabIndex = 76;
            this.lblSum.Text = "Сумма доп. оплаты:";
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.Location = new System.Drawing.Point(12, 9);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(86, 13);
            this.lblDate.TabIndex = 75;
            this.lblDate.Text = "Дата выписки: ";
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
            this.TaxPaymentDate,
            this.PaymentSum,
            this.id,
            this.id_Fines,
            this.DateEdit,
            this.id_Editor,
            this.Editor});
            this.grdPayments.Location = new System.Drawing.Point(12, 149);
            this.grdPayments.MultiSelect = false;
            this.grdPayments.Name = "grdPayments";
            this.grdPayments.ReadOnly = true;
            this.grdPayments.RowHeadersVisible = false;
            this.grdPayments.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdPayments.Size = new System.Drawing.Size(219, 164);
            this.grdPayments.TabIndex = 4;
            this.grdPayments.SelectionChanged += new System.EventHandler(this.grdPayments_SelectionChanged);
            // 
            // TaxPaymentDate
            // 
            this.TaxPaymentDate.DataPropertyName = "Date";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.TaxPaymentDate.DefaultCellStyle = dataGridViewCellStyle2;
            this.TaxPaymentDate.HeaderText = "Дата оплаты";
            this.TaxPaymentDate.Name = "TaxPaymentDate";
            this.TaxPaymentDate.ReadOnly = true;
            // 
            // PaymentSum
            // 
            this.PaymentSum.DataPropertyName = "Summa";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.Format = "N2";
            dataGridViewCellStyle3.NullValue = "0";
            this.PaymentSum.DefaultCellStyle = dataGridViewCellStyle3;
            this.PaymentSum.HeaderText = "Сумма оплаты";
            this.PaymentSum.Name = "PaymentSum";
            this.PaymentSum.ReadOnly = true;
            // 
            // id
            // 
            this.id.DataPropertyName = "id";
            this.id.HeaderText = "id";
            this.id.Name = "id";
            this.id.ReadOnly = true;
            this.id.Visible = false;
            // 
            // id_Fines
            // 
            this.id_Fines.DataPropertyName = "id_Fines";
            this.id_Fines.HeaderText = "id_Fines";
            this.id_Fines.Name = "id_Fines";
            this.id_Fines.ReadOnly = true;
            this.id_Fines.Visible = false;
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
            // txtDateEdit
            // 
            this.txtDateEdit.Location = new System.Drawing.Point(94, 372);
            this.txtDateEdit.Name = "txtDateEdit";
            this.txtDateEdit.ReadOnly = true;
            this.txtDateEdit.Size = new System.Drawing.Size(137, 20);
            this.txtDateEdit.TabIndex = 86;
            this.txtDateEdit.TabStop = false;
            // 
            // txtEditor
            // 
            this.txtEditor.Location = new System.Drawing.Point(94, 346);
            this.txtEditor.Name = "txtEditor";
            this.txtEditor.ReadOnly = true;
            this.txtEditor.Size = new System.Drawing.Size(137, 20);
            this.txtEditor.TabIndex = 85;
            this.txtEditor.TabStop = false;
            // 
            // lblDateEdit
            // 
            this.lblDateEdit.AutoSize = true;
            this.lblDateEdit.Location = new System.Drawing.Point(15, 375);
            this.lblDateEdit.Name = "lblDateEdit";
            this.lblDateEdit.Size = new System.Drawing.Size(60, 13);
            this.lblDateEdit.TabIndex = 84;
            this.lblDateEdit.Text = "Дата доб.:";
            // 
            // lblEditor
            // 
            this.lblEditor.AutoSize = true;
            this.lblEditor.Location = new System.Drawing.Point(15, 349);
            this.lblEditor.Name = "lblEditor";
            this.lblEditor.Size = new System.Drawing.Size(55, 13);
            this.lblEditor.TabIndex = 83;
            this.lblEditor.Text = "Добавил:";
            // 
            // gbxPaymentTax
            // 
            this.gbxPaymentTax.Controls.Add(this.label1);
            this.gbxPaymentTax.Controls.Add(this.txtPaymentTax);
            this.gbxPaymentTax.Controls.Add(this.dtpDatePaymentTax);
            this.gbxPaymentTax.Controls.Add(this.lblPaymentTax);
            this.gbxPaymentTax.Controls.Add(this.lblDatePaymentTax);
            this.gbxPaymentTax.Location = new System.Drawing.Point(273, 149);
            this.gbxPaymentTax.Name = "gbxPaymentTax";
            this.gbxPaymentTax.Size = new System.Drawing.Size(163, 124);
            this.gbxPaymentTax.TabIndex = 87;
            this.gbxPaymentTax.TabStop = false;
            this.gbxPaymentTax.Text = "Ввод оплаты";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(128, 95);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(27, 13);
            this.label1.TabIndex = 56;
            this.label1.Text = "руб.";
            // 
            // txtPaymentTax
            // 
            this.txtPaymentTax.Location = new System.Drawing.Point(9, 92);
            this.txtPaymentTax.Name = "txtPaymentTax";
            this.txtPaymentTax.Size = new System.Drawing.Size(113, 20);
            this.txtPaymentTax.TabIndex = 2;
            this.txtPaymentTax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtPaymentTax.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPaymentTax_KeyPress);
            this.txtPaymentTax.Leave += new System.EventHandler(this.txtPaymentTax_Leave);
            // 
            // dtpDatePaymentTax
            // 
            this.dtpDatePaymentTax.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDatePaymentTax.Location = new System.Drawing.Point(6, 42);
            this.dtpDatePaymentTax.Name = "dtpDatePaymentTax";
            this.dtpDatePaymentTax.Size = new System.Drawing.Size(113, 20);
            this.dtpDatePaymentTax.TabIndex = 1;
            // 
            // lblPaymentTax
            // 
            this.lblPaymentTax.AutoSize = true;
            this.lblPaymentTax.Location = new System.Drawing.Point(6, 76);
            this.lblPaymentTax.Name = "lblPaymentTax";
            this.lblPaymentTax.Size = new System.Drawing.Size(84, 13);
            this.lblPaymentTax.TabIndex = 53;
            this.lblPaymentTax.Text = "Сумма оплаты:";
            // 
            // lblDatePaymentTax
            // 
            this.lblDatePaymentTax.AutoSize = true;
            this.lblDatePaymentTax.Location = new System.Drawing.Point(6, 26);
            this.lblDatePaymentTax.Name = "lblDatePaymentTax";
            this.lblDatePaymentTax.Size = new System.Drawing.Size(79, 13);
            this.lblDatePaymentTax.TabIndex = 52;
            this.lblDatePaymentTax.Text = "Дата оплаты: ";
            // 
            // btnScan
            // 
            this.btnScan.Image = global::Arenda.Properties.Resources.scanner;
            this.btnScan.Location = new System.Drawing.Point(237, 267);
            this.btnScan.Name = "btnScan";
            this.btnScan.Size = new System.Drawing.Size(30, 30);
            this.btnScan.TabIndex = 90;
            this.toolTip1.SetToolTip(this.btnScan, "Сканирование изображения");
            this.btnScan.UseVisualStyleBackColor = true;
            this.btnScan.Click += new System.EventHandler(this.btnScan_Click);
            // 
            // btnDel
            // 
            this.btnDel.Image = ((System.Drawing.Image)(resources.GetObject("btnDel.Image")));
            this.btnDel.Location = new System.Drawing.Point(237, 195);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(30, 30);
            this.btnDel.TabIndex = 5;
            this.toolTip1.SetToolTip(this.btnDel, "Удалить");
            this.btnDel.UseVisualStyleBackColor = true;
            this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Image = ((System.Drawing.Image)(resources.GetObject("btnAdd.Image")));
            this.btnAdd.Location = new System.Drawing.Point(237, 231);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(30, 30);
            this.btnAdd.TabIndex = 3;
            this.toolTip1.SetToolTip(this.btnAdd, "Добавить");
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnQuit
            // 
            this.btnQuit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnQuit.Image = ((System.Drawing.Image)(resources.GetObject("btnQuit.Image")));
            this.btnQuit.Location = new System.Drawing.Point(427, 370);
            this.btnQuit.Name = "btnQuit";
            this.btnQuit.Size = new System.Drawing.Size(30, 30);
            this.btnQuit.TabIndex = 6;
            this.toolTip1.SetToolTip(this.btnQuit, "Выход");
            this.btnQuit.UseVisualStyleBackColor = true;
            this.btnQuit.Click += new System.EventHandler(this.btnQuit_Click);
            // 
            // txtPayment
            // 
            this.txtPayment.Location = new System.Drawing.Point(117, 319);
            this.txtPayment.Name = "txtPayment";
            this.txtPayment.ReadOnly = true;
            this.txtPayment.Size = new System.Drawing.Size(114, 20);
            this.txtPayment.TabIndex = 89;
            this.txtPayment.TabStop = false;
            this.txtPayment.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblItog
            // 
            this.lblItog.AutoSize = true;
            this.lblItog.Location = new System.Drawing.Point(15, 322);
            this.lblItog.Name = "lblItog";
            this.lblItog.Size = new System.Drawing.Size(40, 13);
            this.lblItog.TabIndex = 88;
            this.lblItog.Text = "Итого:";
            // 
            // lblAnotherPay
            // 
            this.lblAnotherPay.AutoSize = true;
            this.lblAnotherPay.Location = new System.Drawing.Point(12, 34);
            this.lblAnotherPay.Name = "lblAnotherPay";
            this.lblAnotherPay.Size = new System.Drawing.Size(102, 13);
            this.lblAnotherPay.TabIndex = 92;
            this.lblAnotherPay.Text = "Наим. доп оплаты:";
            // 
            // cboAnotherPay
            // 
            this.cboAnotherPay.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboAnotherPay.Enabled = false;
            this.cboAnotherPay.FormattingEnabled = true;
            this.cboAnotherPay.Location = new System.Drawing.Point(130, 31);
            this.cboAnotherPay.Name = "cboAnotherPay";
            this.cboAnotherPay.Size = new System.Drawing.Size(139, 21);
            this.cboAnotherPay.TabIndex = 91;
            // 
            // frmTaxPayments
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(469, 412);
            this.ControlBox = false;
            this.Controls.Add(this.lblAnotherPay);
            this.Controls.Add(this.cboAnotherPay);
            this.Controls.Add(this.btnScan);
            this.Controls.Add(this.txtPayment);
            this.Controls.Add(this.lblItog);
            this.Controls.Add(this.btnDel);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnQuit);
            this.Controls.Add(this.gbxPaymentTax);
            this.Controls.Add(this.txtDateEdit);
            this.Controls.Add(this.txtEditor);
            this.Controls.Add(this.lblDateEdit);
            this.Controls.Add(this.lblEditor);
            this.Controls.Add(this.grdPayments);
            this.Controls.Add(this.lblComment);
            this.Controls.Add(this.txtComment);
            this.Controls.Add(this.lblRub);
            this.Controls.Add(this.txtSum);
            this.Controls.Add(this.dtpDate);
            this.Controls.Add(this.lblSum);
            this.Controls.Add(this.lblDate);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmTaxPayments";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ввод дополнительной оплаты";
            this.Load += new System.EventHandler(this.frmTaxPayments_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdPayments)).EndInit();
            this.gbxPaymentTax.ResumeLayout(false);
            this.gbxPaymentTax.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblComment;
        private System.Windows.Forms.TextBox txtComment;
        private System.Windows.Forms.Label lblRub;
        private System.Windows.Forms.TextBox txtSum;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.Label lblSum;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.DataGridView grdPayments;
        private System.Windows.Forms.TextBox txtDateEdit;
        private System.Windows.Forms.TextBox txtEditor;
        private System.Windows.Forms.Label lblDateEdit;
        private System.Windows.Forms.Label lblEditor;
        private System.Windows.Forms.GroupBox gbxPaymentTax;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPaymentTax;
        private System.Windows.Forms.DateTimePicker dtpDatePaymentTax;
        private System.Windows.Forms.Label lblPaymentTax;
        private System.Windows.Forms.Label lblDatePaymentTax;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button btnQuit;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnDel;
        private System.Windows.Forms.DataGridViewTextBoxColumn TaxPaymentDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn PaymentSum;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn id_Fines;
        private System.Windows.Forms.DataGridViewTextBoxColumn DateEdit;
        private System.Windows.Forms.DataGridViewTextBoxColumn id_Editor;
        private System.Windows.Forms.DataGridViewTextBoxColumn Editor;
        private System.Windows.Forms.TextBox txtPayment;
        private System.Windows.Forms.Label lblItog;
        private System.Windows.Forms.Button btnScan;
        private System.Windows.Forms.Label lblAnotherPay;
        private System.Windows.Forms.ComboBox cboAnotherPay;
    }
}