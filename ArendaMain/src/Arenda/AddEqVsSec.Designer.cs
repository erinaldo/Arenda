namespace Arenda
{
    partial class AddEqVsSec
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
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btAddEq = new System.Windows.Forms.Button();
            this.btExit = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tbSec = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbEq = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbCount = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btAddEq
            // 
            this.btAddEq.Image = global::Arenda.Properties.Resources.saveHS;
            this.btAddEq.Location = new System.Drawing.Point(303, 139);
            this.btAddEq.Name = "btAddEq";
            this.btAddEq.Size = new System.Drawing.Size(32, 32);
            this.btAddEq.TabIndex = 18;
            this.toolTip1.SetToolTip(this.btAddEq, "Добавить");
            this.btAddEq.UseVisualStyleBackColor = true;
            this.btAddEq.Click += new System.EventHandler(this.btAddEq_Click);
            // 
            // btExit
            // 
            this.btExit.Image = global::Arenda.Properties.Resources.Log_Out_icon1;
            this.btExit.Location = new System.Drawing.Point(341, 139);
            this.btExit.Name = "btExit";
            this.btExit.Size = new System.Drawing.Size(32, 32);
            this.btExit.TabIndex = 17;
            this.toolTip1.SetToolTip(this.btExit, "Выход");
            this.btExit.UseVisualStyleBackColor = true;
            this.btExit.Click += new System.EventHandler(this.btExit_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Секция";
            // 
            // tbSec
            // 
            this.tbSec.Location = new System.Drawing.Point(128, 12);
            this.tbSec.Name = "tbSec";
            this.tbSec.ReadOnly = true;
            this.tbSec.Size = new System.Drawing.Size(245, 20);
            this.tbSec.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Оборудование";
            // 
            // cbEq
            // 
            this.cbEq.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbEq.DropDownWidth = 20;
            this.cbEq.FormattingEnabled = true;
            this.cbEq.Location = new System.Drawing.Point(128, 64);
            this.cbEq.Name = "cbEq";
            this.cbEq.Size = new System.Drawing.Size(245, 21);
            this.cbEq.TabIndex = 3;
            this.cbEq.SelectedValueChanged += new System.EventHandler(this.cbEq_SelectedValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 114);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Количество";
            // 
            // tbCount
            // 
            this.tbCount.Location = new System.Drawing.Point(128, 114);
            this.tbCount.MaxLength = 6;
            this.tbCount.Name = "tbCount";
            this.tbCount.Size = new System.Drawing.Size(102, 20);
            this.tbCount.TabIndex = 20;
            this.tbCount.TextChanged += new System.EventHandler(this.tbCount_TextChanged);
            this.tbCount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbCount_KeyPress);
            // 
            // AddEqVsSec
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(385, 180);
            this.ControlBox = false;
            this.Controls.Add(this.tbCount);
            this.Controls.Add(this.btAddEq);
            this.Controls.Add(this.btExit);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbEq);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbSec);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddEqVsSec";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Добавить/Редактировать оборудование";
            this.Load += new System.EventHandler(this.AddEqVsSec_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbSec;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbEq;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btAddEq;
        private System.Windows.Forms.Button btExit;
        private System.Windows.Forms.TextBox tbCount;
    }
}