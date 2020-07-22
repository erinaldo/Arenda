namespace Arenda
{
    partial class AddEditPost
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
            this.tbCname = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.btAdd = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.label2 = new System.Windows.Forms.TextBox();
            this.tbDativeCname = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 13);
            this.label1.TabIndex = 18;
            this.label1.Text = "Наименование";
            // 
            // tbCname
            // 
            this.tbCname.Location = new System.Drawing.Point(100, 23);
            this.tbCname.MaxLength = 300;
            this.tbCname.Name = "tbCname";
            this.tbCname.Size = new System.Drawing.Size(318, 20);
            this.tbCname.TabIndex = 1;
            this.tbCname.TextChanged += new System.EventHandler(this.tbCname_TextChanged);
            // 
            // button1
            // 
            this.button1.Image = global::Arenda.Properties.Resources.Log_Out_icon1;
            this.button1.Location = new System.Drawing.Point(386, 94);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(32, 32);
            this.button1.TabIndex = 4;
            this.toolTip1.SetToolTip(this.button1, "Выход");
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btAdd
            // 
            this.btAdd.Image = global::Arenda.Properties.Resources.saveHS;
            this.btAdd.Location = new System.Drawing.Point(338, 94);
            this.btAdd.Name = "btAdd";
            this.btAdd.Size = new System.Drawing.Size(32, 32);
            this.btAdd.TabIndex = 3;
            this.toolTip1.SetToolTip(this.btAdd, "Сохранить");
            this.btAdd.UseVisualStyleBackColor = true;
            this.btAdd.Click += new System.EventHandler(this.btAdd_Click);
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.SystemColors.Control;
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.label2.CausesValidation = false;
            this.label2.Location = new System.Drawing.Point(14, 59);
            this.label2.Multiline = true;
            this.label2.Name = "label2";
            this.label2.ReadOnly = true;
            this.label2.ShortcutsEnabled = false;
            this.label2.Size = new System.Drawing.Size(80, 44);
            this.label2.TabIndex = 52;
            this.label2.TabStop = false;
            this.label2.Text = "Наименование в дательном падеже";
            this.label2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tbDativeCname
            // 
            this.tbDativeCname.Location = new System.Drawing.Point(100, 59);
            this.tbDativeCname.MaxLength = 300;
            this.tbDativeCname.Name = "tbDativeCname";
            this.tbDativeCname.Size = new System.Drawing.Size(318, 20);
            this.tbDativeCname.TabIndex = 2;
            this.tbDativeCname.TextChanged += new System.EventHandler(this.tbDativeCname_TextChanged);
            // 
            // AddEditPost
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(430, 138);
            this.ControlBox = false;
            this.Controls.Add(this.tbDativeCname);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbCname);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btAdd);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddEditPost";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Load += new System.EventHandler(this.AddEditPost_Load);
            this.Shown += new System.EventHandler(this.AddEditPost_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbCname;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button btAdd;
        private System.Windows.Forms.TextBox label2;
        private System.Windows.Forms.TextBox tbDativeCname;
    }
}