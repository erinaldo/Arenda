namespace Arenda
{
    partial class AddSec
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
            this.label3 = new System.Windows.Forms.Label();
            this.tbcName = new System.Windows.Forms.TextBox();
            this.cbZdan = new System.Windows.Forms.ComboBox();
            this.btAddEq = new System.Windows.Forms.Button();
            this.btExit = new System.Windows.Forms.Button();
            this.cbFloor = new System.Windows.Forms.ComboBox();
            this.tbKtl = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbkl = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbphone = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.chbIsAPPZ = new System.Windows.Forms.CheckBox();
            this.lblIsAPPZ = new System.Windows.Forms.Label();
            this.label44 = new System.Windows.Forms.Label();
            this.cmbObject = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Наименование";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Здание";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 89);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(33, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Этаж";
            // 
            // tbcName
            // 
            this.tbcName.Location = new System.Drawing.Point(122, 9);
            this.tbcName.Name = "tbcName";
            this.tbcName.Size = new System.Drawing.Size(195, 20);
            this.tbcName.TabIndex = 3;
            this.tbcName.TextChanged += new System.EventHandler(this.tbcName_TextChanged);
            this.tbcName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbcName_KeyPress);
            // 
            // cbZdan
            // 
            this.cbZdan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbZdan.FormattingEnabled = true;
            this.cbZdan.Location = new System.Drawing.Point(122, 62);
            this.cbZdan.Name = "cbZdan";
            this.cbZdan.Size = new System.Drawing.Size(195, 21);
            this.cbZdan.TabIndex = 4;
            this.cbZdan.SelectedValueChanged += new System.EventHandler(this.cbZdan_SelectedValueChanged);
            // 
            // btAddEq
            // 
            this.btAddEq.Image = global::Arenda.Properties.Resources.saveHS;
            this.btAddEq.Location = new System.Drawing.Point(247, 304);
            this.btAddEq.Name = "btAddEq";
            this.btAddEq.Size = new System.Drawing.Size(32, 32);
            this.btAddEq.TabIndex = 20;
            this.toolTip1.SetToolTip(this.btAddEq, "Сохранить");
            this.btAddEq.UseVisualStyleBackColor = true;
            this.btAddEq.Click += new System.EventHandler(this.btAddEq_Click);
            // 
            // btExit
            // 
            this.btExit.Image = global::Arenda.Properties.Resources.Log_Out_icon1;
            this.btExit.Location = new System.Drawing.Point(285, 304);
            this.btExit.Name = "btExit";
            this.btExit.Size = new System.Drawing.Size(32, 32);
            this.btExit.TabIndex = 19;
            this.toolTip1.SetToolTip(this.btExit, "Выход");
            this.btExit.UseVisualStyleBackColor = true;
            this.btExit.Click += new System.EventHandler(this.btExit_Click);
            // 
            // cbFloor
            // 
            this.cbFloor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFloor.FormattingEnabled = true;
            this.cbFloor.Location = new System.Drawing.Point(122, 92);
            this.cbFloor.Name = "cbFloor";
            this.cbFloor.Size = new System.Drawing.Size(195, 21);
            this.cbFloor.TabIndex = 21;
            this.cbFloor.SelectedValueChanged += new System.EventHandler(this.cbFloor_SelectedValueChanged);
            // 
            // tbKtl
            // 
            this.tbKtl.Location = new System.Drawing.Point(225, 127);
            this.tbKtl.MaxLength = 2;
            this.tbKtl.Name = "tbKtl";
            this.tbKtl.Size = new System.Drawing.Size(92, 20);
            this.tbKtl.TabIndex = 23;
            this.tbKtl.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbKtl_KeyPress);
            this.tbKtl.Leave += new System.EventHandler(this.tbKtl_Leave);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 130);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(164, 13);
            this.label4.TabIndex = 22;
            this.label4.Text = "Количество телефонных линий";
            // 
            // tbkl
            // 
            this.tbkl.Location = new System.Drawing.Point(225, 159);
            this.tbkl.MaxLength = 2;
            this.tbkl.Name = "tbkl";
            this.tbkl.Size = new System.Drawing.Size(92, 20);
            this.tbkl.TabIndex = 25;
            this.tbkl.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbkl_KeyPress);
            this.tbkl.Leave += new System.EventHandler(this.tbkl_Leave);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 162);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(140, 13);
            this.label5.TabIndex = 24;
            this.label5.Text = "Количество светильников";
            // 
            // tbphone
            // 
            this.tbphone.Location = new System.Drawing.Point(147, 188);
            this.tbphone.MaxLength = 14;
            this.tbphone.Name = "tbphone";
            this.tbphone.Size = new System.Drawing.Size(170, 20);
            this.tbphone.TabIndex = 27;
            this.tbphone.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbphone_KeyPress);
            this.tbphone.Leave += new System.EventHandler(this.tbphone_Leave);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 191);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(93, 13);
            this.label6.TabIndex = 26;
            this.label6.Text = "Номер телефона";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(247, 243);
            this.textBox1.MaxLength = 13;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(70, 20);
            this.textBox1.TabIndex = 31;
            this.textBox1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox1_KeyPress);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 246);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(135, 13);
            this.label7.TabIndex = 30;
            this.label7.Text = "Площадь торгового зала";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(247, 214);
            this.textBox2.MaxLength = 13;
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(70, 20);
            this.textBox2.TabIndex = 29;
            this.textBox2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox2_KeyPress);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 217);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(90, 13);
            this.label8.TabIndex = 28;
            this.label8.Text = "Общая площадь";
            // 
            // chbIsAPPZ
            // 
            this.chbIsAPPZ.AutoSize = true;
            this.chbIsAPPZ.Location = new System.Drawing.Point(247, 278);
            this.chbIsAPPZ.Name = "chbIsAPPZ";
            this.chbIsAPPZ.Size = new System.Drawing.Size(15, 14);
            this.chbIsAPPZ.TabIndex = 32;
            this.chbIsAPPZ.UseVisualStyleBackColor = true;
            // 
            // lblIsAPPZ
            // 
            this.lblIsAPPZ.AutoSize = true;
            this.lblIsAPPZ.Location = new System.Drawing.Point(12, 279);
            this.lblIsAPPZ.Name = "lblIsAPPZ";
            this.lblIsAPPZ.Size = new System.Drawing.Size(40, 13);
            this.lblIsAPPZ.TabIndex = 33;
            this.lblIsAPPZ.Text = "АППЗ:";
            // 
            // label44
            // 
            this.label44.AutoSize = true;
            this.label44.Location = new System.Drawing.Point(12, 38);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(45, 13);
            this.label44.TabIndex = 46;
            this.label44.Text = "Объект";
            // 
            // cmbObject
            // 
            this.cmbObject.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbObject.DisplayMember = "cName";
            this.cmbObject.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbObject.FormattingEnabled = true;
            this.cmbObject.Location = new System.Drawing.Point(122, 35);
            this.cmbObject.Name = "cmbObject";
            this.cmbObject.Size = new System.Drawing.Size(195, 21);
            this.cmbObject.TabIndex = 45;
            this.cmbObject.ValueMember = "id";
            this.cmbObject.SelectionChangeCommitted += new System.EventHandler(this.cmbObject_SelectionChangeCommitted);
            this.cmbObject.SelectedValueChanged += new System.EventHandler(this.cmbObject_SelectedValueChanged);
            // 
            // AddSec
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(337, 340);
            this.ControlBox = false;
            this.Controls.Add(this.label44);
            this.Controls.Add(this.cmbObject);
            this.Controls.Add(this.lblIsAPPZ);
            this.Controls.Add(this.chbIsAPPZ);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.tbphone);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.tbkl);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.tbKtl);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cbFloor);
            this.Controls.Add(this.btAddEq);
            this.Controls.Add(this.btExit);
            this.Controls.Add(this.cbZdan);
            this.Controls.Add(this.tbcName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddSec";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Добавить/Редактировать секцию";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbcName;
        private System.Windows.Forms.ComboBox cbZdan;
        private System.Windows.Forms.Button btAddEq;
        private System.Windows.Forms.Button btExit;
        private System.Windows.Forms.ComboBox cbFloor;
        private System.Windows.Forms.TextBox tbKtl;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbkl;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbphone;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox chbIsAPPZ;
        private System.Windows.Forms.Label lblIsAPPZ;
        private System.Windows.Forms.Label label44;
        private System.Windows.Forms.ComboBox cmbObject;
    }
}