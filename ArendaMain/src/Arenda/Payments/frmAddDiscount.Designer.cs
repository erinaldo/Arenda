namespace Arenda.Payments
{
    partial class frmAddDiscount
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
            this.btSave = new System.Windows.Forms.Button();
            this.btClose = new System.Windows.Forms.Button();
            this.dtpStart = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpEnd = new System.Windows.Forms.DateTimePicker();
            this.lDateEnd = new System.Windows.Forms.Label();
            this.chbUnlimitedDiscount = new System.Windows.Forms.CheckBox();
            this.cmbTypeDicount = new System.Windows.Forms.ComboBox();
            this.lTypeDiscont = new System.Windows.Forms.Label();
            this.tbPercentDiscount = new System.Windows.Forms.TextBox();
            this.lPercentDiscount = new System.Windows.Forms.Label();
            this.tbDiscountPrice = new System.Windows.Forms.TextBox();
            this.lDiscountPrice = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btSave
            // 
            this.btSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btSave.Image = global::Arenda.Properties.Resources.saveHS;
            this.btSave.Location = new System.Drawing.Point(451, 84);
            this.btSave.Name = "btSave";
            this.btSave.Size = new System.Drawing.Size(32, 32);
            this.btSave.TabIndex = 87;
            this.btSave.UseVisualStyleBackColor = true;
            this.btSave.Click += new System.EventHandler(this.btSave_Click);
            // 
            // btClose
            // 
            this.btClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btClose.Image = global::Arenda.Properties.Resources.Log_Out_icon1;
            this.btClose.Location = new System.Drawing.Point(489, 84);
            this.btClose.Name = "btClose";
            this.btClose.Size = new System.Drawing.Size(32, 32);
            this.btClose.TabIndex = 86;
            this.btClose.UseVisualStyleBackColor = true;
            this.btClose.Click += new System.EventHandler(this.btClose_Click);
            // 
            // dtpStart
            // 
            this.dtpStart.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpStart.Location = new System.Drawing.Point(105, 10);
            this.dtpStart.Name = "dtpStart";
            this.dtpStart.Size = new System.Drawing.Size(79, 20);
            this.dtpStart.TabIndex = 88;
            this.dtpStart.ValueChanged += new System.EventHandler(this.dtpStart_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 13);
            this.label1.TabIndex = 89;
            this.label1.Text = "Дата начала";
            // 
            // dtpEnd
            // 
            this.dtpEnd.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpEnd.Location = new System.Drawing.Point(301, 10);
            this.dtpEnd.Name = "dtpEnd";
            this.dtpEnd.Size = new System.Drawing.Size(79, 20);
            this.dtpEnd.TabIndex = 88;
            this.dtpEnd.ValueChanged += new System.EventHandler(this.dtpEnd_ValueChanged);
            // 
            // lDateEnd
            // 
            this.lDateEnd.AutoSize = true;
            this.lDateEnd.Location = new System.Drawing.Point(198, 14);
            this.lDateEnd.Name = "lDateEnd";
            this.lDateEnd.Size = new System.Drawing.Size(89, 13);
            this.lDateEnd.TabIndex = 89;
            this.lDateEnd.Text = "Дата окончания";
            // 
            // chbUnlimitedDiscount
            // 
            this.chbUnlimitedDiscount.AutoSize = true;
            this.chbUnlimitedDiscount.Location = new System.Drawing.Point(401, 12);
            this.chbUnlimitedDiscount.Name = "chbUnlimitedDiscount";
            this.chbUnlimitedDiscount.Size = new System.Drawing.Size(126, 17);
            this.chbUnlimitedDiscount.TabIndex = 90;
            this.chbUnlimitedDiscount.Text = "Постоянная скидка";
            this.chbUnlimitedDiscount.UseVisualStyleBackColor = true;
            this.chbUnlimitedDiscount.Click += new System.EventHandler(this.chbUnlimitedDiscount_Click);
            // 
            // cmbTypeDicount
            // 
            this.cmbTypeDicount.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTypeDicount.FormattingEnabled = true;
            this.cmbTypeDicount.Location = new System.Drawing.Point(105, 36);
            this.cmbTypeDicount.Name = "cmbTypeDicount";
            this.cmbTypeDicount.Size = new System.Drawing.Size(413, 21);
            this.cmbTypeDicount.TabIndex = 92;
            this.cmbTypeDicount.SelectionChangeCommitted += new System.EventHandler(this.cmbTypeDicount_SelectionChangeCommitted);
            // 
            // lTypeDiscont
            // 
            this.lTypeDiscont.AutoSize = true;
            this.lTypeDiscont.Location = new System.Drawing.Point(20, 40);
            this.lTypeDiscont.Name = "lTypeDiscont";
            this.lTypeDiscont.Size = new System.Drawing.Size(65, 13);
            this.lTypeDiscont.TabIndex = 91;
            this.lTypeDiscont.Text = "Тип скидки";
            // 
            // tbPercentDiscount
            // 
            this.tbPercentDiscount.Location = new System.Drawing.Point(399, 63);
            this.tbPercentDiscount.MaxLength = 15;
            this.tbPercentDiscount.Name = "tbPercentDiscount";
            this.tbPercentDiscount.Size = new System.Drawing.Size(119, 20);
            this.tbPercentDiscount.TabIndex = 94;
            this.tbPercentDiscount.Text = "0,00";
            this.tbPercentDiscount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbPercentDiscount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbPercentDiscount_KeyPress);
            this.tbPercentDiscount.Validating += new System.ComponentModel.CancelEventHandler(this.tbDiscountPrice_Validating);
            // 
            // lPercentDiscount
            // 
            this.lPercentDiscount.AutoSize = true;
            this.lPercentDiscount.Location = new System.Drawing.Point(20, 67);
            this.lPercentDiscount.Name = "lPercentDiscount";
            this.lPercentDiscount.Size = new System.Drawing.Size(246, 13);
            this.lPercentDiscount.TabIndex = 93;
            this.lPercentDiscount.Text = "Процент скидки от общей стоимости договора";
            // 
            // tbDiscountPrice
            // 
            this.tbDiscountPrice.Location = new System.Drawing.Point(399, 89);
            this.tbDiscountPrice.MaxLength = 15;
            this.tbDiscountPrice.Name = "tbDiscountPrice";
            this.tbDiscountPrice.Size = new System.Drawing.Size(119, 20);
            this.tbDiscountPrice.TabIndex = 96;
            this.tbDiscountPrice.Text = "0,00";
            this.tbDiscountPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbDiscountPrice.Visible = false;
            this.tbDiscountPrice.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbPercentDiscount_KeyPress);
            this.tbDiscountPrice.Validating += new System.ComponentModel.CancelEventHandler(this.tbDiscountPrice_Validating);
            // 
            // lDiscountPrice
            // 
            this.lDiscountPrice.AutoSize = true;
            this.lDiscountPrice.Location = new System.Drawing.Point(20, 93);
            this.lDiscountPrice.Name = "lDiscountPrice";
            this.lDiscountPrice.Size = new System.Drawing.Size(227, 13);
            this.lDiscountPrice.TabIndex = 95;
            this.lDiscountPrice.Text = "Новая цена стоимости 1 квадратного мета";
            this.lDiscountPrice.Visible = false;
            // 
            // frmAddDiscount
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(529, 124);
            this.ControlBox = false;
            this.Controls.Add(this.lDiscountPrice);
            this.Controls.Add(this.tbPercentDiscount);
            this.Controls.Add(this.lPercentDiscount);
            this.Controls.Add(this.cmbTypeDicount);
            this.Controls.Add(this.lTypeDiscont);
            this.Controls.Add(this.chbUnlimitedDiscount);
            this.Controls.Add(this.lDateEnd);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dtpEnd);
            this.Controls.Add(this.dtpStart);
            this.Controls.Add(this.btSave);
            this.Controls.Add(this.btClose);
            this.Controls.Add(this.tbDiscountPrice);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAddDiscount";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Добавление скидки";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmAddDiscount_FormClosing);
            this.Load += new System.EventHandler(this.frmAddDiscount_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btSave;
        private System.Windows.Forms.Button btClose;
        private System.Windows.Forms.DateTimePicker dtpStart;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpEnd;
        private System.Windows.Forms.Label lDateEnd;
        private System.Windows.Forms.CheckBox chbUnlimitedDiscount;
        private System.Windows.Forms.ComboBox cmbTypeDicount;
        private System.Windows.Forms.Label lTypeDiscont;
        private System.Windows.Forms.TextBox tbPercentDiscount;
        private System.Windows.Forms.Label lPercentDiscount;
        private System.Windows.Forms.TextBox tbDiscountPrice;
        private System.Windows.Forms.Label lDiscountPrice;
    }
}