namespace Arenda
{
    partial class AddEditFloor
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddEditFloor));
      this.label2 = new System.Windows.Forms.Label();
      this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
      this.label1 = new System.Windows.Forms.Label();
      this.tbAbbr = new System.Windows.Forms.TextBox();
      this.tbCname = new System.Windows.Forms.TextBox();
      this.btAdd = new System.Windows.Forms.Button();
      this.button1 = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(3, 59);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(78, 13);
      this.label2.TabIndex = 11;
      this.label2.Text = "Аббревиатура";
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(3, 14);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(83, 13);
      this.label1.TabIndex = 10;
      this.label1.Text = "Наименование";
      // 
      // tbAbbr
      // 
      this.tbAbbr.Location = new System.Drawing.Point(92, 56);
      this.tbAbbr.MaxLength = 10;
      this.tbAbbr.Name = "tbAbbr";
      this.tbAbbr.Size = new System.Drawing.Size(198, 20);
      this.tbAbbr.TabIndex = 9;
      this.tbAbbr.TextChanged += new System.EventHandler(this.tbAbbr_TextChanged);
      // 
      // tbCname
      // 
      this.tbCname.Location = new System.Drawing.Point(92, 11);
      this.tbCname.MaxLength = 15;
      this.tbCname.Name = "tbCname";
      this.tbCname.Size = new System.Drawing.Size(198, 20);
      this.tbCname.TabIndex = 8;
      this.tbCname.TextChanged += new System.EventHandler(this.tbCname_TextChanged);
      // 
      // btAdd
      // 
      this.btAdd.Image = global::Arenda.Properties.Resources.saveHS;
      this.btAdd.Location = new System.Drawing.Point(208, 97);
      this.btAdd.Name = "btAdd";
      this.btAdd.Size = new System.Drawing.Size(32, 32);
      this.btAdd.TabIndex = 7;
      this.toolTip1.SetToolTip(this.btAdd, "Сохранить");
      this.btAdd.UseVisualStyleBackColor = true;
      this.btAdd.Click += new System.EventHandler(this.btAdd_Click);
      // 
      // button1
      // 
      this.button1.Image = global::Arenda.Properties.Resources.Log_Out_icon1;
      this.button1.Location = new System.Drawing.Point(258, 97);
      this.button1.Name = "button1";
      this.button1.Size = new System.Drawing.Size(32, 32);
      this.button1.TabIndex = 6;
      this.toolTip1.SetToolTip(this.button1, "Выход");
      this.button1.UseVisualStyleBackColor = true;
      this.button1.Click += new System.EventHandler(this.button1_Click);
      // 
      // AddEditFloor
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(313, 150);
      this.ControlBox = false;
      this.Controls.Add(this.label2);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.tbAbbr);
      this.Controls.Add(this.tbCname);
      this.Controls.Add(this.btAdd);
      this.Controls.Add(this.button1);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "AddEditFloor";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Добавить/редактировать запись";
      this.Load += new System.EventHandler(this.AddEditFloor_Load);
      this.ResumeLayout(false);
      this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button btAdd;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbAbbr;
        private System.Windows.Forms.TextBox tbCname;
    }
}