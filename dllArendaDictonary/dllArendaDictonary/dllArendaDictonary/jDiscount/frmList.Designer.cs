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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tbNumber = new System.Windows.Forms.TextBox();
            this.dgvData = new System.Windows.Forms.DataGridView();
            this.chbNotActive = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btAdd = new System.Windows.Forms.Button();
            this.btEdit = new System.Windows.Forms.Button();
            this.btDelete = new System.Windows.Forms.Button();
            this.btClose = new System.Windows.Forms.Button();
            this.btConfirmD = new System.Windows.Forms.Button();
            this.pConfirmD = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.tbTotalPrice = new System.Windows.Forms.TextBox();
            this.lTotalPrice = new System.Windows.Forms.Label();
            this.tbPrice = new System.Windows.Forms.TextBox();
            this.lPrice = new System.Windows.Forms.Label();
            this.tbDiscountPrice = new System.Windows.Forms.TextBox();
            this.lDiscountPrice = new System.Windows.Forms.Label();
            this.tbPercentDiscount = new System.Windows.Forms.TextBox();
            this.lPercentDiscount = new System.Windows.Forms.Label();
            this.cDateStart = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cDateEnd = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cTypeDiscount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cTypeLand = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cTypeAgreement = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cObject = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            this.SuspendLayout();
            // 
            // tbNumber
            // 
            this.tbNumber.Location = new System.Drawing.Point(12, 12);
            this.tbNumber.Name = "tbNumber";
            this.tbNumber.Size = new System.Drawing.Size(244, 20);
            this.tbNumber.TabIndex = 0;
            this.tbNumber.Visible = false;
            this.tbNumber.TextChanged += new System.EventHandler(this.tbName_TextChanged);
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
            this.cDateStart,
            this.cDateEnd,
            this.cTypeDiscount,
            this.cTypeLand,
            this.cTypeAgreement,
            this.cObject});
            this.dgvData.Location = new System.Drawing.Point(12, 38);
            this.dgvData.MultiSelect = false;
            this.dgvData.Name = "dgvData";
            this.dgvData.ReadOnly = true;
            this.dgvData.RowHeadersVisible = false;
            this.dgvData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvData.Size = new System.Drawing.Size(836, 320);
            this.dgvData.TabIndex = 1;
            this.dgvData.ColumnWidthChanged += new System.Windows.Forms.DataGridViewColumnEventHandler(this.dgvData_ColumnWidthChanged);
            this.dgvData.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgvData_RowPostPaint);
            this.dgvData.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.dgvData_RowPrePaint);
            this.dgvData.SelectionChanged += new System.EventHandler(this.dgvData_SelectionChanged);
            // 
            // chbNotActive
            // 
            this.chbNotActive.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chbNotActive.AutoSize = true;
            this.chbNotActive.Location = new System.Drawing.Point(281, 473);
            this.chbNotActive.Name = "chbNotActive";
            this.chbNotActive.Size = new System.Drawing.Size(113, 17);
            this.chbNotActive.TabIndex = 2;
            this.chbNotActive.Text = "- недействующие";
            this.chbNotActive.UseVisualStyleBackColor = true;
            this.chbNotActive.Visible = false;
            this.chbNotActive.CheckedChanged += new System.EventHandler(this.chbNotActive_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(204)))), ((int)(((byte)(255)))));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Location = new System.Drawing.Point(254, 471);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(21, 21);
            this.panel1.TabIndex = 3;
            this.panel1.Visible = false;
            // 
            // btAdd
            // 
            this.btAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btAdd.Image = global::dllArendaDictonary.Properties.Resources.Add;
            this.btAdd.Location = new System.Drawing.Point(702, 465);
            this.btAdd.Name = "btAdd";
            this.btAdd.Size = new System.Drawing.Size(32, 32);
            this.btAdd.TabIndex = 4;
            this.btAdd.UseVisualStyleBackColor = true;
            this.btAdd.Click += new System.EventHandler(this.btAdd_Click);
            // 
            // btEdit
            // 
            this.btEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btEdit.Image = global::dllArendaDictonary.Properties.Resources.Edit;
            this.btEdit.Location = new System.Drawing.Point(740, 465);
            this.btEdit.Name = "btEdit";
            this.btEdit.Size = new System.Drawing.Size(32, 32);
            this.btEdit.TabIndex = 4;
            this.btEdit.UseVisualStyleBackColor = true;
            this.btEdit.Click += new System.EventHandler(this.btEdit_Click);
            // 
            // btDelete
            // 
            this.btDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btDelete.Image = global::dllArendaDictonary.Properties.Resources.Trash;
            this.btDelete.Location = new System.Drawing.Point(778, 465);
            this.btDelete.Name = "btDelete";
            this.btDelete.Size = new System.Drawing.Size(32, 32);
            this.btDelete.TabIndex = 4;
            this.btDelete.UseVisualStyleBackColor = true;
            this.btDelete.Click += new System.EventHandler(this.btDelete_Click);
            // 
            // btClose
            // 
            this.btClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btClose.Image = global::dllArendaDictonary.Properties.Resources.Exit;
            this.btClose.Location = new System.Drawing.Point(816, 465);
            this.btClose.Name = "btClose";
            this.btClose.Size = new System.Drawing.Size(32, 32);
            this.btClose.TabIndex = 4;
            this.btClose.UseVisualStyleBackColor = true;
            this.btClose.Click += new System.EventHandler(this.btClose_Click);
            // 
            // btConfirmD
            // 
            this.btConfirmD.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btConfirmD.Image = global::dllArendaDictonary.Properties.Resources.tick;
            this.btConfirmD.Location = new System.Drawing.Point(612, 465);
            this.btConfirmD.Name = "btConfirmD";
            this.btConfirmD.Size = new System.Drawing.Size(32, 32);
            this.btConfirmD.TabIndex = 5;
            this.btConfirmD.UseVisualStyleBackColor = true;
            this.btConfirmD.Click += new System.EventHandler(this.btConfirmD_Click);
            // 
            // pConfirmD
            // 
            this.pConfirmD.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pConfirmD.BackColor = System.Drawing.Color.Yellow;
            this.pConfirmD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pConfirmD.Location = new System.Drawing.Point(12, 471);
            this.pConfirmD.Name = "pConfirmD";
            this.pConfirmD.Size = new System.Drawing.Size(21, 21);
            this.pConfirmD.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(39, 475);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Подтвержденно Д";
            // 
            // tbTotalPrice
            // 
            this.tbTotalPrice.Location = new System.Drawing.Point(411, 442);
            this.tbTotalPrice.MaxLength = 15;
            this.tbTotalPrice.Name = "tbTotalPrice";
            this.tbTotalPrice.ReadOnly = true;
            this.tbTotalPrice.Size = new System.Drawing.Size(119, 20);
            this.tbTotalPrice.TabIndex = 17;
            this.tbTotalPrice.Text = "0,00";
            this.tbTotalPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lTotalPrice
            // 
            this.lTotalPrice.AutoSize = true;
            this.lTotalPrice.Location = new System.Drawing.Point(12, 446);
            this.lTotalPrice.Name = "lTotalPrice";
            this.lTotalPrice.Size = new System.Drawing.Size(394, 13);
            this.lTotalPrice.TabIndex = 13;
            this.lTotalPrice.Text = "Общая Стоимость по договору, при которой договора попадают под скидки";
            // 
            // tbPrice
            // 
            this.tbPrice.Location = new System.Drawing.Point(411, 416);
            this.tbPrice.MaxLength = 15;
            this.tbPrice.Name = "tbPrice";
            this.tbPrice.ReadOnly = true;
            this.tbPrice.Size = new System.Drawing.Size(119, 20);
            this.tbPrice.TabIndex = 18;
            this.tbPrice.Text = "0,00";
            this.tbPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lPrice
            // 
            this.lPrice.AutoSize = true;
            this.lPrice.Location = new System.Drawing.Point(12, 420);
            this.lPrice.Name = "lPrice";
            this.lPrice.Size = new System.Drawing.Size(373, 13);
            this.lPrice.TabIndex = 14;
            this.lPrice.Text = "Цена 1 квадратного метра, при которой договора попадают под скидки";
            // 
            // tbDiscountPrice
            // 
            this.tbDiscountPrice.Location = new System.Drawing.Point(411, 390);
            this.tbDiscountPrice.MaxLength = 15;
            this.tbDiscountPrice.Name = "tbDiscountPrice";
            this.tbDiscountPrice.ReadOnly = true;
            this.tbDiscountPrice.Size = new System.Drawing.Size(119, 20);
            this.tbDiscountPrice.TabIndex = 19;
            this.tbDiscountPrice.Text = "0,00";
            this.tbDiscountPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lDiscountPrice
            // 
            this.lDiscountPrice.AutoSize = true;
            this.lDiscountPrice.Location = new System.Drawing.Point(12, 394);
            this.lDiscountPrice.Name = "lDiscountPrice";
            this.lDiscountPrice.Size = new System.Drawing.Size(227, 13);
            this.lDiscountPrice.TabIndex = 15;
            this.lDiscountPrice.Text = "Новая цена стоимости 1 квадратного мета";
            // 
            // tbPercentDiscount
            // 
            this.tbPercentDiscount.Location = new System.Drawing.Point(411, 364);
            this.tbPercentDiscount.MaxLength = 15;
            this.tbPercentDiscount.Name = "tbPercentDiscount";
            this.tbPercentDiscount.ReadOnly = true;
            this.tbPercentDiscount.Size = new System.Drawing.Size(119, 20);
            this.tbPercentDiscount.TabIndex = 20;
            this.tbPercentDiscount.Text = "0,00";
            this.tbPercentDiscount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lPercentDiscount
            // 
            this.lPercentDiscount.AutoSize = true;
            this.lPercentDiscount.Location = new System.Drawing.Point(12, 368);
            this.lPercentDiscount.Name = "lPercentDiscount";
            this.lPercentDiscount.Size = new System.Drawing.Size(246, 13);
            this.lPercentDiscount.TabIndex = 16;
            this.lPercentDiscount.Text = "Процент скидки от общей стоимости договора";
            // 
            // cDateStart
            // 
            this.cDateStart.DataPropertyName = "DateStart";
            this.cDateStart.HeaderText = "Начало";
            this.cDateStart.Name = "cDateStart";
            this.cDateStart.ReadOnly = true;
            // 
            // cDateEnd
            // 
            this.cDateEnd.DataPropertyName = "DateEnd";
            this.cDateEnd.HeaderText = "Конец";
            this.cDateEnd.Name = "cDateEnd";
            this.cDateEnd.ReadOnly = true;
            // 
            // cTypeDiscount
            // 
            this.cTypeDiscount.DataPropertyName = "nameTypeDiscount";
            this.cTypeDiscount.HeaderText = "Тип скидки";
            this.cTypeDiscount.Name = "cTypeDiscount";
            this.cTypeDiscount.ReadOnly = true;
            // 
            // cTypeLand
            // 
            this.cTypeLand.DataPropertyName = "nameTypeTenant";
            this.cTypeLand.HeaderText = "Тип Арендатора";
            this.cTypeLand.Name = "cTypeLand";
            this.cTypeLand.ReadOnly = true;
            // 
            // cTypeAgreement
            // 
            this.cTypeAgreement.DataPropertyName = "nameTypeAgreements";
            this.cTypeAgreement.HeaderText = "Тип договора";
            this.cTypeAgreement.Name = "cTypeAgreement";
            this.cTypeAgreement.ReadOnly = true;
            // 
            // cObject
            // 
            this.cObject.HeaderText = "Объект";
            this.cObject.Name = "cObject";
            this.cObject.ReadOnly = true;
            this.cObject.Visible = false;
            // 
            // frmList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(860, 506);
            this.Controls.Add(this.tbTotalPrice);
            this.Controls.Add(this.lTotalPrice);
            this.Controls.Add(this.tbPrice);
            this.Controls.Add(this.lPrice);
            this.Controls.Add(this.tbDiscountPrice);
            this.Controls.Add(this.lDiscountPrice);
            this.Controls.Add(this.tbPercentDiscount);
            this.Controls.Add(this.lPercentDiscount);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btConfirmD);
            this.Controls.Add(this.btAdd);
            this.Controls.Add(this.btEdit);
            this.Controls.Add(this.btDelete);
            this.Controls.Add(this.btClose);
            this.Controls.Add(this.pConfirmD);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.chbNotActive);
            this.Controls.Add(this.dgvData);
            this.Controls.Add(this.tbNumber);
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

        private System.Windows.Forms.TextBox tbNumber;
        private System.Windows.Forms.DataGridView dgvData;
        private System.Windows.Forms.CheckBox chbNotActive;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btClose;
        private System.Windows.Forms.Button btDelete;
        private System.Windows.Forms.Button btEdit;
        private System.Windows.Forms.Button btAdd;
        private System.Windows.Forms.Button btConfirmD;
        private System.Windows.Forms.Panel pConfirmD;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbTotalPrice;
        private System.Windows.Forms.Label lTotalPrice;
        private System.Windows.Forms.TextBox tbPrice;
        private System.Windows.Forms.Label lPrice;
        private System.Windows.Forms.TextBox tbDiscountPrice;
        private System.Windows.Forms.Label lDiscountPrice;
        private System.Windows.Forms.TextBox tbPercentDiscount;
        private System.Windows.Forms.Label lPercentDiscount;
        private System.Windows.Forms.DataGridViewTextBoxColumn cDateStart;
        private System.Windows.Forms.DataGridViewTextBoxColumn cDateEnd;
        private System.Windows.Forms.DataGridViewTextBoxColumn cTypeDiscount;
        private System.Windows.Forms.DataGridViewTextBoxColumn cTypeLand;
        private System.Windows.Forms.DataGridViewTextBoxColumn cTypeAgreement;
        private System.Windows.Forms.DataGridViewTextBoxColumn cObject;
    }
}