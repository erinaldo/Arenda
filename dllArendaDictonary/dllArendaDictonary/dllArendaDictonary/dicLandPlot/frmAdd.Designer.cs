namespace dllArendaDictonary.dicLandPlot
{
    partial class frmAdd
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
            this.tbNumber = new System.Windows.Forms.TextBox();
            this.lNumber = new System.Windows.Forms.Label();
            this.btSave = new System.Windows.Forms.Button();
            this.btClose = new System.Windows.Forms.Button();
            this.lObject = new System.Windows.Forms.Label();
            this.cmbObject = new System.Windows.Forms.ComboBox();
            this.lArea = new System.Windows.Forms.Label();
            this.tbArea = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // tbNumber
            // 
            this.tbNumber.Location = new System.Drawing.Point(17, 65);
            this.tbNumber.MaxLength = 1024;
            this.tbNumber.Multiline = true;
            this.tbNumber.Name = "tbNumber";
            this.tbNumber.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbNumber.Size = new System.Drawing.Size(326, 75);
            this.tbNumber.TabIndex = 0;
            this.tbNumber.TextChanged += new System.EventHandler(this.tbName_TextChanged);
            // 
            // lNumber
            // 
            this.lNumber.AutoSize = true;
            this.lNumber.Location = new System.Drawing.Point(17, 49);
            this.lNumber.Name = "lNumber";
            this.lNumber.Size = new System.Drawing.Size(147, 13);
            this.lNumber.TabIndex = 6;
            this.lNumber.Text = "Номер земельного участка";
            // 
            // btSave
            // 
            this.btSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btSave.Image = global::dllArendaDictonary.Properties.Resources.Save;
            this.btSave.Location = new System.Drawing.Point(273, 183);
            this.btSave.Name = "btSave";
            this.btSave.Size = new System.Drawing.Size(32, 32);
            this.btSave.TabIndex = 2;
            this.btSave.UseVisualStyleBackColor = true;
            this.btSave.Click += new System.EventHandler(this.btSave_Click);
            // 
            // btClose
            // 
            this.btClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btClose.Image = global::dllArendaDictonary.Properties.Resources.Exit;
            this.btClose.Location = new System.Drawing.Point(311, 183);
            this.btClose.Name = "btClose";
            this.btClose.Size = new System.Drawing.Size(32, 32);
            this.btClose.TabIndex = 3;
            this.btClose.UseVisualStyleBackColor = true;
            this.btClose.Click += new System.EventHandler(this.btClose_Click);
            // 
            // lObject
            // 
            this.lObject.AutoSize = true;
            this.lObject.Location = new System.Drawing.Point(17, 9);
            this.lObject.Name = "lObject";
            this.lObject.Size = new System.Drawing.Size(228, 13);
            this.lObject.TabIndex = 6;
            this.lObject.Text = "Объект расположения земельного участка";
            // 
            // cmbObject
            // 
            this.cmbObject.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbObject.FormattingEnabled = true;
            this.cmbObject.Location = new System.Drawing.Point(17, 25);
            this.cmbObject.Name = "cmbObject";
            this.cmbObject.Size = new System.Drawing.Size(326, 21);
            this.cmbObject.TabIndex = 7;
            // 
            // lArea
            // 
            this.lArea.AutoSize = true;
            this.lArea.Location = new System.Drawing.Point(17, 154);
            this.lArea.Name = "lArea";
            this.lArea.Size = new System.Drawing.Size(152, 13);
            this.lArea.TabIndex = 6;
            this.lArea.Text = "Размер земельного участка";
            // 
            // tbArea
            // 
            this.tbArea.Location = new System.Drawing.Point(175, 150);
            this.tbArea.MaxLength = 15;
            this.tbArea.Name = "tbArea";
            this.tbArea.Size = new System.Drawing.Size(141, 20);
            this.tbArea.TabIndex = 0;
            this.tbArea.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbArea.TextChanged += new System.EventHandler(this.tbName_TextChanged);
            this.tbArea.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbArea_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(322, 154);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(21, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "м2";
            // 
            // frmAdd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(355, 227);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbObject);
            this.Controls.Add(this.tbArea);
            this.Controls.Add(this.lArea);
            this.Controls.Add(this.lObject);
            this.Controls.Add(this.lNumber);
            this.Controls.Add(this.tbNumber);
            this.Controls.Add(this.btSave);
            this.Controls.Add(this.btClose);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAdd";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmAdd";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmAdd_FormClosing);
            this.Load += new System.EventHandler(this.frmAdd_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btClose;
        private System.Windows.Forms.Button btSave;
        private System.Windows.Forms.TextBox tbNumber;
        private System.Windows.Forms.Label lNumber;
        private System.Windows.Forms.Label lObject;
        private System.Windows.Forms.ComboBox cmbObject;
        private System.Windows.Forms.Label lArea;
        private System.Windows.Forms.TextBox tbArea;
        private System.Windows.Forms.Label label1;
    }
}