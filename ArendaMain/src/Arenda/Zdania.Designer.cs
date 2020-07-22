namespace Arenda
{
    partial class Zdania
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Zdania));
            this.bgZdania = new System.Windows.Forms.DataGridView();
            this.cname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.isActive = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.abbreviation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.checAll = new System.Windows.Forms.CheckBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.bds = new System.Windows.Forms.BindingSource(this.components);
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.bgZdania)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bds)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // bgZdania
            // 
            this.bgZdania.AllowUserToAddRows = false;
            this.bgZdania.AllowUserToDeleteRows = false;
            this.bgZdania.AllowUserToResizeRows = false;
            this.bgZdania.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.bgZdania.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.bgZdania.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.bgZdania.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.cname,
            this.isActive,
            this.abbreviation,
            this.ID});
            this.bgZdania.Location = new System.Drawing.Point(12, 12);
            this.bgZdania.MultiSelect = false;
            this.bgZdania.Name = "bgZdania";
            this.bgZdania.ReadOnly = true;
            this.bgZdania.RowHeadersVisible = false;
            this.bgZdania.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.bgZdania.Size = new System.Drawing.Size(286, 188);
            this.bgZdania.TabIndex = 0;
            this.bgZdania.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.bgZdania_CellClick);
            this.bgZdania.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.bgZdania_CellEnter);
            this.bgZdania.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.bgZdania_RowPrePaint);
            this.bgZdania.RowStateChanged += new System.Windows.Forms.DataGridViewRowStateChangedEventHandler(this.bgZdania_RowStateChanged);
            // 
            // cname
            // 
            this.cname.DataPropertyName = "cname";
            this.cname.HeaderText = "Наименование";
            this.cname.Name = "cname";
            this.cname.ReadOnly = true;
            // 
            // isActive
            // 
            this.isActive.DataPropertyName = "isActive";
            this.isActive.HeaderText = "isActive";
            this.isActive.Name = "isActive";
            this.isActive.ReadOnly = true;
            this.isActive.Visible = false;
            // 
            // abbreviation
            // 
            this.abbreviation.DataPropertyName = "abbreviation";
            this.abbreviation.HeaderText = "Аббревиатура";
            this.abbreviation.Name = "abbreviation";
            this.abbreviation.ReadOnly = true;
            // 
            // ID
            // 
            this.ID.DataPropertyName = "id";
            this.ID.HeaderText = "ID";
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            this.ID.Visible = false;
            // 
            // button1
            // 
            this.button1.Image = global::Arenda.Properties.Resources.DocumentHS;
            this.button1.Location = new System.Drawing.Point(146, 239);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(32, 32);
            this.button1.TabIndex = 1;
            this.toolTip1.SetToolTip(this.button1, "Добавить");
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Image = global::Arenda.Properties.Resources.EditTableHS;
            this.button2.Location = new System.Drawing.Point(188, 239);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(32, 32);
            this.button2.TabIndex = 2;
            this.toolTip1.SetToolTip(this.button2, "Редактировать");
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Image = global::Arenda.Properties.Resources.DeleteHS;
            this.button3.Location = new System.Drawing.Point(226, 239);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(32, 32);
            this.button3.TabIndex = 3;
            this.toolTip1.SetToolTip(this.button3, "Удалить");
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Image = ((System.Drawing.Image)(resources.GetObject("button4.Image")));
            this.button4.Location = new System.Drawing.Point(264, 239);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(32, 32);
            this.button4.TabIndex = 4;
            this.toolTip1.SetToolTip(this.button4, "Выход");
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(78, 206);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "неактивные";
            // 
            // checAll
            // 
            this.checAll.AutoSize = true;
            this.checAll.Location = new System.Drawing.Point(12, 206);
            this.checAll.Name = "checAll";
            this.checAll.Size = new System.Drawing.Size(45, 17);
            this.checAll.TabIndex = 6;
            this.checAll.Text = "Все";
            this.checAll.UseVisualStyleBackColor = true;
            this.checAll.CheckedChanged += new System.EventHandler(this.checAll_CheckedChanged);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Coral;
            this.pictureBox1.Location = new System.Drawing.Point(65, 208);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(12, 12);
            this.pictureBox1.TabIndex = 7;
            this.pictureBox1.TabStop = false;
            // 
            // Zdania
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(310, 276);
            this.ControlBox = false;
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.checAll);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.bgZdania);
            this.Controls.Add(this.button3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Zdania";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Справочник зданий";
            this.Load += new System.EventHandler(this.Zdania_Load);
            ((System.ComponentModel.ISupportInitialize)(this.bgZdania)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bds)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView bgZdania;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checAll;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.BindingSource bds;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.DataGridViewTextBoxColumn cname;
        private System.Windows.Forms.DataGridViewTextBoxColumn isActive;
        private System.Windows.Forms.DataGridViewTextBoxColumn abbreviation;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
    }
}