namespace Arenda
{
    partial class PrintForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PrintForm));
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.chbDogovor = new System.Windows.Forms.CheckBox();
            this.chbAct = new System.Windows.Forms.CheckBox();
            this.chbRastor = new System.Windows.Forms.CheckBox();
            this.chbActVozvr = new System.Windows.Forms.CheckBox();
            this.bgwToExcel = new System.ComponentModel.BackgroundWorker();
            this.prbExcel = new System.Windows.Forms.ProgressBar();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmbActReklama = new System.Windows.Forms.ComboBox();
            this.chbActReklama = new System.Windows.Forms.CheckBox();
            this.cmbActKomm = new System.Windows.Forms.ComboBox();
            this.chbActKomm = new System.Windows.Forms.CheckBox();
            this.cboDopSoglasheniye = new System.Windows.Forms.ComboBox();
            this.chbDopSoglasheniye = new System.Windows.Forms.CheckBox();
            this.cboActVozvr = new System.Windows.Forms.ComboBox();
            this.cboRastor = new System.Windows.Forms.ComboBox();
            this.cboAct = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnPrint
            // 
            this.btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrint.Image = ((System.Drawing.Image)(resources.GetObject("btnPrint.Image")));
            this.btnPrint.Location = new System.Drawing.Point(366, 215);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(32, 32);
            this.btnPrint.TabIndex = 7;
            this.toolTip1.SetToolTip(this.btnPrint, "Печать");
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.Image = global::Arenda.Properties.Resources.Log_Out_icon1;
            this.btnExit.Location = new System.Drawing.Point(404, 215);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(32, 32);
            this.btnExit.TabIndex = 8;
            this.toolTip1.SetToolTip(this.btnExit, "Выход");
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // chbDogovor
            // 
            this.chbDogovor.AutoSize = true;
            this.chbDogovor.Location = new System.Drawing.Point(6, 19);
            this.chbDogovor.Name = "chbDogovor";
            this.chbDogovor.Size = new System.Drawing.Size(111, 17);
            this.chbDogovor.TabIndex = 1;
            this.chbDogovor.Text = "Договор аренды";
            this.chbDogovor.UseVisualStyleBackColor = true;
            // 
            // chbAct
            // 
            this.chbAct.AutoSize = true;
            this.chbAct.Location = new System.Drawing.Point(6, 179);
            this.chbAct.Name = "chbAct";
            this.chbAct.Size = new System.Drawing.Size(135, 17);
            this.chbAct.TabIndex = 3;
            this.chbAct.Text = "Акт приёма-передачи";
            this.chbAct.UseVisualStyleBackColor = true;
            this.chbAct.Click += new System.EventHandler(this.chbAct_Click);
            // 
            // chbRastor
            // 
            this.chbRastor.AutoSize = true;
            this.chbRastor.Location = new System.Drawing.Point(6, 71);
            this.chbRastor.Name = "chbRastor";
            this.chbRastor.Size = new System.Drawing.Size(217, 17);
            this.chbRastor.TabIndex = 4;
            this.chbRastor.Text = "Соглашение о расторжении договора";
            this.chbRastor.UseVisualStyleBackColor = true;
            // 
            // chbActVozvr
            // 
            this.chbActVozvr.AutoSize = true;
            this.chbActVozvr.Location = new System.Drawing.Point(6, 152);
            this.chbActVozvr.Name = "chbActVozvr";
            this.chbActVozvr.Size = new System.Drawing.Size(185, 17);
            this.chbActVozvr.TabIndex = 5;
            this.chbActVozvr.Text = "Акт приёма-передачи (возврат)";
            this.chbActVozvr.UseVisualStyleBackColor = true;
            this.chbActVozvr.Click += new System.EventHandler(this.chbActVozvr_Click);
            // 
            // bgwToExcel
            // 
            this.bgwToExcel.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwToExcel_DoWork);
            this.bgwToExcel.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwToExcel_RunWorkerCompleted);
            // 
            // prbExcel
            // 
            this.prbExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.prbExcel.Location = new System.Drawing.Point(13, 228);
            this.prbExcel.Name = "prbExcel";
            this.prbExcel.Size = new System.Drawing.Size(185, 19);
            this.prbExcel.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.prbExcel.TabIndex = 47;
            this.prbExcel.Visible = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmbActReklama);
            this.groupBox1.Controls.Add(this.chbActReklama);
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
            this.groupBox1.Location = new System.Drawing.Point(5, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(431, 208);
            this.groupBox1.TabIndex = 48;
            this.groupBox1.TabStop = false;
            // 
            // cmbActReklama
            // 
            this.cmbActReklama.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbActReklama.FormattingEnabled = true;
            this.cmbActReklama.Location = new System.Drawing.Point(283, 123);
            this.cmbActReklama.Name = "cmbActReklama";
            this.cmbActReklama.Size = new System.Drawing.Size(136, 21);
            this.cmbActReklama.TabIndex = 55;
            // 
            // chbActReklama
            // 
            this.chbActReklama.AutoSize = true;
            this.chbActReklama.Location = new System.Drawing.Point(7, 125);
            this.chbActReklama.Name = "chbActReklama";
            this.chbActReklama.Size = new System.Drawing.Size(182, 17);
            this.chbActReklama.TabIndex = 54;
            this.chbActReklama.Text = "Акт приёма-передачи реклама";
            this.chbActReklama.UseVisualStyleBackColor = true;
            // 
            // cmbActKomm
            // 
            this.cmbActKomm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbActKomm.FormattingEnabled = true;
            this.cmbActKomm.Location = new System.Drawing.Point(283, 96);
            this.cmbActKomm.Name = "cmbActKomm";
            this.cmbActKomm.Size = new System.Drawing.Size(136, 21);
            this.cmbActKomm.TabIndex = 53;
            // 
            // chbActKomm
            // 
            this.chbActKomm.AutoSize = true;
            this.chbActKomm.Location = new System.Drawing.Point(6, 98);
            this.chbActKomm.Name = "chbActKomm";
            this.chbActKomm.Size = new System.Drawing.Size(213, 17);
            this.chbActKomm.TabIndex = 52;
            this.chbActKomm.Text = "Акт приёма-передачи коммуникаций";
            this.chbActKomm.UseVisualStyleBackColor = true;
            // 
            // cboDopSoglasheniye
            // 
            this.cboDopSoglasheniye.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDopSoglasheniye.FormattingEnabled = true;
            this.cboDopSoglasheniye.Location = new System.Drawing.Point(283, 44);
            this.cboDopSoglasheniye.Name = "cboDopSoglasheniye";
            this.cboDopSoglasheniye.Size = new System.Drawing.Size(136, 21);
            this.cboDopSoglasheniye.TabIndex = 49;
            // 
            // chbDopSoglasheniye
            // 
            this.chbDopSoglasheniye.AutoSize = true;
            this.chbDopSoglasheniye.Location = new System.Drawing.Point(6, 46);
            this.chbDopSoglasheniye.Name = "chbDopSoglasheniye";
            this.chbDopSoglasheniye.Size = new System.Drawing.Size(114, 17);
            this.chbDopSoglasheniye.TabIndex = 13;
            this.chbDopSoglasheniye.Text = "Доп. соглашение";
            this.chbDopSoglasheniye.UseVisualStyleBackColor = true;
            // 
            // cboActVozvr
            // 
            this.cboActVozvr.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboActVozvr.FormattingEnabled = true;
            this.cboActVozvr.Location = new System.Drawing.Point(283, 150);
            this.cboActVozvr.Name = "cboActVozvr";
            this.cboActVozvr.Size = new System.Drawing.Size(136, 21);
            this.cboActVozvr.TabIndex = 12;
            // 
            // cboRastor
            // 
            this.cboRastor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboRastor.FormattingEnabled = true;
            this.cboRastor.Location = new System.Drawing.Point(283, 69);
            this.cboRastor.Name = "cboRastor";
            this.cboRastor.Size = new System.Drawing.Size(136, 21);
            this.cboRastor.TabIndex = 11;
            // 
            // cboAct
            // 
            this.cboAct.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboAct.FormattingEnabled = true;
            this.cboAct.Location = new System.Drawing.Point(283, 177);
            this.cboAct.Name = "cboAct";
            this.cboAct.Size = new System.Drawing.Size(136, 21);
            this.cboAct.TabIndex = 10;
            // 
            // PrintForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(443, 259);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.prbExcel);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.btnExit);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PrintForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Печать";
            this.Load += new System.EventHandler(this.PrintForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.CheckBox chbDogovor;
        private System.Windows.Forms.CheckBox chbAct;
        private System.Windows.Forms.CheckBox chbRastor;
        private System.Windows.Forms.CheckBox chbActVozvr;
        private System.ComponentModel.BackgroundWorker bgwToExcel;
        private System.Windows.Forms.ProgressBar prbExcel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cboActVozvr;
        private System.Windows.Forms.ComboBox cboRastor;
        private System.Windows.Forms.ComboBox cboAct;
        private System.Windows.Forms.ComboBox cboDopSoglasheniye;
        private System.Windows.Forms.CheckBox chbDopSoglasheniye;
        private System.Windows.Forms.ComboBox cmbActReklama;
        private System.Windows.Forms.CheckBox chbActReklama;
        private System.Windows.Forms.ComboBox cmbActKomm;
        private System.Windows.Forms.CheckBox chbActKomm;
    }
}