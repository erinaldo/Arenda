namespace Arenda
{
    partial class Type_o_o
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btAddtoo = new System.Windows.Forms.Button();
            this.checAll = new System.Windows.Forms.CheckBox();
            this.bds = new System.Windows.Forms.BindingSource(this.components);
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.button4 = new System.Windows.Forms.Button();
            this.btDel = new System.Windows.Forms.Button();
            this.tbEdit = new System.Windows.Forms.Button();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.grdType = new System.Windows.Forms.DataGridView();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.abbr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.isActive2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.bds)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdType)).BeginInit();
            this.SuspendLayout();
            // 
            // btAddtoo
            // 
            this.btAddtoo.Image = global::Arenda.Properties.Resources.DocumentHS;
            this.btAddtoo.Location = new System.Drawing.Point(222, 213);
            this.btAddtoo.Name = "btAddtoo";
            this.btAddtoo.Size = new System.Drawing.Size(32, 32);
            this.btAddtoo.TabIndex = 17;
            this.btAddtoo.UseVisualStyleBackColor = true;
            this.btAddtoo.Click += new System.EventHandler(this.btAddtoo_Click);
            // 
            // checAll
            // 
            this.checAll.AutoSize = true;
            this.checAll.Location = new System.Drawing.Point(7, 198);
            this.checAll.Name = "checAll";
            this.checAll.Size = new System.Drawing.Size(45, 17);
            this.checAll.TabIndex = 24;
            this.checAll.Text = "Все";
            this.checAll.UseVisualStyleBackColor = true;
            this.checAll.CheckedChanged += new System.EventHandler(this.checAll_CheckedChanged);
            // 
            // button4
            // 
            this.button4.Image = global::Arenda.Properties.Resources.Log_Out_icon1;
            this.button4.Location = new System.Drawing.Point(336, 213);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(32, 32);
            this.button4.TabIndex = 22;
            this.toolTip1.SetToolTip(this.button4, "Выход");
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // btDel
            // 
            this.btDel.Image = global::Arenda.Properties.Resources.DeleteHS;
            this.btDel.Location = new System.Drawing.Point(298, 213);
            this.btDel.Name = "btDel";
            this.btDel.Size = new System.Drawing.Size(32, 32);
            this.btDel.TabIndex = 21;
            this.toolTip1.SetToolTip(this.btDel, "Удалить");
            this.btDel.UseVisualStyleBackColor = true;
            this.btDel.Click += new System.EventHandler(this.btDel_Click);
            // 
            // tbEdit
            // 
            this.tbEdit.Image = global::Arenda.Properties.Resources.EditTableHS;
            this.tbEdit.Location = new System.Drawing.Point(260, 213);
            this.tbEdit.Name = "tbEdit";
            this.tbEdit.Size = new System.Drawing.Size(32, 32);
            this.tbEdit.TabIndex = 20;
            this.toolTip1.SetToolTip(this.tbEdit, "Редактировать");
            this.tbEdit.UseVisualStyleBackColor = true;
            this.tbEdit.Click += new System.EventHandler(this.tbEdit_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.Coral;
            this.pictureBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox2.Location = new System.Drawing.Point(51, 200);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(12, 12);
            this.pictureBox2.TabIndex = 29;
            this.pictureBox2.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(64, 198);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 13);
            this.label3.TabIndex = 28;
            this.label3.Text = "неактивные";
            // 
            // grdType
            // 
            this.grdType.AllowUserToAddRows = false;
            this.grdType.AllowUserToDeleteRows = false;
            this.grdType.AllowUserToResizeColumns = false;
            this.grdType.AllowUserToResizeRows = false;
            this.grdType.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.grdType.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grdType.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.grdType.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdType.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id,
            this.cName,
            this.abbr,
            this.isActive2});
            this.grdType.Location = new System.Drawing.Point(5, 4);
            this.grdType.MultiSelect = false;
            this.grdType.Name = "grdType";
            this.grdType.ReadOnly = true;
            this.grdType.RowHeadersVisible = false;
            this.grdType.Size = new System.Drawing.Size(363, 188);
            this.grdType.TabIndex = 30;
            this.grdType.TabStop = false;
            this.grdType.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdType_CellClick);
            this.grdType.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdType_CellEnter);
            this.grdType.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.grdType_ColumnHeaderMouseClick);
            this.grdType.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.grdType_RowPrePaint);
            this.grdType.RowStateChanged += new System.Windows.Forms.DataGridViewRowStateChangedEventHandler(this.grdType_RowStateChanged);
            // 
            // id
            // 
            this.id.DataPropertyName = "id";
            this.id.FillWeight = 26.64009F;
            this.id.HeaderText = "id";
            this.id.MinimumWidth = 15;
            this.id.Name = "id";
            this.id.ReadOnly = true;
            this.id.Visible = false;
            // 
            // cName
            // 
            this.cName.DataPropertyName = "cName";
            this.cName.FillWeight = 183.2838F;
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
            // isActive2
            // 
            this.isActive2.DataPropertyName = "isActive";
            this.isActive2.HeaderText = "isActive2";
            this.isActive2.Name = "isActive2";
            this.isActive2.ReadOnly = true;
            this.isActive2.Visible = false;
            // 
            // Type_o_o
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(382, 257);
            this.ControlBox = false;
            this.Controls.Add(this.grdType);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.checAll);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.btDel);
            this.Controls.Add(this.tbEdit);
            this.Controls.Add(this.btAddtoo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Type_o_o";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Справочник типов организаций";
            this.Load += new System.EventHandler(this.Type_o_o_Load);
            ((System.ComponentModel.ISupportInitialize)(this.bds)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdType)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btAddtoo;
        private System.Windows.Forms.CheckBox checAll;
        private System.Windows.Forms.BindingSource bds;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button btDel;
        private System.Windows.Forms.Button tbEdit;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView grdType;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn cName;
        private System.Windows.Forms.DataGridViewTextBoxColumn abbr;
        private System.Windows.Forms.DataGridViewTextBoxColumn isActive2;
    }
}