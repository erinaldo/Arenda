namespace Arenda
{
    partial class frmAnotherPayments
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAnotherPayments));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btnExit = new System.Windows.Forms.Button();
            this.btnDel = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.dgAnotherPayments = new System.Windows.Forms.DataGridView();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.isActive = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chbAll = new System.Windows.Forms.CheckBox();
            this.lblUnactive = new System.Windows.Forms.Label();
            this.pbUnactive = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgAnotherPayments)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbUnactive)).BeginInit();
            this.SuspendLayout();
            // 
            // btnExit
            // 
            this.btnExit.Image = ((System.Drawing.Image)(resources.GetObject("btnExit.Image")));
            this.btnExit.Location = new System.Drawing.Point(378, 308);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(32, 32);
            this.btnExit.TabIndex = 36;
            this.toolTip1.SetToolTip(this.btnExit, "Выход");
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnDel
            // 
            this.btnDel.Image = ((System.Drawing.Image)(resources.GetObject("btnDel.Image")));
            this.btnDel.Location = new System.Drawing.Point(340, 308);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(32, 32);
            this.btnDel.TabIndex = 35;
            this.toolTip1.SetToolTip(this.btnDel, "Удалить");
            this.btnDel.UseVisualStyleBackColor = true;
            this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Image = global::Arenda.Properties.Resources.EditTableHS;
            this.btnEdit.Location = new System.Drawing.Point(302, 308);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(32, 32);
            this.btnEdit.TabIndex = 34;
            this.toolTip1.SetToolTip(this.btnEdit, "Редактировать");
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Image = ((System.Drawing.Image)(resources.GetObject("btnAdd.Image")));
            this.btnAdd.Location = new System.Drawing.Point(260, 308);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(32, 32);
            this.btnAdd.TabIndex = 33;
            this.toolTip1.SetToolTip(this.btnAdd, "Добавить");
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // dgAnotherPayments
            // 
            this.dgAnotherPayments.AllowUserToAddRows = false;
            this.dgAnotherPayments.AllowUserToDeleteRows = false;
            this.dgAnotherPayments.AllowUserToResizeRows = false;
            this.dgAnotherPayments.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgAnotherPayments.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgAnotherPayments.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgAnotherPayments.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id,
            this.isActive,
            this.cName});
            this.dgAnotherPayments.Location = new System.Drawing.Point(12, 12);
            this.dgAnotherPayments.MultiSelect = false;
            this.dgAnotherPayments.Name = "dgAnotherPayments";
            this.dgAnotherPayments.ReadOnly = true;
            this.dgAnotherPayments.RowHeadersVisible = false;
            this.dgAnotherPayments.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgAnotherPayments.Size = new System.Drawing.Size(398, 288);
            this.dgAnotherPayments.TabIndex = 40;
            this.dgAnotherPayments.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.dgAnotherPayments_RowPrePaint);
            this.dgAnotherPayments.SelectionChanged += new System.EventHandler(this.dgAnotherPayments_SelectionChanged);
            // 
            // id
            // 
            this.id.DataPropertyName = "id";
            this.id.HeaderText = "id";
            this.id.Name = "id";
            this.id.ReadOnly = true;
            this.id.Visible = false;
            // 
            // isActive
            // 
            this.isActive.DataPropertyName = "isActive";
            this.isActive.HeaderText = "isActive";
            this.isActive.Name = "isActive";
            this.isActive.ReadOnly = true;
            this.isActive.Visible = false;
            // 
            // cName
            // 
            this.cName.DataPropertyName = "cName";
            this.cName.HeaderText = "Наименование";
            this.cName.Name = "cName";
            this.cName.ReadOnly = true;
            // 
            // chbAll
            // 
            this.chbAll.AutoSize = true;
            this.chbAll.Location = new System.Drawing.Point(21, 306);
            this.chbAll.Name = "chbAll";
            this.chbAll.Size = new System.Drawing.Size(45, 17);
            this.chbAll.TabIndex = 38;
            this.chbAll.Text = "Все";
            this.chbAll.UseVisualStyleBackColor = true;
            this.chbAll.CheckedChanged += new System.EventHandler(this.chbAll_CheckedChanged);
            // 
            // lblUnactive
            // 
            this.lblUnactive.AutoSize = true;
            this.lblUnactive.Location = new System.Drawing.Point(87, 306);
            this.lblUnactive.Name = "lblUnactive";
            this.lblUnactive.Size = new System.Drawing.Size(68, 13);
            this.lblUnactive.TabIndex = 37;
            this.lblUnactive.Text = "неактивные";
            // 
            // pbUnactive
            // 
            this.pbUnactive.BackColor = System.Drawing.Color.Coral;
            this.pbUnactive.Location = new System.Drawing.Point(74, 308);
            this.pbUnactive.Name = "pbUnactive";
            this.pbUnactive.Size = new System.Drawing.Size(12, 12);
            this.pbUnactive.TabIndex = 39;
            this.pbUnactive.TabStop = false;
            // 
            // frmAnotherPayments
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(424, 351);
            this.ControlBox = false;
            this.Controls.Add(this.dgAnotherPayments);
            this.Controls.Add(this.pbUnactive);
            this.Controls.Add(this.chbAll);
            this.Controls.Add(this.lblUnactive);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnDel);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnAdd);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAnotherPayments";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Справочник дополнительных оплат";
            this.Load += new System.EventHandler(this.frmAnotherPayments_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgAnotherPayments)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbUnactive)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.DataGridView dgAnotherPayments;
        private System.Windows.Forms.PictureBox pbUnactive;
        private System.Windows.Forms.CheckBox chbAll;
        private System.Windows.Forms.Label lblUnactive;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnDel;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn isActive;
        private System.Windows.Forms.DataGridViewTextBoxColumn cName;
    }
}