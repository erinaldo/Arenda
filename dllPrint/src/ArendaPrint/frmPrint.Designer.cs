namespace ArendaPrint
{
    partial class frmPrint
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPrint));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmbActKomm = new System.Windows.Forms.ComboBox();
            this.chbActKomm = new System.Windows.Forms.CheckBox();
            this.cboDopSoglasheniye = new System.Windows.Forms.ComboBox();
            this.chbDopSoglasheniye = new System.Windows.Forms.CheckBox();
            this.cboActVozvr = new System.Windows.Forms.ComboBox();
            this.cboRastor = new System.Windows.Forms.ComboBox();
            this.cboAct = new System.Windows.Forms.ComboBox();
            this.chbDogovor = new System.Windows.Forms.CheckBox();
            this.chbAct = new System.Windows.Forms.CheckBox();
            this.chbActVozvr = new System.Windows.Forms.CheckBox();
            this.chbRastor = new System.Windows.Forms.CheckBox();
            this.prbExcel = new System.Windows.Forms.ProgressBar();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmbActKomm);
            this.groupBox1.Controls.Add(this.chbActKomm);
            this.groupBox1.Controls.Add(this.cboDopSoglasheniye);
            this.groupBox1.Controls.Add(this.chbDopSoglasheniye);
            this.groupBox1.Controls.Add(this.cboActVozvr);
            this.groupBox1.Controls.Add(this.cboRastor);
            this.groupBox1.Controls.Add(this.cboAct);
            this.groupBox1.Controls.Add(this.chbDogovor);
            this.groupBox1.Controls.Add(this.chbAct);
            this.groupBox1.Controls.Add(this.chbActVozvr);
            this.groupBox1.Controls.Add(this.chbRastor);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(431, 124);
            this.groupBox1.TabIndex = 52;
            this.groupBox1.TabStop = false;
            // 
            // cmbActKomm
            // 
            this.cmbActKomm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbActKomm.FormattingEnabled = true;
            this.cmbActKomm.Location = new System.Drawing.Point(283, 149);
            this.cmbActKomm.Name = "cmbActKomm";
            this.cmbActKomm.Size = new System.Drawing.Size(136, 21);
            this.cmbActKomm.TabIndex = 53;
            this.cmbActKomm.Visible = false;
            // 
            // chbActKomm
            // 
            this.chbActKomm.AutoSize = true;
            this.chbActKomm.Location = new System.Drawing.Point(6, 151);
            this.chbActKomm.Name = "chbActKomm";
            this.chbActKomm.Size = new System.Drawing.Size(213, 17);
            this.chbActKomm.TabIndex = 52;
            this.chbActKomm.Text = "Акт приёма-передачи коммуникаций";
            this.chbActKomm.UseVisualStyleBackColor = true;
            this.chbActKomm.Visible = false;
            // 
            // cboDopSoglasheniye
            // 
            this.cboDopSoglasheniye.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDopSoglasheniye.FormattingEnabled = true;
            this.cboDopSoglasheniye.Location = new System.Drawing.Point(283, 126);
            this.cboDopSoglasheniye.Name = "cboDopSoglasheniye";
            this.cboDopSoglasheniye.Size = new System.Drawing.Size(136, 21);
            this.cboDopSoglasheniye.TabIndex = 49;
            this.cboDopSoglasheniye.Visible = false;
            // 
            // chbDopSoglasheniye
            // 
            this.chbDopSoglasheniye.AutoSize = true;
            this.chbDopSoglasheniye.Location = new System.Drawing.Point(6, 128);
            this.chbDopSoglasheniye.Name = "chbDopSoglasheniye";
            this.chbDopSoglasheniye.Size = new System.Drawing.Size(114, 17);
            this.chbDopSoglasheniye.TabIndex = 13;
            this.chbDopSoglasheniye.Text = "Доп. соглашение";
            this.chbDopSoglasheniye.UseVisualStyleBackColor = true;
            this.chbDopSoglasheniye.Visible = false;
            // 
            // cboActVozvr
            // 
            this.cboActVozvr.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboActVozvr.FormattingEnabled = true;
            this.cboActVozvr.Location = new System.Drawing.Point(283, 66);
            this.cboActVozvr.Name = "cboActVozvr";
            this.cboActVozvr.Size = new System.Drawing.Size(136, 21);
            this.cboActVozvr.TabIndex = 12;
            // 
            // cboRastor
            // 
            this.cboRastor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboRastor.FormattingEnabled = true;
            this.cboRastor.Location = new System.Drawing.Point(283, 40);
            this.cboRastor.Name = "cboRastor";
            this.cboRastor.Size = new System.Drawing.Size(136, 21);
            this.cboRastor.TabIndex = 11;
            // 
            // cboAct
            // 
            this.cboAct.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboAct.FormattingEnabled = true;
            this.cboAct.Location = new System.Drawing.Point(283, 93);
            this.cboAct.Name = "cboAct";
            this.cboAct.Size = new System.Drawing.Size(136, 21);
            this.cboAct.TabIndex = 10;
            // 
            // chbDogovor
            // 
            this.chbDogovor.AutoSize = true;
            this.chbDogovor.Location = new System.Drawing.Point(6, 15);
            this.chbDogovor.Name = "chbDogovor";
            this.chbDogovor.Size = new System.Drawing.Size(111, 17);
            this.chbDogovor.TabIndex = 1;
            this.chbDogovor.Text = "Договор аренды";
            this.chbDogovor.UseVisualStyleBackColor = true;
            // 
            // chbAct
            // 
            this.chbAct.AutoSize = true;
            this.chbAct.Location = new System.Drawing.Point(6, 95);
            this.chbAct.Name = "chbAct";
            this.chbAct.Size = new System.Drawing.Size(135, 17);
            this.chbAct.TabIndex = 3;
            this.chbAct.Text = "Акт приёма-передачи";
            this.chbAct.UseVisualStyleBackColor = true;
            // 
            // chbActVozvr
            // 
            this.chbActVozvr.AutoSize = true;
            this.chbActVozvr.Location = new System.Drawing.Point(6, 68);
            this.chbActVozvr.Name = "chbActVozvr";
            this.chbActVozvr.Size = new System.Drawing.Size(185, 17);
            this.chbActVozvr.TabIndex = 5;
            this.chbActVozvr.Text = "Акт приёма-передачи (возврат)";
            this.chbActVozvr.UseVisualStyleBackColor = true;
            // 
            // chbRastor
            // 
            this.chbRastor.AutoSize = true;
            this.chbRastor.Location = new System.Drawing.Point(6, 42);
            this.chbRastor.Name = "chbRastor";
            this.chbRastor.Size = new System.Drawing.Size(217, 17);
            this.chbRastor.TabIndex = 4;
            this.chbRastor.Text = "Соглашение о расторжении договора";
            this.chbRastor.UseVisualStyleBackColor = true;
            // 
            // prbExcel
            // 
            this.prbExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.prbExcel.Location = new System.Drawing.Point(12, 162);
            this.prbExcel.Name = "prbExcel";
            this.prbExcel.Size = new System.Drawing.Size(185, 19);
            this.prbExcel.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.prbExcel.TabIndex = 51;
            this.prbExcel.Visible = false;
            // 
            // btnPrint
            // 
            this.btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrint.Image = ((System.Drawing.Image)(resources.GetObject("btnPrint.Image")));
            this.btnPrint.Location = new System.Drawing.Point(372, 149);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(32, 32);
            this.btnPrint.TabIndex = 49;
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.Image = global::ArendaPrint.Properties.Resources.Log_Out_icon;
            this.btnExit.Location = new System.Drawing.Point(410, 149);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(32, 32);
            this.btnExit.TabIndex = 50;
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // frmPrint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(452, 193);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.prbExcel);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.btnExit);
            this.Name = "frmPrint";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Печать";
            this.Load += new System.EventHandler(this.frmPrint_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cmbActKomm;
        private System.Windows.Forms.CheckBox chbActKomm;
        private System.Windows.Forms.ComboBox cboDopSoglasheniye;
        private System.Windows.Forms.CheckBox chbDopSoglasheniye;
        private System.Windows.Forms.ComboBox cboActVozvr;
        private System.Windows.Forms.ComboBox cboRastor;
        private System.Windows.Forms.ComboBox cboAct;
        private System.Windows.Forms.CheckBox chbDogovor;
        private System.Windows.Forms.CheckBox chbAct;
        private System.Windows.Forms.CheckBox chbActVozvr;
        private System.Windows.Forms.CheckBox chbRastor;
        private System.Windows.Forms.ProgressBar prbExcel;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnExit;
    }
}

