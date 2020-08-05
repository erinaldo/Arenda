namespace dllArendaDictonary.dicAdvertisingSpace
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
            this.lBuilding = new System.Windows.Forms.Label();
            this.cmbBuilding = new System.Windows.Forms.ComboBox();
            this.gbSizePlace = new System.Windows.Forms.GroupBox();
            this.lWidth = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lLength = new System.Windows.Forms.Label();
            this.tbLength = new System.Windows.Forms.TextBox();
            this.tbWidth = new System.Windows.Forms.TextBox();
            this.gbSizePlace.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbNumber
            // 
            this.tbNumber.Location = new System.Drawing.Point(17, 113);
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
            this.lNumber.Location = new System.Drawing.Point(17, 97);
            this.lNumber.Name = "lNumber";
            this.lNumber.Size = new System.Drawing.Size(142, 13);
            this.lNumber.TabIndex = 6;
            this.lNumber.Text = "Номер рекламного места ";
            // 
            // btSave
            // 
            this.btSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btSave.Image = global::dllArendaDictonary.Properties.Resources.Save;
            this.btSave.Location = new System.Drawing.Point(273, 276);
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
            this.btClose.Location = new System.Drawing.Point(311, 276);
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
            this.lObject.Size = new System.Drawing.Size(220, 13);
            this.lObject.TabIndex = 6;
            this.lObject.Text = "Объект расположения рекламного места";
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
            // lBuilding
            // 
            this.lBuilding.AutoSize = true;
            this.lBuilding.Location = new System.Drawing.Point(17, 54);
            this.lBuilding.Name = "lBuilding";
            this.lBuilding.Size = new System.Drawing.Size(280, 13);
            this.lBuilding.TabIndex = 6;
            this.lBuilding.Text = "Здание, на котором располагается рекламное место";
            // 
            // cmbBuilding
            // 
            this.cmbBuilding.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBuilding.FormattingEnabled = true;
            this.cmbBuilding.Location = new System.Drawing.Point(17, 70);
            this.cmbBuilding.Name = "cmbBuilding";
            this.cmbBuilding.Size = new System.Drawing.Size(326, 21);
            this.cmbBuilding.TabIndex = 7;
            // 
            // gbSizePlace
            // 
            this.gbSizePlace.Controls.Add(this.lWidth);
            this.gbSizePlace.Controls.Add(this.label1);
            this.gbSizePlace.Controls.Add(this.lLength);
            this.gbSizePlace.Controls.Add(this.tbLength);
            this.gbSizePlace.Controls.Add(this.tbWidth);
            this.gbSizePlace.Location = new System.Drawing.Point(77, 194);
            this.gbSizePlace.Name = "gbSizePlace";
            this.gbSizePlace.Size = new System.Drawing.Size(220, 71);
            this.gbSizePlace.TabIndex = 8;
            this.gbSizePlace.TabStop = false;
            this.gbSizePlace.Text = "Размер места";
            // 
            // lWidth
            // 
            this.lWidth.AutoSize = true;
            this.lWidth.Location = new System.Drawing.Point(6, 45);
            this.lWidth.Name = "lWidth";
            this.lWidth.Size = new System.Drawing.Size(46, 13);
            this.lWidth.TabIndex = 6;
            this.lWidth.Text = "Ширина";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(188, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(26, 16);
            this.label1.TabIndex = 6;
            this.label1.Text = "мм";
            // 
            // lLength
            // 
            this.lLength.AutoSize = true;
            this.lLength.Location = new System.Drawing.Point(12, 16);
            this.lLength.Name = "lLength";
            this.lLength.Size = new System.Drawing.Size(40, 13);
            this.lLength.TabIndex = 6;
            this.lLength.Text = "Длина";
            // 
            // tbLength
            // 
            this.tbLength.Location = new System.Drawing.Point(58, 12);
            this.tbLength.MaxLength = 15;
            this.tbLength.Name = "tbLength";
            this.tbLength.Size = new System.Drawing.Size(126, 20);
            this.tbLength.TabIndex = 0;
            this.tbLength.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbLength.TextChanged += new System.EventHandler(this.tbName_TextChanged);
            this.tbLength.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbLength_KeyPress);
            // 
            // tbWidth
            // 
            this.tbWidth.Location = new System.Drawing.Point(58, 41);
            this.tbWidth.MaxLength = 15;
            this.tbWidth.Name = "tbWidth";
            this.tbWidth.Size = new System.Drawing.Size(126, 20);
            this.tbWidth.TabIndex = 0;
            this.tbWidth.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbWidth.TextChanged += new System.EventHandler(this.tbName_TextChanged);
            this.tbWidth.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbLength_KeyPress);
            // 
            // frmAdd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(355, 320);
            this.Controls.Add(this.gbSizePlace);
            this.Controls.Add(this.cmbBuilding);
            this.Controls.Add(this.cmbObject);
            this.Controls.Add(this.lObject);
            this.Controls.Add(this.lBuilding);
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
            this.gbSizePlace.ResumeLayout(false);
            this.gbSizePlace.PerformLayout();
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
        private System.Windows.Forms.Label lBuilding;
        private System.Windows.Forms.ComboBox cmbBuilding;
        private System.Windows.Forms.GroupBox gbSizePlace;
        private System.Windows.Forms.Label lWidth;
        private System.Windows.Forms.Label lLength;
        private System.Windows.Forms.TextBox tbLength;
        private System.Windows.Forms.TextBox tbWidth;
        private System.Windows.Forms.Label label1;
    }
}