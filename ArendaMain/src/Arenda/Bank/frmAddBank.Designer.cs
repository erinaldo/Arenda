namespace Arenda.Bank
{
    partial class frmAddBank
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAddBank));
            this.btExit = new System.Windows.Forms.Button();
            this.btAdd = new System.Windows.Forms.Button();
            this.btSelectBank = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tbName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbKS = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbBik = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbRS = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btExit
            // 
            this.btExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btExit.Image = ((System.Drawing.Image)(resources.GetObject("btExit.Image")));
            this.btExit.Location = new System.Drawing.Point(353, 183);
            this.btExit.Name = "btExit";
            this.btExit.Size = new System.Drawing.Size(32, 32);
            this.btExit.TabIndex = 37;
            this.btExit.UseVisualStyleBackColor = true;
            this.btExit.Click += new System.EventHandler(this.btExit_Click);
            // 
            // btAdd
            // 
            this.btAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btAdd.Image = global::Arenda.Properties.Resources.saveHS;
            this.btAdd.Location = new System.Drawing.Point(312, 183);
            this.btAdd.Name = "btAdd";
            this.btAdd.Size = new System.Drawing.Size(32, 32);
            this.btAdd.TabIndex = 36;
            this.btAdd.UseVisualStyleBackColor = true;
            this.btAdd.Click += new System.EventHandler(this.btAdd_Click);
            // 
            // btSelectBank
            // 
            this.btSelectBank.Location = new System.Drawing.Point(184, 127);
            this.btSelectBank.Name = "btSelectBank";
            this.btSelectBank.Size = new System.Drawing.Size(159, 23);
            this.btSelectBank.TabIndex = 38;
            this.btSelectBank.Text = "Выбрать банк";
            this.btSelectBank.UseVisualStyleBackColor = true;
            this.btSelectBank.Click += new System.EventHandler(this.btSelectBank_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 13);
            this.label1.TabIndex = 39;
            this.label1.Text = "Наименование банка";
            // 
            // tbName
            // 
            this.tbName.Location = new System.Drawing.Point(144, 19);
            this.tbName.Multiline = true;
            this.tbName.Name = "tbName";
            this.tbName.ReadOnly = true;
            this.tbName.Size = new System.Drawing.Size(237, 50);
            this.tbName.TabIndex = 40;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 79);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(25, 13);
            this.label2.TabIndex = 39;
            this.label2.Text = "К/с";
            // 
            // tbKS
            // 
            this.tbKS.Location = new System.Drawing.Point(144, 75);
            this.tbKS.Name = "tbKS";
            this.tbKS.ReadOnly = true;
            this.tbKS.Size = new System.Drawing.Size(237, 20);
            this.tbKS.TabIndex = 40;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(23, 105);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 13);
            this.label3.TabIndex = 39;
            this.label3.Text = "БИК";
            // 
            // tbBik
            // 
            this.tbBik.Location = new System.Drawing.Point(144, 101);
            this.tbBik.Name = "tbBik";
            this.tbBik.ReadOnly = true;
            this.tbBik.Size = new System.Drawing.Size(237, 20);
            this.tbBik.TabIndex = 40;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(23, 160);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(25, 13);
            this.label4.TabIndex = 39;
            this.label4.Text = "Р/с";
            // 
            // tbRS
            // 
            this.tbRS.Location = new System.Drawing.Point(144, 156);
            this.tbRS.MaxLength = 20;
            this.tbRS.Name = "tbRS";
            this.tbRS.Size = new System.Drawing.Size(237, 20);
            this.tbRS.TabIndex = 40;
            this.tbRS.TextChanged += new System.EventHandler(this.tbRS_TextChanged);
            this.tbRS.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbRS_KeyPress);
            // 
            // frmAddBank
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(393, 223);
            this.ControlBox = false;
            this.Controls.Add(this.tbRS);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tbBik);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbKS);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btSelectBank);
            this.Controls.Add(this.btExit);
            this.Controls.Add(this.btAdd);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAddBank";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmAddBank";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmAddBank_FormClosing);
            this.Load += new System.EventHandler(this.frmAddBank_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btExit;
        private System.Windows.Forms.Button btAdd;
        private System.Windows.Forms.Button btSelectBank;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbKS;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbBik;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbRS;
    }
}