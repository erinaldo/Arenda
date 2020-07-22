namespace Arenda
{
    partial class frmAddPayment
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAddPayment));
            this.btnQuit = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.lblDate = new System.Windows.Forms.Label();
            this.lblSum = new System.Windows.Forms.Label();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.txtSum = new System.Windows.Forms.TextBox();
            this.lblRub = new System.Windows.Forms.Label();
            this.gbxSign = new System.Windows.Forms.GroupBox();
            this.rbRek = new System.Windows.Forms.RadioButton();
            this.rbAr = new System.Windows.Forms.RadioButton();
            this.txtTel = new System.Windows.Forms.TextBox();
            this.lblTel = new System.Windows.Forms.Label();
            this.lblTelRub = new System.Windows.Forms.Label();
            this.prbExcel = new System.Windows.Forms.ProgressBar();
            this.bgwToExcel = new System.ComponentModel.BackgroundWorker();
            this.gbxSign.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnQuit
            // 
            this.btnQuit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnQuit.Image = ((System.Drawing.Image)(resources.GetObject("btnQuit.Image")));
            this.btnQuit.Location = new System.Drawing.Point(289, 120);
            this.btnQuit.Name = "btnQuit";
            this.btnQuit.Size = new System.Drawing.Size(30, 30);
            this.btnQuit.TabIndex = 40;
            this.toolTip1.SetToolTip(this.btnQuit, "Выход");
            this.btnQuit.UseVisualStyleBackColor = true;
            this.btnQuit.Click += new System.EventHandler(this.btnQuit_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.Location = new System.Drawing.Point(253, 120);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(30, 30);
            this.btnSave.TabIndex = 41;
            this.toolTip1.SetToolTip(this.btnSave, "Сохранить");
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.Location = new System.Drawing.Point(12, 28);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(39, 13);
            this.lblDate.TabIndex = 42;
            this.lblDate.Text = "Дата: ";
            // 
            // lblSum
            // 
            this.lblSum.AutoSize = true;
            this.lblSum.Location = new System.Drawing.Point(12, 53);
            this.lblSum.Name = "lblSum";
            this.lblSum.Size = new System.Drawing.Size(84, 13);
            this.lblSum.TabIndex = 43;
            this.lblSum.Text = "Сумма оплаты:";
            // 
            // dtpDate
            // 
            this.dtpDate.Location = new System.Drawing.Point(114, 24);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(198, 20);
            this.dtpDate.TabIndex = 44;
            this.dtpDate.ValueChanged += new System.EventHandler(this.dtpDate_ValueChanged);
            // 
            // txtSum
            // 
            this.txtSum.Location = new System.Drawing.Point(114, 50);
            this.txtSum.Name = "txtSum";
            this.txtSum.Size = new System.Drawing.Size(166, 20);
            this.txtSum.TabIndex = 45;
            this.txtSum.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtSum.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSum_KeyPress);
            this.txtSum.Leave += new System.EventHandler(this.txtSum_Leave);
            // 
            // lblRub
            // 
            this.lblRub.AutoSize = true;
            this.lblRub.Location = new System.Drawing.Point(289, 53);
            this.lblRub.Name = "lblRub";
            this.lblRub.Size = new System.Drawing.Size(27, 13);
            this.lblRub.TabIndex = 46;
            this.lblRub.Text = "руб.";
            // 
            // gbxSign
            // 
            this.gbxSign.Controls.Add(this.rbRek);
            this.gbxSign.Controls.Add(this.rbAr);
            this.gbxSign.Location = new System.Drawing.Point(12, 104);
            this.gbxSign.Name = "gbxSign";
            this.gbxSign.Size = new System.Drawing.Size(156, 45);
            this.gbxSign.TabIndex = 47;
            this.gbxSign.TabStop = false;
            this.gbxSign.Text = "Признак оплаты";
            // 
            // rbRek
            // 
            this.rbRek.AutoSize = true;
            this.rbRek.Location = new System.Drawing.Point(74, 19);
            this.rbRek.Name = "rbRek";
            this.rbRek.Size = new System.Drawing.Size(70, 17);
            this.rbRek.TabIndex = 2;
            this.rbRek.Text = "Реклама";
            this.rbRek.UseVisualStyleBackColor = true;
            // 
            // rbAr
            // 
            this.rbAr.AutoSize = true;
            this.rbAr.Checked = true;
            this.rbAr.Location = new System.Drawing.Point(6, 19);
            this.rbAr.Name = "rbAr";
            this.rbAr.Size = new System.Drawing.Size(62, 17);
            this.rbAr.TabIndex = 0;
            this.rbAr.TabStop = true;
            this.rbAr.Text = "Аренда";
            this.rbAr.UseVisualStyleBackColor = true;
            // 
            // txtTel
            // 
            this.txtTel.Location = new System.Drawing.Point(150, 78);
            this.txtTel.Name = "txtTel";
            this.txtTel.ReadOnly = true;
            this.txtTel.Size = new System.Drawing.Size(130, 20);
            this.txtTel.TabIndex = 49;
            this.txtTel.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblTel
            // 
            this.lblTel.AutoSize = true;
            this.lblTel.Location = new System.Drawing.Point(12, 81);
            this.lblTel.Name = "lblTel";
            this.lblTel.Size = new System.Drawing.Size(132, 13);
            this.lblTel.TabIndex = 48;
            this.lblTel.Text = "В том числе за телефон:";
            // 
            // lblTelRub
            // 
            this.lblTelRub.AutoSize = true;
            this.lblTelRub.Location = new System.Drawing.Point(289, 81);
            this.lblTelRub.Name = "lblTelRub";
            this.lblTelRub.Size = new System.Drawing.Size(27, 13);
            this.lblTelRub.TabIndex = 50;
            this.lblTelRub.Text = "руб.";
            // 
            // prbExcel
            // 
            this.prbExcel.Location = new System.Drawing.Point(12, 5);
            this.prbExcel.Name = "prbExcel";
            this.prbExcel.Size = new System.Drawing.Size(300, 13);
            this.prbExcel.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.prbExcel.TabIndex = 51;
            this.prbExcel.Visible = false;
            // 
            // bgwToExcel
            // 
            this.bgwToExcel.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwToExcel_DoWork);
            this.bgwToExcel.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwToExcel_RunWorkerCompleted);
            // 
            // frmAddPayment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(331, 162);
            this.ControlBox = false;
            this.Controls.Add(this.prbExcel);
            this.Controls.Add(this.lblTelRub);
            this.Controls.Add(this.txtTel);
            this.Controls.Add(this.lblTel);
            this.Controls.Add(this.gbxSign);
            this.Controls.Add(this.lblRub);
            this.Controls.Add(this.txtSum);
            this.Controls.Add(this.dtpDate);
            this.Controls.Add(this.lblSum);
            this.Controls.Add(this.lblDate);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnQuit);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAddPayment";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmAddPayment";
            this.Load += new System.EventHandler(this.frmAddPayment_Load);
            this.Click += new System.EventHandler(this.frmAddPayment_Click);
            this.gbxSign.ResumeLayout(false);
            this.gbxSign.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnQuit;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.Label lblSum;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.TextBox txtSum;
        private System.Windows.Forms.Label lblRub;
        private System.Windows.Forms.GroupBox gbxSign;
        private System.Windows.Forms.RadioButton rbRek;
        private System.Windows.Forms.RadioButton rbAr;
        private System.Windows.Forms.TextBox txtTel;
        private System.Windows.Forms.Label lblTel;
        private System.Windows.Forms.Label lblTelRub;
        private System.Windows.Forms.ProgressBar prbExcel;
        private System.ComponentModel.BackgroundWorker bgwToExcel;
    }
}