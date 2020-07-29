namespace Arenda
{
    partial class frmAddTaxes
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAddTaxes));
            this.lblRub = new System.Windows.Forms.Label();
            this.txtSum = new System.Windows.Forms.TextBox();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.lblSum = new System.Windows.Forms.Label();
            this.lblDate = new System.Windows.Forms.Label();
            this.lblComment = new System.Windows.Forms.Label();
            this.txtComment = new System.Windows.Forms.TextBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btnSave = new System.Windows.Forms.Button();
            this.btnQuit = new System.Windows.Forms.Button();
            this.lblAnotherPay = new System.Windows.Forms.Label();
            this.cboAnotherPay = new System.Windows.Forms.ComboBox();
            this.tbDataMeters = new System.Windows.Forms.TextBox();
            this.lMeters = new System.Windows.Forms.Label();
            this.cmbPlaneDate = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblRub
            // 
            this.lblRub.AutoSize = true;
            this.lblRub.Location = new System.Drawing.Point(297, 99);
            this.lblRub.Name = "lblRub";
            this.lblRub.Size = new System.Drawing.Size(27, 13);
            this.lblRub.TabIndex = 51;
            this.lblRub.Text = "руб.";
            // 
            // txtSum
            // 
            this.txtSum.Location = new System.Drawing.Point(138, 96);
            this.txtSum.Name = "txtSum";
            this.txtSum.Size = new System.Drawing.Size(153, 20);
            this.txtSum.TabIndex = 50;
            this.txtSum.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtSum.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSum_KeyPress);
            this.txtSum.Leave += new System.EventHandler(this.txtSum_Leave);
            // 
            // dtpDate
            // 
            this.dtpDate.Location = new System.Drawing.Point(138, 5);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(186, 20);
            this.dtpDate.TabIndex = 49;
            // 
            // lblSum
            // 
            this.lblSum.AutoSize = true;
            this.lblSum.Location = new System.Drawing.Point(14, 99);
            this.lblSum.Name = "lblSum";
            this.lblSum.Size = new System.Drawing.Size(108, 13);
            this.lblSum.TabIndex = 48;
            this.lblSum.Text = "Сумма доп. оплаты:";
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.Location = new System.Drawing.Point(14, 9);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(39, 13);
            this.lblDate.TabIndex = 47;
            this.lblDate.Text = "Дата: ";
            // 
            // lblComment
            // 
            this.lblComment.AutoSize = true;
            this.lblComment.Location = new System.Drawing.Point(14, 148);
            this.lblComment.Name = "lblComment";
            this.lblComment.Size = new System.Drawing.Size(73, 13);
            this.lblComment.TabIndex = 74;
            this.lblComment.Text = "Примечание:";
            // 
            // txtComment
            // 
            this.txtComment.AllowDrop = true;
            this.txtComment.Location = new System.Drawing.Point(138, 148);
            this.txtComment.MaxLength = 255;
            this.txtComment.Multiline = true;
            this.txtComment.Name = "txtComment";
            this.txtComment.Size = new System.Drawing.Size(186, 66);
            this.txtComment.TabIndex = 73;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.Location = new System.Drawing.Point(267, 220);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(30, 30);
            this.btnSave.TabIndex = 76;
            this.toolTip1.SetToolTip(this.btnSave, "Сохранить");
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnQuit
            // 
            this.btnQuit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnQuit.Image = ((System.Drawing.Image)(resources.GetObject("btnQuit.Image")));
            this.btnQuit.Location = new System.Drawing.Point(303, 220);
            this.btnQuit.Name = "btnQuit";
            this.btnQuit.Size = new System.Drawing.Size(30, 30);
            this.btnQuit.TabIndex = 75;
            this.toolTip1.SetToolTip(this.btnQuit, "Выход");
            this.btnQuit.UseVisualStyleBackColor = true;
            this.btnQuit.Click += new System.EventHandler(this.btnQuit_Click);
            // 
            // lblAnotherPay
            // 
            this.lblAnotherPay.AutoSize = true;
            this.lblAnotherPay.Location = new System.Drawing.Point(14, 72);
            this.lblAnotherPay.Name = "lblAnotherPay";
            this.lblAnotherPay.Size = new System.Drawing.Size(102, 13);
            this.lblAnotherPay.TabIndex = 78;
            this.lblAnotherPay.Text = "Наим. доп оплаты:";
            // 
            // cboAnotherPay
            // 
            this.cboAnotherPay.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboAnotherPay.FormattingEnabled = true;
            this.cboAnotherPay.Location = new System.Drawing.Point(138, 69);
            this.cboAnotherPay.Name = "cboAnotherPay";
            this.cboAnotherPay.Size = new System.Drawing.Size(139, 21);
            this.cboAnotherPay.TabIndex = 77;
            this.cboAnotherPay.SelectionChangeCommitted += new System.EventHandler(this.cboAnotherPay_SelectionChangeCommitted);
            // 
            // tbDataMeters
            // 
            this.tbDataMeters.Location = new System.Drawing.Point(138, 122);
            this.tbDataMeters.Name = "tbDataMeters";
            this.tbDataMeters.Size = new System.Drawing.Size(153, 20);
            this.tbDataMeters.TabIndex = 80;
            this.tbDataMeters.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbDataMeters.Leave += new System.EventHandler(this.tbDataMeters_Leave);
            // 
            // lMeters
            // 
            this.lMeters.AutoSize = true;
            this.lMeters.Location = new System.Drawing.Point(14, 125);
            this.lMeters.Name = "lMeters";
            this.lMeters.Size = new System.Drawing.Size(120, 13);
            this.lMeters.TabIndex = 79;
            this.lMeters.Text = "Показание счётчиков:";
            // 
            // cmbPlaneDate
            // 
            this.cmbPlaneDate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPlaneDate.FormattingEnabled = true;
            this.cmbPlaneDate.Location = new System.Drawing.Point(138, 31);
            this.cmbPlaneDate.Name = "cmbPlaneDate";
            this.cmbPlaneDate.Size = new System.Drawing.Size(186, 21);
            this.cmbPlaneDate.TabIndex = 82;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 13);
            this.label2.TabIndex = 81;
            this.label2.Text = "План-отчет за:";
            // 
            // frmAddTaxes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(337, 254);
            this.ControlBox = false;
            this.Controls.Add(this.cmbPlaneDate);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbDataMeters);
            this.Controls.Add(this.lMeters);
            this.Controls.Add(this.lblAnotherPay);
            this.Controls.Add(this.cboAnotherPay);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnQuit);
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
            this.Name = "frmAddTaxes";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmAddTaxes";
            this.Load += new System.EventHandler(this.frmAddTaxes_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblRub;
        private System.Windows.Forms.TextBox txtSum;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.Label lblSum;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.Label lblComment;
        private System.Windows.Forms.TextBox txtComment;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnQuit;
        private System.Windows.Forms.Label lblAnotherPay;
        private System.Windows.Forms.ComboBox cboAnotherPay;
        private System.Windows.Forms.TextBox tbDataMeters;
        private System.Windows.Forms.Label lMeters;
        private System.Windows.Forms.ComboBox cmbPlaneDate;
        private System.Windows.Forms.Label label2;
    }
}