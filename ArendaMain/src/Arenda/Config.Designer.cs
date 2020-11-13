namespace Arenda
{
    partial class Config
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbSRKA = new System.Windows.Forms.TextBox();
            this.tbSRZA = new System.Windows.Forms.TextBox();
            this.tbPotD = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.btAddEq = new System.Windows.Forms.Button();
            this.btExit = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.button2 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtMan = new System.Windows.Forms.TextBox();
            this.txtWoman = new System.Windows.Forms.TextBox();
            this.txtPeni = new System.Windows.Forms.TextBox();
            this.lblPeni = new System.Windows.Forms.Label();
            this.txtNds = new System.Windows.Forms.TextBox();
            this.lblNds = new System.Windows.Forms.Label();
            this.txtMonth = new System.Windows.Forms.TextBox();
            this.txtMonth1 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.tbScanD = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Срок аренды:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(247, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Срок предупреждения о завершении договора:";
            // 
            // tbSRKA
            // 
            this.tbSRKA.Location = new System.Drawing.Point(252, 3);
            this.tbSRKA.MaxLength = 3;
            this.tbSRKA.Name = "tbSRKA";
            this.tbSRKA.Size = new System.Drawing.Size(51, 20);
            this.tbSRKA.TabIndex = 1;
            this.tbSRKA.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbSRKA.TextChanged += new System.EventHandler(this.tbSRKA_TextChanged);
            this.tbSRKA.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbSRKA_KeyPress);
            this.tbSRKA.Leave += new System.EventHandler(this.tbSRKA_Leave);
            // 
            // tbSRZA
            // 
            this.tbSRZA.Location = new System.Drawing.Point(251, 34);
            this.tbSRZA.MaxLength = 2;
            this.tbSRZA.Name = "tbSRZA";
            this.tbSRZA.Size = new System.Drawing.Size(52, 20);
            this.tbSRZA.TabIndex = 4;
            this.tbSRZA.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbSRZA.TextChanged += new System.EventHandler(this.tbSRZA_TextChanged);
            this.tbSRZA.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbSRZA_KeyPress);
            this.tbSRZA.Leave += new System.EventHandler(this.tbSRZA_Leave);
            // 
            // tbPotD
            // 
            this.tbPotD.Location = new System.Drawing.Point(7, 91);
            this.tbPotD.Name = "tbPotD";
            this.tbPotD.ReadOnly = true;
            this.tbPotD.Size = new System.Drawing.Size(296, 20);
            this.tbPotD.TabIndex = 7;
            this.tbPotD.TabStop = false;
            this.tbPotD.TextChanged += new System.EventHandler(this.tbPotD_TextChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(309, 86);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(32, 29);
            this.button1.TabIndex = 8;
            this.button1.Text = "...";
            this.toolTip1.SetToolTip(this.button1, "Обзор папок");
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btAddEq
            // 
            this.btAddEq.Image = global::Arenda.Properties.Resources.saveHS;
            this.btAddEq.Location = new System.Drawing.Point(271, 309);
            this.btAddEq.Name = "btAddEq";
            this.btAddEq.Size = new System.Drawing.Size(32, 32);
            this.btAddEq.TabIndex = 24;
            this.toolTip1.SetToolTip(this.btAddEq, "Сохранить");
            this.btAddEq.UseVisualStyleBackColor = true;
            this.btAddEq.Click += new System.EventHandler(this.btAddEq_Click);
            // 
            // btExit
            // 
            this.btExit.Image = global::Arenda.Properties.Resources.Log_Out_icon1;
            this.btExit.Location = new System.Drawing.Point(309, 309);
            this.btExit.Name = "btExit";
            this.btExit.Size = new System.Drawing.Size(32, 32);
            this.btExit.TabIndex = 25;
            this.toolTip1.SetToolTip(this.btExit, "Выход");
            this.btExit.UseVisualStyleBackColor = true;
            this.btExit.Click += new System.EventHandler(this.btExit_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(313, 6);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(27, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "мес";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(313, 37);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(31, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "дней";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(309, 137);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(32, 29);
            this.button2.TabIndex = 11;
            this.button2.Text = "...";
            this.toolTip1.SetToolTip(this.button2, "Обзор папок");
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 66);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(232, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Путь хранения документов по арендаторам:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(5, 182);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(206, 13);
            this.label7.TabIndex = 12;
            this.label7.Text = "Обращение к представителю-мужчине:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 212);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(209, 13);
            this.label8.TabIndex = 14;
            this.label8.Text = "Обращение к представителю-женщине:";
            // 
            // txtMan
            // 
            this.txtMan.Location = new System.Drawing.Point(253, 179);
            this.txtMan.MaxLength = 64;
            this.txtMan.Name = "txtMan";
            this.txtMan.Size = new System.Drawing.Size(89, 20);
            this.txtMan.TabIndex = 13;
            this.txtMan.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMan_KeyPress);
            // 
            // txtWoman
            // 
            this.txtWoman.Location = new System.Drawing.Point(253, 205);
            this.txtWoman.MaxLength = 64;
            this.txtWoman.Name = "txtWoman";
            this.txtWoman.Size = new System.Drawing.Size(88, 20);
            this.txtWoman.TabIndex = 15;
            this.txtWoman.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtWoman_KeyPress);
            // 
            // txtPeni
            // 
            this.txtPeni.Location = new System.Drawing.Point(252, 231);
            this.txtPeni.MaxLength = 6;
            this.txtPeni.Name = "txtPeni";
            this.txtPeni.Size = new System.Drawing.Size(52, 20);
            this.txtPeni.TabIndex = 17;
            this.txtPeni.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtPeni.TextChanged += new System.EventHandler(this.txtPeni_TextChanged);
            this.txtPeni.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPeni_KeyPress);
            this.txtPeni.Leave += new System.EventHandler(this.txtPeni_Leave);
            // 
            // lblPeni
            // 
            this.lblPeni.AutoSize = true;
            this.lblPeni.Location = new System.Drawing.Point(5, 234);
            this.lblPeni.Name = "lblPeni";
            this.lblPeni.Size = new System.Drawing.Size(76, 13);
            this.lblPeni.TabIndex = 16;
            this.lblPeni.Text = "Размер пени:";
            // 
            // txtNds
            // 
            this.txtNds.Location = new System.Drawing.Point(253, 257);
            this.txtNds.MaxLength = 3;
            this.txtNds.Name = "txtNds";
            this.txtNds.Size = new System.Drawing.Size(52, 20);
            this.txtNds.TabIndex = 20;
            this.txtNds.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtNds.TextChanged += new System.EventHandler(this.txtNds_TextChanged);
            this.txtNds.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNds_KeyPress);
            this.txtNds.Leave += new System.EventHandler(this.txtNds_Leave);
            // 
            // lblNds
            // 
            this.lblNds.AutoSize = true;
            this.lblNds.Location = new System.Drawing.Point(5, 260);
            this.lblNds.Name = "lblNds";
            this.lblNds.Size = new System.Drawing.Size(34, 13);
            this.lblNds.TabIndex = 19;
            this.lblNds.Text = "НДС:";
            // 
            // txtMonth
            // 
            this.txtMonth.Location = new System.Drawing.Point(252, 283);
            this.txtMonth.MaxLength = 2;
            this.txtMonth.Name = "txtMonth";
            this.txtMonth.Size = new System.Drawing.Size(52, 20);
            this.txtMonth.TabIndex = 23;
            this.txtMonth.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtMonth.TextChanged += new System.EventHandler(this.txtMonth_TextChanged);
            this.txtMonth.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMonth_KeyPress);
            this.txtMonth.Leave += new System.EventHandler(this.txtMonth_Leave);
            // 
            // txtMonth1
            // 
            this.txtMonth1.BackColor = System.Drawing.SystemColors.Control;
            this.txtMonth1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtMonth1.Location = new System.Drawing.Point(8, 283);
            this.txtMonth1.MaxLength = 64;
            this.txtMonth1.Multiline = true;
            this.txtMonth1.Name = "txtMonth1";
            this.txtMonth1.ReadOnly = true;
            this.txtMonth1.Size = new System.Drawing.Size(238, 36);
            this.txtMonth1.TabIndex = 22;
            this.txtMonth1.Text = "Число месяца, после которого начисляются пени:*";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(306, 234);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(15, 13);
            this.label4.TabIndex = 18;
            this.label4.Text = "%";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(307, 260);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(15, 13);
            this.label9.TabIndex = 21;
            this.label9.Text = "%";
            // 
            // tbScanD
            // 
            this.tbScanD.BackColor = System.Drawing.SystemColors.Control;
            this.tbScanD.Location = new System.Drawing.Point(7, 142);
            this.tbScanD.Name = "tbScanD";
            this.tbScanD.Size = new System.Drawing.Size(296, 20);
            this.tbScanD.TabIndex = 10;
            this.tbScanD.TabStop = false;
            this.tbScanD.TextChanged += new System.EventHandler(this.tbScanD_TextChanged);
            this.tbScanD.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbScanD_KeyDown);
            this.tbScanD.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbScanD_KeyPress);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(4, 117);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(240, 13);
            this.label10.TabIndex = 9;
            this.label10.Text = "Путь хранения отсканированных документов:";
            // 
            // Config
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(353, 348);
            this.ControlBox = false;
            this.Controls.Add(this.button2);
            this.Controls.Add(this.tbScanD);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtMonth1);
            this.Controls.Add(this.txtMonth);
            this.Controls.Add(this.txtNds);
            this.Controls.Add(this.lblNds);
            this.Controls.Add(this.txtPeni);
            this.Controls.Add(this.lblPeni);
            this.Controls.Add(this.txtWoman);
            this.Controls.Add(this.txtMan);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btAddEq);
            this.Controls.Add(this.btExit);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.tbPotD);
            this.Controls.Add(this.tbSRZA);
            this.Controls.Add(this.tbSRKA);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Config";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Настройки";
            this.Load += new System.EventHandler(this.Config_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbSRKA;
        private System.Windows.Forms.TextBox tbSRZA;
        private System.Windows.Forms.TextBox tbPotD;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btAddEq;
        private System.Windows.Forms.Button btExit;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtMan;
        private System.Windows.Forms.TextBox txtWoman;
        private System.Windows.Forms.TextBox txtPeni;
        private System.Windows.Forms.Label lblPeni;
        private System.Windows.Forms.TextBox txtNds;
        private System.Windows.Forms.Label lblNds;
        private System.Windows.Forms.TextBox txtMonth;
        private System.Windows.Forms.TextBox txtMonth1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox tbScanD;
        private System.Windows.Forms.Label label10;
    }
}