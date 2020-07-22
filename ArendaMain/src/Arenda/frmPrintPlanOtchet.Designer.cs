namespace Arenda
{
    partial class frmPrintPlanOtchet
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPrintPlanOtchet));
            this.cbMonth = new System.Windows.Forms.ComboBox();
            this.lblMonth = new System.Windows.Forms.Label();
            this.lblYear = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btnQuit = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.cbYear = new System.Windows.Forms.ComboBox();
            this.prbExcel = new System.Windows.Forms.ProgressBar();
            this.bgwToExcel = new System.ComponentModel.BackgroundWorker();
            this.lblPercent = new System.Windows.Forms.Label();
            this.prbExcel2 = new System.Windows.Forms.ProgressBar();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbPlan = new System.Windows.Forms.RadioButton();
            this.rbFact = new System.Windows.Forms.RadioButton();
            this.rbCommon = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbMonth
            // 
            this.cbMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbMonth.FormattingEnabled = true;
            this.cbMonth.Location = new System.Drawing.Point(61, 15);
            this.cbMonth.MaxDropDownItems = 12;
            this.cbMonth.Name = "cbMonth";
            this.cbMonth.Size = new System.Drawing.Size(183, 21);
            this.cbMonth.TabIndex = 0;
            // 
            // lblMonth
            // 
            this.lblMonth.AutoSize = true;
            this.lblMonth.Location = new System.Drawing.Point(12, 18);
            this.lblMonth.Name = "lblMonth";
            this.lblMonth.Size = new System.Drawing.Size(43, 13);
            this.lblMonth.TabIndex = 1;
            this.lblMonth.Text = "Месяц:";
            // 
            // lblYear
            // 
            this.lblYear.AutoSize = true;
            this.lblYear.Location = new System.Drawing.Point(12, 50);
            this.lblYear.Name = "lblYear";
            this.lblYear.Size = new System.Drawing.Size(28, 13);
            this.lblYear.TabIndex = 2;
            this.lblYear.Text = "Год:";
            // 
            // btnQuit
            // 
            this.btnQuit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnQuit.Image = ((System.Drawing.Image)(resources.GetObject("btnQuit.Image")));
            this.btnQuit.Location = new System.Drawing.Point(301, 108);
            this.btnQuit.Name = "btnQuit";
            this.btnQuit.Size = new System.Drawing.Size(30, 30);
            this.btnQuit.TabIndex = 41;
            this.toolTip1.SetToolTip(this.btnQuit, "Выход");
            this.btnQuit.UseVisualStyleBackColor = true;
            this.btnQuit.Click += new System.EventHandler(this.btnQuit_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrint.Image = ((System.Drawing.Image)(resources.GetObject("btnPrint.Image")));
            this.btnPrint.Location = new System.Drawing.Point(265, 108);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(30, 30);
            this.btnPrint.TabIndex = 42;
            this.toolTip1.SetToolTip(this.btnPrint, "Печать");
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // cbYear
            // 
            this.cbYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbYear.FormattingEnabled = true;
            this.cbYear.Location = new System.Drawing.Point(61, 47);
            this.cbYear.Name = "cbYear";
            this.cbYear.Size = new System.Drawing.Size(183, 21);
            this.cbYear.TabIndex = 43;
            // 
            // prbExcel
            // 
            this.prbExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.prbExcel.Location = new System.Drawing.Point(15, 108);
            this.prbExcel.Name = "prbExcel";
            this.prbExcel.Size = new System.Drawing.Size(244, 15);
            this.prbExcel.TabIndex = 67;
            this.prbExcel.Visible = false;
            // 
            // bgwToExcel
            // 
            this.bgwToExcel.WorkerReportsProgress = true;
            this.bgwToExcel.WorkerSupportsCancellation = true;
            this.bgwToExcel.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwToExcel_DoWork);
            this.bgwToExcel.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgwToExcel_ProgressChanged);
            this.bgwToExcel.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwToExcel_RunWorkerCompleted);
            // 
            // lblPercent
            // 
            this.lblPercent.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPercent.AutoSize = true;
            this.lblPercent.Location = new System.Drawing.Point(12, 92);
            this.lblPercent.Name = "lblPercent";
            this.lblPercent.Size = new System.Drawing.Size(35, 13);
            this.lblPercent.TabIndex = 68;
            this.lblPercent.Text = "label1";
            this.lblPercent.Visible = false;
            // 
            // prbExcel2
            // 
            this.prbExcel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.prbExcel2.Location = new System.Drawing.Point(15, 129);
            this.prbExcel2.Name = "prbExcel2";
            this.prbExcel2.Size = new System.Drawing.Size(244, 10);
            this.prbExcel2.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.prbExcel2.TabIndex = 69;
            this.prbExcel2.Visible = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbPlan);
            this.groupBox1.Controls.Add(this.rbFact);
            this.groupBox1.Controls.Add(this.rbCommon);
            this.groupBox1.Location = new System.Drawing.Point(250, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(82, 92);
            this.groupBox1.TabIndex = 70;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Вид отчета";
            // 
            // rbPlan
            // 
            this.rbPlan.AutoSize = true;
            this.rbPlan.Location = new System.Drawing.Point(9, 65);
            this.rbPlan.Name = "rbPlan";
            this.rbPlan.Size = new System.Drawing.Size(52, 17);
            this.rbPlan.TabIndex = 2;
            this.rbPlan.Text = "план ";
            this.rbPlan.UseVisualStyleBackColor = true;
            // 
            // rbFact
            // 
            this.rbFact.AutoSize = true;
            this.rbFact.Location = new System.Drawing.Point(9, 43);
            this.rbFact.Name = "rbFact";
            this.rbFact.Size = new System.Drawing.Size(53, 17);
            this.rbFact.TabIndex = 1;
            this.rbFact.Text = "факт ";
            this.rbFact.UseVisualStyleBackColor = true;
            // 
            // rbCommon
            // 
            this.rbCommon.AutoSize = true;
            this.rbCommon.Checked = true;
            this.rbCommon.Location = new System.Drawing.Point(9, 19);
            this.rbCommon.Name = "rbCommon";
            this.rbCommon.Size = new System.Drawing.Size(58, 17);
            this.rbCommon.TabIndex = 0;
            this.rbCommon.TabStop = true;
            this.rbCommon.Text = "общий";
            this.rbCommon.UseVisualStyleBackColor = true;
            // 
            // frmPrintPlanOtchet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(339, 146);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.prbExcel2);
            this.Controls.Add(this.lblPercent);
            this.Controls.Add(this.prbExcel);
            this.Controls.Add(this.cbYear);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.btnQuit);
            this.Controls.Add(this.lblYear);
            this.Controls.Add(this.lblMonth);
            this.Controls.Add(this.cbMonth);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmPrintPlanOtchet";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Сформировать план-отчет";
            this.Load += new System.EventHandler(this.frmPrintPlanOtchet_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbMonth;
        private System.Windows.Forms.Label lblMonth;
        private System.Windows.Forms.Label lblYear;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button btnQuit;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.ComboBox cbYear;
        private System.Windows.Forms.ProgressBar prbExcel;
        private System.ComponentModel.BackgroundWorker bgwToExcel;
        private System.Windows.Forms.Label lblPercent;
        private System.Windows.Forms.ProgressBar prbExcel2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbPlan;
        private System.Windows.Forms.RadioButton rbFact;
        private System.Windows.Forms.RadioButton rbCommon;
    }
}