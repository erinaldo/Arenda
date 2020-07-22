namespace Arenda
{
    partial class Basement
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Basement));
            this.btDel = new System.Windows.Forms.Button();
            this.checAll = new System.Windows.Forms.CheckBox();
            this.button4 = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.tbEdit = new System.Windows.Forms.Button();
            this.btAddtoo = new System.Windows.Forms.Button();
            this.bgBase = new System.Windows.Forms.DataGridView();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.abbr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.isActive = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.needDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bds = new System.Windows.Forms.BindingSource(this.components);
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.bgBase)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bds)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // btDel
            // 
            this.btDel.Image = global::Arenda.Properties.Resources.DeleteHS;
            this.btDel.Location = new System.Drawing.Point(308, 225);
            this.btDel.Name = "btDel";
            this.btDel.Size = new System.Drawing.Size(32, 32);
            this.btDel.TabIndex = 28;
            this.toolTip1.SetToolTip(this.btDel, "Удалить");
            this.btDel.UseVisualStyleBackColor = true;
            this.btDel.Click += new System.EventHandler(this.btDel_Click);
            // 
            // checAll
            // 
            this.checAll.AutoSize = true;
            this.checAll.Location = new System.Drawing.Point(7, 202);
            this.checAll.Name = "checAll";
            this.checAll.Size = new System.Drawing.Size(45, 17);
            this.checAll.TabIndex = 30;
            this.checAll.Text = "Все";
            this.checAll.UseVisualStyleBackColor = true;
            this.checAll.CheckedChanged += new System.EventHandler(this.checAll_CheckedChanged);
            // 
            // button4
            // 
            this.button4.Image = ((System.Drawing.Image)(resources.GetObject("button4.Image")));
            this.button4.Location = new System.Drawing.Point(346, 225);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(32, 32);
            this.button4.TabIndex = 29;
            this.toolTip1.SetToolTip(this.button4, "Выход");
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // tbEdit
            // 
            this.tbEdit.Image = global::Arenda.Properties.Resources.EditTableHS;
            this.tbEdit.Location = new System.Drawing.Point(270, 225);
            this.tbEdit.Name = "tbEdit";
            this.tbEdit.Size = new System.Drawing.Size(32, 32);
            this.tbEdit.TabIndex = 27;
            this.toolTip1.SetToolTip(this.tbEdit, "Редактировать");
            this.tbEdit.UseVisualStyleBackColor = true;
            this.tbEdit.Click += new System.EventHandler(this.tbEdit_Click);
            // 
            // btAddtoo
            // 
            this.btAddtoo.Image = global::Arenda.Properties.Resources.DocumentHS;
            this.btAddtoo.Location = new System.Drawing.Point(232, 225);
            this.btAddtoo.Name = "btAddtoo";
            this.btAddtoo.Size = new System.Drawing.Size(32, 32);
            this.btAddtoo.TabIndex = 25;
            this.toolTip1.SetToolTip(this.btAddtoo, "Добавить");
            this.btAddtoo.UseVisualStyleBackColor = true;
            this.btAddtoo.Click += new System.EventHandler(this.btAddtoo_Click);
            // 
            // bgBase
            // 
            this.bgBase.AllowUserToAddRows = false;
            this.bgBase.AllowUserToDeleteRows = false;
            this.bgBase.AllowUserToResizeRows = false;
            this.bgBase.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.bgBase.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.bgBase.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.bgBase.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id,
            this.cName,
            this.abbr,
            this.isActive,
            this.needDate});
            this.bgBase.Location = new System.Drawing.Point(7, 8);
            this.bgBase.MultiSelect = false;
            this.bgBase.Name = "bgBase";
            this.bgBase.ReadOnly = true;
            this.bgBase.RowHeadersVisible = false;
            this.bgBase.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.bgBase.Size = new System.Drawing.Size(371, 188);
            this.bgBase.TabIndex = 26;
            this.bgBase.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.bgBase_CellClick);
            this.bgBase.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.bgBase_CellEnter);
            this.bgBase.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.bgBase_ColumnHeaderMouseClick);
            this.bgBase.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.bgBase_RowPrePaint);
            this.bgBase.RowStateChanged += new System.Windows.Forms.DataGridViewRowStateChangedEventHandler(this.bgBase_RowStateChanged);
            // 
            // id
            // 
            this.id.DataPropertyName = "id";
            this.id.HeaderText = "id";
            this.id.Name = "id";
            this.id.ReadOnly = true;
            this.id.Visible = false;
            // 
            // cName
            // 
            this.cName.DataPropertyName = "cName";
            this.cName.HeaderText = "Наименование";
            this.cName.Name = "cName";
            this.cName.ReadOnly = true;
            // 
            // abbr
            // 
            this.abbr.DataPropertyName = "Abbreviation";
            this.abbr.HeaderText = "Аббревиатура";
            this.abbr.Name = "abbr";
            this.abbr.ReadOnly = true;
            // 
            // isActive
            // 
            this.isActive.DataPropertyName = "isActive";
            this.isActive.HeaderText = "isActive";
            this.isActive.Name = "isActive";
            this.isActive.ReadOnly = true;
            this.isActive.Visible = false;
            // 
            // needDate
            // 
            this.needDate.DataPropertyName = "needDate";
            this.needDate.HeaderText = "Нал.-ие даты";
            this.needDate.Name = "needDate";
            this.needDate.ReadOnly = true;
            this.needDate.Visible = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.Coral;
            this.pictureBox2.Location = new System.Drawing.Point(61, 205);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(12, 12);
            this.pictureBox2.TabIndex = 32;
            this.pictureBox2.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(74, 203);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 13);
            this.label3.TabIndex = 31;
            this.label3.Text = "неактивные";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(7, 225);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(143, 17);
            this.checkBox1.TabIndex = 33;
            this.checkBox1.Text = "Наличие даты у док.-та";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.LightBlue;
            this.pictureBox1.Location = new System.Drawing.Point(168, 206);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(12, 12);
            this.pictureBox1.TabIndex = 35;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(181, 204);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 34;
            this.label1.Text = "с датой";
            // 
            // Basement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(389, 266);
            this.ControlBox = false;
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btDel);
            this.Controls.Add(this.checAll);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.tbEdit);
            this.Controls.Add(this.bgBase);
            this.Controls.Add(this.btAddtoo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Basement";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Справочник оснований заключения договоров";
            this.Load += new System.EventHandler(this.Basement_Load);
            ((System.ComponentModel.ISupportInitialize)(this.bgBase)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bds)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btDel;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.CheckBox checAll;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button tbEdit;
        private System.Windows.Forms.DataGridView bgBase;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn cName;
        private System.Windows.Forms.DataGridViewTextBoxColumn abbr;
        private System.Windows.Forms.DataGridViewTextBoxColumn isActive;
        private System.Windows.Forms.DataGridViewTextBoxColumn needDate;
        private System.Windows.Forms.Button btAddtoo;
        private System.Windows.Forms.BindingSource bds;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label label3;        
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
    }
}